namespace Domain.App.Constants
{
    public class AppRoles
    {
        public const string Administrator = "admin";
        public const string Student = "student";
        public const string Teacher = "teacher";
        public const string Owner = "owner";
        public static readonly string[] AllRoles = {Administrator, Teacher, Student, Owner};
    }
}