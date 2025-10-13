using Event_Management.Data;
using Event_Management.Exceptions;
using Event_Management.Models;

namespace Event_Management.Repository
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly Event_ManagementContext context;

        public CategoryRepository(Event_ManagementContext ctx)
        {
            context = ctx;
        }


        public Category GetCategoryById(int CategoryId) => context.Category.FirstOrDefault(t => t.CategoryID == CategoryId);

        public IEnumerable<Category> GetAllCategories() => context.Category.ToList();

        public void UpdateCategoryDetails(Category c)
        {
            var existingTicket = context.Category.Find(c.CategoryID);
            if (existingTicket == null)
                throw new TicketNotFoundException(c.CategoryID);
            existingTicket.CategoryName = c.CategoryName;
            
            context.SaveChanges();
        }

        public void DeleteCategory(int CategoryId)
        {
            var cat = context.Category.Find(CategoryId);
            if (cat != null)
            {
                context.Category.Remove(cat);
                context.SaveChanges();
            }
        }


    }
}
