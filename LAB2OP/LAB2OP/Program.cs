using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2OP
{
    class Program
    {
        static string InputFile = "students.csv"; 
        static string OutputFile = "rating.csv";
        static double scholarshipPercentage = 0.4;
            
        static void Main(string[] args)
        {
            
        }

        static Student[] LoadStudentList(string file)
        {
            StreamReader sr = new StreamReader(file);
            int numberOfStudents = int.Parse(sr.ReadLine());
            Student[] students = new Student[numberOfStudents];
            for (int i = 0; i < numberOfStudents; i++)
            {
                students[i] = Student.FromCsv(sr.ReadLine());
            }
            return students;
        }

        static int NumberOfBudgetStudents(Student[] students)
        {
            int number = 0;
            for (int i = 0; i < students.Length; i++)
            {
                if (!students[i].isContract) i++;
            }
            return number;
        }

        static Student[] GetScholars(Student[] students)
        {
            return students.OrderBy(student => student.average).OrderBy(student => student.isContract).Take((int)(NumberOfBudgetStudents(students)/scholarshipPercentage)).ToArray();
        }

    }
}
