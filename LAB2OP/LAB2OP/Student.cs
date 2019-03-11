using System;
namespace LAB2OP
{
    class Student
    {
        public string name;
        public int[] marks;
        public bool isContract;

        public Student(string name, int[] marks, bool isContract)
        {
            this.name = name;
            this.marks = marks;
            this.isContract = isContract;
        }

        static Student FromCsv(string line)
        {
            string name = line.Substring(0, line.IndexOf(","));

            string[] marksStr = line.Substring(line.IndexOf(",") + 1, line.LastIndexOf(",") - line.IndexOf(",") - 1).Split(',');
            int[] marks = Array.ConvertAll<string, int>(marksStr, int.Parse);

            string contract = line.Substring(line.LastIndexOf(",") + 1, line.Length - line.LastIndexOf(",") - 1);
            bool isContract;
            if (contract == "TRUE" || contract == "+")
                isContract = true;
            else
                isContract = false;

            return new Student(name, marks, isContract);
        }
    }
}
