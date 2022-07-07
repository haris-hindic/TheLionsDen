namespace WinUI.Forms.Rooms
{
    partial class frmRooms
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbRoomType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.cmbComparator = new System.Windows.Forms.ComboBox();
            this.btnClearForm = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.dgvRooms = new System.Windows.Forms.DataGridView();
            this.NameField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoomType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amenities = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnTaken = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnActivate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbRoomType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numPrice);
            this.groupBox1.Controls.Add(this.cmbComparator);
            this.groupBox1.Controls.Add(this.btnClearForm);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.cmbState);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1058, 111);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // cmbRoomType
            // 
            this.cmbRoomType.FormattingEnabled = true;
            this.cmbRoomType.Items.AddRange(new object[] {
            "All"});
            this.cmbRoomType.Location = new System.Drawing.Point(817, 28);
            this.cmbRoomType.Name = "cmbRoomType";
            this.cmbRoomType.Size = new System.Drawing.Size(235, 28);
            this.cmbRoomType.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(726, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "Room type:";
            // 
            // numPrice
            // 
            this.numPrice.Location = new System.Drawing.Point(362, 31);
            this.numPrice.Maximum = new decimal(new int[] {
            99900,
            0,
            0,
            0});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(102, 27);
            this.numPrice.TabIndex = 19;
            // 
            // cmbComparator
            // 
            this.cmbComparator.FormattingEnabled = true;
            this.cmbComparator.Items.AddRange(new object[] {
            "All"});
            this.cmbComparator.Location = new System.Drawing.Point(314, 30);
            this.cmbComparator.Name = "cmbComparator";
            this.cmbComparator.Size = new System.Drawing.Size(42, 28);
            this.cmbComparator.TabIndex = 18;
            // 
            // btnClearForm
            // 
            this.btnClearForm.Location = new System.Drawing.Point(6, 69);
            this.btnClearForm.Name = "btnClearForm";
            this.btnClearForm.Size = new System.Drawing.Size(102, 27);
            this.btnClearForm.TabIndex = 17;
            this.btnClearForm.Text = "Clear form";
            this.btnClearForm.UseVisualStyleBackColor = true;
            this.btnClearForm.Click += new System.EventHandler(this.btnClearForm_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(944, 69);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(108, 27);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbState
            // 
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Items.AddRange(new object[] {
            "All"});
            this.cmbState.Location = new System.Drawing.Point(525, 30);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(195, 28);
            this.cmbState.TabIndex = 16;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(64, 30);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(196, 27);
            this.txtName.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(473, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "State:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Price:";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(509, 20);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(149, 26);
            this.btnNew.TabIndex = 21;
            this.btnNew.Text = "Add new";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // dgvRooms
            // 
            this.dgvRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRooms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameField,
            this.Price,
            this.RoomType,
            this.Amenities,
            this.State,
            this.Delete});
            this.dgvRooms.Location = new System.Drawing.Point(8, 227);
            this.dgvRooms.Name = "dgvRooms";
            this.dgvRooms.RowHeadersWidth = 51;
            this.dgvRooms.RowTemplate.Height = 29;
            this.dgvRooms.Size = new System.Drawing.Size(1058, 407);
            this.dgvRooms.TabIndex = 20;
            this.dgvRooms.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRooms_CellContentClick);
            this.dgvRooms.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRooms_CellContentDoubleClick);
            // 
            // NameField
            // 
            this.NameField.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.NameField.DataPropertyName = "Name";
            this.NameField.HeaderText = "Name";
            this.NameField.MinimumWidth = 6;
            this.NameField.Name = "NameField";
            this.NameField.ReadOnly = true;
            this.NameField.Width = 78;
            // 
            // Price
            // 
            this.Price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Price.DataPropertyName = "Price";
            this.Price.HeaderText = "Price";
            this.Price.MinimumWidth = 6;
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            this.Price.Width = 70;
            // 
            // RoomType
            // 
            this.RoomType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RoomType.DataPropertyName = "RoomTypeName";
            this.RoomType.HeaderText = "Room Type";
            this.RoomType.MinimumWidth = 6;
            this.RoomType.Name = "RoomType";
            // 
            // Amenities
            // 
            this.Amenities.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Amenities.DataPropertyName = "Amenities";
            this.Amenities.HeaderText = "Amenities";
            this.Amenities.MinimumWidth = 6;
            this.Amenities.Name = "Amenities";
            // 
            // State
            // 
            this.State.DataPropertyName = "State";
            this.State.HeaderText = "State";
            this.State.MinimumWidth = 6;
            this.State.Name = "State";
            this.State.Width = 125;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.MinimumWidth = 6;
            this.Delete.Name = "Delete";
            this.Delete.Text = "Delete";
            this.Delete.ToolTipText = "Delete";
            this.Delete.UseColumnTextForButtonValue = true;
            this.Delete.Width = 125;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(483, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 46);
            this.label1.TabIndex = 19;
            this.label1.Text = "Rooms";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.btnTaken);
            this.groupBox2.Controls.Add(this.btnHide);
            this.groupBox2.Controls.Add(this.btnNew);
            this.groupBox2.Controls.Add(this.btnActivate);
            this.groupBox2.Location = new System.Drawing.Point(402, 640);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(664, 61);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Actions";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(79)))), ((int)(((byte)(128)))));
            this.panel2.Location = new System.Drawing.Point(478, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1, 26);
            this.panel2.TabIndex = 24;
            // 
            // btnTaken
            // 
            this.btnTaken.Location = new System.Drawing.Point(163, 20);
            this.btnTaken.Name = "btnTaken";
            this.btnTaken.Size = new System.Drawing.Size(149, 26);
            this.btnTaken.TabIndex = 24;
            this.btnTaken.Text = "Mark as taken";
            this.btnTaken.UseVisualStyleBackColor = true;
            this.btnTaken.Click += new System.EventHandler(this.btnTaken_Click);
            // 
            // btnHide
            // 
            this.btnHide.Location = new System.Drawing.Point(318, 20);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(149, 26);
            this.btnHide.TabIndex = 23;
            this.btnHide.Text = "Hide";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnActivate
            // 
            this.btnActivate.Location = new System.Drawing.Point(6, 20);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(149, 26);
            this.btnActivate.TabIndex = 22;
            this.btnActivate.Text = "Activate";
            this.btnActivate.UseVisualStyleBackColor = true;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // frmRooms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 732);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvRooms);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRooms";
            this.Text = "frmRooms";
            this.Load += new System.EventHandler(this.frmRooms_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;
        private Button btnClearForm;
        private Button btnSearch;
        private ComboBox cmbState;
        private TextBox txtName;
        private Label label7;
        private Label label2;
        private Label label3;
        private Button btnNew;
        private DataGridView dgvRooms;
        private Label label1;
        private NumericUpDown numPrice;
        private ComboBox cmbComparator;
        private ComboBox cmbRoomType;
        private Label label4;
        private DataGridViewTextBoxColumn NameField;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn RoomType;
        private DataGridViewTextBoxColumn Amenities;
        private DataGridViewTextBoxColumn State;
        private DataGridViewButtonColumn Delete;
        private GroupBox groupBox2;
        private Button btnTaken;
        private Button btnHide;
        private Button btnActivate;
        private Panel panel2;
    }
}