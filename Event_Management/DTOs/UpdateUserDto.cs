namespace Event_Management.DTOs
{
    public class UpdateUserDto
    {
        public string? UserName { get; set; }
        public long? PhoneNumber { get; set; } // made nullable to detect omitted value
        public string? Location { get; set; }
    }
}