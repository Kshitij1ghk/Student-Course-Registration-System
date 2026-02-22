using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Student_Course_Registration_System
{
    
    internal class Program
    {
        static RegistrationService service = new RegistrationService();

        static void StudentManagementMenu()
        {
            bool inMenu = true;

            while (inMenu)
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine("         STUDENT MANAGEMENT              ");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Search Student by ID");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Remove Student");
                Console.WriteLine("6. Back to Main Menu");
                Console.WriteLine("==========================================");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ViewAllStudents();
                        break;
                    case "3":
                        SearchStudent();
                        break;
                    case "4":
                        UpdateStudent();
                        break;
                    case "5":
                        RemoveStudent();
                        break;
                    case "6":
                        inMenu = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice!");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("             ADD STUDENT                 ");
            Console.WriteLine("==========================================");

            try
            {
                Console.Write("Enter Student ID:");
                int studentId = int.Parse(Console.ReadLine());
                Console.Write("Enter Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Email: ");
                string email = Console.ReadLine();

                Console.WriteLine("\nDepartments");
                Console.WriteLine("0.Computer Science");
                Console.WriteLine("1. MATHEMATICS");
                Console.WriteLine("2. PHYSICS");
                Console.WriteLine("3. CHEMISTRY");
                Console.WriteLine("4. BIOLOGY");
                Console.WriteLine("5. ENGLISH");
                Console.WriteLine("6. HISTORY");
                Console.WriteLine("7. BUSINESS");
                Console.Write("Enter Department number: ");
                int deptChoice = int.Parse(Console.ReadLine());
                Department major = (Department)deptChoice;

                Console.Write("Enter Enrollment Year: ");
                int enrollmentYear = int.Parse(Console.ReadLine());

                Console.Write("Enter Expected Graduation Year: ");
                int graduationYear = int.Parse(Console.ReadLine());

                service.AddStudent(studentId, name, email, major, enrollmentYear, graduationYear);

                Console.WriteLine("\n✓ Student added successfully!");
            }
            catch (FormatException)
            {
                Console.WriteLine("\n Invalid input! please enter correct vales ");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine("\n error:" + ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine("\n error:" + ex.Message);
            }

            Console.WriteLine("press any key to continue..");
            Console.ReadKey();
        }

        static void ViewAllStudents()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("           ALL STUDENTS                  ");
            Console.WriteLine("==========================================");

            List<Student> allStudents=service.GetAllStudents();
            if (allStudents.Count == 0)
            {
                Console.WriteLine("No students found.");
            }
            else
            {
                Console.WriteLine(String.Format("{0,-10} {1,-25} {2,-30} {3,-20} {4,-10}",
           "ID", "Name", "Email", "Major", "Status"));
                Console.WriteLine(new string('-', 95));
                
                foreach(Student student in allStudents)
                {
                    Console.WriteLine(String.Format("{0,-10} {1,-25} {2,-30} {3,-20} {4,-10}",
                student.StudentId,
                student.Name,
                student.Email,
                student.Major,
                student.Status));
                }
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        static void SearchStudent()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("          SEARCH STUDENT BY ID           ");
            Console.WriteLine("==========================================");

            try
            {
                Console.Write("Enter Student ID:");
                int studentId = int.Parse(Console.ReadLine());
                Student student = service.GetStudent(studentId);
                if (student == null)
                {
                    Console.WriteLine("\nStudent not found.");
                }
                else
                {
                    Console.WriteLine("\n--- Student Details ---");
                    Console.WriteLine("ID: " + student.StudentId);
                    Console.WriteLine("Name: " + student.Name);
                    Console.WriteLine("Email: " + student.Email);
                    Console.WriteLine("Major: " + student.Major);
                    Console.WriteLine("Status: " + student.Status);
                    Console.WriteLine("Enrollment Year: " + student.EnrollmentYear);
                    Console.WriteLine("Expected Graduation: " + student.ExpectedGraduationYear);

                    List<Course> courses = service.GetStudentCourses(studentId);
                    Console.WriteLine("\nEnrolled Courses: " + courses.Count);
                    foreach(Course course in courses)
                    {
                        Console.WriteLine("  - " + course.Name + " (" + course.Credits + " credits)");
                    }
                    Console.WriteLine("TOTAL CREDITS: " + service.GetStudentTotalCredits(studentId);

                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\n invalid input");
            }
            Console.WriteLine("press any key to continue");
            Console.ReadKey();
        }

        static void UpdateStudent()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("           UPDATE STUDENT                ");
            Console.WriteLine("==========================================");

            try
            {
                Console.Write("Enter Student ID: ");
                int studentId = int.Parse(Console.ReadLine());

                Student existing = service.GetStudent(studentId);

                if (existing == null)
                {
                    Console.WriteLine("\nno such student found");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("\nCurrent Details:");
                    Console.WriteLine("Name: " + existing.Name);
                    Console.WriteLine("Email: " + existing.Email);
                    Console.WriteLine("Major: " + existing.Major);

                    Console.WriteLine("\n Enter new details (press enter to keep current):");
                    Console.Write("New Name[" + existing.Name + "]:");
                    string newName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(newName))
                    {
                        newName = existing.Name;
                    }
                    Console.Write("New Email [" + existing.Email + "]: ");
                    string newEmail = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(newEmail)) newEmail = existing.Email;

                    Console.WriteLine("\nDepartments:");
                    Console.WriteLine("0. COMPUTER_SCIENCE  1. MATHEMATICS  2. PHYSICS  3. CHEMISTRY");
                    Console.WriteLine("4. BIOLOGY  5. ENGLISH  6. HISTORY  7. BUSINESS");
                    Console.Write("New Major [current:" + existing.Major + "](press enter to keep the same one: ");
                    string majorInput = Console.ReadLine();
                    Department newMajor = existing.Major;
                    if (!string.IsNullOrWhiteSpace(majorInput))
                    {
                        newMajor = (Department)int.Parse(majorInput);
                    }
                    service.UpdateStudent(studentId, newName, newEmail, newMajor);
                    Console.WriteLine("\n updated succesfully");

                }
            }
            catch (FormatException)
            {
                Console.WriteLine("INvalid input!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.WriteLine("press any key to continue....");
            Console.ReadKey();
        }

        static void RemoveStudent()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("           REMOVE STUDENT                ");
            Console.WriteLine("==========================================");

            try
            {
                Console.WriteLine("Enter Student id to remove: ");
                int studentId = int.Parse(Console.ReadLine());

                Student existing = service.GetStudent(studentId);
                if (existing == null)
                {
                    Console.WriteLine("Student not found: ");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    service.RemoveStudent(studentId);
                    Console.WriteLine("STUDENT REMOVED SUCCEFULLY");
                }
                
            }
            catch (FormatException)
            {
                Console.WriteLine("INVALID INPUT !");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.WriteLine("ENTER ANY KEY TO CONTINUE...");
            Console.ReadKey();
        }

        //ABOVE ALL METHODS OF STUDENT MANAGEMENT 

        static void ClearAllData()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("            CLEAR ALL DATA               ");
            Console.WriteLine("==========================================");
            Console.WriteLine("WARNING: This will delete ALL data!");
            Console.Write("Type YES to confirm: ");

            string confirm = Console.ReadLine();

            if (confirm.ToUpper() == "YES")
            {
                service.ClearAllData();
                Console.WriteLine("\n✓ All data cleared!");
            }
            else
            {
                Console.WriteLine("\nOperation cancelled.");
            }

            Console.WriteLine("Enter any key to continue:");

            Console.ReadKey();
        }
        //above is clear all data method

        static void CourseManagementMenu()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("          COURSE MANAGEMENT              ");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Add Course");
            Console.WriteLine("2. View All Courses");
            Console.WriteLine("3. View Available Courses");
            Console.WriteLine("4. View Courses by Semester");
            Console.WriteLine("5. Search Course by ID");
            Console.WriteLine("6. Remove Course");
            Console.WriteLine("7. Back to Main Menu");
            Console.WriteLine("==========================================");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddCourse();
                    break;
                case "2":
                    ViewAllCoures();
                    break;
                case "3":
                    ViewAvailableCourses();
                    break;
                case "4":
                    ViewCoursesBySemester();
                    break;
                case "5":
                    SearchCourse();
                    break;
                case "6":
                    RemoveCourse();
                    break;
                case "7":
                    inMenu = false;
                    break;
                default:
                    Console.WriteLine("\nInvalid choice!");
                    Console.ReadKey();
                    break;

            }
        }
        static void AddCours()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("              ADD COURSE                 ");
            Console.WriteLine("==========================================");
            try
            {
                Console.WriteLine("Enter Course ID: ");
                int courseId = int.Parse(Console.ReadLine());
                Console.Write("Enter Course Name: ");
                string name = Console.ReadLine();

                Console.WriteLine("\nDepartments:");
                Console.WriteLine("0. COMPUTER_SCIENCE  1. MATHEMATICS  2. PHYSICS  3. CHEMISTRY");
                Console.WriteLine("4. BIOLOGY  5. ENGLISH  6. HISTORY  7. BUSINESS");
                Console.Write("Enter Department number: ");
                Department department = (Department)int.Parse(Console.ReadLine());

                Console.Write("Enter Credits(1-6): ");
                int credits = int.Parse(Console.ReadLine());

                Console.Write("Enter Capacity: ");
                int capacity = int.Parse(Console.ReadLine());

                Console.Write("Enter Schedule(eg. MWF 10:00-11:00): ");
                string schedule = Console.ReadLine();

                Console.WriteLine("\nSemesters: 0. FALL  1. SPRING  2. SUMMER");
                Console.Write("Enter Semester number: ");
                Semester semester = (Semester)int.Parse(Console.ReadLine());

                Console.Write("Enter Year:");
                int year = int.Parse(Console.ReadLine());

                service.AddCourse(courseId, name, department, credits, capacity, schedule, semester, year);
                Console.WriteLine("course added succesfully");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine("error: " + ex.Message);
            }
            Console.WriteLine("Press any key to continue: ");
            Console.ReadKey();
        }

        static void ViewAllCourses()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("             ALL COURSES                 ");
            Console.WriteLine("==========================================");

            List<Course> allCourses=service.GetAllCourse();
            if (allCourses.Count == 0)
            {
                Console.WriteLine("No courses found.");
            }
            else
            {
                Console.WriteLine(String.Format("{0,-8} {1,-30} {2,-20} {3,-8} {4,-10} {5,-15}",
           "ID", "Name", "Department", "Credits", "Enrolled", "Schedule"));
                Console.WriteLine(new string('-', 91));
                
                foreach (Course course in allCourses)
                {
                    Console.WriteLine(String.Format("{0,-8} {1,-30} {2,-20} {3,-8} {4,-10} {5,-15}",
                course.CourseId,
                course.Name,
                course.Department,
                course.Credits,
                course.EnrolledCount + "/" + course.Capacity,
                course.Schedule));
                }
            }
            Console.WriteLine("\n PRESS ANY KEY TO CONTINUE....");
            Console.ReadKey();
        }

        static void ViewAvaialbleCourses()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("          AVAILABLE COURSES              ");
            Console.WriteLine("==========================================");
             List<Course> avaialble=service.GetAvailableCourses();
            if(avaialble.Count== 0)
            {
                Console.WriteLine("No available courses.");
            }
            else
            {

                Console.WriteLine(String.Format("{0,-8} {1,-30} {2,-8} {3,-12} {4,-15}",
                    "ID", "Name", "Credits", "Spots Left", "Schedule"));
                Console.WriteLine(new string('-', 73));
                foreach(Course course in avaialble)
                {
                    int spotsLeft = course.Capacity - course.EnrolledCount;
                    Console.WriteLine(String.Format("{0,-8} {1,-30} {2,-8} {3,-12} {4,-15}",
                course.CourseId,
                course.Name,
                course.Credits,
                spotsLeft,
                course.Schedule));
                }
            }
            Console.WriteLine("\n press any key to continue.");
            Console.ReadKey();
        }
        static void ViewCouyrsesBySemester()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("        COURSES BY SEMESTER              ");
            Console.WriteLine("==========================================");
            try
            {
                Console.WriteLine("Semsters: 0.Fail  1.Spring  2.Summer");
                Console.Write("Enter Semester number: ");
                Semester semester = (Semester)int.Parse(Console.ReadLine());
                List<Course> semesterCourses = service.GetCourseBySemester(semester);
                Console.WriteLine("\n--- " + semester + " Courses ---");
                if (semesterCourses.Count == 0)
                {
                    Console.WriteLine("No courses found for this semster");

                }
                else
                {
                    foreach(Course course in semesterCourses)
                    {
                        Console.WriteLine(course.CourseId + " - " + course.Name +
                                " (" + course.Credits + " credits) " + course.Schedule);
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\n Invalid Input!");
            }
            Console.WriteLine("PRESS ANY KEY TO CONTINUE..");
            Console.ReadKey();

        }

        static void SearchCourses()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("   SEARCH COURSE BY ID   ");
            Console.WriteLine("==========================================");

            try
            {
                Console.Write("Enter Course ID: ");
                int courseId = int.Parse(Console.ReadLine());

                Course course = service.GetCourse(courseId);
                if (course == null)
                {
                    Console.WriteLine("\n Course not found");
                }
                else
                {
                    Console.WriteLine("\n--- Course Details ---");
                    Console.WriteLine("ID: " + course.CourseId);
                    Console.WriteLine("Name: " + course.Name);
                    Console.WriteLine("Department: " + course.Department);
                    Console.WriteLine("Credits: " + course.Credits);
                    Console.WriteLine("Capacity: " + course.Capacity);
                    Console.WriteLine("Enrolled: " + course.EnrolledCount + "/" + course.Capacity);
                    Console.WriteLine("Schedule: " + course.Schedule);
                    Console.WriteLine("Semester: " + course.Semester);
                    Console.WriteLine("Year: " + course.Year);
                    Console.WriteLine("Status: " + (course.IsFull() ? "FULL" : "AVAILABLE"));

                    List<Student> students = service.GetCourseStudents(courseId);
                    Console.WriteLine("\n Enrolled Students : " + students.Count);
                    foreach(Student student in students)
                    {
                        Console.WriteLine("  - " + student.Name + " (ID: " + student.StudentId + ")");

                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input");
                
            }
            Console.WriteLine("\n PRESS ANY KEY TO CONTINUE");
            Console.ReadKey();
        }
        static void RemoveCourse()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("            REMOVE COURSE                ");
            Console.WriteLine("==========================================");

            try
            {
                Console.WriteLine("Enter course Id to remove: ");
                int courseId = int.Parse(Console.ReadLine());

                Course existing = service.GetCourse(courseId);
                if (existing == null)
                {
                    Console.WriteLine("\n course not found");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    service.RemoveCourse(courseId);                  
                }
                
            }
            catch (FormatException)
            {
                Console.WriteLine("INVALID input");

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("error:"+ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine("error: "+ex.Message);
            }
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            service.Load();
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine("   STUDENT COURSE REGISTRATION SYSTEM    ");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Student Management");
                Console.WriteLine("2. Course Management");
                Console.WriteLine("3. Enrollment Management");
                Console.WriteLine("4. Save and Exit");
                Console.WriteLine("5. Clear All Data");
                Console.WriteLine("==========================================");
                Console.Write("Enter your choice: ");

                string choice= Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StudentManagementMenu();
                        break;
                    case "2":
                        CourseManagementMenu();
                        break;
                    case "3":
                        EnrollmentManagementMenu();
                        break;
                    case "4":
                        service.Save();
                        Console.WriteLine("\nData saved successfully! Goodbye!");
                        Console.ReadKey();
                        running = false;
                        break;
                    case "5":
                        ClearAllData();
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice! Please try again.");
                        Console.ReadKey();
                        break;

                }
            }
        }
    }
}
