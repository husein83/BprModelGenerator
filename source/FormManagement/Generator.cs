using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Text;
using ModelGenerator.Utility;

namespace ModelGenerator.FormManagement
{
    public partial class Generator : Form
    {
        #region GlobalVariables
        private HashSet<string> _schemaDirectories = new();
        private HashSet<string> _tableNameWithSchema = new();
        private List<EntityRefSetStructureModel> _entityRefSetStructureList = new();
        private enum DirectoryType
        {
            DataModelPath,
            DbContextPath,
            ProjectPath
        }

        #endregion

        #region InitialAndLoad
        public Generator()
        {
            InitializeComponent();
            var jsonSetting = new JsonSetting();
            _databaseList = jsonSetting.GetAllDatabases();
            DatabaseComboBox.DataSource = _databaseList.ToList();
        }
        private void Generator_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region ButtonEvent
        private void GenerateButton_Click(object sender, EventArgs e)
        {
            SetKeyEditableState(false);
            Process process;
            bool isSuccess = false;
            string selectedSchema = SchemaComboBox.SelectedValue == null ? string.Empty : SchemaComboBox.SelectedValue.ToString()!;
            try
            {
                InformationListBox.Items.Clear();
                const string dotnetInitialCommand = "clean && dotnet build";
                var directory = GetDirectory(DirectoryType.ProjectPath);

                process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "dotnet",
                        WorkingDirectory = directory,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        Arguments = $"{dotnetInitialCommand}",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    },
                    EnableRaisingEvents = true
                };

                RunProcess(ref process, selectedSchema, out isSuccess);

                process.Close();

                var jsonSetting = new JsonSetting();
                var dbName = DatabaseComboBox.SelectedValue == null ? string.Empty : DatabaseComboBox.SelectedValue.ToString();
                string dotnetCommand = $"ef dbcontext scaffold \"{jsonSetting.GetConnectionStringByDatabaseName(dbName)}\" Microsoft.EntityFrameworkCore.SqlServer";

                string buildDataModelCommand = $"--data-annotations " +
                "--context-dir GeneratedDbContext " +
                $"--context {selectedSchema}DbContext " +
                $"{GetSchemaAccess(selectedSchema)} " +
                $"--output-dir {dbName}\\{selectedSchema}\\DataModel " +
                "--no-pluralize " +
                "--no-onconfiguring " +
                "--use-database-names " +
                "--force";

                if (string.IsNullOrEmpty(directory))
                {
                    InformationListBox.Invoke(() => InformationListBox.Items.Add($"'{dbName}.DataModel' Directory Not Found."));
                    return;
                }

                process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "dotnet",
                        WorkingDirectory = directory,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        Arguments = $"{dotnetCommand} {buildDataModelCommand}",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    },
                    EnableRaisingEvents = true
                };

                RunProcess(ref process, selectedSchema, out isSuccess);
            }
            catch (Exception ex)
            {
                InformationListBox.Invoke(() => InformationListBox.Items.Add($"An Error Occurred: {ex.Message}"));
            }
            finally
            {
                if (isSuccess)
                {
                    InformationListBox.Items.Add($"```{selectedSchema} DataModel Generated Successfully```");
                }
                SetKeyEditableState(true);
            }
        }
        private void OrganizeDataModelButton_Click(object sender, EventArgs e)
        {
            SetKeyEditableState(false);
            InformationListBox.Items.Clear();

            string dbName = DatabaseComboBox.SelectedValue == null ? string.Empty : DatabaseComboBox.SelectedValue.ToString();
            string schema = SchemaComboBox.SelectedValue == null ? string.Empty : SchemaComboBox.SelectedValue.ToString()!;

            // Define The Path To The Models Directory
            string dataModelPath = GetDirectory(DirectoryType.DataModelPath);
            string modelsPath = Path.Combine(dataModelPath, schema, "DataModel");

            // Check If The Models Directory Exists
            if (!Directory.Exists(modelsPath))
            {
                InformationListBox.Items.Add($"The Folder '{modelsPath}' Was Not Found.");
                return;
            }

            // Retrieve a List Of All .cs Files In The DataModel Folder
            string[] files = Directory.GetFiles(modelsPath, "*.cs");

            InitializeDataModelInformation();
            DeleteExtraItemButton(dbName);

            foreach (var file in files)
            {
                string className = Path.GetFileNameWithoutExtension(file);
                if (!File.Exists(file))
                {
                    continue;
                }
                string fileContent = File.ReadAllText(file);

                // Skip Modification If The File Name Contains "DbContext"
                if (className.Contains("DbContext"))
                {
                    continue;
                }
                var tableName = schema + ".DataModel." + className;
                if (!_tableNameWithSchema.Where(p => p.Contains(tableName)).Any())
                {
                    continue;
                }

                var cfg = new OrganizeTemplateConfig();
                var organizer = new ModelOrganizer(ref cfg, dbName, schema, _tableNameWithSchema);

                if (cfg == null || organizer == null)
                {
                    InformationListBox.Items.Add($"Please Generate Template Config First!!");
                    return;
                }

                // Check If Class Already Modified
                if (fileContent.Contains($"public partial class {className} : {cfg.ClassDeclarationInheritanceFormat}"))
                {
                    continue;
                }

                var parser = new RawModelParser(fileContent, dbName, schema, className, _tableNameWithSchema, _entityRefSetStructureList);
                var columns = parser.GetColumns();
                var references = parser.GetReferences();
                var relations = parser.GetRelations();
                var tableAttrs = parser.GetTableAttribute();

                // Organize The Class
                string organized = organizer.Organize(className, columns, references, relations, tableAttrs);

                // Save The Modified Content Back To The File
                File.WriteAllText(file, organized);
                InformationListBox.Items.Add($"Organized {dbName}.{schema}.{className} In Path {file}");
            }

            SetKeyEditableState(true);
        }
        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            SetKeyEditableState(false);
            InformationListBox.Items.Clear();
            var dbName = DatabaseComboBox.SelectedValue == null ? string.Empty : DatabaseComboBox.SelectedValue.ToString();
            string dataModelPath = GetDirectory(DirectoryType.DataModelPath);
            string dbContextPath = GetDirectory(DirectoryType.DbContextPath);

            if (Directory.Exists(dataModelPath))
            {
                Directory.Delete(dataModelPath, true);
                InformationListBox.Items.Add($"'{dbName}' Directory Deleted.");
            }
            else
            {
                InformationListBox.Items.Add($"'{dbName}' Directory Not Found.");
            }

            if (Directory.Exists(dbContextPath))
            {
                Directory.Delete(dbContextPath, true);
                InformationListBox.Items.Add("'GeneratedDbContext' Directory Deleted.");
            }
            else
            {
                InformationListBox.Items.Add("'GeneratedDbContext' Directory Not Found.");
            }

            SetKeyEditableState(true);
        }
        private void DbContextDeleteButton_Click(object sender, EventArgs e)
        {
            SetKeyEditableState(false);
            InformationListBox.Items.Clear();

            string dbContextPath = GetDirectory(DirectoryType.DbContextPath);

            if (Directory.Exists(dbContextPath))
            {
                Directory.Delete(dbContextPath, true);
                InformationListBox.Items.Add("'GeneratedDbContext' Directory Deleted.");
            }
            else
            {
                InformationListBox.Items.Add("'GeneratedDbContext' Directory Not Found.");
            }

            SetKeyEditableState(true);
        }
        private void ReturnButton_Click(object sender, EventArgs e)
        {
            var menuForm = new Menu();
            this.Close();
            menuForm.Show();
        }
        private void UpdateProjectDataModelsButton_Click(object sender, EventArgs e)
        {
            SetKeyEditableState(false);
            InformationListBox.Items.Clear();

            string dbName = DatabaseComboBox.SelectedValue == null ? string.Empty : DatabaseComboBox.SelectedValue.ToString();
            string schema = SchemaComboBox.SelectedValue == null ? string.Empty : SchemaComboBox.SelectedValue.ToString();

            var jsonSetting = new JsonSetting();
            var projectDataModelPath = jsonSetting.GetSchemaSolutionPathByDatabaseNameAndSchemaName(dbName, schema);
            if (Directory.Exists(projectDataModelPath))
            {
                string dataModelPath = GetDirectory(DirectoryType.DataModelPath);
                string modelsPath = Path.Combine(dataModelPath, schema, "DataModel");
                var files = Directory.GetFiles(modelsPath);
                InformationListBox.Items.Add($"Destination Copy Directory: {projectDataModelPath}.");
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var destFile = Path.Combine(projectDataModelPath, fileName);
                    File.Copy(file, destFile, true);
                    InformationListBox.Items.Add($"{fileName} DataModel Copy Successfully.");
                }
            }
            else
            {
                InformationListBox.Items.Add($"{schema} DataModel Path Directory NotFound.");
            }
            SetKeyEditableState(true);
        }
        private void DeleteDataModelButton_Click(object sender, EventArgs e)
        {
            SetKeyEditableState(false);
            InformationListBox.Items.Clear();
            var dbName = DatabaseComboBox.SelectedValue == null ? string.Empty : DatabaseComboBox.SelectedValue.ToString();
            var schema = SchemaComboBox.SelectedValue == null ? string.Empty : SchemaComboBox.SelectedValue.ToString();
            string dataModelPath = GetDirectory(DirectoryType.DataModelPath);
            string modelsPath = Path.Combine(dataModelPath, schema, "DataModel");

            if (Directory.Exists(modelsPath))
            {
                Directory.Delete(modelsPath, true);
                InformationListBox.Items.Add($"'{dbName}.{schema}.DataModel' Directory Deleted.");
            }
            else
            {
                InformationListBox.Items.Add($"'{dbName}.{schema}.DataModel' Directory Not Found.");
            }

            SetKeyEditableState(true);
        }

        #endregion

        #region EventHandler
        private void DatabaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var jsonSetting = new JsonSetting();
            var dbName = DatabaseComboBox.SelectedValue == null ? string.Empty : DatabaseComboBox.SelectedValue.ToString();
            _schemaList = jsonSetting.GetAllSchemaByDatabaseName(dbName);
            SchemaComboBox.DataSource = _schemaList.ToList();
        }
        private void DeleteExtraItemButton(string dbName)
        {
            string dataModelPath = GetDirectory(DirectoryType.DataModelPath);

            if (Directory.Exists(dataModelPath))
            {
                string[] schemaDirectories = Directory.GetDirectories(dataModelPath);
                foreach (string schema in schemaDirectories)
                {
                    _schemaDirectories.Add(schema);
                }

                InformationListBox.Items.Clear();
                HashSet<string> fileNames = new HashSet<string>();
                bool visited = false;
                string selectedSchema = SchemaComboBox.SelectedValue == null ? string.Empty : SchemaComboBox.SelectedValue.ToString()!;
                foreach (var item in _schemaList)
                {
                    if (!visited)
                    {
                        string schemaPath = _schemaDirectories.Where(p => p.Contains(item)).FirstOrDefault() ?? string.Empty;
                        if (Directory.Exists(schemaPath))
                        {
                            var files = Directory.GetFiles($"{schemaPath}\\DataModel", "*.cs");

                            foreach (var file in files)
                            {
                                string fileName = Path.GetFileName(file);

                                if (!fileNames.Add(fileName))
                                {
                                    File.Delete(file);
                                }
                            }
                        }
                        if (item != null && item == selectedSchema) visited = true;
                    }
                }
            }
        }

        #endregion

        #region SchemaMethod
        private readonly HashSet<string> _databaseList = new();
        private HashSet<string> _schemaList = new();
        private string GetSchemaAccess(string schema)
        {
            var jsonSetting = new JsonSetting();
            var dbName = DatabaseComboBox.SelectedValue?.ToString() ?? string.Empty;

            var accessList = jsonSetting.GetSchemaAccessListByDatabaseName(dbName);
            if (accessList == null || !accessList.Any()) return string.Empty;

            var targetSchema = accessList.FirstOrDefault(s => s.Name.Equals(schema, StringComparison.OrdinalIgnoreCase));
            if (targetSchema == null) return string.Empty;

            var schemas = new HashSet<string>(targetSchema.AccessTo, StringComparer.OrdinalIgnoreCase)
            {
                targetSchema.Name
            };

            var result = string.Join(" ", schemas.Select(s => $"--schema {s}"));
            return result;
        }
        private string GetUsingSchema(string schema)
        {
            var jsonSetting = new JsonSetting();
            var dbName = DatabaseComboBox.SelectedValue?.ToString() ?? string.Empty;

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

        #endregion

        #region PrivateMethod
        private string GetDirectory(DirectoryType directoryType)
        {
            string result = string.Empty;

            var jsonSetting = new JsonSetting();
            var dbName = DatabaseComboBox.SelectedValue == null ? string.Empty : DatabaseComboBox.SelectedValue.ToString();
            string dataModelProjectPath = jsonSetting.GetDataModelPathByDatabaseName(dbName);
            if (!Directory.Exists(dataModelProjectPath))
            {
                InformationListBox.Items.Add("Invalid DataModel Solution Path !!!.");
                return result;
            }

            string dataModelPath = Path.Combine(dataModelProjectPath, dbName);
            if (!Directory.Exists(dataModelPath))
            {
                Directory.CreateDirectory(dataModelPath);
            }

            string dbContextPath = Path.Combine(dataModelProjectPath, "GeneratedDbContext");
            if (!Directory.Exists(dataModelPath))
            {
                Directory.CreateDirectory(dataModelPath);
            }

            switch (directoryType)
            {
                case DirectoryType.ProjectPath:
                    result = dataModelProjectPath;
                    break;
                case DirectoryType.DataModelPath:
                    result = dataModelPath;
                    break;
                case DirectoryType.DbContextPath:
                    result = dbContextPath;
                    break;
            }

            return result;
        }
        private void SetKeyEditableState(bool isActive)
        {
            GenerateButton.Enabled = isActive;
            OrganizeDataModelButton.Enabled = isActive;
            ClearAllButton.Enabled = isActive;
            DeleteDataModelButton.Enabled = isActive;
            DbContextDeleteButton.Enabled = isActive;
            UpdateProjectDataModelsButton.Enabled = isActive;
        }
        private void RunProcess(ref Process process, string selectedSchema, out bool isSuccess)
        {
            bool outResult = true;
            try
            {
                //process.OutputDataReceived += (sender, e) =>
                //{
                //    if (!string.IsNullOrEmpty(e.Data))
                //    {
                //        InformationListBox.Invoke((MethodInvoker)delegate
                //        {
                //            InformationListBox.Items.Add(e.Data);
                //        });
                //    }
                //};
                //process.ErrorDataReceived += (sender, e) =>
                //{
                //    if (!string.IsNullOrEmpty(e.Data))
                //    {
                //        InformationListBox.Invoke((MethodInvoker)delegate
                //        {
                //            InformationListBox.Items.Add(e.Data);
                //            outResult = false;
                //        });
                //    }
                //};

                List<string> badKeywords = new List<string>
                {
                    "No project was found",
                    "Unable to create an object",
                    "No design-time services were found",
                    "Build failed",
                    "Scaffold-DbContext failed",
                    "Unrecognized option",
                    "error",
                    "not found",
                    "doesn't reference"
                };

                process.Start();
                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrWhiteSpace(output))
                {
                    InformationListBox.Items.Add(output);
                    if (badKeywords.Contains(output))
                    {
                        outResult = false;
                    }
                }

                if (!string.IsNullOrWhiteSpace(error))
                {
                    InformationListBox.Items.Add(error);
                    outResult = false;
                }

                if (!process.WaitForExit(10000))
                {
                    process.Kill();
                    InformationListBox.Invoke(() =>
                    {
                        InformationListBox.Items.Add("Process Timed Out!!!");
                        outResult = false;
                    });
                }
                else
                {
                    outResult = true;
                }
            }
            catch (Exception)
            {
                isSuccess = false;
                return;
            }
            isSuccess = outResult;
        }
        private void InitializeDataModelInformation()
        {
            var dbName = DatabaseComboBox.SelectedValue == null ? string.Empty : DatabaseComboBox.SelectedValue.ToString();

            var jsonSetting = new JsonSetting();
            var connectionString = jsonSetting.GetConnectionStringByDatabaseName(dbName);
            var dbConnection = new DbConnection(connectionString);
            _entityRefSetStructureList = dbConnection.LoadStructure();
            _tableNameWithSchema = dbConnection.LoadTableNameWithSchema();

        }

        #endregion

    }
}
