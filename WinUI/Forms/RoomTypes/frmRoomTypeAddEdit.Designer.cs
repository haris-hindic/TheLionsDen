namespace WinUI.Forms.RoomTypes
{
    partial class frmRoomTypeAddEdit
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
            this.components = new System.ComponentModel.Container();
            this.error = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.pbNewImage = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.EmploymentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.Remove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.numCapacity = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numSize = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRules = new System.Windows.Forms.TextBox();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.pbImages = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacity)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImages)).BeginInit();
            this.SuspendLayout();
            // 
            // error
            // 
            this.error.ContainerControl = this;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(810, 266);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 27);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.Location = new System.Drawing.Point(848, 343);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(129, 30);
            this.btnUploadImage.TabIndex = 10;
            this.btnUploadImage.Text = "Upload Image";
            this.btnUploadImage.UseVisualStyleBackColor = true;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // pbNewImage
            // 
            this.pbNewImage.Location = new System.Drawing.Point(634, 376);
            this.pbNewImage.Name = "pbNewImage";
            this.pbNewImage.Size = new System.Drawing.Size(343, 196);
            this.pbNewImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbNewImage.TabIndex = 9;
            this.pbNewImage.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(634, 348);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "New Image:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(138, 112);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(329, 148);
            this.txtDescription.TabIndex = 6;
            // 
            // EmploymentDate
            // 
            this.EmploymentDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.EmploymentDate.DataPropertyName = "EmploymentDate";
            this.EmploymentDate.HeaderText = "Employment Date";
            this.EmploymentDate.MinimumWidth = 6;
            this.EmploymentDate.Name = "EmploymentDate";
            this.EmploymentDate.ReadOnly = true;
            this.EmploymentDate.Width = 125;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Capacity:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(138, 29);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(766, 27);
            this.txtName.TabIndex = 0;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // Remove
            // 
            this.Remove.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Remove.HeaderText = "Remove";
            this.Remove.MinimumWidth = 6;
            this.Remove.Name = "Remove";
            this.Remove.Text = "Remove";
            this.Remove.ToolTipText = "Remove";
            this.Remove.UseColumnTextForButtonValue = true;
            this.Remove.Width = 125;
            // 
            // numCapacity
            // 
            this.numCapacity.Location = new System.Drawing.Point(138, 64);
            this.numCapacity.Name = "numCapacity";
            this.numCapacity.Size = new System.Drawing.Size(329, 27);
            this.numCapacity.TabIndex = 2;
            this.numCapacity.Validating += new System.ComponentModel.CancelEventHandler(this.numCapacity_Validating);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numSize);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtRules);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numCapacity);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Location = new System.Drawing.Point(18, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(959, 302);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Facility details";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(473, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Size (m2):";
            // 
            // numSize
            // 
            this.numSize.Location = new System.Drawing.Point(553, 64);
            this.numSize.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numSize.Name = "numSize";
            this.numSize.Size = new System.Drawing.Size(351, 27);
            this.numSize.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(500, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Rules:";
            // 
            // txtRules
            // 
            this.txtRules.Location = new System.Drawing.Point(553, 112);
            this.txtRules.Multiline = true;
            this.txtRules.Name = "txtRules";
            this.txtRules.Size = new System.Drawing.Size(351, 148);
            this.txtRules.TabIndex = 12;
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Location = new System.Drawing.Point(883, 578);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(94, 27);
            this.btnSaveImage.TabIndex = 16;
            this.btnSaveImage.Text = "Save image";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.btnPrevious);
            this.groupBox3.Controls.Add(this.btnNext);
            this.groupBox3.Controls.Add(this.pbImages);
            this.groupBox3.Location = new System.Drawing.Point(12, 320);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(616, 357);
            this.groupBox3.TabIndex = 47;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Image";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(244, 322);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(109, 27);
            this.btnDelete.TabIndex = 48;
            this.btnDelete.Text = "Delete Image";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(6, 322);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(73, 29);
            this.btnPrevious.TabIndex = 47;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(531, 322);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(73, 29);
            this.btnNext.TabIndex = 48;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // pbImages
            // 
            this.pbImages.Location = new System.Drawing.Point(6, 23);
            this.pbImages.Name = "pbImages";
            this.pbImages.Size = new System.Drawing.Size(598, 293);
            this.pbImages.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImages.TabIndex = 36;
            this.pbImages.TabStop = false;
            // 
            // frmRoomTypeAddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 685);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnSaveImage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnUploadImage);
            this.Controls.Add(this.pbNewImage);
            this.Name = "frmRoomTypeAddEdit";
            this.Text = "frmRoomTypeAddEdit";
            this.Load += new System.EventHandler(this.frmRoomTypeAddEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacity)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ErrorProvider error;
        private GroupBox groupBox1;
        private Label label6;
        private TextBox txtRules;
        private Button btnSave;
        private Label label4;
        private TextBox txtDescription;
        private Label label2;
        private NumericUpDown numCapacity;
        private Label label1;
        private TextBox txtName;
        private Label label5;
        private Button btnUploadImage;
        private PictureBox pbNewImage;
        private DataGridViewTextBoxColumn EmploymentDate;
        //private DataGridViewTextBoxColumn Name;
        private DataGridViewButtonColumn Remove;
        private Button btnSaveImage;
        private Label label3;
        private NumericUpDown numSize;
        private GroupBox groupBox3;
        private Button btnPrevious;
        private Button btnNext;
        private PictureBox pbImages;
        private Button btnDelete;
    }
}