using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ModelGenerator.FormManagement;

namespace ModelGenerator.Utility
{
    internal class ModelOrganizer
    {
        private readonly OrganizeTemplateConfig _cfg;
        private readonly string _database, _schema;
        private readonly HashSet<string> _tableNameWithSchema = new HashSet<string>();

        public ModelOrganizer(ref OrganizeTemplateConfig cfg, string dbName, string schema, HashSet<string> tableNameWithSchema)
        {
            var jsonSetting = new JsonSetting();
            var databaseSetting = jsonSetting.GetDatabaseByName(dbName);
            _cfg = databaseSetting.TemplateConfig;
            cfg = databaseSetting.TemplateConfig;
            _database = dbName;
            _schema = schema;
            _tableNameWithSchema = tableNameWithSchema;
        }

        public string Organize
            (
                string className,
                List<(string Type, string Name, List<string> Annotations)> columns,
                List<(string RefType, string FieldName, string PropertyName, List<string> Annotations)> references,
                List<(string RelatedType, string PropertyName, List<string> Annotations)> relations,
                List<string> tableAttr
            )
        {
            var sb = new StringBuilder();

            // Using statements
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            sb.AppendLine("using Microsoft.EntityFrameworkCore;");
            var parts = _cfg.CustomUsingItem.Split(';', StringSplitOptions.RemoveEmptyEntries);
            string formatted = string.Join(Environment.NewLine, parts
                                     .Select(p => p.Trim())
                                     .Where(p => !string.IsNullOrEmpty(p))
                                     .Select(p => p + ";"));
            if (!string.IsNullOrEmpty(formatted)) sb.AppendLine($"using {formatted}");
            sb.AppendLine($"{GetUsingSchema(_database, _schema)}");
            sb.AppendLine();

            // Namespace declaration
            var namespaceName = Template.ReplaceTemplatePlaceHolders(_cfg.NamespaceFormat, database: _database, schema: _schema);
            if (!namespaceName.Contains("DataModel")) namespaceName += ".DataModel";
            sb.AppendLine($"namespace {namespaceName}");
            sb.AppendLine("{");

            // Table Attribute + Indexes
            var attrName = Template.ReplaceTemplatePlaceHolders(_cfg.TableAttributeFormat, className: className, schema: _schema);
            sb.AppendLine($"\t{ConvertToNameofAttribute(attrName)}");
            foreach (var attr in tableAttr)
                sb.AppendLine($"\t{ConvertToNameofAttribute(attr)}");

            // Class Declaration
            var classHeader = Template.ReplaceTemplatePlaceHolders(_cfg.ClassDeclarationNameFormat, className: className);
            if (!string.IsNullOrWhiteSpace(_cfg.ClassDeclarationInheritanceFormat))
                classHeader += $" : {_cfg.ClassDeclarationInheritanceFormat}";
            sb.AppendLine($"\t{classHeader}");
            sb.AppendLine("\t{");

            // Constructors
            sb.AppendLine("\t\t#region Constructors");
            var constructor = Template.ReplaceTemplatePlaceHolders(_cfg.ConstructorFormat, className: className, schema: _schema, database: _database);
            sb.AppendLine($"\t\t{constructor}");
            sb.AppendLine("\t\t#endregion");
            sb.AppendLine();

            // Columns
            sb.AppendLine("\t\t#region Columns");
            foreach (var col in columns)
            {
                string overrideKeyword = _cfg.OverrideMainColumns?.Contains(col.Name) == true ? "override " : "";

                string attrLines = col.Annotations.Any() ? string.Join("\n\t\t", col.Annotations.Select(a => $"[{a}]")) + "\n\t\t" : string.Empty;
                var format = _cfg.ColumnFormat;
                if ((attrLines.Contains("Required") || !col.Type.Contains("?")) && col.Type.Contains("string")) format = format + " = null!;";
                if (attrLines.Contains("Timestamp") && col.Type.Contains("byte")) format = format + " = [];";
                string line = Template.ReplaceTemplatePlaceHolders(format, attributes: attrLines, type: col.Type, name: col.Name);
                line = line.Replace("public ", $"public {overrideKeyword}");

                sb.AppendLine("\t\t" + line);
                sb.AppendLine();
            }
            sb.AppendLine("\t\t#endregion");
            sb.AppendLine();

            // References
            sb.AppendLine("\t\t#region References");
            foreach (var refItem in references)
            {
                string refModel;
                var schemaAccessList = GetSchemaAccessList(_database, _schema);
                if (schemaAccessList != null)
                {
                    refModel = _tableNameWithSchema.Where(p => p.Split(".").Last().Equals(refItem.RefType, StringComparison.OrdinalIgnoreCase) && schemaAccessList.Where(q => p.Contains(q)).Any()).First();
                }
                else
                {
                    refModel = _tableNameWithSchema.Where(p => p.Split(".").Last().Equals(refItem.RefType, StringComparison.OrdinalIgnoreCase) && p.Contains(_schema)).First();
                }
                string refLine = Template.ReplaceTemplatePlaceHolders(_cfg.ReferenceFormat, fieldName: refItem.FieldName, refType: refModel, propertyName: refItem.PropertyName);
                var publicIndex = refLine.IndexOf("public");
                if (publicIndex >= 0)
                {
                    string annotations = string.Join("\n\t\t", refItem.Annotations.Select(a =>
                    {
                        if (a.StartsWith("InverseProperty("))
                        {
                            var match = Regex.Match(a, @"InverseProperty\(\""(.*?)\""\)");
                            if (match.Success)
                            {
                                var propName = match.Groups[1].Value;
                                var fullName = $"nameof({refModel}.{propName})";
                                return $"[InverseProperty({fullName})]";
                            }
                        }
                        return $"[{a}]";
                    })) + (refItem.Annotations.Any() ? "\t\t" : string.Empty);
                    string declaration = refLine.Substring(publicIndex).Trim();

                    if (!string.IsNullOrEmpty(annotations))
                    {
                        var attrLines = annotations.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var attr in attrLines)
                        {
                            sb.AppendLine("\t\t" + attr.Trim());
                        }
                    }
                    sb.AppendLine("\t\t" + declaration);
                    sb.AppendLine();
                }
            }
            sb.AppendLine("\t\t#endregion");
            sb.AppendLine();

            // Relations
            sb.AppendLine("\t\t#region Relations");
            foreach (var rel in relations)
            {
                if (_tableNameWithSchema.Where(p => p.Contains($"{namespaceName}.{rel.RelatedType}")).Any())
                {
                    string relLine = Template.ReplaceTemplatePlaceHolders(_cfg.RelationFormat, relatedType: $"{_database}.{_schema}.DataModel.{rel.RelatedType}", propertyName: rel.PropertyName);
                    var publicIndex = relLine.IndexOf("public");
                    if (publicIndex >= 0)
                    {
                        string annotations = string.Join("\n\t\t", rel.Annotations.Select(a =>
                        {
                            if (a.StartsWith("InverseProperty("))
                            {
                                var match = Regex.Match(a, @"InverseProperty\(\""(.*?)\""\)");
                                if (match.Success)
                                {
                                    var propName = match.Groups[1].Value;
                                    var fullName = $"nameof({_database}.{_schema}.DataModel.{rel.RelatedType}.{propName}Ref{propName}Id)";
                                    return $"[InverseProperty({fullName})]";
                                }
                            }
                            return $"[{a}]";
                        })) + (rel.Annotations.Any() ? "\t\t" : string.Empty);

                        string declaration = relLine.Substring(publicIndex).Trim();

                        if (!string.IsNullOrEmpty(annotations))
                        {
                            var attrLines = annotations.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var attr in attrLines)
                            {
                                sb.AppendLine("\t\t" + attr.Trim());
                            }
                        }
                        sb.AppendLine("\t\t" + declaration);
                        sb.AppendLine();
                    }
                }
            }
            sb.AppendLine("\t\t#endregion");
            sb.AppendLine();

            // Custom Regions
            if (_cfg.CustomRegionsItem?.Any() == true)
            {
                foreach (var region in _cfg.CustomRegionsItem)
                {
                    sb.AppendLine($"\t\t#region {region.Key}");
                    foreach (var line in region.Value.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        sb.AppendLine($"\t\t{Template.ReplaceTemplatePlaceHolders(line, className: className, schema: _schema)}");
                    }
                    sb.AppendLine($"\t\t#endregion");
                    sb.AppendLine();
                }
            }

            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string GetUsingSchema(string dbName, string schema)
        {
            var jsonSetting = new JsonSetting();

            var schemaAccessList = jsonSetting.GetSchemaAccessListByDatabaseName(dbName);
            if (schemaAccessList == null) return string.Empty;

            var currentSchema = schemaAccessList.FirstOrDefault(s => s.Name.Equals(schema, StringComparison.OrdinalIgnoreCase));
            if (currentSchema == null) return string.Empty;

            var allSchemas = new HashSet<string>(currentSchema.AccessTo, StringComparer.OrdinalIgnoreCase)
            {
                schema
            };

            var result = string.Join("\n", allSchemas.Select(s => $"using {dbName}.{s};"));
            return result;
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

        private string ConvertToNameofAttribute(string attribute)
        {
            return Regex.Replace(attribute, @"(?<!Name\s*=\s*)""([^""]+)""", match =>
            {
                var inner = match.Groups[1].Value.Trim();
                if (string.IsNullOrWhiteSpace(inner)) return match.Value;
                return $"nameof({inner})";
            });
        }
    }

}
