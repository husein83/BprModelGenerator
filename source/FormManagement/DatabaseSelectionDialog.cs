using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelGenerator.FormManagement
{
    public partial class DatabaseSelectionDialog : Form
    {
        private readonly ListBox listBox;
        private readonly Button btnSelect;
        private readonly List<string> databaseNames;

        public DatabaseSelectionDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // DatabaseSelectionDialog
            // 
            ClientSize = new Size(350, 300);
            Name = "DatabaseSelectionDialog";
            Load += DatabaseSelectionDialog_Load;
            ResumeLayout(false);
        }

        public string SelectedDatabase { get; private set; }

        public DatabaseSelectionDialog(List<string> dbNames)
        {
            databaseNames = dbNames;
            listBox = new ListBox { Height = 350, Width = 400 };
            btnSelect = new Button { Text = "Select", Location = new Point(90, 340), Width = 200, Height = 45, Font = new Font("Ubuntu", 13) };

            this.Text = "Select Database";
            this.Width = 400;
            this.Height = 450;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterParent;

            listBox.Items.AddRange(databaseNames.ToArray());

            btnSelect.Click += (s, e) =>
            {
                if (listBox.SelectedItem is string selected)
                {
                    SelectedDatabase = selected;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            };

            listBox.DoubleClick += (s, e) => btnSelect.PerformClick();

            this.Controls.Add(listBox);
            this.Controls.Add(btnSelect);
        }

        private void DatabaseSelectionDialog_Load(object sender, EventArgs e)
        {

        }
    }

}
