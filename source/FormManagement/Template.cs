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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ModelGenerator.FormManagement
{
    public partial class Template : Form
    {
        #region GlobalVariables
        private int _currentRowIndex = 0;

        #endregion

        #region InitialAndLoad
        public Template()
        {
            InitializeComponent();
        }
        private void Template_Load(object sender, EventArgs e)
        {
            LoadTemplateTokens();
        }
        private void LoadTemplateTokens()
        {
            ConstantsListBox.Items.Clear();

            foreach (var token in InitialConstantsListBox.Keys)
                ConstantsListBox.Items.Add(token);

            var toolTip = new ToolTip();
            ConstantsListBox.MouseMove += (s, e) =>
            {
                int index = ConstantsListBox.IndexFromPoint(e.Location);
                if (index >= 0 && index < ConstantsListBox.Items.Count)
                {
                    string item = ConstantsListBox.Items[index].ToString();
                    toolTip.SetToolTip(ConstantsListBox, InitialConstantsListBox[item]);
                }
            };
        }

        #endregion

        #region Public
        public static Dictionary<string, string> InitialConstantsListBox = new()
        {
            ["{Database}"] = "Name Of The Database",
            ["{Schema}"] = "Schema Name Of The Model",
            ["{ClassName}"] = "Name Of The Entity Class",
            ["{Type}"] = "Type Of The Property",
            ["{Name}"] = "Name Of The Property",
            ["{Attributes}"] = "Annotations Like [Key], [Required]",
            ["{FieldName}"] = "Foreign Key Property Name",
            ["{RefType}"] = "Type Of The Referenced Entity",
            ["{RelatedType}"] = "Type Of The Related Collection Entity",
            ["{PropertyName}"] = "Name Of Navigation Or Relation Property",
            ["{NewLine}"] = "New Line Character (\\n)",
            ["{Tab}"] = "Line Tab Character (\\t)"
        };
        public static string ReplaceTemplatePlaceHolders
            (
                string template,
                string database = null,
                string schema = null,
                string className = null,
                string type = null,
                string name = null,
                string attributes = null,
                string fieldName = null,
                string refType = null,
                string relatedType = null,
                string propertyName = null
            )
        {
            string newLine = "\n";
            string tab = "\t";

            if (!string.IsNullOrEmpty(database)) template = template.Replace("{Database}", database);
            else template = template.Replace("{Database}", string.Empty);

            if (!string.IsNullOrEmpty(schema)) template = template.Replace("{Schema}", schema);
            else template = template.Replace("{Schema}", string.Empty);

            if (!string.IsNullOrEmpty(className)) template = template.Replace("{ClassName}", className);
            else template = template.Replace("{ClassName}", string.Empty);

            if (!string.IsNullOrEmpty(type)) template = template.Replace("{Type}", type);
            else template = template.Replace("{Type}", string.Empty);

            if (!string.IsNullOrEmpty(name)) template = template.Replace("{Name}", name);
            else template = template.Replace("{Name}", string.Empty);

            if (!string.IsNullOrEmpty(attributes)) template = template.Replace("{Attributes}", attributes);
            else template = template.Replace("{Attributes}", string.Empty);

            if (!string.IsNullOrEmpty(fieldName)) template = template.Replace("{FieldName}", fieldName);
            else template = template.Replace("{FieldName}", string.Empty);

            if (!string.IsNullOrEmpty(refType)) template = template.Replace("{RefType}", refType);
            else template = template.Replace("{RefType}", string.Empty);

            if (!string.IsNullOrEmpty(relatedType)) template = template.Replace("{RelatedType}", relatedType);
            else template = template.Replace("{RelatedType}", string.Empty);

            if (!string.IsNullOrEmpty(propertyName)) template = template.Replace("{PropertyName}", propertyName);
            else template = template.Replace("{PropertyName}", string.Empty);

            if (!string.IsNullOrEmpty(newLine)) template = template.Replace("{NewLine}", newLine);
            if (!string.IsNullOrEmpty(tab)) template = template.Replace("{Tab}", tab);

            return template;
        }

        #endregion

        #region ButtonEvent
        private void ReturnButton_Click(object sender, EventArgs e)
        {
            var menuForm = new Menu();
            this.Close();
            menuForm.Show();
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure Delete This Config?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(DatabaseNameTextBox.Text))
                {
                    var jsonSetting = new JsonSetting();
                    jsonSetting.DeleteConfigTemplateByDatabaseName(DatabaseNameTextBox.Text);
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
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DatabaseNameTextBox.Text))
            {
                var newConfig = GetConfigFromFormItem();
                var jsonSetting = new JsonSetting();
                bool isSucess = false;
                jsonSetting.InsertOrUpdateDatabase(DatabaseNameTextBox.Text, newConfig, out isSucess);
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
        private void FillDefaultButton_Click(object sender, EventArgs e)
        {
            NamespaceFormatTextBox.Text = "{Database}.{Schema}.DataModel";
            TableAttributeFormatTextBox.Text = "[Table(\"{ClassName}\", Schema = \"{Schema}\")]";
            CustomUsingItemTextBox.Text = "Bpr.Data;";
            CustomColumnAttributeFormatTextBox.Text = "[Editable(true)]";
            ClassDeclarationInheritanceFormatTextBox.Text = "Bpr.Data.EntityBase";
            ClassDeclarationNameFormatTextBox.Text = "public partial class {ClassName}";
            ConstructorFormatTextBox.Text = "public {ClassName}() { }";
            ColumnFormatTextBox.Text = "{Attributes}public {Type} {Name} { get; set; }";
            ReferenceFormatTextBox.Text = "[ForeignKey(\"{FieldName}\")]{NewLine}public virtual {RefType}? {PropertyName} { get; set; }";
            RelationFormatTextBox.Text = "public virtual ICollection<{RelatedType}>? {PropertyName} { get; set; }";

            var customRegionsDict = new Dictionary<string, List<string>>
            {
                ["Constants"] = new List<string>
                {
                    "private const string TABLE_NAME = \"{ClassName}\";",
                    "private const string SCHEMA_NAME = \"{Schema}\";"
                }
            };

            CustomRegionsItemDataGridView.Columns.Clear();
            CustomRegionsItemDataGridView.Rows.Clear();

            CustomRegionsItemDataGridView.Columns.Add("Region", "Region");
            CustomRegionsItemDataGridView.Columns.Add("Items", "Items");

            foreach (var kvp in customRegionsDict)
            {
                CustomRegionsItemDataGridView.Rows.Add(kvp.Key, string.Join(Environment.NewLine, kvp.Value));
            }

        }
        private void LoadButton_Click(object sender, EventArgs e)
        {
            ClearForm();
            var jsonSetting = new JsonSetting();
            jsonSetting.LoadAllDatabases(DatabaseNameTextBox);
            if (!string.IsNullOrEmpty(DatabaseNameTextBox.Text))
                jsonSetting.LoadSelectedDatabaseTemplateConfig
                    (
                        DatabaseNameTextBox.Text,
                        NamespaceFormatTextBox,
                        TableAttributeFormatTextBox,
                        CustomUsingItemTextBox,
                        CustomColumnAttributeFormatTextBox,
                        ClassDeclarationInheritanceFormatTextBox,
                        ClassDeclarationNameFormatTextBox,
                        ConstructorFormatTextBox,
                        ColumnFormatTextBox,
                        ReferenceFormatTextBox,
                        RelationFormatTextBox,
                        OverrideFieldsDataGridView,
                        CustomRegionsItemDataGridView
                    );
        }

        #endregion

        #region EventHandler
        private void ConstantsListBox_MouseDown(object sender, MouseEventArgs e)
        {
            int index = ConstantsListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                string token = ConstantsListBox.Items[index].ToString();
                ConstantsListBox.DoDragDrop(token, DragDropEffects.Copy);
            }
        }
        private void ConstantsListBoxSelectedItem_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        private void ConstantsListBoxSelectedItem_DragDrop(object sender, DragEventArgs e)
        {
            string token = (string)e.Data.GetData(DataFormats.StringFormat);
            var targetBox = (TextBox)sender;
            int pos = targetBox.SelectionStart;
            targetBox.Text = targetBox.Text.Insert(pos, token);
            targetBox.SelectionStart = pos + token.Length;
        }
        private void CustomRegionsItemDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != CustomRegionsItemDataGridView.Columns["Items"].Index)
                return;

            var currentRowIndex = e.RowIndex;
            _currentRowIndex = currentRowIndex;

            if (e.RowIndex >= 0 && e.ColumnIndex == CustomRegionsItemDataGridView.Columns["Items"].Index)
            {
                var currentValue = CustomRegionsItemDataGridView.Rows[e.RowIndex].Cells["Items"].Value?.ToString();
                DataGridViewItemRichTextBox.TextChanged -= DataGridViewItemTextBox_TextChanged;
                DataGridViewItemRichTextBox.Text = currentValue;
                DataGridViewItemRichTextBox.TextChanged += DataGridViewItemTextBox_TextChanged;

                DataGridViewItemRichTextBox.Visible = true;
                DataGridViewItemRichTextBox.Focus();
            }
        }
        private void CustomRegionsItemDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (CustomRegionsItemDataGridView.Columns[e.ColumnIndex].Name == "Items")
            {
                e.Cancel = true;
            }
        }
        private void DataGridViewItemTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_currentRowIndex >= 0)
            {
                CustomRegionsItemDataGridView.Rows[_currentRowIndex].Cells["Items"].Value = DataGridViewItemRichTextBox.Text;
            }
        }
        private OrganizeTemplateConfig GetConfigFromFormItem()
        {
            return new OrganizeTemplateConfig()
            {
                NamespaceFormat = NamespaceFormatTextBox.Text,
                ColumnFormat = ColumnFormatTextBox.Text,
                CustomUsingItem = CustomUsingItemTextBox.Text,
                ClassDeclarationInheritanceFormat = ClassDeclarationInheritanceFormatTextBox.Text,
                ClassDeclarationNameFormat = ClassDeclarationNameFormatTextBox.Text,
                ConstructorFormat = ConstructorFormatTextBox.Text,
                CustomColumnAttributeFormat = CustomColumnAttributeFormatTextBox.Text,
                TableAttributeFormat = TableAttributeFormatTextBox.Text,
                ReferenceFormat = ReferenceFormatTextBox.Text,
                RelationFormat = RelationFormatTextBox.Text,
                OverrideMainColumns = OverrideFieldsDataGridView.Rows
                                                                .Cast<DataGridViewRow>()
                                                                .Where(row => row.Cells["Field"].Value != null)
                                                                .Select(row => row.Cells["Field"].Value.ToString())
                                                                .ToList(),
                CustomRegionsItem = CustomRegionsItemDataGridView.Rows
                                                                .Cast<DataGridViewRow>()
                                                                .Where(row => row.Cells["Region"].Value != null && row.Cells["Items"].Value != null)
                                                                .ToDictionary(
                                                                    row => row.Cells["Region"].Value.ToString()!,
                                                                    row => row.Cells["Items"].Value.ToString()!
                                                                )

            };
        }
        private void ClearForm()
        {
            NamespaceFormatTextBox.Clear();
            TableAttributeFormatTextBox.Clear();
            CustomUsingItemTextBox.Clear();
            CustomColumnAttributeFormatTextBox.Clear();
            ClassDeclarationInheritanceFormatTextBox.Clear();
            ClassDeclarationNameFormatTextBox.Clear();
            ConstructorFormatTextBox.Clear();
            ColumnFormatTextBox.Clear();
            ReferenceFormatTextBox.Clear();
            RelationFormatTextBox.Clear();
            OverrideFieldsDataGridView.Rows.Clear();
            CustomRegionsItemDataGridView.Rows.Clear();
        }

        #endregion
    }
}
