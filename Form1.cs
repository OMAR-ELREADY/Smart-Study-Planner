using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SmartStudyPlanner._2
{
    public partial class Form1 : Form
    {
        public static List<Subject> allSubjects = new List<Subject>();
        // 4. Array of Objects - Array of 7 StudyDay objects for the week
        public static StudyDay[] weekSchedule = new StudyDay[7];

        public Form1()
        {
            InitializeComponent();
            InitializeWeekSchedule();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form load event handler
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Label click event handler
        }

        // Initialize the 7 days of the week
        private void InitializeWeekSchedule()
        {
            string[] dayNames = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            for (int i = 0; i < 7; i++)
            {
                weekSchedule[i] = new StudyDay(dayNames[i]);
            }
        }

        // Distribute subjects across the week using simple greedy algorithm
        public static void DistributeSubjects()
        {
            // Reset all days
            for (int i = 0; i < 7; i++)
            {
                weekSchedule[i].Subjects.Clear();
                weekSchedule[i].TotalWeight = 0;
            }

            // For each subject, find the day with lowest weight and add it there
            foreach (Subject subject in allSubjects)
            {
                int minWeightIndex = 0;
                int minWeight = weekSchedule[0].TotalWeight;

                // Find day with minimum total weight
                for (int i = 1; i < 7; i++)
                {
                    if (weekSchedule[i].TotalWeight < minWeight)
                    {
                        minWeight = weekSchedule[i].TotalWeight;
                        minWeightIndex = i;
                    }
                }

                // Add subject to the day with minimum weight
                weekSchedule[minWeightIndex].AddSubject(subject);
            }
        }

        private void btnAddSubject_Click(object sender, System.EventArgs e)
        {
            AddSubjectForm f2 = new AddSubjectForm();
            f2.ShowDialog();
        }

        private void btnViewSchedule_Click(object sender, System.EventArgs e)
        {
            ScheduleForm f3 = new ScheduleForm();
            f3.ShowDialog();
        }

        private void btnProgress_Click(object sender, System.EventArgs e)
        {
            ProgressForm f4 = new ProgressForm();
            f4.ShowDialog();
        }
    }
}
