using ModelGenerator.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelGenerator.FormManagement
{
    public partial class UpdateProjectDataModel : Form
    {
        #region InitialAndLoad
        public UpdateProjectDataModel()
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
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DatabaseNameTextBox.Text))
            {
                var newDataModelPath = GetSchemaDataModelPathDictionary();
                var jsonSetting = new JsonSetting();
                bool isSucess = false;
                jsonSetting.InsertOrUpdateDatabase(DatabaseNameTextBox.Text, newDataModelPath, out isSucess);
                if (isSucess && MessageBox.Show("Apply Changes Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    var menuForm = new Menu();
                    this.Close();
                    menuForm.Show();
                }
            }
            else
            {
                MessageBox.Show("---Please Enter Database Name---", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure Delete Schemas DataModel Path Of This Database?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(DatabaseNameTextBox.Text))
                {
                    var jsonSetting = new JsonSetting();
                    jsonSetting.DeleteSchemaDataModelPathByDatabaseName(DatabaseNameTextBox.Text);
                    if (MessageBox.Show("Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        var menuForm = new Menu();
                        this.Close();
                        menuForm.Show();
                    }
                }
                else
                {
                    MessageBox.Show("---Please Enter Database Name---", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void LoadButton_Click(object sender, EventArgs e)
        {
            var jsonSetting = new JsonSetting();
            jsonSetting.LoadAllDatabases(DatabaseNameTextBox);
            if (!string.IsNullOrEmpty(DatabaseNameTextBox.Text))
            {
                jsonSetting.LoadSelectedDatabaseUpdateSchemaSolution
                    (
                        DatabaseNameTextBox.Text,
                        SchemaSolutionDataGridView
                    );
                LoadSchemaComboBoxValues();
            }
        }

        #endregion

        #region EventHandler
        private List<string> _schemaComboBoxItemList = new List<string>();
        private Dictionary<string, string> GetSchemaDataModelPathDictionary()
        {
            var dict = new Dictionary<string, string>();

            foreach (DataGridViewRow row in SchemaSolutionDataGridView.Rows)
            {
                if (row.IsNewRow) continue;

                string key = row.Cells["Schema"].Value?.ToString();
                string value = row.Cells["SolutionPath"].Value?.ToString();

                if (!string.IsNullOrWhiteSpace(key))
                {
                    dict[key] = value;
                }
            }

            return dict;
        }
        private void SchemaSolutionDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != SchemaSolutionDataGridView.Columns["SolutionPath"].Index)
                return;

            using var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SchemaSolutionDataGridView.Rows[e.RowIndex].Cells["SolutionPath"].Value = dialog.SelectedPath;
            }
        }
        public void LoadSchemaComboBoxValues()
        {
            var jsonSetting = new JsonSetting();
            var schemaList = jsonSetting.GetAllSchemaByDatabaseName(DatabaseNameTextBox.Text);
            if (SchemaSolutionDataGridView.Columns["Schema"] is DataGridViewComboBoxColumn comboColumn)
            {
                comboColumn.DataSource = null;
                comboColumn.Items.Clear();

                foreach (var item in schemaList)
                {
                    comboColumn.Items.Add(item);
                }
            }
            _schemaComboBoxItemList = schemaList.ToList();
        }
        private void SchemaSolutionDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (SchemaSolutionDataGridView.CurrentCell.ColumnIndex == SchemaSolutionDataGridView.Columns["Schema"].Index &&
                e.Control is ComboBox comboBox)
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox.Items.Clear();

                var selectedValues = new HashSet<string>();

                foreach (DataGridViewRow row in SchemaSolutionDataGridView.Rows)
                {
                    if (row.Cells["Schema"].Value is string val && !string.IsNullOrWhiteSpace(val) &&
                        row.Index != SchemaSolutionDataGridView.CurrentCell.RowIndex)
                    {
                        selectedValues.Add(val);
                    }
                }

                var availableOptions = _schemaComboBoxItemList.Except(selectedValues).ToList();

                foreach (var option in availableOptions)
                    comboBox.Items.Add(option);
            }
        }

        #endregion
    }
}
