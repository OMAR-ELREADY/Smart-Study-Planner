namespace SmartStudyPlanner._2
{
    partial class WeeklyScheduleForm
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
            this.dgvWeekly = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeekly)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvWeekly
            // 
            this.dgvWeekly.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWeekly.Location = new System.Drawing.Point(12, 12);
            this.dgvWeekly.Name = "dgvWeekly";
            this.dgvWeekly.RowHeadersWidth = 51;
            this.dgvWeekly.RowTemplate.Height = 24;
            this.dgvWeekly.Size = new System.Drawing.Size(776, 332);
            this.dgvWeekly.TabIndex = 0;
            // 
            // WeeklyScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvWeekly);
            this.Name = "WeeklyScheduleForm";
            this.Text = "WeeklyScheduleForm";
            this.Load += new System.EventHandler(this.WeeklyScheduleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeekly)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvWeekly;
    }
}