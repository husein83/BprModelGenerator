using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace ModelGenerator.Utility
{
    internal class RawModelParser
    {
        private readonly string[] _lines;
        private readonly string _dbName, _schemaName, _className;
        private readonly HashSet<string> _tableNameWithSchema = new HashSet<string>();
        private readonly List<EntityRefSetStructureModel> _entityRefSetStructureList;
        private KeyValuePair<string, int> modelCounter = new KeyValuePair<string, int>(string.Empty, 0);
        private int counter = 0;

        public RawModelParser(string fileContent, string dbName, string schemaName, string className, HashSet<string> tableNameWithSchema, List<EntityRefSetStructureModel> entityRefSetStructureList)
        {
            _lines = fileContent
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                .Select(l => l.Trim())
                .ToArray();
            _dbName = dbName;
            _schemaName = schemaName;
            _className = className;
            _entityRefSetStructureList = entityRefSetStructureList;
            _tableNameWithSchema = tableNameWithSchema;
        }

        public List<(string Type, string Name, List<string> Annotations)> GetColumns()
        {
            var result = new List<(string, string, List<string>)>();
            var attrBuffer = new List<string>();

            foreach (var line in _lines)
            {
                if (line.StartsWith("[") && !line.StartsWith("[Index") &&
                    !line.StartsWith("[Table") && !line.StartsWith("[Key") &&
                    !line.StartsWith("[Required") && !line.StartsWith("[Column"))
                {
                    attrBuffer.Add(line.Trim('[', ']'));
                    continue;
                }

                var m = Regex.Match(line, @"public\s+(?<T>[^\s]+)\s+(?<P>\w+)\s*\{\s*get;\s*set;\s*\}");
                if (!m.Success) continue;

                var type = m.Groups["T"].Value;
                var name = m.Groups["P"].Value;
                var annotations = new List<string>();

                var indexedItem = GetTableAttribute();
                if (name == "Id") annotations.Add("Key");
                if (type == "byte[]" && name.Contains("RowVersion")) annotations.Add("Timestamp");
                else if (!type.EndsWith("?") && name != "Id") annotations.Add("Required");
                if (indexedItem.Contains("[Keyless]"))
                {
                    annotations = annotations.Where(p => !p.Contains("String")).ToList();
                    annotations.Clear();
                }
                if (annotations.Any()) annotations.AddRange(attrBuffer);

                if (type.StartsWith("decimal")) annotations.Add("Column(TypeName = \"money\")");
                if (type.StartsWith("DateTime")) annotations.Add("Column(TypeName = \"datetime\")");

                //if (type == "string?") type = "string";

                result.Add((type, name, annotations));
                attrBuffer.Clear();
            }

            return result;
        }

        public List<(string RefType, string FieldName, string PropertyName, List<string> Annotations)> GetReferences()
        {
            var results = new List<(string, string, string, List<string>)>();

            for (int i = 0; i < _lines.Length; i++)
            {
                var line = _lines[i].Trim();

                if (!line.StartsWith("public virtual") || line.Contains("ICollection<"))
                    continue;

                var match = Regex.Match(line, @"public virtual (?<T>\w+)\??\s+(?<P>\w+)");
                if (!match.Success)
                    continue;

                var refType = match.Groups["T"].Value;
                var prop = match.Groups["P"].Value;

                string fieldName = null;

                var annotations = new List<string>();

                for (int j = i - 1; j >= 0; j--)
                {
                    var prevLine = _lines[j].Trim();
                    if (string.IsNullOrWhiteSpace(prevLine)) continue;
                    if (!prevLine.StartsWith("[")) break;

                    if (prevLine.StartsWith("[ForeignKey"))
                    {
                        string cleaned = prevLine.Trim('[', ']');

                        var matchForeignKey = Regex.Match(cleaned, @"\(\s*\""(.*?)\""\s*\)");
                        if (matchForeignKey.Success)
                        {
                            fieldName = matchForeignKey.Groups[1].Value;
                            annotations.Insert(0, $"ForeignKey(nameof({fieldName}))");
                        }
                        else
                        {
                            annotations.Insert(0, cleaned);
                        }
                    }
                }

                if (string.IsNullOrEmpty(fieldName))
                {
                    fieldName = prop == refType
                        ? "Id"
                        : prop.StartsWith(refType)
                            ? prop[refType.Length..] + "Id"
                            : prop + "Id";
                }

                var refMeta = _entityRefSetStructureList.FirstOrDefault(m =>
                    (
                        m.TableName.Equals(_className, StringComparison.OrdinalIgnoreCase) &&
                        m.SchemaName == _schemaName &&
                        m.Field.Equals(fieldName, StringComparison.OrdinalIgnoreCase)
                    ));

                if (refMeta != null && (refMeta?.RefJson != null && !string.IsNullOrEmpty(refMeta.RefJson.InverseKey)) ||
                                       (refMeta?.SetJson != null && refMeta.SetJson.Count == 1 && !string.IsNullOrEmpty(refMeta.SetJson.First().InverseKey)))
                {
                    string propertyName, inverseKey;
                    if (refMeta.RefJson != null)
                    {
                        propertyName = refMeta.RefJson.Field;
                        inverseKey = refMeta.RefJson.InverseKey;
                    }
                    else
                    {
                        propertyName = refMeta.SetJson.First().Field;
                        inverseKey = refMeta.SetJson.First().InverseKey;
                    }

                    var parts = inverseKey.Split('_');

                    if (parts.Length >= 6 && parts[0] == "FK")
                    {
                        string destinationModel, destinationField, field;
                        if (refMeta.RefJson != null)
                        {
                            destinationModel = parts[4];
                            destinationField = parts[1];
                            field = parts[2];
                        }
                        else
                        {
                            destinationModel = parts[1];
                            destinationField = parts[4];
                            field = parts[2];
                        }

                        string fieldType;
                        if (annotations.Any(p => p.Contains("ForeignKey(")))
                            fieldType = "Set";
                        else
                            fieldType = "Ref";

                        var destination = _tableNameWithSchema.FirstOrDefault(p =>
                            p.EndsWith($".DataModel.{destinationModel}", StringComparison.OrdinalIgnoreCase));

                        if (!string.IsNullOrEmpty(destination) && destination.Contains(_schemaName))
                        {
                            var inverseProp = $"InverseProperty(nameof({destination}.{destinationField}{fieldType}{field}))";
                            var indexedItem = GetTableAttribute();
                            if (IsSingleColumnIndexed(indexedItem, field))
                            {
                                inverseProp = inverseProp.Replace("Set", "Ref");
                            }
                            annotations.Add(inverseProp);
                        }
                    }
                    results.Add((refType, fieldName, propertyName, annotations));
                }
            }
            return results;
        }

        public List<(string RelatedType, string PropertyName, List<string> Annotations)> GetRelations()
        {
            var results = new List<(string, string, List<string>)>();

            for (int i = 0; i < _lines.Length; i++)
            {
                string line = _lines[i].Trim();

                if (!line.Contains("ICollection<") || !line.StartsWith("public virtual")) continue;

                var annotations = new List<string>();

                var match = Regex.Match(line, @"ICollection<(?<T>\w+)>\s+(?<P>\w+)");
                if (!match.Success) continue;

                var setMeta = _entityRefSetStructureList.FirstOrDefault(e =>
                    e.TableName == _className &&
                    e.SchemaName == _schemaName);

                if (setMeta != null)
                {
                    var relatedType = match.Groups["T"].Value;
                    var setItems = setMeta?.SetItem?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    var count = setItems.Count(p => p.Contains(relatedType));

                    counter++;
                    if (modelCounter.Key != _className || counter >= count) counter = 0;
                    modelCounter = new KeyValuePair<string, int>(_className, counter);

                    string propertyName;
                    var schemaAccessList = GetSchemaAccessList(_dbName, _schemaName);
                    if (schemaAccessList != null)
                    {
                        propertyName = setItems?.Where(item =>
                            item.Contains(relatedType, StringComparison.OrdinalIgnoreCase) &&
                            setMeta.SetJson.Where(p => p.Field == item && schemaAccessList.Any(q => q == p.Schema)).Any()).ToList()[modelCounter.Value];
                    }
                    else
                    {
                        propertyName = setItems?.Where(item =>
                            item.Contains(relatedType, StringComparison.OrdinalIgnoreCase) &&
                            setMeta.SetJson.Where(p => p.Field == item && p.Schema == _schemaName).Any()).ToList()[modelCounter.Value];
                    }

                    var setItem = setMeta.SetJson.FirstOrDefault(s => s.Field == propertyName);
                    if (setItem != null)
                    {
                        var parts = setItem.InverseKey.Split('_');

                        if (parts.Length >= 6 && parts[0] == "FK")
                        {
                            var destinationModel = parts[1];
                            var destinationField = parts[4];
                            var field = parts[2];

                            var destinationProperty = $"{destinationField}Ref{field}";

                            var destination = _tableNameWithSchema.FirstOrDefault(p =>
                                p.EndsWith($".DataModel.{destinationModel}", StringComparison.OrdinalIgnoreCase));

                            if (!string.IsNullOrEmpty(destination))
                            {
                                var inverseProp = $"InverseProperty(nameof({destination}.{destinationProperty}))";
                                annotations.Add(inverseProp);
                            }
                        }
                    }
                    results.Add((relatedType, propertyName, annotations));
                }
            }

            return results;
        }

        public List<string> GetTableAttribute()
        {
            if (_lines.Contains("[Keyless]"))
                return _lines
                   .Where(l => l.StartsWith("[Keyless]"))
                   .ToList();
            else
                return _lines
                       .Where(l => l.StartsWith("[Index("))
                       .ToList();
        }

        private HashSet<string> GetSchemaAccessList(string dbName, string schema)
        {
            var jsonSetting = new JsonSetting();

            var schemaAccessList = jsonSetting.GetSchemaAccessListByDatabaseName(dbName);
            if (schemaAccessList == null) return null;

            var currentSchema = schemaAccessList.FirstOrDefault(s => s.Name.Equals(schema, StringComparison.OrdinalIgnoreCase));
            if (currentSchema == null) return null;

            return new HashSet<string>(currentSchema.AccessTo, StringComparer.OrdinalIgnoreCase)
            {
                schema
            };
        }

        private bool IsSingleColumnIndexed(List<string> indexAttributes, string field)
        {
            foreach (var attr in indexAttributes)
            {
                var beforeName = attr.Split(new[] { "Name =", "name =" }, StringSplitOptions.None)[0];

                var matches = Regex.Matches(beforeName, "\"([^\"]+)\"");
                var columnNames = matches.Cast<Match>().Select(m => m.Groups[1].Value).ToList();

                if (columnNames.Count == 1 && columnNames[0].Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
