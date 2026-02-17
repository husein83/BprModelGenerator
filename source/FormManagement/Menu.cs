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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void GeneratorServiceButton_Click(object sender, EventArgs e)
        {
            var generatorForm = new Generator();
            this.Hide();
            generatorForm.ShowDialog();
        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            var settingForm = new Setting();
            this.Hide();
            settingForm.ShowDialog();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure Close Application?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void TemplateManagementButton_Click(object sender, EventArgs e)
        {
            var templateForm = new Template();
            this.Hide();
            templateForm.ShowDialog();
        }

        private void SchemaSolutionButton_Click(object sender, EventArgs e)
        {
            var updateProjectDataModel = new UpdateProjectDataModel();
            this.Hide();
            updateProjectDataModel.ShowDialog();
        }
    }
}
