using Resource = Resources.FrontEnd;


namespace PublicAPI.DTO.v1
{
    public class LangResources
    {
        public FrontEnd FrontEnd { get; set; } = new FrontEnd();
    }

    public class FrontEnd
    {
        public Components Components { get; set; } = new Components();
        public Containers Containers { get; set; } = new Containers();
    }

    public class Components
    {
        public Alert Alert { get; set; } = new Alert();
        public Footer Footer { get; set; } = new Footer();
        public Header Header { get; set; } = new Header();
        public Loader Loader { get; set; } = new Loader();
    }

    public class Containers
    {
        public Home Home { get; set; } = new Home();

        public Identity Identity { get; set; } = new Identity();

        public Student Student { get; set; } = new Student();
        
        public Teacher Teacher { get; set; } = new Teacher();

        public Page404 Page404 { get; set; } = new Page404();

    }
    
    public class Home
    {
        public HomeIndex HomeIndex { get; set; } = new HomeIndex();
    }


    
    public class HomeIndex
    {
        public string Description { get; set; } = Resource.Containers.Home.HomeIndex.Description;
        public string Invites { get; set; } = Resource.Containers.Home.HomeIndex.Invites;
        public string Courses { get; set; } = Resource.Containers.Home.HomeIndex.Courses;
        public string Student { get; set; } = Resource.Containers.Home.HomeIndex.Student;
        public string Teacher { get; set; } = Resource.Containers.Home.HomeIndex.Teacher;
        public string Title { get; set; } = Resource.Containers.Home.HomeIndex.Title;
    }

    public class Identity
    {
        public Login Login { get; set; } = new Login();
        public Register Register { get; set; } = new Register();
    }

    public class Login
    {
        public string EmptyInputs { get; set; } = Resource.Containers.Identity.Login.EmptyInputs;
        public string LogIn { get; set; } = Resource.Containers.Identity.Login.LogIn;
        public string Password { get; set; } = Resource.Containers.Identity.Login.Password;
        public string Username { get; set; } = Resource.Containers.Identity.Login.Username;
    } 
    public class Register
    {
        public string Username { get; set; } = Resource.Containers.Identity.Register.Username;
        public string Email { get; set; } = Resource.Containers.Identity.Register.Email;
        public string EmptyInputs { get; set; } = Resource.Containers.Identity.Register.EmptyInputs;
        public string Firstname { get; set; } = Resource.Containers.Identity.Register.Firstname;
        public string Lastname { get; set; } = Resource.Containers.Identity.Register.Lastname;
        public string Password { get; set; } = Resource.Containers.Identity.Register.Password;
        public string PasswordConfirmation { get; set; } = Resource.Containers.Identity.Register.PasswordConfirmation;
        public string PasswordDoesntMatch { get; set; } = Resource.Containers.Identity.Register.PasswordDoesntMatch;
        public string RegisterButton { get; set; } = Resource.Containers.Identity.Register.RegisterButton;
        public string UnableToLogIn { get; set; } = Resource.Containers.Identity.Register.UnableToLogIn;
    }

    
    public class Student
    {
        public Course Course { get; set; } = new Course();
        public StudentCourses StudentCourses { get; set; } = new StudentCourses();
    }
    
    public class Course
    {
        public CourseIndex CourseIndex { get; set; } = new CourseIndex();
        public CourseSchedule CourseSchedule { get; set; } = new CourseSchedule();
    }
    
    public class StudentCourses
    {
        public string Category { get; set; } = Resource.Containers.Student.StudentCourses.Category;
        public string Courses { get; set; } = Resource.Containers.Student.StudentCourses.Courses;
    }
    
    public class CourseIndex
    {
        public string Complete { get; set; } = Resource.Containers.Student.Course.CourseIndex.Complete;
        public string DrivingLessons { get; set; } = Resource.Containers.Student.Course.CourseIndex.DrivingLessons;
        public string Progress { get; set; } = Resource.Containers.Student.Course.CourseIndex.Progress;
    }
    public class Page404
    {
        public string NotFound { get; set; } = Resource.Containers.Page404.NotFound;
    }
    
    public class CourseSchedule
    {
        public string Complete { get; set; } = Resource.Containers.Student.Course.CourseSchedule.Complete;
        public string BackToOverview { get; set; } = Resource.Containers.Student.Course.CourseSchedule.BackToOverview;
        public string Day { get; set; } = Resource.Containers.Student.Course.CourseSchedule.Day;
        public string DrivingLessons { get; set; } = Resource.Containers.Student.Course.CourseSchedule.DrivingLessons;
        public string Month { get; set; } = Resource.Containers.Student.Course.CourseSchedule.Month;
        public string Progress { get; set; } = Resource.Containers.Student.Course.CourseSchedule.Progress;
        public string Teacher { get; set; } = Resource.Containers.Student.Course.CourseSchedule.Teacher;
        public string Time { get; set; } = Resource.Containers.Student.Course.CourseSchedule.Time;
        public string Year { get; set; } = Resource.Containers.Student.Course.CourseSchedule.Year;
    }
    
    public class Alert
    {
    }
    
    public class Footer
    {
        public string AppName { get; set; } = Resource.Components.Footer.AppName;
    }
    
    public class Header
    {
        public string Brand { get; set; } = Resource.Components.Header.Brand;
        public string Courses { get; set; } = Resource.Components.Header.Courses;
        public string LogIn { get; set; } = Resource.Components.Header.LogIn;
        public string LogOut { get; set; } = Resource.Components.Header.LogOut;
        public string Overview { get; set; } = Resource.Components.Header.Overview;
        public string Register { get; set; } = Resource.Components.Header.Register;
        public string Schedule { get; set; } = Resource.Components.Header.Schedule;
        public string Students { get; set; } = Resource.Components.Header.Students;
    }
    
    public class Loader
    {
        public string Error { get; set; } = Resource.Components.Loader.Error;
        public string Loading { get; set; } = Resource.Components.Loader.Loading;
    }
    
    public class Teacher
    {
        public TeacherOverview TeacherOverview { get; set; } = new TeacherOverview();
        public TeacherSchedule TeacherSchedule { get; set; } = new TeacherSchedule();
        public Students Students { get; set; } = new Students();
    }
    
    public class Students
    {
        public StudentsIndex StudentsIndex { get; set; } = new StudentsIndex();
        public TeacherStudentCourse TeacherStudentCourse { get; set; } = new TeacherStudentCourse();
        public TeacherStudentCourses TeacherStudentCourses { get; set; } = new TeacherStudentCourses();
        public TeacherStudentCourseSchedule TeacherStudentCourseSchedule { get; set; } = new TeacherStudentCourseSchedule();

    }
    
    public class StudentsIndex
    {
        public string Email { get; set; } = Resource.Containers.Teacher.Students.StudentsIndex.Email;
        public string Filter { get; set; } = Resource.Containers.Teacher.Students.StudentsIndex.Filter;
        public string Invite { get; set; } = Resource.Containers.Teacher.Students.StudentsIndex.Invite;
        public string Name { get; set; } = Resource.Containers.Teacher.Students.StudentsIndex.Name;
        public string SchoolStudents { get; set; } = Resource.Containers.Teacher.Students.StudentsIndex.SchoolStudents;
        public string Username { get; set; } = Resource.Containers.Teacher.Students.StudentsIndex.Username;
    }
    
    public class TeacherStudentCourse
    {
        public string Complete { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourse.Complete;
        public string DrivingLessons { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourse.DrivingLessons;
        public string Delete { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourse.Delete;
        public string Progress { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourse.Progress;
        public string ReportUpdated { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourse.ReportUpdated;
        public string Update { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourse.Update;

    }

    public class TeacherStudentCourses
    {
        public string Category { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourses.Category;
        public string Courses { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourses.Courses;
        public string Kick { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourses.Kick;
        public string AddCourse { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourses.AddCourse;

    }
    
    public class TeacherStudentCourseSchedule
    {
        public string Add { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.Add;
        public string AddLesson { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.AddLesson;
        public string BackToStudentCourse { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.BackToStudentCourse;
        public string Complete { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.Complete;
        public string Day { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.Day;
        public string DrivingLessons { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.DrivingLessons;
        public string LessonLength { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.LessonLength;
        public string LessonLengthError { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.LessonLengthError;
        public string LessonRemoved { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.LessonRemoved;
        public string Month { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.Month;
        public string NextLesson { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.NextLesson;
        public string Progress { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.Progress;
        public string Remove { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.Remove;
        public string Student { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.Student;
        public string StudentLessons { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.StudentLessons;
        public string Teacher { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.Teacher;
        public string LessonAdded { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.LessonAdded;
        public string TeacherLessons { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.TeacherLessons;
        public string Time { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.Time;
        public string Year { get; set; } = Resource.Containers.Teacher.Students.TeacherStudentCourseSchedule.Year;

    }


    public class TeacherOverview
    {
        public string Course { get; set; } = Resource.Containers.Teacher.TeacherOveview.Course;
        public string DailyLessons { get; set; } = Resource.Containers.Teacher.TeacherOveview.DailyLessons;
        public string NextLesson { get; set; } = Resource.Containers.Teacher.TeacherOveview.NextLesson;
        public string RestOfTheDay { get; set; } = Resource.Containers.Teacher.TeacherOveview.RestOfTheDay;
        public string Student { get; set; } = Resource.Containers.Teacher.TeacherOveview.Student;
        public string Time { get; set; } = Resource.Containers.Teacher.TeacherOveview.Time;

    }
    
    public class TeacherSchedule
    {
        public string LessonRemoved { get; set; } = Resource.Containers.Teacher.TeacherSchedule.LessonRemoved;
        public string Lessons { get; set; } = Resource.Containers.Teacher.TeacherSchedule.Lessons;
        public string Remove { get; set; } = Resource.Containers.Teacher.TeacherSchedule.Remove;
        public string RemovedStudent { get; set; } = Resource.Containers.Teacher.TeacherSchedule.RemovedStudent;
        public string Student { get; set; } = Resource.Containers.Teacher.TeacherSchedule.Student;
        public string Time { get; set; } = Resource.Containers.Teacher.TeacherSchedule.Time;
    }
    
}