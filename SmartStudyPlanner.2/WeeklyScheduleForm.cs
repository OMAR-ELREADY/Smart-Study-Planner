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
    public partial class WeeklyScheduleForm : Form
    {
        public WeeklyScheduleForm()
        {
            InitializeComponent();
        }

       
            private void WeeklyScheduleForm_Load(object sender, EventArgs e)
        {
            // إضافة الأعمدة (أيام الأسبوع)
            dgvWeekly.Columns.Add("Saturday", "Saturday");
            dgvWeekly.Columns.Add("Sunday", "Sunday");
            dgvWeekly.Columns.Add("Monday", "Monday");
            dgvWeekly.Columns.Add("Tuesday", "Tuesday");
            dgvWeekly.Columns.Add("Wednesday", "Wednesday");
            dgvWeekly.Columns.Add("Thursday", "Thursday");
            dgvWeekly.Columns.Add("Friday", "Friday");

            // إضافة صفوف فارغة (مثلاً 5 صفوف للمواد)
            for (int i = 0; i < 5; i++)
            {
                dgvWeekly.Rows.Add();
            }
        }
    }
    }

