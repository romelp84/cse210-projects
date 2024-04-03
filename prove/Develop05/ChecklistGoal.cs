using System;

namespace GoalTracker
{
    public class ChecklistGoal : Goal
    {
        private int timesPretended;
        private int pointsPerPretend;

        public int TimesCompleted { get; set; }

        public ChecklistGoal(string name, int timesPretended)
            : base(name)
        {
            this.timesPretended = timesPretended;
            pointsPerPretend = 500 / timesPretended;
            TimesCompleted = 0;
        }

        public override void RecordEvent(User user)
        {
            TimesCompleted++;
            IsCompleted = TimesCompleted >= timesPretended;

            user.Score += pointsPerPretend; // Points calculated based on the number of times pretended
        }

        public override void DisplayGoalStatus()
        {
            Console.WriteLine($"Checklist Goal: {Name}, Status: {(IsCompleted ? "Completed" : "Not Completed")} [{TimesCompleted}/{timesPretended}]");
        }

        public override string Serialize()
        {
            return $"Checklist,{Name},{IsCompleted},{TimesCompleted},{timesPretended}";
        }
    }
}
