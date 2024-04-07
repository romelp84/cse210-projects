using System;

namespace GoalTracker
{
    public class ProgressGoal : Goal
    {
        public int CurrentProgress { get; set; } // Add CurrentProgress property

        public int TargetProgress { get; set; }

        public ProgressGoal(string name, int targetProgress) : base(name)
        {
            TargetProgress = targetProgress;
            CurrentProgress = 0; // Initialize CurrentProgress to 0
        }

        public override void RecordEvent(User user)
        {
            // Record progress towards the goal
            CurrentProgress++;

            // Check if the goal is completed
            IsCompleted = CurrentProgress >= TargetProgress;

            // Update user score if necessary
            if (IsCompleted)
            {
                user.Score += 1000; // Add points for completing the goal
            }
        }

        public override void DisplayGoalStatus()
        {
            Console.WriteLine($"Progress Goal: {Name}, Status: {(IsCompleted ? "Completed" : "Not Completed")} [Progress: {CurrentProgress}/{TargetProgress}]");
        }

        public override string Serialize()
        {
            return $"Progress,{Name},{CurrentProgress},{TargetProgress},{IsCompleted}";
        }
    }
}
