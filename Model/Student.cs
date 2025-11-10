namespace APICRUD.Model
{
    public class Student
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public  required string Email { get; set; }
        public required string Password { get; set; }
        public int? Age { get; set; }

    }
}
