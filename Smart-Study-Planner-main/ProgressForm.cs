using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartStudyPlanner._2
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            BuildProgressRows();
        }

        // Dynamically builds one card per subject with a real ProgressBar
        public void BuildProgressRows()
        {
            scrollPanel.Controls.Clear();

            if (Form1.allSubjects.Count == 0)
            {
                Label empty = new Label();
                empty.Text = "No subjects added yet. Go add some first!";
                empty.Font = new Font("Segoe UI", 11F);
                empty.ForeColor = Color.Gray;
                empty.AutoSize = true;
                empty.Location = new Point(20, 20);
                scrollPanel.Controls.Add(empty);
                lblOverall.Text = "Overall Progress: 0%";
                return;
            }

            int y = 10;
            int totalHours = 0, totalStudied = 0;

            foreach (Subject sub in Form1.allSubjects)
            {
                totalHours   += sub.Hours;
                totalStudied += sub.StudiedHours;

                // --- Card panel ---
                Panel card = new Panel();
                card.BorderStyle = BorderStyle.FixedSingle;
                card.BackColor = Color.White;
                card.Location = new Point(10, y);
                card.Size = new Size(1130, 90);

                // Subject name + difficulty
                Label lblName = new Label();
                lblName.Text = $"{sub.Name}  [{sub.Difficulty}]";
                lblName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                lblName.ForeColor = sub.Difficulty == DifficultyLevel.Hard ? Color.Crimson :
                                    sub.Difficulty == DifficultyLevel.Medium ? Color.DarkOrange :
                                                                                 Color.SeaGreen;
                lblName.AutoSize = true;
                lblName.Location = new Point(10, 8);
                card.Controls.Add(lblName);

                // Hours info
                Label lblHrs = new Label();
                lblHrs.Text = $"{sub.StudiedHours} / {sub.Hours} hrs studied";
                lblHrs.Font = new Font("Segoe UI", 9F);
                lblHrs.ForeColor = Color.DimGray;
                lblHrs.AutoSize = true;
                lblHrs.Location = new Point(10, 30);
                card.Controls.Add(lblHrs);

                // Progress bar
                ProgressBar pb = new ProgressBar();
                pb.Minimum = 0;
                pb.Maximum = 100;
                pb.Value   = sub.ProgressPercent;
                pb.Style   = ProgressBarStyle.Continuous;
                pb.Location = new Point(10, 52);
                pb.Size = new Size(480, 22);
                card.Controls.Add(pb);

                // Percentage label
                Label lblPct = new Label();
                lblPct.Text = sub.IsCompleted ? "Done!" : $"{sub.ProgressPercent}%";
                lblPct.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                lblPct.ForeColor = sub.IsCompleted ? Color.SeaGreen : Color.DimGray;
                lblPct.AutoSize = true;
                lblPct.Location = new Point(500, 56);
                card.Controls.Add(lblPct);

                // +1 hour button
                Button btnPlus = new Button();
                btnPlus.Text = "+1 Hour";
                btnPlus.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                btnPlus.BackColor = Color.FromArgb(30, 100, 200);
                btnPlus.ForeColor = Color.White;
                btnPlus.FlatStyle = FlatStyle.Flat;
                btnPlus.Size = new Size(80, 26);
                btnPlus.Location = new Point(590, 50);
                btnPlus.Tag = sub;
                btnPlus.Click += BtnPlus_Click;
                card.Controls.Add(btnPlus);

                scrollPanel.Controls.Add(card);
                y += 105;
            }

            // Update overall bar and label
            int overallPct = totalHours == 0 ? 0 : (totalStudied * 100) / totalHours;
            if (overallPct > 100) overallPct = 100;
            pbOverall.Value = overallPct;
            lblOverall.Text = $"Overall Progress: {overallPct}%  ({totalStudied}/{totalHours} total hours)";
        }

        private void BtnPlus_Click(object sender, EventArgs e)
        {
            Subject sub = (Subject)((Button)sender).Tag;
            if (sub.IsCompleted)
            {
                MessageBox.Show($"\"{sub.Name}\" is already 100% complete!", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sub.StudiedHours++;
            BuildProgressRows(); // refresh all cards
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to reset all progress? This will set all studied hours to 0 for a new week.",
                "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                // Reset all subjects' studied hours to 0
                foreach (Subject sub in Form1.allSubjects)
                {
                    sub.StudiedHours = 0;
                }
                BuildProgressRows(); // refresh the display
                MessageBox.Show("Progress has been reset! You can start a new week.", "Reset Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
