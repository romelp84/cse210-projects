using System;
using System.Threading;

public abstract class BaseActivity
{
    protected int duration;

    protected BaseActivity(int duration)
    {
        this.duration = duration;
    }

    public void StartActivity(string activityName, string description)
    {
        Console.WriteLine($"\n--- {activityName} ---");
        Console.WriteLine(description);
        Console.WriteLine($"Duration: {duration} seconds");
        PrepareToBegin();
    }

    public void EndActivity(string activityName)
    {
        Console.WriteLine("\nGood job!");
        Console.WriteLine($"You've completed {activityName} for {duration} seconds.");
        Pause();
        Console.WriteLine("\n--- Activity Completed ---");
    }

    protected void PrepareToBegin()
    {
        Console.WriteLine("Get ready to begin...");
        Pause();
    }

    protected void Pause()
    {
        for (int i = 0; i < 3; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

public class BreathingActivity : BaseActivity
{
    public BreathingActivity(int duration) : base(duration) { }

    public void PerformActivity()
    {
        StartActivity("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");
        Console.WriteLine("Starting breathing exercise:");

        for (int i = 0; i < duration / 2; i++)
        {
            Console.WriteLine("Breathe in...");
            DisplayBreathAnimation("Breathe in...");
            Console.WriteLine("Breathe out...");
            DisplayBreathAnimation("Breathe out...");
        }
        EndActivity("Breathing Activity");
    }

    private void DisplayBreathAnimation(string message)
    {
        int maxBreathSize = 10;
        int animationDuration = 100;
        int initialDelay = 100;

        for (int i = 1; i <= maxBreathSize; i++)
        {
            string animatedMessage = message.PadLeft(i);
            Console.WriteLine(animatedMessage);
            Thread.Sleep(animationDuration);

            if (i >= maxBreathSize / 2)
            {
                Thread.Sleep(animationDuration * 2);
            }
        }
    }
}

public class ReflectionActivity : BaseActivity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] reflectionQuestions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity(int duration) : base(duration) { }

    public void PerformActivity()
    {
        StartActivity("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
        string prompt = prompts[new Random().Next(prompts.Length)];
        Console.WriteLine($"Think about the following prompt: {prompt}");
        Pause();
        Console.WriteLine("Now, reflect on the following questions:");
        foreach (string question in reflectionQuestions)
        {
            Console.WriteLine(question);
            PauseWithSpinner();
        }
        EndActivity("Reflection Activity");
    }

    private void PauseWithSpinner()
    {
        for (int i = 0; i < 3; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

public class ListingActivity : BaseActivity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(int duration) : base(duration) { }

    public void PerformActivity()
    {
        StartActivity("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
        string prompt = prompts[new Random().Next(prompts.Length)];
        Console.WriteLine($"Think about the following prompt: {prompt}");
        Console.WriteLine($"You have {duration} seconds to list as many items as you can:");
        Pause();
        int itemsCount = ListItems();
        Console.WriteLine($"\nYou listed {itemsCount} items.");
        EndActivity("Listing Activity");
    }

    private int ListItems()
    {
        DateTime startTime = DateTime.Now;
        int itemsCount = 0;
        while (DateTime.Now - startTime < TimeSpan.FromSeconds(duration))
        {
            Console.Write("Enter an item (press Enter to finish): ");
            string item = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(item))
                break;
            itemsCount++;
        }
        return itemsCount;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Mindfulness Activities App!");

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter duration in seconds: ");
                    int duration1 = int.Parse(Console.ReadLine());
                    BreathingActivity breathingActivity = new BreathingActivity(duration1);
                    breathingActivity.PerformActivity();
                    break;
                case "2":
                    Console.Write("Enter duration in seconds: ");
                    int duration2 = int.Parse(Console.ReadLine());
                    ReflectionActivity reflectionActivity = new ReflectionActivity(duration2);
                    reflectionActivity.PerformActivity();
                    break;
                case "3":
                    Console.Write("Enter duration in seconds: ");
                    int duration3 = int.Parse(Console.ReadLine());
                    ListingActivity listingActivity = new ListingActivity(duration3);
                    listingActivity.PerformActivity();
                    break;
                case "4":
                    Console.WriteLine("Thank you for using the Mindfulness Activities App. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please choose again.");
                    break;
            }
        }
    }
}