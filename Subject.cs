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
        public int Hours { get; set; }  // Number of sessions needed (each session = 2 hours)
        public int StudiedHours { get; set; }
        public int CompletedSessions { get; set; }  // Track completed sessions for progress
        public string Type { get; set; }  // "Easy", "Medium", or "Hard" - for JSON serialization
        public virtual DifficultyLevel Difficulty { get; }

        // Weight calculation: difficulty ratio only (Hard=3, Medium=2, Easy=1)
        public int Weight
        {
            get
            {
                int difficultyPoints = (int)Difficulty;
                return difficultyPoints + 1;  // Easy=0+1=1, Medium=1+1=2, Hard=2+1=3
            }
        }

        public int ProgressPercent
        {
            get
            {
                if (Hours == 0) return 0;
                int p = (CompletedSessions * 100) / Hours;
                return p > 100 ? 100 : p;
            }
        }

        public bool IsCompleted => ProgressPercent >= 100;
    }

    // 2. Inheritance - Child classes that inherit from Subject
    public class EasySubject : Subject
    {
        public EasySubject()
        {
            Type = "Easy";
        }
        public override DifficultyLevel Difficulty => DifficultyLevel.Easy;
    }

    public class MediumSubject : Subject
    {
        public MediumSubject()
        {
            Type = "Medium";
        }
        public override DifficultyLevel Difficulty => DifficultyLevel.Medium;
    }

    public class HardSubject : Subject
    {
        public HardSubject()
        {
            Type = "Hard";
        }
        public override DifficultyLevel Difficulty => DifficultyLevel.Hard;
    }

    // 4. Array of Objects - StudyDay class to hold sessions for one day
    public class StudyDay
    {
        public string DayName { get; set; }
        public List<Subject> Sessions { get; set; }  // Each session is 2 hours, 3 sessions per day by default
        public int TotalWeight { get; set; }
        public int MaxSessions { get; set; }  // Default 3, can be increased

        public StudyDay(string dayName)
        {
            DayName = dayName;
            Sessions = new List<Subject>();
            TotalWeight = 0;
            MaxSessions = 3;  // 3 sessions per day = 6 hours
        }

        public void AddSubject(Subject subject)
        {
            Sessions.Add(subject);
            TotalWeight += subject.Weight;
        }

        public bool CanAddSession()
        {
            return Sessions.Count < MaxSessions;
        }

        public void AddSession()
        {
            MaxSessions++;  // Add one more session (2 hours)
        }
    }
}
