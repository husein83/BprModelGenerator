using ModelGenerator.FormManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModelGenerator.Utility
{
    internal class JsonSetting
    {
        #region Initial
        public JsonSetting() { }
        private const string settingFileName = "BprModelGeneratorSetting.json";
        private readonly string _settingPath = Path.Combine(AppContext.BaseDirectory, settingFileName);
        #endregion

        #region Public
        public void LoadAllDatabases(TextBox textBox)
        {
            if (!File.Exists(_settingPath))
            {
                MessageBox.Show($"{settingFileName} File Not Found.");
                return;
            }

            var settings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(_settingPath));
            if (settings?.Databases?.Count > 0)
            {
                var dialog = new DatabaseSelectionDialog(settings.Databases.Select(d => d.Name).ToList());
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = dialog.SelectedDatabase;
                }
            }
            else
            {
                MessageBox.Show("No Databases Found.");
            }
        }
        public HashSet<string> GetAllDatabases()
        {
            var result = new HashSet<string>();
            if (!File.Exists(_settingPath))
            {
                MessageBox.Show($"{settingFileName} File Not Found.");
                return result;
            }

            var settings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(_settingPath));
            if (settings?.Databases?.Count > 0)
            {
                result = new HashSet<string>(settings.Databases.Select(d => d.Name));
            }
            else
            {
                MessageBox.Show("No Databases Found.");
            }
            return result;
        }
        public HashSet<string> GetAllSchemaByDatabaseName(string databaseName)
        {
            var result = new HashSet<string>();
            if (!File.Exists(_settingPath))
            {
                MessageBox.Show($"{settingFileName} File Not Found.");
                return result;
            }

            var settings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(_settingPath));
            if (settings?.Databases?.Count > 0)
            {
                result = new HashSet<string>(settings.Databases.Where(p => p.Name == databaseName).SelectMany(d => d.Schemas.Select(p => p.Name)));
            }
            else
            {
                MessageBox.Show("No Databases Found.");
            }
            return result;
        }
        public List<SchemaAccess> GetSchemaAccessListByDatabaseName(string databaseName)
        {
            var result = new List<SchemaAccess>();
            if (!File.Exists(_settingPath))
            {
                MessageBox.Show($"{settingFileName} File Not Found.");
                return result;
            }

            var settings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(_settingPath));
            if (settings?.Databases?.Count > 0)
            {
                result = settings.Databases.Where(p => p.Name == databaseName).SelectMany(d => d.Schemas).ToList();
            }
            else
            {
                MessageBox.Show("No Databases Found.");
            }
            return result;
        }
        public void LoadSelectedDatabaseSetting
            (
                string dbName,
                TextBox connectionStringTextbox,
                TextBox dataModelSolutionPathTextbox,
                DataGridView schemaDataGridView
            )
        {
            if (!File.Exists(_settingPath))
            {
                MessageBox.Show($"{settingFileName} File Not Found.");
                return;
            }

            var content = File.ReadAllText(_settingPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(content) ?? new();

            var db = settings.Databases.FirstOrDefault(d => d.Name.Equals(dbName, StringComparison.OrdinalIgnoreCase));
            if (db == null) return;

            connectionStringTextbox.Text = db.ConnectionString;
            dataModelSolutionPathTextbox.Text = db.DataModelSolutionPath;

            schemaDataGridView.Rows.Clear();
            foreach (var schema in db.Schemas)
            {
                int i = schemaDataGridView.Rows.Add();
                var row = schemaDataGridView.Rows[i];

                row.Cells["SchemaName"].Value = schema.Name;
                row.Cells["AccessTo"].Tag = schema.AccessTo;
                row.Cells["AccessTo"].Value = string.Join(", ", schema.AccessTo);
            }
        }
        public void LoadSelectedDatabaseTemplateConfig
            (
                string dbName,
                TextBox namespaceFormatTextbox,
                TextBox tableAttributeFormatTextbox,
                TextBox customUsingItemTextBox,
                TextBox customColumnAttributeFormatTextbox,
                TextBox classDeclarationInheritanceFormatTextbox,
                TextBox classDeclarationNameFormatTextbox,
                TextBox constructorFormatTextbox,
                TextBox columnFormatTextbox,
                TextBox referenceFormatTextbox,
                TextBox relationFormatTextbox,
                DataGridView overrideFieldsDataGridView,
                DataGridView customRegionsItemDataGridView
            )
        {
            var content = File.ReadAllText(_settingPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(content) ?? new();

            var db = settings.Databases.FirstOrDefault(d => d.Name.Equals(dbName, StringComparison.OrdinalIgnoreCase));
            if (db == null) return;
            if (db.TemplateConfig == null) return;

            namespaceFormatTextbox.Text = db.TemplateConfig.NamespaceFormat;
            tableAttributeFormatTextbox.Text = db.TemplateConfig.TableAttributeFormat;
            customUsingItemTextBox.Text = db.TemplateConfig.CustomUsingItem;
            classDeclarationInheritanceFormatTextbox.Text = db.TemplateConfig.ClassDeclarationInheritanceFormat;
            classDeclarationNameFormatTextbox.Text = db.TemplateConfig.ClassDeclarationNameFormat;
            constructorFormatTextbox.Text = db.TemplateConfig.ConstructorFormat;
            columnFormatTextbox.Text = db.TemplateConfig.ColumnFormat;
            referenceFormatTextbox.Text = db.TemplateConfig.ReferenceFormat;
            relationFormatTextbox.Text = db.TemplateConfig.RelationFormat;
            customColumnAttributeFormatTextbox.Text = db.TemplateConfig.CustomColumnAttributeFormat;
            if (db.TemplateConfig.OverrideMainColumns != null && db.TemplateConfig.OverrideMainColumns.Any())
            {
                foreach (var item in db.TemplateConfig.OverrideMainColumns)
                {
                    int i = overrideFieldsDataGridView.Rows.Add();
                    var row = overrideFieldsDataGridView.Rows[i];

                    row.Cells["Field"].Value = item;
                }
            }
            if (db.TemplateConfig.CustomRegionsItem != null && db.TemplateConfig.CustomRegionsItem.Any())
            {
                foreach (var item in db.TemplateConfig.CustomRegionsItem)
                {
                    int i = customRegionsItemDataGridView.Rows.Add();
                    var row = customRegionsItemDataGridView.Rows[i];

                    row.Cells["Region"].Value = item.Key;
                    row.Cells["Items"].Value = item.Value;
                }
            }
        }
        public void LoadSelectedDatabaseUpdateSchemaSolution
            (
                string dbName,
                DataGridView schemaSolutionDataGridView
            )
        {
            var content = File.ReadAllText(_settingPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(content) ?? new();

            var db = settings.Databases.FirstOrDefault(d => d.Name.Equals(dbName, StringComparison.OrdinalIgnoreCase));
            if (db == null) return;
            if (db.SchemaDataModelPath == null) return;

            if (db.SchemaDataModelPath != null && db.SchemaDataModelPath.Any())
            {
                foreach (var item in db.SchemaDataModelPath)
                {
                    int i = schemaSolutionDataGridView.Rows.Add();
                    var row = schemaSolutionDataGridView.Rows[i];

                    row.Cells["Schema"].Value = item.Key;
                    row.Cells["SolutionPath"].Value = item.Value;
                }
            }
        }
        public void InsertOrUpdateDatabase(DatabaseSettings newDb, out bool isSuccess)
        {
            EnsureSettingsFileExists();

            var content = File.ReadAllText(_settingPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(content) ?? new();

            settings.Databases.RemoveAll(d => d.Name.Equals(newDb.Name, StringComparison.OrdinalIgnoreCase));

            settings.Databases.Add(newDb);

            string error;
            if (!ValidateEfEnvironment(newDb.ConnectionString, newDb.DataModelSolutionPath, out error))
            {
                MessageBox.Show(error, "EF Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isSuccess = false;
                return;
            }
            else
            {
                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_settingPath, json);
                isSuccess = true;
            }
        }
        public void InsertOrUpdateDatabase(string databaseName, OrganizeTemplateConfig newConfig, out bool isSuccess)
        {
            EnsureSettingsFileExists();

            var content = File.ReadAllText(_settingPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(content) ?? new();
            DatabaseSettings databaseSetting = null;
            if (settings?.Databases?.Count > 0)
            {
                databaseSetting = settings.Databases.Where(p => p.Name == databaseName)?.FirstOrDefault();
            }

            if (databaseSetting != null)
            {
                databaseSetting.TemplateConfig = newConfig;
            }
            else
            {
                MessageBox.Show("---Not Found Database In Setting---", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isSuccess = false;
                return;
            }

            string error;
            if (!newConfig.Validate(out error))
            {
                MessageBox.Show(error, "InValid Config Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isSuccess = false;
                return;
            }
            else
            {
                settings.Databases.RemoveAll(d => d.Name.Equals(databaseName, StringComparison.OrdinalIgnoreCase));
                settings.Databases.Add(databaseSetting);
                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_settingPath, json);
                isSuccess = true;
            }
        }
        public void InsertOrUpdateDatabase(string databaseName, Dictionary<string, string> newSchemaSolutionPath, out bool isSuccess)
        {
            EnsureSettingsFileExists();

            var content = File.ReadAllText(_settingPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(content) ?? new();
            DatabaseSettings databaseSetting = null;
            if (settings?.Databases?.Count > 0)
            {
                databaseSetting = settings.Databases.Where(p => p.Name == databaseName)?.FirstOrDefault();
            }

            if (databaseSetting != null)
            {
                databaseSetting.SchemaDataModelPath = newSchemaSolutionPath;
            }
            else
            {
                MessageBox.Show("---Not Found Database In Setting---", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isSuccess = false;
                return;
            }

            settings.Databases.RemoveAll(d => d.Name.Equals(databaseName, StringComparison.OrdinalIgnoreCase));
            settings.Databases.Add(databaseSetting);
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_settingPath, json);
            isSuccess = true;
        }
        public DatabaseSettings GetDatabaseFromForm
            (
                TextBox databaseNameTextbox,
                TextBox connectionStringTextbox,
                TextBox dataModelSolutionPathTextbox,
                DataGridView schemaDataGridView
            )
        {
            var db = new DatabaseSettings
            {
                Name = databaseNameTextbox.Text.Trim(),
                ConnectionString = connectionStringTextbox.Text.Trim(),
                DataModelSolutionPath = dataModelSolutionPathTextbox.Text.Trim(),
                Schemas = new List<SchemaAccess>()
            };

            foreach (DataGridViewRow row in schemaDataGridView.Rows)
            {
                if (row.IsNewRow) continue;

                var name = row.Cells["SchemaName"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(name)) continue;

                var access = row.Cells["AccessTo"].Tag as List<string> ?? new();
                db.Schemas.Add(new SchemaAccess { Name = name, AccessTo = access });
            }

            return db;
        }
        public DatabaseSettings GetDatabaseByName(string dbName)
        {
            if (!File.Exists(_settingPath))
            {
                MessageBox.Show($"{settingFileName} File Not Found.");
                return null;
            }
            var content = File.ReadAllText(_settingPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(content) ?? new();

            return settings.Databases.FirstOrDefault(d => d.Name.Equals(dbName, StringComparison.OrdinalIgnoreCase));
        }
        public string GetSchemaSolutionPathByDatabaseNameAndSchemaName(string dbName, string schema)
        {
            string result = string.Empty;

            if (!File.Exists(_settingPath))
            {
                MessageBox.Show($"{settingFileName} File Not Found.");
                return null;
            }
            var content = File.ReadAllText(_settingPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(content) ?? new();

            var dbSetting = settings.Databases.FirstOrDefault(d => d.Name.Equals(dbName, StringComparison.OrdinalIgnoreCase));
            if (dbSetting != null && dbSetting.SchemaDataModelPath != null && dbSetting.SchemaDataModelPath.Any())
            {
                result = dbSetting.SchemaDataModelPath.Where(p => p.Key == schema &&
                                                               !string.IsNullOrEmpty(p.Value))
                                                        .FirstOrDefault()
                                                        .Value;
            }
            return result;
        }
        public void DeleteDatabaseByName(string dbName)
        {
            if (!File.Exists(_settingPath)) return;

            var content = File.ReadAllText(_settingPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(content) ?? new();

            settings.Databases.RemoveAll(d => d.Name.Equals(dbName, StringComparison.OrdinalIgnoreCase));

            File.WriteAllText(_settingPath, JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true }));
        }
        public void DeleteConfigTemplateByDatabaseName(string dbName)
        {
            if (!File.Exists(_settingPath))
            {
                MessageBox.Show($"{settingFileName} File Not Found.");
                return;
            }

            var content = File.ReadAllText(_settingPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(content) ?? new();
            DatabaseSettings databaseSetting = null;
            if (settings?.Databases?.Count > 0)
            {
                databaseSetting = settings.Databases.Where(p => p.Name == dbName)?.FirstOrDefault();
            }

            if (databaseSetting != null)
            {
                databaseSetting.TemplateConfig = null;
            }
            else
            {
                MessageBox.Show("---Not Found Database In Setting---", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            settings.Databases.RemoveAll(d => d.Name.Equals(dbName, StringComparison.OrdinalIgnoreCase));
            settings.Databases.Add(databaseSetting);
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_settingPath, json);
        }
        public void DeleteSchemaDataModelPathByDatabaseName(string dbName)
        {
            if (!File.Exists(_settingPath))
            {
                MessageBox.Show($"{settingFileName} File Not Found.");
                return;
            }

            var content = File.ReadAllText(_settingPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(content) ?? new();
            DatabaseSettings databaseSetting = null;
            if (settings?.Databases?.Count > 0)
            {
                databaseSetting = settings.Databases.Where(p => p.Name == dbName)?.FirstOrDefault();
            }

            if (databaseSetting != null)
            {
                databaseSetting.SchemaDataModelPath = null;
            }
            else
            {
                MessageBox.Show("---Not Found Database In Setting---", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            settings.Databases.RemoveAll(d => d.Name.Equals(dbName, StringComparison.OrdinalIgnoreCase));
            settings.Databases.Add(databaseSetting);
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_settingPath, json);
        }
        public string GetDataModelPathByDatabaseName(string databaseName)
        {
            string result = string.Empty;
            if (!File.Exists(_settingPath))
            {
                MessageBox.Show($"{settingFileName} File Not Found.");
                return result;
            }

            var settings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(_settingPath));
            if (settings?.Databases?.Count > 0)
            {
                result = settings.Databases.Where(p => p.Name == databaseName)?.First().DataModelSolutionPath;
            }
            else
            {
                MessageBox.Show("No Databases Found.");
            }
            return result;
        }
        public string GetConnectionStringByDatabaseName(string databaseName)
        {
            string result = string.Empty;
            if (!File.Exists(_settingPath))
            {
                MessageBox.Show($"{settingFileName} File Not Found.");
                return result;
            }

            var settings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(_settingPath));
            if (settings?.Databases?.Count > 0)
            {
                result = settings.Databases.Where(p => p.Name == databaseName).First().ConnectionString;
            }
            else
            {
                MessageBox.Show("No Databases Found.");
            }
            return result;
        }

        #endregion

        #region Private
        private void EnsureSettingsFileExists()
        {
            if (!File.Exists(_settingPath))
            {
                var emptySettings = new AppSettings { Databases = new() };
                var json = JsonSerializer.Serialize(emptySettings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_settingPath, json);
            }
        }
        private bool ValidateEfEnvironment(string connectionString, string dataModelPath, out string error)
        {
            error = string.Empty;

            if (!Directory.Exists(dataModelPath))
            {
                error = "DataModel Project Path Is Invalid.";
                return false;
            }

            var efCheck = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = "ef --version",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            try
            {
                efCheck.Start();
                string output = efCheck.StandardOutput.ReadToEnd();
                efCheck.WaitForExit(10000);

                if (efCheck.ExitCode != 0 || string.IsNullOrWhiteSpace(output))
                {
                    error = "--- EF Tools Not Installed. Please Run: dotnet tool install --global dotnet-ef";
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = $"--- EF Tools validation failed: {ex.Message}";
                return false;
            }

            var testCmd = $"ef dbcontext scaffold \"{connectionString}\" Microsoft.EntityFrameworkCore.SqlServer --schema TestSchema --output-dir TempTest --no-build --force";

            var scaffold = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = testCmd,
                    WorkingDirectory = dataModelPath,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            try
            {
                scaffold.Start();
                string stderr = scaffold.StandardError.ReadToEnd();
                scaffold.WaitForExit();

                if (stderr.Contains("No design-time Services Were Found") || stderr.Contains("provider") || stderr.Contains("could not load"))
                {
                    error = $"--- EF Packages are incomplete or SqlServer provider is missing.\nDetails:\n{stderr}";
                    return false;
                }

                var tempDir = Path.Combine(dataModelPath, "TempTest");
                if (Directory.Exists(tempDir))
                    Directory.Delete(tempDir, true);

                return true;
            }
            catch (Exception ex)
            {
                error = $"--- EF Scaffold check failed: {ex.Message}";
                return false;
            }
        }

        #endregion
    }
}
