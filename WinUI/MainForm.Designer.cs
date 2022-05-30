namespace WinUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelSide = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblLoggedUser = new System.Windows.Forms.Label();
            this.btnAnalytics = new System.Windows.Forms.Button();
            this.btnRoomTypes = new System.Windows.Forms.Button();
            this.btnEmployees = new System.Windows.Forms.Button();
            this.btnReservations = new System.Windows.Forms.Button();
            this.btnFacilites = new System.Windows.Forms.Button();
            this.btnAmenidies = new System.Windows.Forms.Button();
            this.btnRooms = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSide
            // 
            this.panelSide.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelSide.Controls.Add(this.btnLogout);
            this.panelSide.Controls.Add(this.lblLoggedUser);
            this.panelSide.Controls.Add(this.btnAnalytics);
            this.panelSide.Controls.Add(this.btnRoomTypes);
            this.panelSide.Controls.Add(this.btnEmployees);
            this.panelSide.Controls.Add(this.btnReservations);
            this.panelSide.Controls.Add(this.btnFacilites);
            this.panelSide.Controls.Add(this.btnAmenidies);
            this.panelSide.Controls.Add(this.btnRooms);
            this.panelSide.Controls.Add(this.btnUsers);
            this.panelSide.Controls.Add(this.pictureBox1);
            this.panelSide.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSide.Location = new System.Drawing.Point(0, 0);
            this.panelSide.Name = "panelSide";
            this.panelSide.Size = new System.Drawing.Size(336, 767);
            this.panelSide.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Location = new System.Drawing.Point(209, 733);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(121, 30);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblLoggedUser
            // 
            this.lblLoggedUser.AutoSize = true;
            this.lblLoggedUser.Location = new System.Drawing.Point(12, 738);
            this.lblLoggedUser.Name = "lblLoggedUser";
            this.lblLoggedUser.Size = new System.Drawing.Size(0, 20);
            this.lblLoggedUser.TabIndex = 8;
            // 
            // btnAnalytics
            // 
            this.btnAnalytics.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAnalytics.FlatAppearance.BorderSize = 0;
            this.btnAnalytics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalytics.Location = new System.Drawing.Point(12, 630);
            this.btnAnalytics.Name = "btnAnalytics";
            this.btnAnalytics.Size = new System.Drawing.Size(312, 37);
            this.btnAnalytics.TabIndex = 5;
            this.btnAnalytics.Text = "Analytics";
            this.btnAnalytics.UseVisualStyleBackColor = false;
            this.btnAnalytics.Click += new System.EventHandler(this.btnAnalytics_Click);
            // 
            // btnRoomTypes
            // 
            this.btnRoomTypes.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnRoomTypes.FlatAppearance.BorderSize = 0;
            this.btnRoomTypes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoomTypes.Location = new System.Drawing.Point(12, 413);
            this.btnRoomTypes.Name = "btnRoomTypes";
            this.btnRoomTypes.Size = new System.Drawing.Size(312, 37);
            this.btnRoomTypes.TabIndex = 4;
            this.btnRoomTypes.Text = "Room Types";
            this.btnRoomTypes.UseVisualStyleBackColor = false;
            this.btnRoomTypes.Click += new System.EventHandler(this.btnRoomTypes_Click);
            // 
            // btnEmployees
            // 
            this.btnEmployees.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnEmployees.FlatAppearance.BorderSize = 0;
            this.btnEmployees.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmployees.Location = new System.Drawing.Point(12, 305);
            this.btnEmployees.Name = "btnEmployees";
            this.btnEmployees.Size = new System.Drawing.Size(312, 37);
            this.btnEmployees.TabIndex = 7;
            this.btnEmployees.Text = "Employees";
            this.btnEmployees.UseVisualStyleBackColor = false;
            this.btnEmployees.Click += new System.EventHandler(this.btnEmployees_Click);
            // 
            // btnReservations
            // 
            this.btnReservations.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnReservations.FlatAppearance.BorderSize = 0;
            this.btnReservations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReservations.Location = new System.Drawing.Point(12, 576);
            this.btnReservations.Name = "btnReservations";
            this.btnReservations.Size = new System.Drawing.Size(312, 37);
            this.btnReservations.TabIndex = 6;
            this.btnReservations.Text = "Reservations";
            this.btnReservations.UseVisualStyleBackColor = false;
            this.btnReservations.Click += new System.EventHandler(this.btnReservations_Click);
            // 
            // btnFacilites
            // 
            this.btnFacilites.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFacilites.FlatAppearance.BorderSize = 0;
            this.btnFacilites.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFacilites.Location = new System.Drawing.Point(12, 469);
            this.btnFacilites.Name = "btnFacilites";
            this.btnFacilites.Size = new System.Drawing.Size(312, 37);
            this.btnFacilites.TabIndex = 3;
            this.btnFacilites.Text = "Facilities";
            this.btnFacilites.UseVisualStyleBackColor = false;
            this.btnFacilites.Click += new System.EventHandler(this.btnFacilites_Click);
            // 
            // btnAmenidies
            // 
            this.btnAmenidies.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAmenidies.FlatAppearance.BorderSize = 0;
            this.btnAmenidies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAmenidies.Location = new System.Drawing.Point(12, 524);
            this.btnAmenidies.Name = "btnAmenidies";
            this.btnAmenidies.Size = new System.Drawing.Size(312, 37);
            this.btnAmenidies.TabIndex = 2;
            this.btnAmenidies.Text = "Amenidies";
            this.btnAmenidies.UseVisualStyleBackColor = false;
            this.btnAmenidies.Click += new System.EventHandler(this.btnAmenidies_Click);
            // 
            // btnRooms
            // 
            this.btnRooms.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnRooms.FlatAppearance.BorderSize = 0;
            this.btnRooms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRooms.Location = new System.Drawing.Point(12, 360);
            this.btnRooms.Name = "btnRooms";
            this.btnRooms.Size = new System.Drawing.Size(312, 37);
            this.btnRooms.TabIndex = 1;
            this.btnRooms.Text = "Rooms";
            this.btnRooms.UseVisualStyleBackColor = false;
            this.btnRooms.Click += new System.EventHandler(this.btnRooms_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnUsers.FlatAppearance.BorderSize = 0;
            this.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsers.Location = new System.Drawing.Point(12, 249);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(312, 37);
            this.btnUsers.TabIndex = 0;
            this.btnUsers.Text = "Users";
            this.btnUsers.UseVisualStyleBackColor = false;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(330, 218);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(336, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1082, 767);
            this.panelMain.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1418, 767);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelSide);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "The Lion\'s Den";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelSide.ResumeLayout(false);
            this.panelSide.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelSide;
        private Panel panelMain;
        private PictureBox pictureBox1;
        private Button btnEmployees;
        private Button btnReservations;
        private Button btnAnalytics;
        private Button btnRoomTypes;
        private Button btnFacilites;
        private Button btnAmenidies;
        private Button btnRooms;
        private Button btnUsers;
        private Button btnLogout;
        private Label lblLoggedUser;
    }
}