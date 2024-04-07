using System;

namespace GoalTracker
{
    public class NegativeGoal : Goal
    {
        public int PenaltyPoints { get; }

        public NegativeGoal(string name, int penaltyPoints) : base(name)
        {
            PenaltyPoints = penaltyPoints;
        }

        public override void RecordEvent(User user)
        {
            user.Score -= PenaltyPoints; // Deduct penalty points for recording a negative goal
        }

        public override void DisplayGoalStatus()
        {
            Console.WriteLine($"Negative Goal: {Name}");
        }

        public override string Serialize()
        {
            return $"Negative,{Name},{PenaltyPoints},{IsCompleted}";
        }
    }
}
