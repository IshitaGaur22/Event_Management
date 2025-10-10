using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface ICategoryRepository
    {
        //public int AddCategory(Category c);
        public Category GetCategoryById(int CategoryId);
        public IEnumerable<Category> GetAllCategories();
        public void UpdateCategoryDetails(Category c);

        public void DeleteCategory(int CategoryId);
    }
}
