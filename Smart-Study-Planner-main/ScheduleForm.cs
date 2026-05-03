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
            // Initialize DataGridView columns
            dgvSchedule.Columns.Add("Day", "Day");
            dgvSchedule.Columns.Add("Subject", "Subject");
            dgvSchedule.Columns.Add("Difficulty", "Difficulty");
            dgvSchedule.Columns.Add("Hours", "Hours");
            dgvSchedule.Columns.Add("Progress", "Progress");
            BuildScheduleRows();
        }

        // Rebuilds rows dynamically — one row per day of the week
        private void BuildScheduleRows()
        {
            // Clear existing dynamic rows (keep header row at index 0)
            while (dgvSchedule.Rows.Count > 0)
                dgvSchedule.Rows.Clear();

            if (Form1.allSubjects.Count == 0)
            {
                lblStatus.Text = "No subjects yet. Add some using the main menu!";
                lblStatus.ForeColor = Color.Gray;
                return;
            }

            // Display the 7 days of the week with their subjects
            int totalHours = 0;
            foreach (StudyDay day in Form1.weekSchedule)
            {
                if (day.Subjects.Count == 0)
                {
                    // Show free day
                    dgvSchedule.Rows.Add(day.DayName, "Free Day", "-", 0, "-");
                }
                else
                {
                    // Show each subject on this day
                    foreach (Subject sub in day.Subjects)
                    {
                        string status = sub.IsCompleted ? "Done" : $"{sub.ProgressPercent}%";
                        string diff = sub.Difficulty.ToString();
                        dgvSchedule.Rows.Add(day.DayName, sub.Name, diff, sub.Hours, status);

                        // Color the difficulty cell
                        DataGridViewCell diffCell = dgvSchedule.Rows[dgvSchedule.Rows.Count - 1].Cells[2];
                        diffCell.Style.ForeColor = diff == "Hard" ? Color.Crimson :
                                                   diff == "Medium" ? Color.DarkOrange :
                                                                      Color.SeaGreen;
                        diffCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                        // Color completed rows
                        if (sub.IsCompleted)
                            dgvSchedule.Rows[dgvSchedule.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220);

                        totalHours += sub.Hours;
                    }
                }
            }

            lblStatus.Text = $"Total: {Form1.allSubjects.Count} subject(s)  |  {totalHours} total hours planned";
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
                    BuildScheduleRows();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BuildScheduleRows();
        }
    }
}
