using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private readonly string reference;
    private readonly List<string> words;
    private readonly List<int> hiddenIndices;

    public Scripture(string reference, string text)
    {
        this.reference = reference;
        words = text.Split(' ').ToList();
        hiddenIndices = new List<int>();
    }

    public bool HideRandomWord()
    {
        if (hiddenIndices.Count == words.Count)
            return false;

        Random random = new Random();
        int index;
        do
        {
            index = random.Next(0, words.Count);
        } while (hiddenIndices.Contains(index));

        hiddenIndices.Add(index);
        return true;
    }

    public void Display()
    {
        List<string> displayedText = new List<string>();
        for (int i = 0; i < words.Count; i++)
        {
            if (hiddenIndices.Contains(i))
                displayedText.Add(new string('_', words[i].Length));
            else
                displayedText.Add(words[i]);
        }

        Console.WriteLine($"{reference}: {string.Join(" ", displayedText)}");
    }

    public void ShowLastHiddenWord()
    {
        if (hiddenIndices.Count == 0)
        {
            Console.WriteLine("No words are hidden.");
            return;
        }

        for (int i = words.Count - 1; i >= 0; i--)
        {
            if (hiddenIndices.Contains(i))
            {
                hiddenIndices.Remove(i);
                Display();
                return;
            }
        }

        Console.WriteLine("No more hidden words to show.");
    }
}

public class ScriptureReference
{
    private readonly string reference;

    public ScriptureReference(string reference)
    {
        this.reference = reference;
    }

    public override string ToString()
    {
        return reference;
    }
}

public class Word
{
    private readonly string text;

    public Word(string text)
    {
        this.text = text;
    }

    public override string ToString()
    {
        return text;
    }
}

public class ScriptureMemorizationProgram
{
    public void Run()
    {
        Console.Write("Enter the scripture reference: ");
        string reference = Console.ReadLine();
        Console.Write("Enter the scripture text: ");
        string text = Console.ReadLine();

        Scripture scripture = new Scripture(reference, text);
        scripture.Display();

        while (true)
        {
            Console.Write("Press Enter to hide a word, press Esc to show the last hidden word, or type 'quit' to exit: ");
            var key = Console.ReadKey(intercept: true).Key;
            if (key == ConsoleKey.Escape) //If user press ESC las hidden word will be revealed again.
            {
                Console.Clear();
                scripture.ShowLastHiddenWord();
            }
            else if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                if (!scripture.HideRandomWord())
                {
                    Console.WriteLine("All words are hidden.");
                    break;
                }
                scripture.Display();
            }
            else if (Console.ReadLine().ToLower() == "quit")
            
            {
                break;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ScriptureMemorizationProgram program = new ScriptureMemorizationProgram();
        program.Run();
    }
}
