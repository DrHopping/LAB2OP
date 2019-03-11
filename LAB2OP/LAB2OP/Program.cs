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
            Student[] students = LoadStudentList(InputFile);
            SaveRating(OutputFile, GetScholars(students));
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
            sr.Close();
            return students;
        }

        static int NumberOfBudgetStudents(Student[] students)
        {
            int number = 0;
            for (int i = 0; i < students.Length; i++)
            {
                if (!students[i].isContract) number++;
            }
            return number;
        }

        static Student[] GetScholars(Student[] students)
        {
            return students.OrderByDescending(student => student.average).OrderBy(student => student.isContract).Take((int)(NumberOfBudgetStudents(students)*scholarshipPercentage)).ToArray();
        }

        static void SaveRating(string file, Student[] scholars)
        {
            StreamWriter sw = new StreamWriter(OutputFile);
            sw.WriteLine("Minimum score for scholarship is {0}", scholars.Last().average.ToString("0.00"));
            foreach (var student in scholars)
            {
                sw.WriteLine("{0}: {1}", student.name, student.average.ToString("0.00"));
            }
            sw.Close();
        }
    }
}
