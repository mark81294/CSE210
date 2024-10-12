// JournalProgram.cs
using System;
using System.Collections.Generic;
using System.IO;

public class JournalEntry
{
    // Properties for the entry's prompt, response, and date
    public string Prompt { get; private set; }
    public string Response { get; private set; }
    public string Date { get; private set; }

    // Constructor to initialize the journal entry
    public JournalEntry(string prompt, string response)
    {
        Prompt = prompt;
        Response = response;
        Date = DateTime.Now.ToShortDateString();
    }

    // Override ToString for formatted output
    public override string ToString()
    {
        return $"{Date} | {Prompt} | {Response}";
    }
}

public class Journal
{
    // List to hold journal entries
    private List<JournalEntry> entries = new List<JournalEntry>();

    // Predefined prompts for journal entries
    private static readonly string[] prompts = {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    // Method to add a new journal entry
    public void AddEntry(string response)
    {
        Random random = new Random();
        int index = random.Next(prompts.Length);
        JournalEntry entry = new JournalEntry(prompts[index], response);
        entries.Add(entry);
        Console.WriteLine("Entry added!");
    }

    // Method to display all journal entries
    public void DisplayEntries()
    {
        Console.WriteLine("Journal Entries:");
        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    // Method to save journal entries to a file
    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                outputFile.WriteLine(entry);
            }
        }
        Console.WriteLine("Journal saved to file!");
    }

    // Method to load journal entries from a file
    public void LoadFromFile(string filename)
    {
        entries.Clear();
        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 3)
            {
                string date = parts[0].Trim();
                string prompt = parts[1].Trim();
                string response = parts[2].Trim();
                entries.Add(new JournalEntry(prompt, response) { Date = date });
            }
        }
        Console.WriteLine("Journal loaded from file!");
    }
}

class Program
{
    // Entry point of the application
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;

        // Main loop for the journal application
        while (running)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to a file");
            Console.WriteLine("4. Load journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option (1-5): ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Enter your response: ");
                    string response = Console.ReadLine();
                    journal.AddEntry(response);
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    Console.Write("Enter filename to save: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveToFile(saveFilename);
                    break;
                case "4":
                    Console.Write("Enter filename to load: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadFromFile(loadFilename);
                    break;
                case "5":
                    running = false;
                    Console.WriteLine("Exiting the program.");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
