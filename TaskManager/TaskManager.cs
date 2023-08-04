using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;


// enum TaskCategory { Personal, Work, Errands, Health, Family }
// used to categorize tasks into different categories
public enum TaskCategory
{
    Personal,
    Work,
    Errands,
    Health,
    Family
}


// TaskItem class to store information about a task
// Name, Description, Category, IsCompleted
public class TaskItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskCategory Category { get; set; }
    public bool IsCompleted { get; set; }
}

// TaskManager class to manage tasks in a list and save/load tasks from a CSV file 
// AddTaskAsync() - Add a new task to the list
// ViewTasks() - Display all tasks in the list
// SaveTasksToCsvAsync() - Save all tasks in the list to a CSV file
// LoadTasksFromCsvAsync() - Load all tasks from a CSV file into the list
// StartAsync() - Display the main menu and handle user input 

public class TaskManager
{
    private List<TaskItem> tasks = new List<TaskItem>();
    private string filePath = "tasks.csv";
    

    // Prompt the user to enter the task name, description, and category
    public async Task StartAsync()
    {
        await LoadTasksFromCsvAsync();
        while (true)
        {
            Console.WriteLine("\n==== Simple Task Manager ====");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await AddTaskAsync();
                    break;
                case "2":
                    ViewTasks();
                    break;
                case "3":
                    await SaveTasksToCsvAsync();
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
    
    // Load tasks from a CSV file into the list 
    public async Task LoadTasksFromCsvAsync()
    {
        if (File.Exists(filePath))
        {

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    await reader.ReadLineAsync(); // Skip header line
                    while (!reader.EndOfStream)
                    {
                        string line = await reader.ReadLineAsync();
                        string[] parts = line.Split(',');
                        if (parts.Length == 4 &&
                            Enum.TryParse(parts[2], out TaskCategory category) &&
                            bool.TryParse(parts[3], out bool isCompleted))
                        {
                            TaskItem task = new TaskItem
                            {
                                Name = parts[0],
                                Description = parts[1],
                                Category = category,
                                IsCompleted = isCompleted
                            };

                            tasks.Add(task);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while reading tasks from the CSV file: " + ex.Message);
            }
        }
    }
    
    // Save all tasks in the list to a CSV file
    public async Task SaveTasksToCsvAsync()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // await writer.WriteLineAsync("Name,Description,Category,IsCompleted");
                foreach (TaskItem task in tasks)
                {
                    await writer.WriteLineAsync($"{task.Name},{task.Description},{task.Category},{task.IsCompleted}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred while writing tasks to the CSV file: " + ex.Message);
        }
    }
    
    // Add a new task to the list 
    public async Task AddTaskAsync()
    {
        Console.Write("Enter Task Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Task Description: ");
        string description = Console.ReadLine();

        Console.WriteLine("Select Task Category:");
        int categoryIndex = -1;
        bool isValidInput = false;

        while (!isValidInput)
        {
            Console.WriteLine("Select Task Category:");
            for (int i = 0; i < Enum.GetNames(typeof(TaskCategory)).Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Enum.GetNames(typeof(TaskCategory))[i]}");
            }

            string input = Console.ReadLine();
            if (int.TryParse(input, out categoryIndex) && categoryIndex >= 1 && categoryIndex <= Enum.GetNames(typeof(TaskCategory)).Length)
            {
                isValidInput = true;
            }
            else
            {
                Console.WriteLine("Invalid category. Try again.");
            }
        }

        TaskCategory category = (TaskCategory)(categoryIndex);
        // for (int i = 0; i < Enum.GetNames(typeof(TaskCategory)).Length; i++)
        // {
        //     Console.WriteLine($"{i + 1}. {Enum.GetNames(typeof(TaskCategory))[i]}");
        // }

        // int categoryIndex;

        // // handdle int input error with try and catch
        // try
        // {
        //     categoryIndex = int.Parse(Console.ReadLine()) - 1;
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine("Invalid category. Try again.");
        //     categoryIndex = int.Parse(Console.ReadLine()) - 1;
        // }
        

        

        // TaskCategory category = (TaskCategory)categoryIndex;

        TaskItem task = new TaskItem
        {
            Name = name,
            Description = description,
            Category = category,
            IsCompleted = false
        };

        tasks.Add(task);
        Console.WriteLine("Task added successfully!");
    }

    public void ViewTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks to display.");
        }
        else
        {
            Console.WriteLine("\n==== Tasks ====");
            foreach (TaskItem task in tasks)
            {
                Console.WriteLine($"Name: {task.Name}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Category: {task.Category}");
                Console.WriteLine($"Completed: {(task.IsCompleted ? "Yes" : "No")}\n");
            }
        }
    }
}

public class Program
{
    public static async Task Main()
    {
        TaskManager taskManager = new TaskManager();
        await taskManager.StartAsync();
    }
}