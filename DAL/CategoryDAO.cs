using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class CategoryDAO : PostContext
    {
        public int AddCategory(Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return category.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<CategoryDTO> GetCategories()
        {
            List<Category> list = db.Categories.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate)
                .ToList();
            List<CategoryDTO> dtoList = new List<CategoryDTO>();
            foreach (var item in list)
            {
                CategoryDTO dto = new CategoryDTO();
                dto.CategoryName = item.CategoryName;
                dto.ID = item.ID;
                dtoList.Add(dto);
            }
            return dtoList;
        }

        public CategoryDTO GetCategoryWithID(int ID)
        {
            try
            {
                Category category = db.Categories.First(x => x.ID == ID);
                CategoryDTO dto = new CategoryDTO();
                dto.ID = category.ID;
                dto.CategoryName = category.CategoryName;
                return dto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateCategory(CategoryDTO model)
        {
            try
            {
                Category category = db.Categories.First(x => x.ID == model.ID);
                category.CategoryName = model.CategoryName;
                category.LastUpdateDate = DateTime.Now;
                category.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
