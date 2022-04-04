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
        public GeneralDTO GetAllPosts()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.SliderPost = dao.GetSliderPosts();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.PopularPost = dao.GetPopularPosts();
            dto.MostViewedPost = dao.GetMostViewedPosts();
            dto.Videos = dao.GetAllVideos();
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
    }
}
