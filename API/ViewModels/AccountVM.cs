namespace API.ViewModels
{
    public class AccountVM
    {
        public string? Employee_Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Dept_ID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class empDataVM
    {
        public string? NIK { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public string? DeptName { get; set; }
    }

    public class LoginVM
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
