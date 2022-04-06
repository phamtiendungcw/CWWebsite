using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GeneralBLL
    {
        private GeneralDAO dao = new GeneralDAO();
        private AdsDAO adsdao = new AdsDAO();
        private CategoryDAO categorydao = new CategoryDAO();
        private AddressDAO addressdao = new AddressDAO();
        public GeneralDTO GetAllPosts()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.SliderPost = dao.GetSliderPosts();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.PopularPost = dao.GetPopularPosts();
            dto.MostViewedPost = dao.GetMostViewedPosts();
            dto.Videos = dao.GetVideos();
            dto.AdsList = adsdao.GetAds();
            return dto;
        }

        public GeneralDTO GetPostDetailPageItemsWithID(int ID)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.AdsList = adsdao.GetAds();
            dto.PostDetail = dao.GetPostDetail(ID);
            return dto;
        }

        public GeneralDTO GetCategoryPostList(string categoryName)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.AdsList = adsdao.GetAds();
            if (categoryName == "video")
            {
                dto.Videos = dao.GetAllVideos();
                dto.CategoryName = "Video";
            }
            else
            {
                List<CategoryDTO> categoryList = categorydao.GetCategories();
                int categoryID = 0;
                foreach (var item in categoryList)
                {
                    if (categoryName == SeoLink.GenerateUrl(item.CategoryName))
                    {
                        categoryID = item.ID;
                        dto.CategoryName = item.CategoryName;
                        break;
                    }
                }
                dto.CategoryPostList = dao.GetCategoryPostList(categoryID);
            }
            return dto;
        }

        public GeneralDTO GetContactPageItems()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.AdsList = adsdao.GetAds();
            dto.Address = addressdao.GetAddresses().First();
            return dto;
        }
    }
}
