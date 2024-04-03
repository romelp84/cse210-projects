using System;

namespace GoalTracker
{
    public abstract class Goal
    {
        public string Name { get; set; }
        public bool IsCompleted { get; set; }

        public Goal(string name)
        {
            Name = name;
            IsCompleted = false;
        }

        public abstract void RecordEvent(User user);
        public abstract void DisplayGoalStatus();
        public abstract string Serialize();
    }
}
