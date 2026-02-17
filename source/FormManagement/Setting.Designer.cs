namespace ModelGenerator.FormManagement
{
    partial class Setting
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
            BrowseButton = new Button();
            DatabaseSolutionLabel = new Label();
            DatabaseSolutionTextBox = new TextBox();
            DatabaseNameTextBox = new TextBox();
            DatabaseNameLabel = new Label();
            SchemaLabel = new Label();
            ConnectionStringTextBox = new TextBox();
            ConnectionStringLabel = new Label();
            SchemaDataGridView = new DataGridView();
            SchemaName = new DataGridViewTextBoxColumn();
            AccessTo = new DataGridViewButtonColumn();
            HeaderLabel = new Label();
            ReturnButton = new Button();
            SaveButton = new Button();
            LoadButton = new Button();
            ConnectionStringTemplateButton = new Button();
            DeleteButton = new Button();
            ((System.ComponentModel.ISupportInitialize)SchemaDataGridView).BeginInit();
            SuspendLayout();
            // 
            // BrowseButton
            // 
            BrowseButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BrowseButton.Location = new Point(895, 499);
            BrowseButton.Name = "BrowseButton";
            BrowseButton.Size = new Size(104, 56);
            BrowseButton.TabIndex = 0;
            BrowseButton.Text = "Browse";
            BrowseButton.UseVisualStyleBackColor = true;
            BrowseButton.Click += BrowseButton_Click;
            // 
            // DatabaseSolutionLabel
            // 
            DatabaseSolutionLabel.AutoSize = true;
            DatabaseSolutionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DatabaseSolutionLabel.Location = new Point(82, 499);
            DatabaseSolutionLabel.Name = "DatabaseSolutionLabel";
            DatabaseSolutionLabel.Size = new Size(314, 32);
            DatabaseSolutionLabel.TabIndex = 1;
            DatabaseSolutionLabel.Text = "DataModel Solution Path :";
            // 
            // DatabaseSolutionTextBox
            // 
            DatabaseSolutionTextBox.Location = new Point(402, 488);
            DatabaseSolutionTextBox.Multiline = true;
            DatabaseSolutionTextBox.Name = "DatabaseSolutionTextBox";
            DatabaseSolutionTextBox.ReadOnly = true;
            DatabaseSolutionTextBox.Size = new Size(487, 78);
            DatabaseSolutionTextBox.TabIndex = 2;
            // 
            // DatabaseNameTextBox
            // 
            DatabaseNameTextBox.Location = new Point(402, 100);
            DatabaseNameTextBox.Multiline = true;
            DatabaseNameTextBox.Name = "DatabaseNameTextBox";
            DatabaseNameTextBox.Size = new Size(289, 46);
            DatabaseNameTextBox.TabIndex = 5;
            DatabaseNameTextBox.TextChanged += DatabaseNameTextBox_TextChanged;
            // 
            // DatabaseNameLabel
            // 
            DatabaseNameLabel.AutoSize = true;
            DatabaseNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DatabaseNameLabel.Location = new Point(157, 106);
            DatabaseNameLabel.Name = "DatabaseNameLabel";
            DatabaseNameLabel.Size = new Size(207, 32);
            DatabaseNameLabel.TabIndex = 4;
            DatabaseNameLabel.Text = "Database Name :";
            // 
            // SchemaLabel
            // 
            SchemaLabel.AutoSize = true;
            SchemaLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SchemaLabel.Location = new Point(137, 173);
            SchemaLabel.Name = "SchemaLabel";
            SchemaLabel.Size = new Size(227, 32);
            SchemaLabel.TabIndex = 6;
            SchemaLabel.Text = "Database Schema :";
            // 
            // ConnectionStringTextBox
            // 
            ConnectionStringTextBox.Location = new Point(402, 382);
            ConnectionStringTextBox.Multiline = true;
            ConnectionStringTextBox.Name = "ConnectionStringTextBox";
            ConnectionStringTextBox.Size = new Size(487, 81);
            ConnectionStringTextBox.TabIndex = 9;
            // 
            // ConnectionStringLabel
            // 
            ConnectionStringLabel.AutoSize = true;
            ConnectionStringLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ConnectionStringLabel.Location = new Point(50, 387);
            ConnectionStringLabel.Name = "ConnectionStringLabel";
            ConnectionStringLabel.Size = new Size(346, 32);
            ConnectionStringLabel.TabIndex = 8;
            ConnectionStringLabel.Text = "Database Connection String :";
            // 
            // SchemaDataGridView
            // 
            SchemaDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            SchemaDataGridView.Columns.AddRange(new DataGridViewColumn[] { SchemaName, AccessTo });
            SchemaDataGridView.Location = new Point(402, 166);
            SchemaDataGridView.Name = "SchemaDataGridView";
            SchemaDataGridView.RowHeadersWidth = 62;
            SchemaDataGridView.Size = new Size(597, 188);
            SchemaDataGridView.TabIndex = 10;
            SchemaDataGridView.CellClick += SchemaDataGridView_CellClick;
            SchemaDataGridView.CellFormatting += SchemaDataGridView_CellFormatting;
            // 
            // SchemaName
            // 
            SchemaName.HeaderText = "Schema Name";
            SchemaName.MinimumWidth = 8;
            SchemaName.Name = "SchemaName";
            SchemaName.Width = 150;
            // 
            // AccessTo
            // 
            AccessTo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AccessTo.HeaderText = "Access To";
            AccessTo.MinimumWidth = 8;
            AccessTo.Name = "AccessTo";
            AccessTo.UseColumnTextForButtonValue = true;
            // 
            // HeaderLabel
            // 
            HeaderLabel.AutoSize = true;
            HeaderLabel.Font = new Font("Dancing Script", 27F, FontStyle.Bold);
            HeaderLabel.Location = new Point(825, 41);
            HeaderLabel.Margin = new Padding(0);
            HeaderLabel.Name = "HeaderLabel";
            HeaderLabel.Size = new Size(174, 76);
            HeaderLabel.TabIndex = 11;
            HeaderLabel.Text = "Setting";
            // 
            // ReturnButton
            // 
            ReturnButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ReturnButton.Location = new Point(147, 597);
            ReturnButton.Name = "ReturnButton";
            ReturnButton.Size = new Size(248, 53);
            ReturnButton.TabIndex = 13;
            ReturnButton.Text = "Return";
            ReturnButton.UseVisualStyleBackColor = true;
            ReturnButton.Click += ReturnButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SaveButton.Location = new Point(435, 597);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(248, 53);
            SaveButton.TabIndex = 14;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // LoadButton
            // 
            LoadButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LoadButton.Location = new Point(697, 100);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(80, 46);
            LoadButton.TabIndex = 15;
            LoadButton.Text = "Load...";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // ConnectionStringTemplateButton
            // 
            ConnectionStringTemplateButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ConnectionStringTemplateButton.Location = new Point(895, 396);
            ConnectionStringTemplateButton.Name = "ConnectionStringTemplateButton";
            ConnectionStringTemplateButton.Size = new Size(104, 56);
            ConnectionStringTemplateButton.TabIndex = 16;
            ConnectionStringTemplateButton.Text = "Template";
            ConnectionStringTemplateButton.UseVisualStyleBackColor = true;
            ConnectionStringTemplateButton.Click += ConnectionStringTemplateButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DeleteButton.Location = new Point(726, 597);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(248, 53);
            DeleteButton.TabIndex = 17;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1097, 682);
            ControlBox = false;
            Controls.Add(DeleteButton);
            Controls.Add(ConnectionStringTemplateButton);
            Controls.Add(LoadButton);
            Controls.Add(SaveButton);
            Controls.Add(ReturnButton);
            Controls.Add(HeaderLabel);
            Controls.Add(SchemaDataGridView);
            Controls.Add(ConnectionStringTextBox);
            Controls.Add(ConnectionStringLabel);
            Controls.Add(SchemaLabel);
            Controls.Add(DatabaseNameTextBox);
            Controls.Add(DatabaseNameLabel);
            Controls.Add(DatabaseSolutionTextBox);
            Controls.Add(DatabaseSolutionLabel);
            Controls.Add(BrowseButton);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "Setting";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Setting";
            ((System.ComponentModel.ISupportInitialize)SchemaDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BrowseButton;
        private Label DatabaseSolutionLabel;
        private TextBox DatabaseSolutionTextBox;
        private TextBox DatabaseNameTextBox;
        private Label DatabaseNameLabel;
        private Label SchemaLabel;
        private TextBox ConnectionStringTextBox;
        private Label ConnectionStringLabel;
        private DataGridView SchemaDataGridView;
        private DataGridViewTextBoxColumn SchemaName;
        private DataGridViewButtonColumn AccessTo;
        private Label HeaderLabel;
        private Button ReturnButton;
        private Button SaveButton;
        private Button LoadButton;
        private Button ConnectionStringTemplateButton;
        private Button DeleteButton;
    }
}