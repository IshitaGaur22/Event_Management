namespace Event_Management.Exceptions
{
    public class CategoryCreationException:ApplicationException
    {
        public CategoryCreationException() { }
        public CategoryCreationException(string message):base($"Error occurred while creating category: {message}") { }
    }
}
