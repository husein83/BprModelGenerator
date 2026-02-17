namespace ModelGenerator.FormManagement
{
    partial class Template
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
            NamespaceFormatTextBox = new TextBox();
            NamespaceFormatLabel = new Label();
            ColumnFormatTextBox = new TextBox();
            ColumnFormatLabel = new Label();
            ConstructorFormatTextBox = new TextBox();
            ConstructorFormatLabel = new Label();
            ClassDeclarationNameFormatTextBox = new TextBox();
            ClassDeclarationNameFormatLabel = new Label();
            ClassDeclarationInheritanceFormatTextBox = new TextBox();
            ClassDeclarationInheritanceFormatLabel = new Label();
            TableAttributeFormatTextBox = new TextBox();
            TableAttributeFormatLabel = new Label();
            ReferenceFormatTextBox = new TextBox();
            ReferenceFormatLabel = new Label();
            RelationFormatTextBox = new TextBox();
            RelationFormatLabel = new Label();
            ConstantsListBox = new ListBox();
            FillDefaultButton = new Button();
            ConstantsLabel = new Label();
            OverrideFieldsLabel = new Label();
            CustomRegionsLabel = new Label();
            DeleteButton = new Button();
            SaveButton = new Button();
            ReturnButton = new Button();
            OverrideFieldsDataGridView = new DataGridView();
            Field = new DataGridViewTextBoxColumn();
            CustomRegionsItemDataGridView = new DataGridView();
            Region = new DataGridViewTextBoxColumn();
            Items = new DataGridViewTextBoxColumn();
            CustomColumnAttributeFormatTextBox = new TextBox();
            CustomColumnAttributeFormatLabel = new Label();
            DataGridViewItemRichTextBox = new RichTextBox();
            ItemLabel = new Label();
            CustomUsingItemTextBox = new TextBox();
            CustomUsingItemLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)OverrideFieldsDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CustomRegionsItemDataGridView).BeginInit();
            SuspendLayout();
            // 
            // LoadButton
            // 
            LoadButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LoadButton.Location = new Point(594, 142);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(80, 48);
            LoadButton.TabIndex = 18;
            LoadButton.Text = "Load...";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // DatabaseNameTextBox
            // 
            DatabaseNameTextBox.Location = new Point(294, 142);
            DatabaseNameTextBox.Multiline = true;
            DatabaseNameTextBox.Name = "DatabaseNameTextBox";
            DatabaseNameTextBox.ReadOnly = true;
            DatabaseNameTextBox.Size = new Size(289, 46);
            DatabaseNameTextBox.TabIndex = 17;
            // 
            // DatabaseNameLabel
            // 
            DatabaseNameLabel.AutoSize = true;
            DatabaseNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DatabaseNameLabel.Location = new Point(65, 146);
            DatabaseNameLabel.Name = "DatabaseNameLabel";
            DatabaseNameLabel.Size = new Size(207, 32);
            DatabaseNameLabel.TabIndex = 16;
            DatabaseNameLabel.Text = "Database Name :";
            // 
            // HeaderLabel
            // 
            HeaderLabel.AutoSize = true;
            HeaderLabel.Font = new Font("Dancing Script", 29F, FontStyle.Bold);
            HeaderLabel.Location = new Point(571, 21);
            HeaderLabel.Margin = new Padding(0);
            HeaderLabel.Name = "HeaderLabel";
            HeaderLabel.Size = new Size(355, 82);
            HeaderLabel.TabIndex = 19;
            HeaderLabel.Text = "ConfigTemplate";
            // 
            // NamespaceFormatTextBox
            // 
            NamespaceFormatTextBox.AllowDrop = true;
            NamespaceFormatTextBox.Location = new Point(326, 207);
            NamespaceFormatTextBox.Multiline = true;
            NamespaceFormatTextBox.Name = "NamespaceFormatTextBox";
            NamespaceFormatTextBox.Size = new Size(663, 46);
            NamespaceFormatTextBox.TabIndex = 21;
            NamespaceFormatTextBox.DragDrop += ConstantsListBoxSelectedItem_DragDrop;
            NamespaceFormatTextBox.DragEnter += ConstantsListBoxSelectedItem_DragEnter;
            // 
            // NamespaceFormatLabel
            // 
            NamespaceFormatLabel.AutoSize = true;
            NamespaceFormatLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            NamespaceFormatLabel.Location = new Point(65, 213);
            NamespaceFormatLabel.Name = "NamespaceFormatLabel";
            NamespaceFormatLabel.Size = new Size(247, 32);
            NamespaceFormatLabel.TabIndex = 20;
            NamespaceFormatLabel.Text = "Namespace Format :";
            // 
            // ColumnFormatTextBox
            // 
            ColumnFormatTextBox.AllowDrop = true;
            ColumnFormatTextBox.Location = new Point(276, 573);
            ColumnFormatTextBox.Multiline = true;
            ColumnFormatTextBox.Name = "ColumnFormatTextBox";
            ColumnFormatTextBox.Size = new Size(1174, 56);
            ColumnFormatTextBox.TabIndex = 23;
            ColumnFormatTextBox.DragDrop += ConstantsListBoxSelectedItem_DragDrop;
            ColumnFormatTextBox.DragEnter += ConstantsListBoxSelectedItem_DragEnter;
            // 
            // ColumnFormatLabel
            // 
            ColumnFormatLabel.AutoSize = true;
            ColumnFormatLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ColumnFormatLabel.Location = new Point(65, 583);
            ColumnFormatLabel.Name = "ColumnFormatLabel";
            ColumnFormatLabel.Size = new Size(205, 32);
            ColumnFormatLabel.TabIndex = 22;
            ColumnFormatLabel.Text = "Column Format :";
            // 
            // ConstructorFormatTextBox
            // 
            ConstructorFormatTextBox.AllowDrop = true;
            ConstructorFormatTextBox.Location = new Point(323, 507);
            ConstructorFormatTextBox.Multiline = true;
            ConstructorFormatTextBox.Name = "ConstructorFormatTextBox";
            ConstructorFormatTextBox.Size = new Size(1127, 53);
            ConstructorFormatTextBox.TabIndex = 25;
            ConstructorFormatTextBox.DragDrop += ConstantsListBoxSelectedItem_DragDrop;
            ConstructorFormatTextBox.DragEnter += ConstantsListBoxSelectedItem_DragEnter;
            // 
            // ConstructorFormatLabel
            // 
            ConstructorFormatLabel.AutoSize = true;
            ConstructorFormatLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ConstructorFormatLabel.Location = new Point(65, 515);
            ConstructorFormatLabel.Name = "ConstructorFormatLabel";
            ConstructorFormatLabel.Size = new Size(252, 32);
            ConstructorFormatLabel.TabIndex = 24;
            ConstructorFormatLabel.Text = "Constructor Format :";
            // 
            // ClassDeclarationNameFormatTextBox
            // 
            ClassDeclarationNameFormatTextBox.AllowDrop = true;
            ClassDeclarationNameFormatTextBox.Location = new Point(457, 441);
            ClassDeclarationNameFormatTextBox.Multiline = true;
            ClassDeclarationNameFormatTextBox.Name = "ClassDeclarationNameFormatTextBox";
            ClassDeclarationNameFormatTextBox.Size = new Size(993, 54);
            ClassDeclarationNameFormatTextBox.TabIndex = 27;
            ClassDeclarationNameFormatTextBox.DragDrop += ConstantsListBoxSelectedItem_DragDrop;
            ClassDeclarationNameFormatTextBox.DragEnter += ConstantsListBoxSelectedItem_DragEnter;
            // 
            // ClassDeclarationNameFormatLabel
            // 
            ClassDeclarationNameFormatLabel.AutoSize = true;
            ClassDeclarationNameFormatLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ClassDeclarationNameFormatLabel.Location = new Point(65, 447);
            ClassDeclarationNameFormatLabel.Name = "ClassDeclarationNameFormatLabel";
            ClassDeclarationNameFormatLabel.Size = new Size(386, 32);
            ClassDeclarationNameFormatLabel.TabIndex = 26;
            ClassDeclarationNameFormatLabel.Text = "Class Declaration Name Format :";
            // 
            // ClassDeclarationInheritanceFormatTextBox
            // 
            ClassDeclarationInheritanceFormatTextBox.AllowDrop = true;
            ClassDeclarationInheritanceFormatTextBox.Location = new Point(519, 382);
            ClassDeclarationInheritanceFormatTextBox.Multiline = true;
            ClassDeclarationInheritanceFormatTextBox.Name = "ClassDeclarationInheritanceFormatTextBox";
            ClassDeclarationInheritanceFormatTextBox.Size = new Size(931, 46);
            ClassDeclarationInheritanceFormatTextBox.TabIndex = 29;
            ClassDeclarationInheritanceFormatTextBox.DragDrop += ConstantsListBoxSelectedItem_DragDrop;
            ClassDeclarationInheritanceFormatTextBox.DragEnter += ConstantsListBoxSelectedItem_DragEnter;
            // 
            // ClassDeclarationInheritanceFormatLabel
            // 
            ClassDeclarationInheritanceFormatLabel.AutoSize = true;
            ClassDeclarationInheritanceFormatLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ClassDeclarationInheritanceFormatLabel.Location = new Point(65, 385);
            ClassDeclarationInheritanceFormatLabel.Name = "ClassDeclarationInheritanceFormatLabel";
            ClassDeclarationInheritanceFormatLabel.Size = new Size(448, 32);
            ClassDeclarationInheritanceFormatLabel.TabIndex = 28;
            ClassDeclarationInheritanceFormatLabel.Text = "Class Declaration Inheritance Format :";
            // 
            // TableAttributeFormatTextBox
            // 
            TableAttributeFormatTextBox.AllowDrop = true;
            TableAttributeFormatTextBox.Location = new Point(358, 266);
            TableAttributeFormatTextBox.Multiline = true;
            TableAttributeFormatTextBox.Name = "TableAttributeFormatTextBox";
            TableAttributeFormatTextBox.Size = new Size(631, 46);
            TableAttributeFormatTextBox.TabIndex = 31;
            TableAttributeFormatTextBox.DragDrop += ConstantsListBoxSelectedItem_DragDrop;
            TableAttributeFormatTextBox.DragEnter += ConstantsListBoxSelectedItem_DragEnter;
            // 
            // TableAttributeFormatLabel
            // 
            TableAttributeFormatLabel.AutoSize = true;
            TableAttributeFormatLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TableAttributeFormatLabel.Location = new Point(65, 270);
            TableAttributeFormatLabel.Name = "TableAttributeFormatLabel";
            TableAttributeFormatLabel.Size = new Size(287, 32);
            TableAttributeFormatLabel.TabIndex = 30;
            TableAttributeFormatLabel.Text = "Table Attribute Format :";
            // 
            // ReferenceFormatTextBox
            // 
            ReferenceFormatTextBox.AllowDrop = true;
            ReferenceFormatTextBox.Location = new Point(300, 711);
            ReferenceFormatTextBox.Multiline = true;
            ReferenceFormatTextBox.Name = "ReferenceFormatTextBox";
            ReferenceFormatTextBox.Size = new Size(1150, 56);
            ReferenceFormatTextBox.TabIndex = 33;
            ReferenceFormatTextBox.DragDrop += ConstantsListBoxSelectedItem_DragDrop;
            ReferenceFormatTextBox.DragEnter += ConstantsListBoxSelectedItem_DragEnter;
            // 
            // ReferenceFormatLabel
            // 
            ReferenceFormatLabel.AutoSize = true;
            ReferenceFormatLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ReferenceFormatLabel.Location = new Point(65, 720);
            ReferenceFormatLabel.Name = "ReferenceFormatLabel";
            ReferenceFormatLabel.Size = new Size(229, 32);
            ReferenceFormatLabel.TabIndex = 32;
            ReferenceFormatLabel.Text = "Reference Format :";
            // 
            // RelationFormatTextBox
            // 
            RelationFormatTextBox.AllowDrop = true;
            RelationFormatTextBox.Location = new Point(281, 780);
            RelationFormatTextBox.Multiline = true;
            RelationFormatTextBox.Name = "RelationFormatTextBox";
            RelationFormatTextBox.Size = new Size(1169, 59);
            RelationFormatTextBox.TabIndex = 35;
            RelationFormatTextBox.DragDrop += ConstantsListBoxSelectedItem_DragDrop;
            RelationFormatTextBox.DragEnter += ConstantsListBoxSelectedItem_DragEnter;
            // 
            // RelationFormatLabel
            // 
            RelationFormatLabel.AutoSize = true;
            RelationFormatLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            RelationFormatLabel.Location = new Point(65, 792);
            RelationFormatLabel.Name = "RelationFormatLabel";
            RelationFormatLabel.Size = new Size(210, 32);
            RelationFormatLabel.TabIndex = 34;
            RelationFormatLabel.Text = "Relation Format :";
            // 
            // ConstantsListBox
            // 
            ConstantsListBox.AllowDrop = true;
            ConstantsListBox.FormattingEnabled = true;
            ConstantsListBox.ItemHeight = 25;
            ConstantsListBox.Location = new Point(1041, 133);
            ConstantsListBox.Name = "ConstantsListBox";
            ConstantsListBox.Size = new Size(409, 179);
            ConstantsListBox.TabIndex = 36;
            ConstantsListBox.MouseDown += ConstantsListBox_MouseDown;
            // 
            // FillDefaultButton
            // 
            FillDefaultButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FillDefaultButton.Location = new Point(691, 142);
            FillDefaultButton.Name = "FillDefaultButton";
            FillDefaultButton.Size = new Size(252, 48);
            FillDefaultButton.TabIndex = 37;
            FillDefaultButton.Text = "Fill By Default Template";
            FillDefaultButton.UseVisualStyleBackColor = true;
            FillDefaultButton.Click += FillDefaultButton_Click;
            // 
            // ConstantsLabel
            // 
            ConstantsLabel.AutoSize = true;
            ConstantsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ConstantsLabel.Location = new Point(1037, 97);
            ConstantsLabel.Name = "ConstantsLabel";
            ConstantsLabel.Size = new Size(141, 32);
            ConstantsLabel.TabIndex = 38;
            ConstantsLabel.Text = "Constants :";
            // 
            // OverrideFieldsLabel
            // 
            OverrideFieldsLabel.AutoSize = true;
            OverrideFieldsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            OverrideFieldsLabel.Location = new Point(264, 850);
            OverrideFieldsLabel.Name = "OverrideFieldsLabel";
            OverrideFieldsLabel.Size = new Size(199, 32);
            OverrideFieldsLabel.TabIndex = 39;
            OverrideFieldsLabel.Text = "Override Fields :";
            // 
            // CustomRegionsLabel
            // 
            CustomRegionsLabel.AutoSize = true;
            CustomRegionsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CustomRegionsLabel.Location = new Point(513, 850);
            CustomRegionsLabel.Name = "CustomRegionsLabel";
            CustomRegionsLabel.Size = new Size(206, 32);
            CustomRegionsLabel.TabIndex = 41;
            CustomRegionsLabel.Text = "Custom Regions:";
            // 
            // DeleteButton
            // 
            DeleteButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DeleteButton.Location = new Point(35, 971);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(201, 53);
            DeleteButton.TabIndex = 45;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SaveButton.Location = new Point(35, 901);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(201, 53);
            SaveButton.TabIndex = 44;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // ReturnButton
            // 
            ReturnButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ReturnButton.Location = new Point(35, 1042);
            ReturnButton.Name = "ReturnButton";
            ReturnButton.Size = new Size(201, 53);
            ReturnButton.TabIndex = 43;
            ReturnButton.Text = "Return";
            ReturnButton.UseVisualStyleBackColor = true;
            ReturnButton.Click += ReturnButton_Click;
            // 
            // OverrideFieldsDataGridView
            // 
            OverrideFieldsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            OverrideFieldsDataGridView.Columns.AddRange(new DataGridViewColumn[] { Field });
            OverrideFieldsDataGridView.Location = new Point(270, 897);
            OverrideFieldsDataGridView.Name = "OverrideFieldsDataGridView";
            OverrideFieldsDataGridView.RowHeadersWidth = 62;
            OverrideFieldsDataGridView.Size = new Size(231, 210);
            OverrideFieldsDataGridView.TabIndex = 46;
            // 
            // Field
            // 
            Field.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Field.HeaderText = "Field";
            Field.MinimumWidth = 8;
            Field.Name = "Field";
            // 
            // CustomRegionsItemDataGridView
            // 
            CustomRegionsItemDataGridView.AllowDrop = true;
            CustomRegionsItemDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CustomRegionsItemDataGridView.Columns.AddRange(new DataGridViewColumn[] { Region, Items });
            CustomRegionsItemDataGridView.Location = new Point(519, 897);
            CustomRegionsItemDataGridView.Name = "CustomRegionsItemDataGridView";
            CustomRegionsItemDataGridView.RowHeadersWidth = 62;
            CustomRegionsItemDataGridView.Size = new Size(430, 210);
            CustomRegionsItemDataGridView.TabIndex = 47;
            CustomRegionsItemDataGridView.CellBeginEdit += CustomRegionsItemDataGridView_CellBeginEdit;
            CustomRegionsItemDataGridView.CellClick += CustomRegionsItemDataGridView_CellClick;
            // 
            // Region
            // 
            Region.Frozen = true;
            Region.HeaderText = "Region";
            Region.MinimumWidth = 8;
            Region.Name = "Region";
            Region.Width = 150;
            // 
            // Items
            // 
            Items.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Items.Frozen = true;
            Items.HeaderText = "Items";
            Items.MinimumWidth = 8;
            Items.Name = "Items";
            Items.ReadOnly = true;
            Items.Width = 216;
            // 
            // CustomColumnAttributeFormatTextBox
            // 
            CustomColumnAttributeFormatTextBox.AllowDrop = true;
            CustomColumnAttributeFormatTextBox.Location = new Point(481, 642);
            CustomColumnAttributeFormatTextBox.Multiline = true;
            CustomColumnAttributeFormatTextBox.Name = "CustomColumnAttributeFormatTextBox";
            CustomColumnAttributeFormatTextBox.Size = new Size(969, 56);
            CustomColumnAttributeFormatTextBox.TabIndex = 49;
            CustomColumnAttributeFormatTextBox.DragDrop += ConstantsListBoxSelectedItem_DragDrop;
            CustomColumnAttributeFormatTextBox.DragEnter += ConstantsListBoxSelectedItem_DragEnter;
            // 
            // CustomColumnAttributeFormatLabel
            // 
            CustomColumnAttributeFormatLabel.AutoSize = true;
            CustomColumnAttributeFormatLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CustomColumnAttributeFormatLabel.Location = new Point(65, 652);
            CustomColumnAttributeFormatLabel.Name = "CustomColumnAttributeFormatLabel";
            CustomColumnAttributeFormatLabel.Size = new Size(410, 32);
            CustomColumnAttributeFormatLabel.TabIndex = 48;
            CustomColumnAttributeFormatLabel.Text = "Custom Column Attribute Format :";
            // 
            // DataGridViewItemRichTextBox
            // 
            DataGridViewItemRichTextBox.Location = new Point(961, 897);
            DataGridViewItemRichTextBox.Name = "DataGridViewItemRichTextBox";
            DataGridViewItemRichTextBox.Size = new Size(479, 210);
            DataGridViewItemRichTextBox.TabIndex = 51;
            DataGridViewItemRichTextBox.Text = "";
            DataGridViewItemRichTextBox.TextChanged += DataGridViewItemTextBox_TextChanged;
            // 
            // ItemLabel
            // 
            ItemLabel.AutoSize = true;
            ItemLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ItemLabel.Location = new Point(956, 850);
            ItemLabel.Name = "ItemLabel";
            ItemLabel.Size = new Size(171, 32);
            ItemLabel.TabIndex = 52;
            ItemLabel.Text = "Regions Item:";
            // 
            // CustomUsingItemTextBox
            // 
            CustomUsingItemTextBox.AllowDrop = true;
            CustomUsingItemTextBox.Location = new Point(317, 325);
            CustomUsingItemTextBox.Multiline = true;
            CustomUsingItemTextBox.Name = "CustomUsingItemTextBox";
            CustomUsingItemTextBox.Size = new Size(1133, 46);
            CustomUsingItemTextBox.TabIndex = 54;
            // 
            // CustomUsingItemLabel
            // 
            CustomUsingItemLabel.AutoSize = true;
            CustomUsingItemLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CustomUsingItemLabel.Location = new Point(65, 328);
            CustomUsingItemLabel.Name = "CustomUsingItemLabel";
            CustomUsingItemLabel.Size = new Size(246, 32);
            CustomUsingItemLabel.TabIndex = 53;
            CustomUsingItemLabel.Text = "Custom Using Item :";
            // 
            // Template
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1484, 1133);
            ControlBox = false;
            Controls.Add(CustomUsingItemTextBox);
            Controls.Add(CustomUsingItemLabel);
            Controls.Add(ItemLabel);
            Controls.Add(DataGridViewItemRichTextBox);
            Controls.Add(CustomColumnAttributeFormatTextBox);
            Controls.Add(CustomColumnAttributeFormatLabel);
            Controls.Add(CustomRegionsItemDataGridView);
            Controls.Add(OverrideFieldsDataGridView);
            Controls.Add(DeleteButton);
            Controls.Add(SaveButton);
            Controls.Add(ReturnButton);
            Controls.Add(CustomRegionsLabel);
            Controls.Add(OverrideFieldsLabel);
            Controls.Add(ConstantsLabel);
            Controls.Add(FillDefaultButton);
            Controls.Add(ConstantsListBox);
            Controls.Add(RelationFormatTextBox);
            Controls.Add(RelationFormatLabel);
            Controls.Add(ReferenceFormatTextBox);
            Controls.Add(ReferenceFormatLabel);
            Controls.Add(TableAttributeFormatTextBox);
            Controls.Add(TableAttributeFormatLabel);
            Controls.Add(ClassDeclarationInheritanceFormatTextBox);
            Controls.Add(ClassDeclarationInheritanceFormatLabel);
            Controls.Add(ClassDeclarationNameFormatTextBox);
            Controls.Add(ClassDeclarationNameFormatLabel);
            Controls.Add(ConstructorFormatTextBox);
            Controls.Add(ConstructorFormatLabel);
            Controls.Add(ColumnFormatTextBox);
            Controls.Add(ColumnFormatLabel);
            Controls.Add(NamespaceFormatTextBox);
            Controls.Add(NamespaceFormatLabel);
            Controls.Add(HeaderLabel);
            Controls.Add(LoadButton);
            Controls.Add(DatabaseNameTextBox);
            Controls.Add(DatabaseNameLabel);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "Template";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Template";
            Load += Template_Load;
            ((System.ComponentModel.ISupportInitialize)OverrideFieldsDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)CustomRegionsItemDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button LoadButton;
        private TextBox DatabaseNameTextBox;
        private Label DatabaseNameLabel;
        private Label HeaderLabel;
        private TextBox NamespaceFormatTextBox;
        private Label NamespaceFormatLabel;
        private TextBox ColumnFormatTextBox;
        private Label ColumnFormatLabel;
        private TextBox ConstructorFormatTextBox;
        private Label ConstructorFormatLabel;
        private TextBox ClassDeclarationNameFormatTextBox;
        private Label ClassDeclarationNameFormatLabel;
        private TextBox ClassDeclarationInheritanceFormatTextBox;
        private Label ClassDeclarationInheritanceFormatLabel;
        private TextBox TableAttributeFormatTextBox;
        private Label TableAttributeFormatLabel;
        private TextBox ReferenceFormatTextBox;
        private Label ReferenceFormatLabel;
        private TextBox RelationFormatTextBox;
        private Label RelationFormatLabel;
        private ListBox ConstantsListBox;
        private Button FillDefaultButton;
        private Label ConstantsLabel;
        private Label OverrideFieldsLabel;
        private Label CustomRegionsLabel;
        private Button DeleteButton;
        private Button SaveButton;
        private Button ReturnButton;
        private DataGridView OverrideFieldsDataGridView;
        private DataGridView CustomRegionsItemDataGridView;
        private DataGridViewTextBoxColumn Field;
        private TextBox CustomColumnAttributeFormatTextBox;
        private Label CustomColumnAttributeFormatLabel;
        private TextBox DataGridViewItemTextBox;
        private RichTextBox DataGridViewItemRichTextBox;
        private Label ItemLabel;
        private DataGridViewTextBoxColumn Region;
        private DataGridViewTextBoxColumn Items;
        private TextBox CustomUsingItemTextBox;
        private Label CustomUsingItemLabel;
    }
}