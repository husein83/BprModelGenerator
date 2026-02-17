namespace ModelGenerator.FormManagement
{
    partial class UpdateProjectDataModel
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
            LoadButton = new Button();
            DatabaseNameTextBox = new TextBox();
            DatabaseNameLabel = new Label();
            HeaderLabel = new Label();
            DeleteButton = new Button();
            SaveButton = new Button();
            ReturnButton = new Button();
            SchemaSolutionDataGridView = new DataGridView();
            Schema = new DataGridViewComboBoxColumn();
            SolutionPath = new DataGridViewButtonColumn();
            SchemaSolutionPathLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)SchemaSolutionDataGridView).BeginInit();
            SuspendLayout();
            // 
            // LoadButton
            // 
            LoadButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LoadButton.Location = new Point(605, 138);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(109, 48);
            LoadButton.TabIndex = 21;
            LoadButton.Text = "Load...";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // DatabaseNameTextBox
            // 
            DatabaseNameTextBox.Location = new Point(305, 138);
            DatabaseNameTextBox.Multiline = true;
            DatabaseNameTextBox.Name = "DatabaseNameTextBox";
            DatabaseNameTextBox.ReadOnly = true;
            DatabaseNameTextBox.Size = new Size(289, 46);
            DatabaseNameTextBox.TabIndex = 20;
            // 
            // DatabaseNameLabel
            // 
            DatabaseNameLabel.AutoSize = true;
            DatabaseNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DatabaseNameLabel.Location = new Point(76, 142);
            DatabaseNameLabel.Name = "DatabaseNameLabel";
            DatabaseNameLabel.Size = new Size(207, 32);
            DatabaseNameLabel.TabIndex = 19;
            DatabaseNameLabel.Text = "Database Name :";
            // 
            // HeaderLabel
            // 
            HeaderLabel.AutoSize = true;
            HeaderLabel.Font = new Font("Dancing Script", 29F, FontStyle.Bold);
            HeaderLabel.Location = new Point(135, 20);
            HeaderLabel.Margin = new Padding(0);
            HeaderLabel.Name = "HeaderLabel";
            HeaderLabel.Size = new Size(602, 82);
            HeaderLabel.TabIndex = 22;
            HeaderLabel.Text = "Update Project DataModel";
            // 
            // DeleteButton
            // 
            DeleteButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DeleteButton.Location = new Point(591, 513);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(201, 53);
            DeleteButton.TabIndex = 48;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SaveButton.Location = new Point(322, 513);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(201, 53);
            SaveButton.TabIndex = 47;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // ReturnButton
            // 
            ReturnButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ReturnButton.Location = new Point(52, 513);
            ReturnButton.Name = "ReturnButton";
            ReturnButton.Size = new Size(201, 53);
            ReturnButton.TabIndex = 46;
            ReturnButton.Text = "Return";
            ReturnButton.UseVisualStyleBackColor = true;
            ReturnButton.Click += ReturnButton_Click;
            // 
            // SchemaSolutionDataGridView
            // 
            SchemaSolutionDataGridView.AllowDrop = true;
            SchemaSolutionDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            SchemaSolutionDataGridView.Columns.AddRange(new DataGridViewColumn[] { Schema, SolutionPath });
            SchemaSolutionDataGridView.Location = new Point(91, 265);
            SchemaSolutionDataGridView.Name = "SchemaSolutionDataGridView";
            SchemaSolutionDataGridView.RowHeadersWidth = 62;
            SchemaSolutionDataGridView.Size = new Size(659, 210);
            SchemaSolutionDataGridView.TabIndex = 50;
            SchemaSolutionDataGridView.CellClick += SchemaSolutionDataGridView_CellClick;
            SchemaSolutionDataGridView.EditingControlShowing += SchemaSolutionDataGridView_EditingControlShowing;
            // 
            // Schema
            // 
            Schema.HeaderText = "Schema";
            Schema.MinimumWidth = 8;
            Schema.Name = "Schema";
            Schema.Width = 150;
            // 
            // SolutionPath
            // 
            SolutionPath.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            SolutionPath.HeaderText = "Solution Path";
            SolutionPath.MinimumWidth = 8;
            SolutionPath.Name = "SolutionPath";
            // 
            // SchemaSolutionPathLabel
            // 
            SchemaSolutionPathLabel.AutoSize = true;
            SchemaSolutionPathLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SchemaSolutionPathLabel.Location = new Point(85, 218);
            SchemaSolutionPathLabel.Name = "SchemaSolutionPathLabel";
            SchemaSolutionPathLabel.Size = new Size(268, 32);
            SchemaSolutionPathLabel.TabIndex = 49;
            SchemaSolutionPathLabel.Text = "Schema Solution Path:";
            // 
            // UpdateProjectDataModel
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(861, 588);
            ControlBox = false;
            Controls.Add(SchemaSolutionDataGridView);
            Controls.Add(SchemaSolutionPathLabel);
            Controls.Add(DeleteButton);
            Controls.Add(SaveButton);
            Controls.Add(ReturnButton);
            Controls.Add(HeaderLabel);
            Controls.Add(LoadButton);
            Controls.Add(DatabaseNameTextBox);
            Controls.Add(DatabaseNameLabel);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "UpdateProjectDataModel";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "UpdateProjectDataModel";
            ((System.ComponentModel.ISupportInitialize)SchemaSolutionDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button LoadButton;
        private TextBox DatabaseNameTextBox;
        private Label DatabaseNameLabel;
        private Label HeaderLabel;
        private Button DeleteButton;
        private Button SaveButton;
        private Button ReturnButton;
        private DataGridView SchemaSolutionDataGridView;
        private DataGridViewComboBoxColumn Schema;
        private DataGridViewButtonColumn SolutionPath;
        private Label SchemaSolutionPathLabel;
    }
}