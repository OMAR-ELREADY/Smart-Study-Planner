using System;
using System.Windows.Forms;

namespace SmartStudyPlanner._2
{
    public partial class AddSubjectForm : Form
    {
        public AddSubjectForm()
        {
            InitializeComponent();
        }

        private void AddSubjectForm_Load(object sender, EventArgs e)
        {
            numHours.Minimum = 1;
            numHours.Maximum = 200;
            numHours.Value = 1;
        }

        private void btnAddSubject_Click(object sender, EventArgs e)
        {
            // Validate name
            if (string.IsNullOrWhiteSpace(txtSubjectName.Text))
            {
                MessageBox.Show("Please enter a subject name.", "Missing Name",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubjectName.Focus();
                return;
            }

            // Validate difficulty (no blank items)
            if (cmbDifficulty.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a difficulty level.", "Missing Difficulty",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDifficulty.Focus();
                return;
            }

            // Validate hours >= 1
            if (numHours.Value < 1)
            {
                MessageBox.Show("Study hours must be at least 1.", "Invalid Hours",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numHours.Focus();
                return;
            }

            // Duplicate check
            string name = txtSubjectName.Text.Trim();
            foreach (Subject s in Form1.allSubjects)
            {
                if (s.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show($"\"{name}\" already exists!", "Duplicate",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Save - create appropriate child class based on difficulty
            Subject newSub;
            string difficulty = cmbDifficulty.SelectedItem.ToString();

            if (difficulty == "Easy")
                newSub = new EasySubject { Name = name, Hours = (int)numHours.Value, StudiedHours = 0 };
            else if (difficulty == "Medium")
                newSub = new MediumSubject { Name = name, Hours = (int)numHours.Value, StudiedHours = 0 };
            else // Hard
                newSub = new HardSubject { Name = name, Hours = (int)numHours.Value, StudiedHours = 0 };

            Form1.allSubjects.Add(newSub);

            // Redistribute schedule after adding new subject
            Form1.DistributeSubjects();

            // Save to JSON file
            Form1.SaveSubjects();

            MessageBox.Show($"Subject \"{newSub.Name}\" added successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset fields
            txtSubjectName.Clear();
            cmbDifficulty.SelectedIndex = -1;
            numHours.Value = 1;
            txtSubjectName.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
