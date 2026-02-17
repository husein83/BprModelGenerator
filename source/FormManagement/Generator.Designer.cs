namespace ModelGenerator.FormManagement
{
    partial class Generator
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SchemaComboBox = new ComboBox();
            SchemaLabel = new Label();
            InformationListBox = new ListBox();
            HeaderLabel = new Label();
            OrganizeDataModelButton = new Button();
            GenerateButton = new Button();
            ClearAllButton = new Button();
            DbContextDeleteButton = new Button();
            DatabaseLabel = new Label();
            DatabaseComboBox = new ComboBox();
            ReturnButton = new Button();
            UpdateProjectDataModelsButton = new Button();
            DeleteDataModelButton = new Button();
            SuspendLayout();
            // 
            // SchemaComboBox
            // 
            SchemaComboBox.FormattingEnabled = true;
            SchemaComboBox.Location = new Point(321, 152);
            SchemaComboBox.Name = "SchemaComboBox";
            SchemaComboBox.Size = new Size(268, 33);
            SchemaComboBox.TabIndex = 1;
            // 
            // SchemaLabel
            // 
            SchemaLabel.AutoSize = true;
            SchemaLabel.Font = new Font("Ubuntu", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SchemaLabel.Location = new Point(321, 123);
            SchemaLabel.Name = "SchemaLabel";
            SchemaLabel.Size = new Size(89, 25);
            SchemaLabel.TabIndex = 2;
            SchemaLabel.Text = "Schema:";
            // 
            // InformationListBox
            // 
            InformationListBox.FormattingEnabled = true;
            InformationListBox.HorizontalScrollbar = true;
            InformationListBox.ItemHeight = 25;
            InformationListBox.Location = new Point(26, 233);
            InformationListBox.Name = "InformationListBox";
            InformationListBox.Size = new Size(563, 504);
            InformationListBox.Sorted = true;
            InformationListBox.TabIndex = 3;
            // 
            // HeaderLabel
            // 
            HeaderLabel.AutoSize = true;
            HeaderLabel.Font = new Font("Dancing Script", 26F, FontStyle.Bold);
            HeaderLabel.Location = new Point(321, 23);
            HeaderLabel.Margin = new Padding(0);
            HeaderLabel.Name = "HeaderLabel";
            HeaderLabel.Size = new Size(414, 73);
            HeaderLabel.TabIndex = 4;
            HeaderLabel.Text = "DataModelOrganize";
            // 
            // OrganizeDataModelButton
            // 
            OrganizeDataModelButton.BackColor = SystemColors.GrayText;
            OrganizeDataModelButton.Font = new Font("Ubuntu", 13.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            OrganizeDataModelButton.ForeColor = SystemColors.Control;
            OrganizeDataModelButton.Location = new Point(626, 272);
            OrganizeDataModelButton.Name = "OrganizeDataModelButton";
            OrganizeDataModelButton.Size = new Size(386, 87);
            OrganizeDataModelButton.TabIndex = 5;
            OrganizeDataModelButton.Text = "Organize DataModel";
            OrganizeDataModelButton.UseVisualStyleBackColor = false;
            OrganizeDataModelButton.Click += OrganizeDataModelButton_Click;
            // 
            // GenerateButton
            // 
            GenerateButton.BackColor = Color.SteelBlue;
            GenerateButton.Font = new Font("Ubuntu", 13.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            GenerateButton.ForeColor = SystemColors.Control;
            GenerateButton.Location = new Point(626, 173);
            GenerateButton.Name = "GenerateButton";
            GenerateButton.Size = new Size(386, 87);
            GenerateButton.TabIndex = 6;
            GenerateButton.Text = "Generate";
            GenerateButton.UseVisualStyleBackColor = false;
            GenerateButton.Click += GenerateButton_Click;
            // 
            // ClearAllButton
            // 
            ClearAllButton.BackColor = SystemColors.ScrollBar;
            ClearAllButton.Font = new Font("Ubuntu", 13.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ClearAllButton.Location = new Point(626, 619);
            ClearAllButton.Name = "ClearAllButton";
            ClearAllButton.Size = new Size(386, 62);
            ClearAllButton.TabIndex = 7;
            ClearAllButton.Text = "Delete All DataModels";
            ClearAllButton.UseVisualStyleBackColor = false;
            ClearAllButton.Click += ClearAllButton_Click;
            // 
            // DbContextDeleteButton
            // 
            DbContextDeleteButton.BackColor = SystemColors.ScrollBar;
            DbContextDeleteButton.Font = new Font("Ubuntu", 13.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DbContextDeleteButton.Location = new Point(626, 395);
            DbContextDeleteButton.Name = "DbContextDeleteButton";
            DbContextDeleteButton.Size = new Size(386, 62);
            DbContextDeleteButton.TabIndex = 8;
            DbContextDeleteButton.Text = "Delete DbContext File";
            DbContextDeleteButton.UseVisualStyleBackColor = false;
            DbContextDeleteButton.Click += DbContextDeleteButton_Click;
            // 
            // DatabaseLabel
            // 
            DatabaseLabel.AutoSize = true;
            DatabaseLabel.Font = new Font("Ubuntu", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DatabaseLabel.Location = new Point(26, 123);
            DatabaseLabel.Name = "DatabaseLabel";
            DatabaseLabel.Size = new Size(101, 25);
            DatabaseLabel.TabIndex = 10;
            DatabaseLabel.Text = "Database:";
            // 
            // DatabaseComboBox
            // 
            DatabaseComboBox.FormattingEnabled = true;
            DatabaseComboBox.Location = new Point(26, 152);
            DatabaseComboBox.Name = "DatabaseComboBox";
            DatabaseComboBox.Size = new Size(268, 33);
            DatabaseComboBox.TabIndex = 9;
            DatabaseComboBox.SelectedIndexChanged += DatabaseComboBox_SelectedIndexChanged;
            // 
            // ReturnButton
            // 
            ReturnButton.Font = new Font("Ubuntu", 12F, FontStyle.Bold);
            ReturnButton.Location = new Point(717, 706);
            ReturnButton.Name = "ReturnButton";
            ReturnButton.Size = new Size(201, 53);
            ReturnButton.TabIndex = 14;
            ReturnButton.Text = "Return";
            ReturnButton.UseVisualStyleBackColor = true;
            ReturnButton.Click += ReturnButton_Click;
            // 
            // UpdateProjectDataModelsButton
            // 
            UpdateProjectDataModelsButton.BackColor = SystemColors.ScrollBar;
            UpdateProjectDataModelsButton.Font = new Font("Ubuntu", 13.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UpdateProjectDataModelsButton.Location = new Point(626, 544);
            UpdateProjectDataModelsButton.Name = "UpdateProjectDataModelsButton";
            UpdateProjectDataModelsButton.Size = new Size(386, 62);
            UpdateProjectDataModelsButton.TabIndex = 15;
            UpdateProjectDataModelsButton.Text = "Update Project DataModels";
            UpdateProjectDataModelsButton.UseVisualStyleBackColor = false;
            UpdateProjectDataModelsButton.Click += UpdateProjectDataModelsButton_Click;
            // 
            // DeleteDataModelButton
            // 
            DeleteDataModelButton.BackColor = SystemColors.ScrollBar;
            DeleteDataModelButton.Font = new Font("Ubuntu", 13.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DeleteDataModelButton.Location = new Point(626, 470);
            DeleteDataModelButton.Name = "DeleteDataModelButton";
            DeleteDataModelButton.Size = new Size(386, 62);
            DeleteDataModelButton.TabIndex = 17;
            DeleteDataModelButton.Text = "Delete DataModel File";
            DeleteDataModelButton.UseVisualStyleBackColor = false;
            DeleteDataModelButton.Click += DeleteDataModelButton_Click;
            // 
            // Generator
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1063, 797);
            ControlBox = false;
            Controls.Add(DeleteDataModelButton);
            Controls.Add(UpdateProjectDataModelsButton);
            Controls.Add(ReturnButton);
            Controls.Add(DatabaseLabel);
            Controls.Add(DatabaseComboBox);
            Controls.Add(DbContextDeleteButton);
            Controls.Add(ClearAllButton);
            Controls.Add(GenerateButton);
            Controls.Add(OrganizeDataModelButton);
            Controls.Add(HeaderLabel);
            Controls.Add(InformationListBox);
            Controls.Add(SchemaLabel);
            Controls.Add(SchemaComboBox);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "Generator";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Generator";
            Load += Generator_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox SchemaComboBox;
        private Label SchemaLabel;
        private ListBox InformationListBox;
        private Label HeaderLabel;
        private Button OrganizeDataModelButton;
        private Button GenerateButton;
        private Button ClearAllButton;
        private Button DbContextDeleteButton;
        private Label DatabaseLabel;
        private ComboBox DatabaseComboBox;
        private Button ReturnButton;
        private Button UpdateProjectDataModelsButton;
        private Button DeleteDataModelButton;
    }
}
