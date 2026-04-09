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

        
            private void btnAddSubject_Click(object sender, EventArgs e)
        {
            // 1. التحقق من البيانات (Validation)
            if (string.IsNullOrWhiteSpace(txtSubjectName.Text))
            {
                MessageBox.Show("من فضلك اكتب اسم المادة أولاً!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // هيوقف الكود هنا مش هيكمل لو الاسم فاضي
            }

            if (cmbDifficulty.SelectedIndex == -1) // لو مختارش حاجة من القائمة
            {
                MessageBox.Show("من فضلك اختر مستوى الصعوبة!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. لو البيانات تمام، هنظهر رسالة نجاح (مؤقتاً لحد ما نربط الـ List)
            string message = $"تم إضافة مادة {txtSubjectName.Text}\nبصعوبة {cmbDifficulty.SelectedItem}\nولمدة {numHours.Value} ساعات.";
            MessageBox.Show(message, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // --- كود الحفظ الجديد ---
            Subject newSub = new Subject();
            newSub.Name = txtSubjectName.Text;
            newSub.Difficulty = cmbDifficulty.SelectedItem.ToString();
            newSub.Hours = (int)numHours.Value;

            Form1.allSubjects.Add(newSub);
            // -----------------------

            // 3. تنظيف الخانات عشان يضيف مادة تانية
            txtSubjectName.Clear();
            cmbDifficulty.SelectedIndex = -1;
            numHours.Value = 0;
        }

        private void NumericUpDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddSubjectForm_Load(object sender, EventArgs e)
        {

        }
    }
    }

