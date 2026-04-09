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
    public partial class Form1 : Form
    {
        // دي القائمة اللي هتشيل كل المواد وتكون متشافة في البرنامج كله
        public static List<Subject> allSubjects = new List<Subject>();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddSubject_Click(object sender, EventArgs e)
        {
            // إنشاء نسخة من صفحة إضافة مادة
            AddSubjectForm f2 = new AddSubjectForm();
            // إظهار الصفحة
            f2.Show();
        }

        private void btnViewSchedule_Click(object sender, EventArgs e)
        {
            // إنشاء نسخة من صفحة الجدول
            ScheduleForm f3 = new ScheduleForm();
            // إظهار الصفحة
            f3.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
