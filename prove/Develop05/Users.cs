using System;
using System.Collections.Generic;
using System.IO;

namespace GoalTracker
{
    public class User
    {
        public List<Goal> Goals { get; set; }
        public int Score { get; set; }

        public User()
        {
            Goals = new List<Goal>();
            Score = 0;
            LoadGoals("goals.txt");
        }

        public void AddGoal(Goal goal)
        {
            Goals.Add(goal);
            SaveGoals("goals.txt");
        }

        public void RecordEvent(int goalIndex)
        {
            if (goalIndex >= 0 && goalIndex < Goals.Count)
            {
                Goals[goalIndex].RecordEvent(this);
                SaveGoals("goals.txt");
            }
            else
            {
                Console.WriteLine("Invalid goal index.");
            }
        }

        public void DisplayGoals()
        {
            Console.WriteLine("Current Goals:");
            foreach (Goal goal in Goals)
            {
                goal.DisplayGoalStatus();
            }
        }

        public void DisplayScore()
        {
            Console.WriteLine($"Total Score: {Score}");
        }

        public void SaveGoals(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Goal goal in Goals)
                {
                    writer.WriteLine(goal.Serialize());
                }
                writer.WriteLine($"Score,{Score}"); // Save score at the end of the file
            }
        }

        public void LoadGoals(string filename)
        {
            Goals.Clear();
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts[0] == "Simple")
                    {
                        Goals.Add(new SimpleGoal(parts[1]) { IsCompleted = bool.Parse(parts[2]) });
                    }
                    else if (parts[0] == "Eternal")
                    {
                        Goals.Add(new EternalGoal(parts[1]) { IsCompleted = bool.Parse(parts[2]) });
                    }
                    else if (parts[0] == "Checklist")
                    {
                        int timesCompleted = int.Parse(parts[3]);
                        int timesPretended = int.Parse(parts[4]);
                        Goals.Add(new ChecklistGoal(parts[1], timesPretended) { IsCompleted = bool.Parse(parts[2]), TimesCompleted = timesCompleted });
                    }
                    else if (parts[0] == "Score")
                    {
                        Score = int.Parse(parts[1]); // Load score from the last line
                    }
                }
            }
        }
    }
}
