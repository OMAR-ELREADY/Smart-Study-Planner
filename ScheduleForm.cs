using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartStudyPlanner._2
{
    public partial class ScheduleForm : Form
    {
        public ScheduleForm()
        {
            InitializeComponent();
        }

        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            // Initialize DataGridView columns - Matrix layout: Days as rows, Sessions as columns
            // Columns will be added dynamically in BuildScheduleRows
            dgvSchedule.Columns.Add("Day", "Day");
            BuildScheduleRows();
        }

        // Rebuilds rows dynamically — one row per day, sessions as columns
        private void BuildScheduleRows()
        {
            // Clear existing rows and columns (except Day column)
            while (dgvSchedule.Rows.Count > 0)
                dgvSchedule.Rows.Clear();
            while (dgvSchedule.Columns.Count > 1)
                dgvSchedule.Columns.RemoveAt(1);

            if (Form1.allSubjects.Count == 0)
            {
                lblStatus.Text = "No subjects yet. Add some using the main menu!";
                lblStatus.ForeColor = Color.Gray;
                return;
            }

            // Get max sessions from first day (all days should have same max)
            int maxSessions = Form1.weekSchedule[0].MaxSessions;

            // Add session columns dynamically
            for (int i = 1; i <= maxSessions; i++)
            {
                dgvSchedule.Columns.Add($"Session{i}", $"Session {i} (2h)");
            }

            // Display the 7 days of the week with sessions in columns
            int totalSessions = 0;
            foreach (StudyDay day in Form1.weekSchedule)
            {
                // Create row for this day
                string[] rowCells = new string[maxSessions + 1];
                rowCells[0] = day.DayName;  // Day name

                // Fill session columns
                for (int i = 0; i < maxSessions; i++)
                {
                    if (i < day.Sessions.Count)
                    {
                        rowCells[i + 1] = day.Sessions[i].Name;  // Subject name
                        totalSessions++;
                    }
                    else
                    {
                        rowCells[i + 1] = "Free";  // Empty session
                    }
                }

                dgvSchedule.Rows.Add(rowCells);
            }

            lblStatus.Text = $"Total: {Form1.allSubjects.Count} subject(s)  |  {totalSessions} sessions scheduled  |  {maxSessions} sessions per day";
            lblStatus.ForeColor = Color.DimGray;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSchedule.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string name = dgvSchedule.SelectedRows[0].Cells[1].Value?.ToString();
            if (string.IsNullOrEmpty(name)) return;

            var confirm = MessageBox.Show($"Delete \"{name}\"?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                Subject toRemove = null;
                foreach (Subject s in Form1.allSubjects)
                    if (s.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    { toRemove = s; break; }

                if (toRemove != null)
                {
                    Form1.allSubjects.Remove(toRemove);
                    // Redistribute schedule after deleting
                    Form1.DistributeSubjects();
                    // Save to JSON file
                    Form1.SaveSubjects();
                    BuildScheduleRows();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BuildScheduleRows();
        }

        private void btnAddSession_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to add more sessions?\n\nThis will add 1 session (2 hours) to EVERY day of the week.",
                "Confirm Add Session", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                // Add one session to each day
                foreach (StudyDay day in Form1.weekSchedule)
                {
                    day.AddSession();
                }
                BuildScheduleRows();
                Form1.SaveSubjects();  // Save session limits
                MessageBox.Show("Added 1 session (2 hours) to each day!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
