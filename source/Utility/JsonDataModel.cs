using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelGenerator.Utility
{
    internal class AppSettings
    {
        public List<DatabaseSettings> Databases { get; set; }
    }

    internal class DatabaseSettings
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string DataModelSolutionPath { get; set; }
        public List<SchemaAccess> Schemas { get; set; }
        public Dictionary<string, string> SchemaDataModelPath { get; set; }
        public OrganizeTemplateConfig TemplateConfig { get; set; }
    }

    internal class SchemaAccess
    {
        public string Name { get; set; }
        public List<string> AccessTo { get; set; }
    }

    internal class OrganizeTemplateConfig
    {
        public string NamespaceFormat { get; set; }
        public string CustomUsingItem { get; set; }
        public string TableAttributeFormat { get; set; }
        public string CustomColumnAttributeFormat { get; set; }
        public string ClassDeclarationInheritanceFormat { get; set; }
        public string ClassDeclarationNameFormat { get; set; }
        public string ConstructorFormat { get; set; }
        public string ColumnFormat { get; set; }
        public string ReferenceFormat { get; set; }
        public string RelationFormat { get; set; }

        public List<string> OverrideMainColumns { get; set; } = new();
        public Dictionary<string, string> CustomRegionsItem { get; set; } = new();

        public bool Validate(out string error)
        {
            var errors = new List<string>();

            void Require(string name, string value, params string[] mustContainTokens)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    errors.Add($"{name} must not be empty.");
                    return;
                }

                foreach (var token in mustContainTokens)
                {
                    if (!value.Contains(token))
                        errors.Add($"{name} must contain token '{token}'.");
                }
            }

            void Optional(string name, string value, params string[] mustContainTokens)
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;

                foreach (var token in mustContainTokens)
                {
                    if (!value.Contains(token))
                        errors.Add($"{name} must contain token '{token}' when set.");
                }
            }

            // Required properties
            Require(nameof(NamespaceFormat), NamespaceFormat, "{Database}", "{Schema}");
            Require(nameof(TableAttributeFormat), TableAttributeFormat, "{ClassName}", "{Schema}");
            Require(nameof(ClassDeclarationNameFormat), ClassDeclarationNameFormat, "{ClassName}");
            Require(nameof(ConstructorFormat), ConstructorFormat, "{ClassName}");
            Require(nameof(ColumnFormat), ColumnFormat, "{Type}", "{Name}");
            Require(nameof(ReferenceFormat), ReferenceFormat, "{RefType}", "{PropertyName}");
            Require(nameof(RelationFormat), RelationFormat, "{RelatedType}", "{PropertyName}");

            // Optional properties
            Optional(nameof(CustomColumnAttributeFormat), CustomColumnAttributeFormat, "[", "]");
            Optional(nameof(ClassDeclarationInheritanceFormat), ClassDeclarationInheritanceFormat);
            Optional(nameof(CustomUsingItem), CustomUsingItem, ";");

            // OverrideMainColumns check (only validate if set)
            if (OverrideMainColumns != null && OverrideMainColumns.Any(c => string.IsNullOrWhiteSpace(c)))
            {
                errors.Add("OverrideMainColumns contains empty column name.");
            }

            // CustomRegionsItem check (optional)
            if (CustomRegionsItem != null)
            {
                var reserved = new[] { "Constructors", "Columns", "References", "Relations" };
                foreach (var kvp in CustomRegionsItem)
                {
                    if (reserved.Any(r => r.Equals(kvp.Key, StringComparison.OrdinalIgnoreCase)))
                        errors.Add($"CustomRegionsItem contains reserved region name '{kvp.Key}'.");

                    if (string.IsNullOrWhiteSpace(kvp.Key))
                        errors.Add("CustomRegionsItem contains empty region name.");

                    if (string.IsNullOrWhiteSpace(kvp.Value))
                        errors.Add($"Region '{kvp.Key}' contains empty content.");
                }
            }

            error = errors.Any() ? string.Join(Environment.NewLine, errors) : string.Empty;
            return !errors.Any();
        }
    }

}
