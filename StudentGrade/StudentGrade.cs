/*
Use variables and data types to store student data.
Use conditional statements to validate input (e.g., ensure grade values are within a valid range).
Implement loops to handle multiple subjects and grades.
Utilize collections (e.g., List, Dictionary) to store subject names and corresponding grades.
Define a method to calculate the average grade based on the entered grades.
Use string interpolation to display the results in a user-friendly format.
*/
using System;
using System.Collections.Generic;


float averageGrade(int size, Dictionary<string, float> subjectDict)
{
   float sum = 0;

   foreach(var subject in subjectDict){
         sum += subject.Value;
   }
   
   float result = sum / size;
   return result;
}

void addStudentData()
{
    Console.Write("Enter student name: ");
    string name = Console.ReadLine() ?? "StudentName";
    Console.Write("Enter number of subjects: ");
    int numSubjects = int.Parse(Console.ReadLine()?? "0");
    Dictionary<string, float> subjectDict = subjectValue(numSubjects);

    float avg = averageGrade(numSubjects, subjectDict);
    printStudentData(name, subjectDict, avg);
    
    // return 0;
}


void printStudentData(string name, Dictionary<string, float> subjectDict,float avg){
    Console.WriteLine("-------------------------");
    Console.WriteLine("Student name: {0}", name);
    // Console.WriteLine($"Student name: {name}");


    // Dictionary<string, float> subjectDict = subjectValue(numSubjects);


    foreach(KeyValuePair<string, float> grade in subjectDict){
        Console.WriteLine("Subject: {9}, Grade: {1}", grade.Key, grade.Value);
    }

    Console.WriteLine("Average grade: {0}", avg);
    Console.WriteLine("-------------------------");
}





Dictionary<string, float> subjectValue(int size)
{
    // Read in subject name and grade
    // Add to dictionary
    Dictionary<string, float> subjectDict = new Dictionary<string, float>();

    int indx = 0;
    while(indx < size){
        Console.Write("Enter subject name:");
        string subject = Console.ReadLine() ?? "SubjectName";
        Console.Write("Enter grade:");
        float grade = float.Parse(Console.ReadLine() ?? "0");

        if(grade < 0 || grade > 100){
            Console.WriteLine("Invalid grade. Please enter a grade between 0 and 100.");
            continue;
        }else if(subjectDict.ContainsKey(subject)){
            Console.WriteLine("Subject already exists. Please enter a new subject.");
            continue;
        }
        subjectDict.Add(subject, grade);
        indx ++;
    }

    return subjectDict;
}

void Main()
{
    addStudentData();
    // return 0;
}



Main();
