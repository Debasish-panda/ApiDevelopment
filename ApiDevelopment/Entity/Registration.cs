namespace ApiDevelopment.Entity
{
    public class Registration
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public long Phone { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
