using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Course_Registration_System
{
    public class Student
    {
        public int Id { get; private set; }
        public string Name { get;private set; }
        public string Email { get;private set; }
        public Department Major { get; private set; }
        public StudentStatus Status {  get; private set; }
        public int EnrollmentYear { get; private set; }
        public int ExpectedGraduationYear { get; private set; }

        public Student(int id,string name,string email,Department major,
            StudentStatus status,int enrollmentYear,int expectedGraduationYear)
        {
            if (id <= 0)
            {
                throw new ArgumentException("student id must be positive");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name is required");
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is Required");
            }
            if (!email.Contains("@"))
            {
                throw new ArgumentException("Email must contain @");

            }
            if (expectedGraduationYear < enrollmentYear)
            {
                throw new ArgumentException("Graduation year must be after enrollment year");
            }

            Id = id;
            Name=name.ToUpper();
            Email=email.ToLower();
            Major = major;
            Status = status;
            EnrollmentYear = enrollmentYear;
            ExpectedGraduationYear = expectedGraduationYear;
        }
    }
}
