using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ModelGenerator.FormManagement
{
    public partial class AccessSelectionDialog : Form
    {

        private List<string> _options;
        private CheckedListBox _checkedListBox;

        public AccessSelectionDialog(List<string> options)
        {
            _options = options;
            InitializeComponent();
        }

        public List<string> GetSelectedSchemas()
        {
            return _checkedListBox.CheckedItems.Cast<string>().ToList();
        }

        private void InitializeComponent()
        {
            _checkedListBox = new CheckedListBox
            {
                Dock = DockStyle.Fill,
                CheckOnClick = true
            };

            Controls.Add(_checkedListBox);

            _checkedListBox.Items.AddRange(_options.ToArray());

            ClientSize = new Size(512, 348);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AccessSelectionDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Access Permissions";

            var okButton = new Button
            {
                Text = "OK",
                Dock = DockStyle.Bottom,
                Height = 40
            };
            okButton.Click += (s, e) => { DialogResult = DialogResult.OK; Close(); };
            Controls.Add(okButton);
        }
    }
}
