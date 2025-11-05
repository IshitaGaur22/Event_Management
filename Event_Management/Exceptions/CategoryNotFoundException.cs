namespace Event_Management.Exceptions
{
    public class CategoryNotFoundException:ApplicationException
    {
        public CategoryNotFoundException(int Id)
            : base($"Category with name '{Id}' was not found.")
        {
        }
        public CategoryNotFoundException() : base("Category name doesnot exists.") { }
    }
}
