using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartStudyPlanner._2
{
    public partial class AddSubjectForm : Form
    {
        public AddSubjectForm()
        {
            InitializeComponent();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 1. التأكد إن الخانات مش فاضية (عشان البرنامج ما يضربش)
            if (string.IsNullOrWhiteSpace(txtSubjectName.Text))
            {
                MessageBox.Show("Please enter the Subject Name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbDifficulty.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the Difficulty level!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. لو البيانات تمام، هننشئ المادة ونخزنها
            Subject newSub = new Subject();
            newSub.Name = txtSubjectName.Text;
            newSub.Difficulty = cmbDifficulty.SelectedItem.ToString();
            newSub.Hours = (int)numHours.Value;

            // سحب التاريخ من الأداة الجديدة
            newSub.Deadline = dtpDeadline.Value;

            // إضافة المادة للمخزن الرئيسي
            Form1.allSubjects.Add(newSub);

            // 3. رسالة نجاح بالإنجليزي
            MessageBox.Show($"Subject '{txtSubjectName.Text}' added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 4. تنظيف الخانات عشان تضيف مادة تانية
            txtSubjectName.Clear();
            cmbDifficulty.SelectedIndex = -1;
            numHours.Value = 0;
            dtpDeadline.Value = DateTime.Now; // بيرجع التاريخ لتاريخ النهاردة
        }

        private void NumericUpDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddSubjectForm_Load(object sender, EventArgs e)
        {

        }
    }
    }

