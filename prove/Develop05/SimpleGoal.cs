using System;

namespace GoalTracker
{
    public class SimpleGoal : Goal
    {
        public SimpleGoal(string name) : base(name) { }

        public override void RecordEvent(User user)
        {
            IsCompleted = true;
            user.Score += 1000; // 1000 points for completing a simple goal
        }

        public override void DisplayGoalStatus()
        {
            Console.WriteLine($"Simple Goal: {Name}, Status: {(IsCompleted ? "Completed" : "Not Completed")}");
        }

        public override string Serialize()
        {
            return $"Simple,{Name},{IsCompleted}";
        }
    }
}
