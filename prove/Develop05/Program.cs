using System;

namespace GoalTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            string goalsFilePath = "goals.txt";

            // Open the goals.txt file if it exists, otherwise create it
            if (!System.IO.File.Exists(goalsFilePath))
            {
                System.IO.File.Create(goalsFilePath).Close();
            }

            // Load goals from the file
            user.LoadGoals(goalsFilePath);

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Enter New Goals");
                Console.WriteLine("2. Display Goals");
                Console.WriteLine("3. Record Event");
                Console.WriteLine("4. Display Score");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    continue;
                }

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
                        // Save goals and exit
                        user.SaveGoals(goalsFilePath);
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
                Console.WriteLine("4. Negative Goal");
                Console.WriteLine("5. Back to Main Menu");

                Console.Write("Enter your choice: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    continue;
                }

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
                        int timesPretended;
                        if (!int.TryParse(Console.ReadLine(), out timesPretended))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                            continue;
                        }
                        user.AddGoal(new ChecklistGoal(checklistGoalName, timesPretended));
                        break;
                    case 4:
                        // Negative Goal
                        Console.Write("Enter the name of the negative goal: ");
                        string negativeGoalName = Console.ReadLine();
                        Console.Write("Enter the penalty points for this goal: ");
                        int penaltyPoints;
                        if (!int.TryParse(Console.ReadLine(), out penaltyPoints))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                            continue;
                        }
                        user.AddGoal(new NegativeGoal(negativeGoalName, penaltyPoints));
                        break;
                    case 5:
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
            int goalIndex;
            if (!int.TryParse(Console.ReadLine(), out goalIndex) || goalIndex <= 0 || goalIndex > user.Goals.Count)
            {
                Console.WriteLine("Invalid goal index.");
                return;
            }

            user.RecordEvent(goalIndex - 1);
            Console.WriteLine("Event recorded successfully.");
        }
    }
}
