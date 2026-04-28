using System.Collections.Generic;

namespace SmartStudyPlanner._2
{
    // 3. Enumeration - defines difficulty levels
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }

    // 1. Class - Base class for all subjects
    public class Subject
    {
        public string Name { get; set; }
        public int Hours { get; set; }
        public int StudiedHours { get; set; }
        public virtual DifficultyLevel Difficulty { get; }

        // Weight calculation: difficulty points + hours
        public int Weight
        {
            get
            {
                int difficultyPoints = (int)Difficulty;
                return difficultyPoints + Hours;
            }
        }

        public int ProgressPercent
        {
            get
            {
                if (Hours == 0) return 0;
                int p = (StudiedHours * 100) / Hours;
                return p > 100 ? 100 : p;
            }
        }

        public bool IsCompleted => ProgressPercent >= 100;
    }

    // 2. Inheritance - Child classes that inherit from Subject
    public class EasySubject : Subject
    {
        public override DifficultyLevel Difficulty => DifficultyLevel.Easy;
    }

    public class MediumSubject : Subject
    {
        public override DifficultyLevel Difficulty => DifficultyLevel.Medium;
    }

    public class HardSubject : Subject
    {
        public override DifficultyLevel Difficulty => DifficultyLevel.Hard;
    }

    // 4. Array of Objects - StudyDay class to hold subjects for one day
    public class StudyDay
    {
        public string DayName { get; set; }
        public List<Subject> Subjects { get; set; }
        public int TotalWeight { get; set; }

        public StudyDay(string dayName)
        {
            DayName = dayName;
            Subjects = new List<Subject>();
            TotalWeight = 0;
        }

        public void AddSubject(Subject subject)
        {
            Subjects.Add(subject);
            TotalWeight += subject.Weight;
        }
    }
}
