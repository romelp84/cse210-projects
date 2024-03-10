using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();

        while (true)
        {
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    journal.WriteNewEntry();
                    break;

                case 2:
                    journal.DisplayJournal();
                    break;

                case 3:
                    Console.Write("Enter filename to save: ");
                    string saveFileName = Console.ReadLine();
                    journal.SaveToFile(saveFileName);
                    break;

                case 4:
                    Console.Write("Enter filename to load: ");
                    string loadFileName = Console.ReadLine();
                    journal.LoadFromFile(loadFileName);
                    break;

                case 5:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }
    }
}
class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public Entry(string prompt, string response)
    {
        Prompt = prompt;
        Response = response;
        Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
    public override string ToString()
    {
        return $"{Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}
class Journal
{
    private List<Entry> entries;
    public Journal()
    {
        entries = new List<Entry>();
    }
    public void WriteNewEntry()
    {
        List<string> prompts = new List<string>
        {
            "Who left a lasting impression on me today?",
            "What was the best part of my day?",
            "What brought me the greatest joy or satisfaction today?",
            "What was the strongest emotion I felt today?",
            "If I could redo one part of my day, what would it be and how might I change it?"
        };

        Random random = new Random();
        string randomPrompt = prompts[random.Next(prompts.Count)];

        Console.WriteLine($"Prompt: {randomPrompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        Entry newEntry = new Entry(randomPrompt, response);
        entries.Add(newEntry);

        Console.WriteLine("Entry recorded successfully!\n");
    }
    public void DisplayJournal()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("Journal is empty.\n");
            return;
        }

        Console.WriteLine("Journal Entries:\n");

        foreach (Entry entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string fileName)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (Entry entry in entries)
                {
                    sw.WriteLine($"{entry.Prompt}|{entry.Response}|{entry.Date}");
                }
            }

            Console.WriteLine($"Journal saved to {fileName} successfully!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving journal to file: {ex.Message}\n");
        }
    }
    public void LoadFromFile(string fileName)
    {
        try
        {
            List<Entry> loadedEntries = new List<Entry>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    string prompt = parts[0];
                    string response = parts[1];
                    string date = parts[2];

                    loadedEntries.Add(new Entry(prompt, response) { Date = date });
                }
            }

            entries = loadedEntries;
            Console.WriteLine($"Journal loaded from {fileName} successfully!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading journal from file: {ex.Message}\n");
        }
    }
}