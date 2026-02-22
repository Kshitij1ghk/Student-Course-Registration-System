using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Course_Registration_System
{
    public class FileStorage : IStorageIntrerface
    {
        private const string StudentsFile = "students.csv";
        private const string CoursesFile = "courses.csv";
        private const string EnrollmentsFile = "enrollments.csv";

        public void SaveStudents(Dictionary<int, Student> students)
        {
            List<string> lines = new List<string>();
            lines.Add("StudentId,Name,Email,Major,Status,EnrollmentYear,ExpectedGraduationYear");
            foreach (Student student in students.Values)
            {
                string line = student.StudentId + "," +
                             student.Name + "," +
                             student.Email + "," +
                             student.Major + "," +
                             student.Status + "," +
                             student.EnrollmentYear + "," +
                             student.ExpectedGraduationYear;
                lines.Add(line);
            }
            File.WriteAllLines(StudentsFile, lines.ToArray());
        }

        public void SaveCourses(Dictionary<int,Course> courses)
        {
            List <string> lines = new List<string>();
            lines.Add("CourseId,Name,Department,Credits,Capacity,EnrolledCount,Schedule,Semester,Year");

            foreach(Course course in courses.Values)
            {
                string line= course.CourseId + "," +
                             course.Name + "," +
                             course.Department + "," +
                             course.Credits + "," +
                             course.Capacity + "," +
                             course.EnrolledCount + "," +
                             course.Schedule + "," +
                             course.Semester + "," +
                             course.Year;
                lines .Add(line);
            }
            File.WriteAllLines(CoursesFile, lines.ToArray());
        }

        public void SaveEnrollments(Dictionary<int,List<int>> enrollments)
        {
            List<string> lines = new List<string>();
            lines.Add("StudentId,courseId");
            foreach(KeyValuePair<int,List<int>> enrollment in enrollments)
            {
                foreach(int courseId in enrollment.Value)
                {
                    string line = enrollment.Key + "," + courseId;
                    lines.Add(line);
                }
            }
            File.WriteAllLines(EnrollmentsFile, lines.ToArray());
        }

        public Dictionary<int,Student> LoadStudents()
        {
            Dictionary<int, Student> students=new Dictionary<int, Student> ();
            if (!File.Exists(StudentsFile))
            {
                return students;
            }
            string[] lines=File.ReadAllLines(StudentsFile);
            for(int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;
                try
                {
                    string[] parts = lines[i].Split(',');
                    int studentId = int.Parse(parts[0]);
                    string name = parts[1];
                    string email = parts[2];
                    Department major = (Department)Enum.Parse(typeof(Department), parts[3]);
                    StudentStatus status = (StudentStatus)Enum.Parse(typeof(StudentStatus), parts[4]);
                    int enrollmentYear = int.Parse(parts[5]);
                    int expectedGraduationYear = int.Parse(parts[6]);

                    Student student = new Student(studentId, name, email, major, status, enrollmentYear, expectedGraduationYear);
                    students.Add(studentId, student);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return students;

        }

        public Dictionary<int,Course> LoadCourses()
        {
            Dictionary<int,Course> courses=new Dictionary<int,Course>();
            if (!File.Exists(CoursesFile))
                return courses;
            string[] lines=File.ReadAllLines(CoursesFile);
            for(int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                    continue;
                try
                {
                    string[] parts = lines[i].Split(',');
                    int courseId = int.Parse(parts[0]);
                    string name = parts[1];
                    Department department = (Department)Enum.Parse(typeof(Department), parts[2]);
                    int credits = int.Parse(parts[3]);
                    int capacity = int.Parse(parts[4]);
                    int enrolledCount = int.Parse(parts[5]);
                    string schedule = parts[6];
                    Semester semester = (Semester)Enum.Parse(typeof(Semester), parts[7]);
                    int year = int.Parse(parts[8]);

                    Course course= new Course(courseId, name, department, credits,
                                             capacity, schedule, semester, year);
                    for(int j = 0; j < enrolledCount; j++)
                    {
                        course.IncrementEnrollment();
                    }
                    courses.Add(courseId, course);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return courses;
        }
        public Dictionary<int,List<int>> LoadEnrollments()
        {
            Dictionary<int,List<int>> enrollments=new Dictionary<int,List<int>>();
            if (!File.Exists(EnrollmentsFile))
                return enrollments;
            string[] lines=File.ReadAllLines(EnrollmentsFile);

            for(int i = 1; i < lines.Length; i++)
            {
                if(string.IsNullOrWhiteSpace(lines[i]))
                    continue;
                try
                {
                    string[] parts = lines[i].Split(',');

                    int studentId = int.Parse(parts[0]);
                    int courseId = int.Parse(parts[1]);

                    if (enrollments.ContainsKey(studentId))
                    {
                        enrollments[studentId].Add(courseId);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return enrollments;
        }

        public void ClearAllData()
        {
            File.WriteAllLines(StudentsFile, new string[] { "StudentId,Name,Email,Major,Status,EnrollmentYear,ExpectedGraduationYear" });
            File.WriteAllLines(CoursesFile, new string[] { "CourseId,Name,Department,Credits,Capacity,EnrolledCount,Schedule,Semester,Year" });
            File.WriteAllLines(EnrollmentsFile, new string[] { "StudentId,CourseId" });
        }
    }


}
