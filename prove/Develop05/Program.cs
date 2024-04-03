using System;

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
                    UserInterface.EnterNewGoals(user);
                    break;
                case 2:
                    // Display Goals
                    user.DisplayGoals();
                    break;
                case 3:
                    // Record Event
                    UserInterface.RecordEvent(user);
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
}
