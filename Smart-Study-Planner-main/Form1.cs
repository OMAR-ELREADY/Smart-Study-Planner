using System;
using System.Collections.Generic;
using System.IO;
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
            LoadData(); // Load data when form starts
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

        // Simple file saving method
        public static void SaveData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("study_data.txt"))
                {
                    // Save subjects
                    writer.WriteLine("SUBJECTS");
                    foreach (Subject subject in allSubjects)
                    {
                        writer.WriteLine($"{subject.GetType().Name}|{subject.Name}|{subject.Hours}|{subject.StudiedHours}|{(int)subject.Difficulty}");
                    }

                    // Save week schedule
                    writer.WriteLine("SCHEDULE");
                    foreach (StudyDay day in weekSchedule)
                    {
                        writer.WriteLine($"DAY|{day.DayName}|{day.TotalWeight}");
                        foreach (Subject subject in day.Subjects)
                        {
                            writer.WriteLine($"SUBJECT|{subject.GetType().Name}|{subject.Name}|{subject.Hours}|{subject.StudiedHours}|{(int)subject.Difficulty}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }

        // Simple file loading method
        public static void LoadData()
        {
            try
            {
                if (!File.Exists("study_data.txt"))
                    return; // No saved data exists

                allSubjects.Clear();

                using (StreamReader reader = new StreamReader("study_data.txt"))
                {
                    string line;
                    bool inSubjectsSection = false;
                    bool inScheduleSection = false;
                    int currentDayIndex = -1;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line == "SUBJECTS")
                        {
                            inSubjectsSection = true;
                            inScheduleSection = false;
                            continue;
                        }
                        else if (line == "SCHEDULE")
                        {
                            inSubjectsSection = false;
                            inScheduleSection = true;
                            continue;
                        }

                        if (inSubjectsSection && (line.StartsWith("EasySubject|") || line.StartsWith("MediumSubject|") || line.StartsWith("HardSubject|")))
                        {
                            string[] parts = line.Split('|');
                            if (parts.Length == 5)
                            {
                                Subject subject = CreateSubject(parts[0]);
                                if (subject != null)
                                {
                                    subject.Name = parts[1];
                                    subject.Hours = int.Parse(parts[2]);
                                    subject.StudiedHours = int.Parse(parts[3]);
                                    subject.Difficulty = (DifficultyLevel)int.Parse(parts[4]);
                                    allSubjects.Add(subject);
                                }
                            }
                        }
                        else if (inScheduleSection)
                        {
                            if (line.StartsWith("DAY|"))
                            {
                                string[] parts = line.Split('|');
                                if (parts.Length == 3)
                                {
                                    // Find the corresponding day index
                                    for (int i = 0; i < 7; i++)
                                    {
                                        if (weekSchedule[i].DayName == parts[1])
                                        {
                                            currentDayIndex = i;
                                            weekSchedule[i].TotalWeight = int.Parse(parts[2]);
                                            weekSchedule[i].Subjects.Clear();
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (line.StartsWith("SUBJECT|") && currentDayIndex >= 0)
                            {
                                string[] parts = line.Split('|');
                                if (parts.Length == 7)
                                {
                                    Subject subject = CreateSubject(parts[1]);
                                    if (subject != null)
                                    {
                                        subject.Name = parts[2];
                                        subject.Hours = int.Parse(parts[3]);
                                        subject.StudiedHours = int.Parse(parts[4]);
                                        subject.Difficulty = (DifficultyLevel)int.Parse(parts[5]);
                                        weekSchedule[currentDayIndex].Subjects.Add(subject);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        // Helper method to create subjects by type
        private static Subject CreateSubject(string type)
        {
            switch (type)
            {
                case "EasySubject":
                    return new EasySubject();
                case "MediumSubject":
                    return new MediumSubject();
                case "HardSubject":
                    return new HardSubject();
                default:
                    return null;
            }
        }

        // Override form closing event to save data
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveData();
            base.OnFormClosing(e);
        }
    }
}
