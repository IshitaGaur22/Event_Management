using Event_Management.Exceptions;
using Event_Management.Models;
using Event_Management.Repository;

namespace Event_Management.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository repository;

        public CategoryService(ICategoryRepository repo)
        {
            repository = repo;
        }
        //public int CreateCategory(Category c)
        //{
        //    if (repository.GetCategoryById(c.CategoryID) != null)
        //        throw new CategoryAlreadyExistsException(c.CategoryID);

        //    try
        //    {
        //        return repository.AddCategory(c);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new CategoryCreationException(ex.Message);
        //    }

        //}
        public void UpdateCategoryDetails(Category c)
        {
            if (repository.GetCategoryById(c.CategoryID) == null)
                throw new CategoryNotFoundException(c.CategoryID);
            try
            {
                repository.UpdateCategoryDetails(c);
            }
            catch
            {
                throw new CategoryUpdateException(c.CategoryID);
            }
        }

        public Category GetCategoryById(int id)
        {
            var cat = repository.GetCategoryById(id);
            if (cat == null)
                throw new CategoryNotFoundException(id);
            return cat;
        }
        public IEnumerable<Category> GetAllCategories() => repository.GetAllCategories();
        public void DeleteCategory(int categoryId)
        {
            if (repository.GetCategoryById(categoryId) == null)
                throw new CategoryNotFoundException(categoryId);
            try
            {
                repository.DeleteCategory(categoryId);
            }
            catch 
            {
                throw new CategoryDeletionException(categoryId);
            }
        }
    }
}
