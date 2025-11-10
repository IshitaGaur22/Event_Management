namespace Event_Management.Exceptions
{
    // Thrown when a user with the given username or ID is not found.
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found.") { }

        //public UserNotFoundException(string message) : base(message) { }

        public UserNotFoundException(string message, Exception inner) : base(message, inner) { }

        public UserNotFoundException(string username) : base($"User with username '{username}' not found.") { }
    }
}