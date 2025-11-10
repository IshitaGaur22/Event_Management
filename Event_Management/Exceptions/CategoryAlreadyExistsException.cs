namespace Event_Management.Exceptions
{
    public class CategoryAlreadyExistsException:ApplicationException
    {
        public CategoryAlreadyExistsException(string catName) : base($"Category with Category Name {catName} already exists.") { }
        public CategoryAlreadyExistsException() : base("Category already exists.") { }

    }
}
