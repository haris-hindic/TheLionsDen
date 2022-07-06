namespace WinUI.Forms.Analytics
{
    partial class frmAnalytics
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbChart = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.formsPlot2 = new ScottPlot.FormsPlot();
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(438, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 46);
            this.label1.TabIndex = 20;
            this.label1.Text = "Analytics";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbChart);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Location = new System.Drawing.Point(184, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(685, 62);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reports";
            // 
            // cmbChart
            // 
            this.cmbChart.FormattingEnabled = true;
            this.cmbChart.Items.AddRange(new object[] {
            "All"});
            this.cmbChart.Location = new System.Drawing.Point(128, 26);
            this.cmbChart.Name = "cmbChart";
            this.cmbChart.Size = new System.Drawing.Size(430, 28);
            this.cmbChart.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 23;
            this.label4.Text = "Choose report: ";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(564, 26);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(108, 27);
            this.btnSearch.TabIndex = 22;
            this.btnSearch.Text = "Display";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.formsPlot2);
            this.groupBox2.Controls.Add(this.formsPlot1);
            this.groupBox2.Location = new System.Drawing.Point(12, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1054, 584);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Load chart";
            // 
            // formsPlot2
            // 
            this.formsPlot2.Location = new System.Drawing.Point(8, 27);
            this.formsPlot2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.Size = new System.Drawing.Size(1038, 556);
            this.formsPlot2.TabIndex = 23;
            // 
            // formsPlot1
            // 
            this.formsPlot1.Location = new System.Drawing.Point(8, 27);
            this.formsPlot1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(1038, 550);
            this.formsPlot1.TabIndex = 0;
            // 
            // frmAnalytics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 717);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAnalytics";
            this.Text = "frmAnalytics";
            this.Load += new System.EventHandler(this.frmAnalytics_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private ComboBox cmbChart;
        private Label label4;
        private Button btnSearch;
        private GroupBox groupBox2;
        private ScottPlot.FormsPlot formsPlot1;
        private ScottPlot.FormsPlot formsPlot2;
    }
}