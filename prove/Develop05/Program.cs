using System;
using System.Collections.Generic;
using System.IO;

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
