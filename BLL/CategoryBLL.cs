using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DAL;
using DTO;

namespace BLL
{
    public class CategoryBLL
    {
        private CategoryDAO dao = new CategoryDAO();
        public bool AddCategory(CategoryDTO model)
        {
            Category category = new Category();
            category.CategoryName = model.CategoryName;
            category.AddDate = DateTime.Now;
            category.LastUpdateDate = DateTime.Now;
            category.LastUpdateUserID = UserStatic.UserID;
            int ID = dao.AddCategory(category);
            LogDAO.AddLog(General.ProcessType.CategoryAdd, General.TableName.Category, ID);
            return true;
        }

        public List<CategoryDTO> GetCategories()
        {
            return dao.GetCategories();
        }

        public CategoryDTO GetCategoryWithID(int ID)
        {
            return dao.GetCategoryWithID(ID);
        }

        public bool UpdateCategory(CategoryDTO model)
        {
            dao.UpdateCategory(model);
            LogDAO.AddLog(General.ProcessType.CategoryUpdate, General.TableName.Category, model.ID);
            return true;
        }

        public static IEnumerable<SelectListItem> GetCategoriesForDropdown()
        {
            return CategoryDAO.GetCategoriesForDropdown();
        }

        private PostBLL postbll = new PostBLL();
        public List<PostImageDTO> DeleteCategory(int ID)
        {
            List<Post> postList = dao.DeleteCategory(ID);
            LogDAO.AddLog(General.ProcessType.CategoryDelete, General.TableName.Category, ID);
            List<PostImageDTO> imageList = new List<PostImageDTO>();
            foreach (var item in postList)
            {
                List<PostImageDTO> imageList1 = postbll.DeletePost(item.ID);
                LogDAO.AddLog(General.ProcessType.PostDelete, General.TableName.Post, item.ID);
                foreach (var item1 in imageList1)
                {
                    imageList.Add(item1);
                }
            }
            return imageList;
        }
    }
}
