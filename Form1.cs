using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

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
            LoadSubjects();  // Load subjects from JSON file on startup
        }

        // Save subjects to text file (CSV format: Name,Type,Hours,StudiedHours,CompletedSessions)
        // Also save session limits to sessions.txt
        public static void SaveSubjects()
        {
            // Save subjects
            using (StreamWriter writer = new StreamWriter("subjects.txt"))
            {
                foreach (Subject sub in allSubjects)
                {
                    writer.WriteLine($"{sub.Name},{sub.Type},{sub.Hours},{sub.StudiedHours},{sub.CompletedSessions}");
                }
            }

            // Save session limits (MaxSessions for each day)
            using (StreamWriter writer = new StreamWriter("sessions.txt"))
            {
                foreach (StudyDay day in weekSchedule)
                {
                    writer.WriteLine(day.MaxSessions);
                }
            }
        }

        // Load subjects from text file
        public static void LoadSubjects()
        {
            if (File.Exists("subjects.txt"))
            {
                allSubjects.Clear();
                using (StreamReader reader = new StreamReader("subjects.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length >= 4)
                        {
                            string name = parts[0];
                            string type = parts[1];
                            int hours = int.Parse(parts[2]);
                            int studiedHours = int.Parse(parts[3]);
                            int completedSessions = parts.Length >= 5 ? int.Parse(parts[4]) : 0;

                            Subject newSub;
                            if (type == "Easy")
                                newSub = new EasySubject { Name = name, Hours = hours, StudiedHours = studiedHours, CompletedSessions = completedSessions };
                            else if (type == "Medium")
                                newSub = new MediumSubject { Name = name, Hours = hours, StudiedHours = studiedHours, CompletedSessions = completedSessions };
                            else // Hard
                                newSub = new HardSubject { Name = name, Hours = hours, StudiedHours = studiedHours, CompletedSessions = completedSessions };

                            allSubjects.Add(newSub);
                        }
                    }
                }

                // Load session limits
                if (File.Exists("sessions.txt"))
                {
                    using (StreamReader reader = new StreamReader("sessions.txt"))
                    {
                        int dayIndex = 0;
                        string line;
                        while ((line = reader.ReadLine()) != null && dayIndex < 7)
                        {
                            if (int.TryParse(line, out int maxSessions))
                            {
                                weekSchedule[dayIndex].MaxSessions = maxSessions;
                                dayIndex++;
                            }
                        }
                    }
                }

                DistributeSubjects();  // Redistribute after loading
            }
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

        // Distribute subjects across the week with Round Robin - no consecutive repeats
        public static void DistributeSubjects()
        {
            // Reset all days
            for (int i = 0; i < 7; i++)
            {
                weekSchedule[i].Sessions.Clear();
                weekSchedule[i].TotalWeight = 0;
            }

            // Calculate total available sessions
            int totalSessions = 0;
            foreach (StudyDay day in weekSchedule)
            {
                totalSessions += day.MaxSessions;
            }

            if (totalSessions == 0)
            {
                MessageBox.Show("No sessions available! Please add sessions first.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calculate total needed sessions (sum of all subject Hours)
            int totalNeeded = 0;
            foreach (Subject sub in allSubjects)
            {
                totalNeeded += sub.Hours;
            }

            if (totalNeeded == 0)
            {
                MessageBox.Show("No subjects to distribute!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Each subject's target = its Hours (number of sessions needed)
            Dictionary<Subject, int> targetSessions = new Dictionary<Subject, int>();
            foreach (Subject sub in allSubjects)
            {
                targetSessions[sub] = sub.Hours;
            }

            // Track assigned sessions for each subject
            Dictionary<Subject, int> assignedSessions = new Dictionary<Subject, int>();
            foreach (Subject sub in allSubjects)
            {
                assignedSessions[sub] = 0;
            }

            // Track last assigned subject for each day to avoid consecutive repeats
            Subject[] lastAssignedToDay = new Subject[7];

            // Round Robin: Cycle through days, assigning one subject per day at a time
            int currentDayIndex = 0;
            bool madeProgress = true;

            while (madeProgress)
            {
                madeProgress = false;

                // Try to assign one subject to current day
                for (int attempts = 0; attempts < 7; attempts++)
                {
                    StudyDay currentDay = weekSchedule[currentDayIndex];

                    if (currentDay.CanAddSession())
                    {
                        // Find best subject for this day (considering Round Robin and difficulty)
                        Subject bestSubject = null;
                        int bestScore = -1;

                        foreach (Subject sub in allSubjects)
                        {
                            // Skip if already assigned all needed sessions
                            if (assignedSessions[sub] >= targetSessions[sub])
                                continue;

                            // Skip if same as last assigned to this day (avoid consecutive)
                            if (sub == lastAssignedToDay[currentDayIndex])
                                continue;

                            // Calculate score based on difficulty and need
                            int difficulty = sub.Weight;  // 3=Hard, 2=Medium, 1=Easy
                            int remaining = targetSessions[sub] - assignedSessions[sub];

                            // Score = (remaining sessions needed) × (difficulty weight)
                            // Hard subjects get higher priority when they have many remaining sessions
                            int score = remaining * difficulty * 10;

                            // Bonus for subjects that haven't been assigned to any day yet
                            if (assignedSessions[sub] == 0)
                                score += 50;

                            // Bonus based on session position in day
                            int positionInDay = currentDay.Sessions.Count;
                            if (positionInDay == 0)  // First session
                                score += difficulty * 5;  // Hard subjects preferred first
                            else if (positionInDay == currentDay.MaxSessions - 1)  // Last session
                                score += (4 - difficulty) * 5;  // Easy subjects preferred last

                            if (score > bestScore)
                            {
                                bestScore = score;
                                bestSubject = sub;
                            }
                        }

                        // Assign subject if found
                        if (bestSubject != null)
                        {
                            currentDay.AddSubject(bestSubject);
                            assignedSessions[bestSubject]++;
                            lastAssignedToDay[currentDayIndex] = bestSubject;
                            madeProgress = true;
                        }
                    }

                    // Move to next day (Round Robin)
                    currentDayIndex = (currentDayIndex + 1) % 7;
                }
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
