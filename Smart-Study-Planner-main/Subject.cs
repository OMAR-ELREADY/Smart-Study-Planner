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
        public virtual DifficultyLevel Difficulty { get; set; }

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
        private DifficultyLevel _difficulty = DifficultyLevel.Easy;
        public override DifficultyLevel Difficulty 
        { 
            get => _difficulty; 
            set => _difficulty = value; 
        }
    }

    public class MediumSubject : Subject
    {
        private DifficultyLevel _difficulty = DifficultyLevel.Medium;
        public override DifficultyLevel Difficulty 
        { 
            get => _difficulty; 
            set => _difficulty = value; 
        }
    }

    public class HardSubject : Subject
    {
        private DifficultyLevel _difficulty = DifficultyLevel.Hard;
        public override DifficultyLevel Difficulty 
        { 
            get => _difficulty; 
            set => _difficulty = value; 
        }
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
