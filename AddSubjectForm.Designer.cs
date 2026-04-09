namespace SmartStudyPlanner._2
{
    partial class AddSubjectForm
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
            this.txtSubjectName = new System.Windows.Forms.TextBox();
            this.cmbDifficulty = new System.Windows.Forms.ComboBox();
            this.numHours = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddSubject = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numHours)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSubjectName
            // 
            this.txtSubjectName.Location = new System.Drawing.Point(314, 172);
            this.txtSubjectName.Name = "txtSubjectName";
            this.txtSubjectName.Size = new System.Drawing.Size(100, 22);
            this.txtSubjectName.TabIndex = 0;
            // 
            // cmbDifficulty
            // 
            this.cmbDifficulty.FormattingEnabled = true;
            this.cmbDifficulty.Items.AddRange(new object[] {
            "Easy",
            "",
            "Medium",
            "",
            "Hard"});
            this.cmbDifficulty.Location = new System.Drawing.Point(314, 233);
            this.cmbDifficulty.Name = "cmbDifficulty";
            this.cmbDifficulty.Size = new System.Drawing.Size(121, 24);
            this.cmbDifficulty.TabIndex = 1;
            this.cmbDifficulty.SelectedIndexChanged += new System.EventHandler(this.NumericUpDown_SelectedIndexChanged);
            // 
            // numHours
            // 
            this.numHours.Location = new System.Drawing.Point(314, 287);
            this.numHours.Name = "numHours";
            this.numHours.Size = new System.Drawing.Size(120, 22);
            this.numHours.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Subject Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Difficulty";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(198, 293);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Study Hours";
            // 
            // btnAddSubject
            // 
            this.btnAddSubject.Location = new System.Drawing.Point(314, 371);
            this.btnAddSubject.Name = "btnAddSubject";
            this.btnAddSubject.Size = new System.Drawing.Size(171, 23);
            this.btnAddSubject.TabIndex = 8;
            this.btnAddSubject.Text = "Add Subject";
            this.btnAddSubject.UseVisualStyleBackColor = true;
            this.btnAddSubject.Click += new System.EventHandler(this.btnAddSubject_Click);
            // 
            // AddSubjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAddSubject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numHours);
            this.Controls.Add(this.cmbDifficulty);
            this.Controls.Add(this.txtSubjectName);
            this.Name = "AddSubjectForm";
            this.Text = "AddSubjectForm";
            this.Load += new System.EventHandler(this.AddSubjectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numHours)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSubjectName;
        private System.Windows.Forms.ComboBox cmbDifficulty;
        private System.Windows.Forms.NumericUpDown numHours;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddSubject;
    }
}