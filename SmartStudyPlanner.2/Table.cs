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
    public partial class Table : Form
    {
        public Table()
        {
            InitializeComponent();
        }

        private void ScheduleForm_Load(object sender, EventArgs e)
        {

        }



        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // 1. ربط البيانات (زي ما إنت عامل بالظبط)
            dgvSchedule.DataSource = null;
            dgvSchedule.DataSource = Form1.allSubjects;

            // 2. خواص التنظيم الجديدة (دي اللي هتخلي الجدول شكله احترافي)
            if (dgvSchedule.Columns["Name"] != null)
            {
                dgvSchedule.Columns["Name"].HeaderText = "Subject Name";
                dgvSchedule.Columns["Difficulty"].HeaderText = "Level";
                dgvSchedule.Columns["Hours"].HeaderText = "Study Hours";

                // سطر التاريخ اللي ضفناه
                if (dgvSchedule.Columns["Deadline"] != null)
                {
                    dgvSchedule.Columns["Deadline"].HeaderText = "Deadline Date";
                    dgvSchedule.Columns["Deadline"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }

            // 3. كود الحساب (نفس اللي عندك بالظبط)
            int total = 0;
            foreach (var sub in Form1.allSubjects)
            {
                total += sub.Hours;
            }
            lblTotalHours.Text = "Total Study Hours: " + total.ToString() + " hrs";
        }


        private void btnClear_Click(object sender, EventArgs e)

        {

            {
                // 1. التأكد إن المستخدم اختار سطر فعلاً في الجدول
                if (dgvSchedule.SelectedRows.Count > 0)
                {
                    // 2. الحصول على رقم السطر المختار (Index)
                    int rowIndex = dgvSchedule.SelectedRows[0].Index;

                    // 3. مسح المادة من القائمة الرئيسية بناءً على رقم السطر
                    Form1.allSubjects.RemoveAt(rowIndex);

                    // 4. تحديث الجدول لعرض البيانات الجديدة
                    dgvSchedule.DataSource = null;
                    dgvSchedule.DataSource = Form1.allSubjects;

                    // 5. إعادة حساب إجمالي الساعات بعد المسح
                    int total = 0;
                    foreach (var sub in Form1.allSubjects)
                    {
                        total += sub.Hours;
                    }
                    lblTotalHours.Text = "Total Study Hours: " + total.ToString() + " hrs";

                    // إعادة ضبط أسماء الأعمدة للإنجليزي (لأننا عملنا DataSource = null)
                    dgvSchedule.Columns["Name"].HeaderText = "Subject Name";
                    dgvSchedule.Columns["Difficulty"].HeaderText = "Level";
                    dgvSchedule.Columns["Hours"].HeaderText = "Study Hours";
                }
                else
                {
                    // رسالة تنبيه لو المستخدم داس مسح من غير ما يحدد سطر
                    MessageBox.Show("Please select a full row from the table first!", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dgvSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
  

