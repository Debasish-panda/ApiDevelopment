namespace ApiDevelopment.Entity
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public byte[]? Image { get; set; }
    }
}
