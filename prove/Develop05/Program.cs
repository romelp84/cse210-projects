using System;

namespace GoalTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Enter New Goals");
                Console.WriteLine("2. Display Goals");
                Console.WriteLine("3. Record Event");
                Console.WriteLine("4. Display Score");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // Enter New Goals
                        EnterNewGoals(user);
                        break;
                    case 2:
                        // Display Goals
                        user.DisplayGoals();
                        break;
                    case 3:
                        // Record Event
                        RecordEvent(user);
                        break;
                    case 4:
                        // Display Score
                        user.DisplayScore();
                        break;
                    case 5:
                        // Exit
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }

        static void EnterNewGoals(User user)
        {
            while (true)
            {
                Console.WriteLine("\nEnter New Goals:");
                Console.WriteLine("1. Simple Goal");
                Console.WriteLine("2. Eternal Goal");
                Console.WriteLine("3. Checklist Goal");
                Console.WriteLine("4. Back to Main Menu");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // Simple Goal
                        Console.Write("Enter the name of the simple goal: ");
                        string simpleGoalName = Console.ReadLine();
                        user.AddGoal(new SimpleGoal(simpleGoalName));
                        break;
                    case 2:
                        // Eternal Goal
                        Console.Write("Enter the name of the eternal goal: ");
                        string eternalGoalName = Console.ReadLine();
                        user.AddGoal(new EternalGoal(eternalGoalName));
                        break;
                    case 3:
                        // Checklist Goal
                        Console.Write("Enter the name of the checklist goal: ");
                        string checklistGoalName = Console.ReadLine();
                        Console.Write("Enter the number of times you pretend to complete the goal: ");
                        int timesPretended = int.Parse(Console.ReadLine());
                        user.AddGoal(new ChecklistGoal(checklistGoalName, timesPretended));
                        break;
                    case 4:
                        // Back to Main Menu
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }

        static void RecordEvent(User user)
        {
            Console.WriteLine("\nRecord Event:");

            Console.WriteLine("Select a goal to record event for:");
            for (int i = 0; i < user.Goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {user.Goals[i].Name}");
            }

            Console.Write("Enter the index of the goal: ");
            int goalIndex = int.Parse(Console.ReadLine()) - 1;

            user.RecordEvent(goalIndex);
            Console.WriteLine("Event recorded successfully.");
        }
    }
}
