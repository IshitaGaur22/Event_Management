using Event_Management.Models;

namespace Event_Management.Services
{
    public interface ICategoryService
    {
        //int CreateCategory(Category c);
        void UpdateCategoryDetails(Category c);
        Category GetCategoryById(int id);
        IEnumerable<Category> GetAllCategories();
        void DeleteCategory(int categoryId);
    }
}
