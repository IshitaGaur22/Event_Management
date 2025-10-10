namespace Event_Management.Exceptions
{
    public class CategoryAlreadyExistsException:ApplicationException
    {
        public CategoryAlreadyExistsException(int ID) : base($"Category with ID {ID} already exists.") { }

    }
}
