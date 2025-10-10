namespace Event_Management.Exceptions
{
    public class CategoryDeletionException: ApplicationException
    {
        public CategoryDeletionException() { }
        public CategoryDeletionException(int ID) : base($"Category with ID {ID} could not be deleted.") { }

    }
}
