using System;

namespace GoalTracker
{
    public class EternalGoal : Goal
    {
        public EternalGoal(string name) : base(name) { }

        public override void RecordEvent(User user)
        {
            user.Score += 100; // 100 points for recording an event for an eternal goal
        }

        public override void DisplayGoalStatus()
        {
            Console.WriteLine($"Eternal Goal: {Name}, Status: {(IsCompleted ? "Completed" : "Not Completed")}");
        }

        public override string Serialize()
        {
            return $"Eternal,{Name},{IsCompleted}";
        }
    }
}
