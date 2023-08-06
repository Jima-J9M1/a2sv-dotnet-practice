using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

    public class StudentList<T> where T : Student
    {
        private List<T> students = new List<T>();
        // current Directory is the directory where the application is running and file path
        // is the path to the file where we want to save the data

        private string filePath = $"{Environment.CurrentDirectory}/students.json";
        // private string filePath = "students.json";

        public async Task StartAsync()
        {
        Console.WriteLine(filePath);

            // try and catch block
            try
            {
                // load to students from json file
                
                await LoadStudentsFromJsonAsync();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // await LoadStudentsFromJsonAsync();

            while (true)
            {
                Console.WriteLine("\n==== Student Management System ====");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Search Students by Name");
                Console.WriteLine("4. Search Students by Roll Number");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        await AddStudentAsync();
                        break;
                    case "2":
                        ViewStudents();
                        break;
                    case "3":
                        await SearchStudentsByNameAsync();
                        break;
                    case "4":
                        await SearchStudentsByRollNumberAsync();
                        break;
                    case "5":
                        await SaveStudentsToJsonAsync();
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        private async Task AddStudentAsync()
        {
            Console.Write("Enter student name: ");
            string name = Console.ReadLine();

            Console.Write("Enter student age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter student grade: ");
            string grade = Console.ReadLine();

            int rollNumber = students.Count + 1;

            T student = (T)Activator.CreateInstance(typeof(T), name, age, rollNumber, grade);
            students.Add(student);

            await SaveStudentsToJsonAsync();

            Console.WriteLine("Student added successfully.");
        }

        private void ViewStudents()
        {
            Console.WriteLine("\n==== All Students ====");
            foreach (T student in students)
            {
                Console.WriteLine($"Name: {student.Name}");
                Console.WriteLine($"Age: {student.Age}");
                Console.WriteLine($"Roll Number: {student.RollNumber}");
                Console.WriteLine($"Grade: {student.Grade}");
                Console.WriteLine();
            }
        }

        private async Task SearchStudentsByNameAsync()
        {
            Console.Write("Enter student name: ");
            string name = Console.ReadLine();

            var query = from student in students
                        where student.Name.ToLower().Contains(name.ToLower())
                        select student;

            if (query.Count() == 0)
            {
                Console.WriteLine("No students found.");
            }
            else
            {
                Console.WriteLine("\n==== Search Results ====");
                foreach (T student in query)
                {
                    Console.WriteLine($"Name: {student.Name}");
                    Console.WriteLine($"Age: {student.Age}");
                    Console.WriteLine($"Roll Number: {student.RollNumber}");
                    Console.WriteLine($"Grade: {student.Grade}");
                    Console.WriteLine();
                }
            }
        }

        private async Task SearchStudentsByRollNumberAsync()
        {
            Console.Write("Enter student roll number: ");
            int rollNumber = int.Parse(Console.ReadLine());

            var query = from student in students
                        where student.RollNumber == rollNumber
                        select student;

            if (query.Count() == 0)
            {
                Console.WriteLine("No students found.");
            }
            else
            {
                Console.WriteLine("\n==== Search Results ====");
                foreach (T student in query)
                {
                    Console.WriteLine($"Name: {student.Name}");
                    Console.WriteLine($"Age: {student.Age}");
                    Console.WriteLine($"Roll Number: {student.RollNumber}");
                    Console.WriteLine($"Grade: {student.Grade}");
                    Console.WriteLine();
                }
            }
        }

        private async Task SaveStudentsToJsonAsync()
        {
            string json = JsonSerializer.Serialize(students);
            await File.WriteAllTextAsync(filePath, json);
        }

        private async Task LoadStudentsFromJsonAsync()
        {
            /*
            Error: Each parameter in the deserialization constructor on type 'Student' must bind to an object property or field on deserialization. Each parameter name must match with a property or field on the object. 
            Fields are only considered when 'JsonSerializerOptions.IncludeFields' is enabled. The match can be case-insensitive.
            */


            if (File.Exists(filePath))
            {
                string json = await File.ReadAllTextAsync(filePath);
                students = JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions { IncludeFields = true });
                Console.WriteLine("Students loaded successfully.",json);
            }else{
                Console.WriteLine("File not found");}
        }

    }

    class Program
    {
        static async Task Main(string[] args)
        {
            StudentList<Student> studentList = new StudentList<Student>();
            await studentList.StartAsync();
        }
    }



