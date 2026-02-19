namespace ModelGenerator.FormManagement
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            HeaderLabel = new Label();
            SettingButton = new Button();
            GeneratorServiceButton = new Button();
            ExitButton = new Button();
            TemplateManagementButton = new Button();
            SchemaSolutionButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // HeaderLabel
            // 
            HeaderLabel.AutoSize = true;
            HeaderLabel.Font = new Font("Dancing Script", 34F, FontStyle.Bold);
            HeaderLabel.Location = new Point(192, 61);
            HeaderLabel.Margin = new Padding(0);
            HeaderLabel.Name = "HeaderLabel";
            HeaderLabel.Size = new Size(683, 96);
            HeaderLabel.TabIndex = 5;
            HeaderLabel.Text = "Bpr Model Generator Tool";
            // 
            // SettingButton
            // 
            SettingButton.BackColor = Color.LightSlateGray;
            SettingButton.Font = new Font("Ubuntu", 16F);
            SettingButton.ForeColor = SystemColors.Control;
            SettingButton.Location = new Point(171, 210);
            SettingButton.Name = "SettingButton";
            SettingButton.Size = new Size(728, 81);
            SettingButton.TabIndex = 7;
            SettingButton.Text = "Generator Management Setting";
            SettingButton.UseVisualStyleBackColor = false;
            SettingButton.Click += SettingButton_Click;
            // 
            // GeneratorServiceButton
            // 
            GeneratorServiceButton.BackColor = Color.LightSlateGray;
            GeneratorServiceButton.Font = new Font("Ubuntu", 16F);
            GeneratorServiceButton.ForeColor = SystemColors.Control;
            GeneratorServiceButton.Location = new Point(332, 482);
            GeneratorServiceButton.Name = "GeneratorServiceButton";
            GeneratorServiceButton.Size = new Size(412, 81);
            GeneratorServiceButton.TabIndex = 8;
            GeneratorServiceButton.Text = "Generator Service";
            GeneratorServiceButton.UseVisualStyleBackColor = false;
            GeneratorServiceButton.Click += GeneratorServiceButton_Click;
            // 
            // ExitButton
            // 
            ExitButton.BackColor = Color.Crimson;
            ExitButton.Font = new Font("Ubuntu", 16F);
            ExitButton.ForeColor = SystemColors.Control;
            ExitButton.Location = new Point(397, 575);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(285, 70);
            ExitButton.TabIndex = 9;
            ExitButton.Text = "Exit";
            ExitButton.UseVisualStyleBackColor = false;
            ExitButton.Click += ExitButton_Click;
            // 
            // TemplateManagementButton
            // 
            TemplateManagementButton.BackColor = Color.LightSlateGray;
            TemplateManagementButton.Font = new Font("Ubuntu", 16F);
            TemplateManagementButton.ForeColor = SystemColors.Control;
            TemplateManagementButton.Location = new Point(213, 300);
            TemplateManagementButton.Name = "TemplateManagementButton";
            TemplateManagementButton.Size = new Size(649, 81);
            TemplateManagementButton.TabIndex = 10;
            TemplateManagementButton.Text = "Template Management Setting";
            TemplateManagementButton.UseVisualStyleBackColor = false;
            TemplateManagementButton.Click += TemplateManagementButton_Click;
            // 
            // SchemaSolutionButton
            // 
            SchemaSolutionButton.BackColor = Color.LightSlateGray;
            SchemaSolutionButton.Font = new Font("Ubuntu", 16F);
            SchemaSolutionButton.ForeColor = SystemColors.Control;
            SchemaSolutionButton.Location = new Point(274, 391);
            SchemaSolutionButton.Name = "SchemaSolutionButton";
            SchemaSolutionButton.Size = new Size(537, 81);
            SchemaSolutionButton.TabIndex = 11;
            SchemaSolutionButton.Text = "Schema Solution Setting";
            SchemaSolutionButton.UseVisualStyleBackColor = false;
            SchemaSolutionButton.Click += SchemaSolutionButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Dancing Script", 30F, FontStyle.Bold);
            label1.Location = new Point(85, 207);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(74, 84);
            label1.TabIndex = 12;
            label1.Text = "1.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Control;
            label2.Font = new Font("Dancing Script", 30F, FontStyle.Bold);
            label2.Location = new Point(116, 298);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(86, 84);
            label2.TabIndex = 13;
            label2.Text = "2.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Dancing Script", 30F, FontStyle.Bold);
            label3.Location = new Point(181, 389);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(83, 84);
            label3.TabIndex = 14;
            label3.Text = "3.";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Dancing Script", 30F, FontStyle.Bold);
            label4.Location = new Point(242, 480);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(80, 84);
            label4.TabIndex = 15;
            label4.Text = "4.";
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1088, 679);
            ControlBox = false;
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(SchemaSolutionButton);
            Controls.Add(TemplateManagementButton);
            Controls.Add(ExitButton);
            Controls.Add(GeneratorServiceButton);
            Controls.Add(SettingButton);
            Controls.Add(HeaderLabel);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label HeaderLabel;
        private Button SettingButton;
        private Button GeneratorServiceButton;
        private Button ExitButton;
        private Button TemplateManagementButton;
        private Button SchemaSolutionButton;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}