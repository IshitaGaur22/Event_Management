namespace Event_Management.Exceptions
{
    public class CategoryUpdateException:ApplicationException
    {
        public CategoryUpdateException(int ID) : base($"Category with ID {ID} could not be updated.") { }
    }
}
