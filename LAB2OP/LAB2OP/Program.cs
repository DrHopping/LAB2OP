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
            
        //srtudentA, studentB 75
        //student 60,50,60,60,60,60
        //student 75,

        static void Main(string[] args)
        {
            Student[] students = LoadStudentList(InputFile);
            RetakesUpdate(students);
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
            return students.OrderByDescending(student => student.average).OrderBy(student => student.isContract).OrderBy(student => student.retakes).Take((int)(NumberOfBudgetStudents(students)*scholarshipPercentage)).ToArray();
        }

        static void SaveRating(string file, Student[] scholars)
        {
            scholars.GroupBy(student => student.average);
            StreamWriter sw = new StreamWriter(OutputFile);
            sw.WriteLine("Minimum score for scholarship,{0}", scholars.Last().average.ToString("0.00"));
            foreach (var group in scholars.GroupBy(student => student.average))
            {
                foreach (var student in group)
                {
                    sw.Write(student.name+",");
                }
                sw.WriteLine(group.First().average.ToString("0.00"));
            }
            
            //bool equalMark = false;
            //for (int i = 0; i < scholars.Length; i++)
            //{
            //    if (i + 1 < scholars.Length && scholars[i].average == scholars[i + 1].average)
            //    {
            //        sw.Write(scholars[i].name + ",");
            //        equalMark = true;
            //    }
            //    else
            //        if (equalMark == true)
            //    {
            //        sw.WriteLine("{0},{1}", scholars[i].name, scholars[i].average.ToString("0.00"));
            //        equalMark = false;
            //    }
            //    else
            //        sw.WriteLine("{0},{1}", scholars[i].name, scholars[i].average.ToString("0.00"));
            //}
            sw.Close();
        }

        static void RetakesUpdate(Student[] students)
        {
            int maxMarks = students.Max(student => student.marks.Length);
            foreach (var student in students)
            {
                if (student.marks.Length < maxMarks)
                    student.retakes = true;
            }
        }
    }
}
