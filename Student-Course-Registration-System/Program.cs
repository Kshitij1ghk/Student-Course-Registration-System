using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Course_Registration_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Department dept = Department.COMPUTER_SCEINCE;
            Console.WriteLine("Department:" + dept);

            StudentStatus status=StudentStatus.ACTIVE;
            Console.WriteLine("STATUS:"+status);

            Semester semester = Semester.FALL;
            Console.WriteLine("Semester: " + semester);

            //test comparision
            if (dept == Department.COMPUTER_SCEINCE)
            {
                Console.WriteLine("this is a cs student");
            }

            Department parseDept=(Department)Enum.Parse(typeof(Department),"MATHEMATICS");
            //This converts a string ("MATHEMATICS") into an enum value (Department.MATHEMATICS).

            Console.WriteLine("PARSE DEPT:"+parseDept);

            Console.WriteLine("ALL ENUMS ARE WORKING");
            Console.ReadKey();
        }
    }
}
