using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModelGenerator.Utility;

namespace ModelGenerator.FormManagement
{
    public partial class Setting : Form
    {
        #region InitialAndLoad
        public Setting()
        {
            InitializeComponent();
        }

        #endregion

        #region ButtonEvent
        private void ReturnButton_Click(object sender, EventArgs e)
        {
            var menuForm = new Menu();
            this.Close();
            menuForm.Show();
        }
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select Folder To Save Data Models";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    DatabaseSolutionTextBox.Text = dialog.SelectedPath;
                }
            }
        }
        private void LoadButton_Click(object sender, EventArgs e)
        {
            ClearForm();
            var jsonSetting = new JsonSetting();
            jsonSetting.LoadAllDatabases(DatabaseNameTextBox);
            if (!string.IsNullOrEmpty(DatabaseNameTextBox.Text))
                jsonSetting.LoadSelectedDatabaseSetting(DatabaseNameTextBox.Text, ConnectionStringTextBox, DatabaseSolutionTextBox, SchemaDataGridView);
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            var jsonSetting = new JsonSetting();
            var db = jsonSetting.GetDatabaseFromForm(DatabaseNameTextBox, ConnectionStringTextBox, DatabaseSolutionTextBox, SchemaDataGridView);
            string error = string.Empty;
            if (db != null && IsValidData(db, out error))
            {
                bool isSucess = false;
                jsonSetting.InsertOrUpdateDatabase(db, out isSucess);
                if (isSucess && MessageBox.Show("Apply Changes Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    var menuForm = new Menu();
                    this.Close();
                    menuForm.Show();
                }
            }
            else
            {
                MessageBox.Show(error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure Delete This Database Setting?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(DatabaseNameTextBox.Text))
                {
                    var jsonSetting = new JsonSetting();
                    jsonSetting.DeleteDatabaseByName(DatabaseNameTextBox.Text);
                    if (MessageBox.Show("Delete Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        var menuForm = new Menu();
                        this.Close();
                        menuForm.Show();
                    }
                }
                else
                {
                    MessageBox.Show("---Please Select Database---", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void ConnectionStringTemplateButton_Click(object sender, EventArgs e)
        {
            ConnectionStringTextBox.Text = "Server=.;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        #endregion

        #region EventHandler
        private void SchemaDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != SchemaDataGridView.Columns["AccessTo"].Index)
                return;

            var currentRowIndex = e.RowIndex;

            var previousSchemas = new List<string>();
            for (int i = 0; i < currentRowIndex; i++)
            {
                var schema = SchemaDataGridView.Rows[i].Cells["SchemaName"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(schema))
                    previousSchemas.Add(schema);
            }

            var dialog = new AccessSelectionDialog(previousSchemas);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var selectedSchemas = dialog.GetSelectedSchemas();
                string displayValue = string.Join(", ", selectedSchemas);

                var cell = SchemaDataGridView.Rows[currentRowIndex].Cells["AccessTo"];

                cell.Tag = selectedSchemas;
                cell.Value = displayValue;
            }
        }
        private void SchemaDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (SchemaDataGridView.Columns[e.ColumnIndex].Name == "AccessTo")
            {
                var cell = SchemaDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Tag is List<string> tagList)
                {
                    e.Value = string.Join(", ", tagList);
                    e.FormattingApplied = true;
                }
            }
        }
        private bool IsValidData(DatabaseSettings databaseSettings, out string error)
        {
            if (databaseSettings == null)
            {
                error = "---Please Fill Item---";
                return false;
            }

            if (string.IsNullOrEmpty(databaseSettings.Name))
            {
                error = "---Please Enter Database Name---";
                return false;
            }

            if (databaseSettings.Schemas == null || databaseSettings.Schemas.Count == 0)
            {
                error = "---Please Enter Database Schemas---";
                return false;
            }

            if (string.IsNullOrWhiteSpace(databaseSettings.ConnectionString))
            {
                error = "---Please Fill ConnectionString---";
                return false;
            }

            if (string.IsNullOrEmpty(databaseSettings.DataModelSolutionPath) || !Directory.Exists(databaseSettings.DataModelSolutionPath))
            {
                error = "---Please Enter Valide DataModelSolutionPath---";
                return false;
            }

            error = string.Empty;
            return true;
        }
        private void ClearForm()
        {
            ConnectionStringTextBox.Clear();
            DatabaseSolutionTextBox.Clear();
            SchemaDataGridView.Rows.Clear();
        }
        private void DatabaseNameTextBox_TextChanged(object sender, EventArgs e)
        {
            ClearForm();
        }

        #endregion
    }
}
