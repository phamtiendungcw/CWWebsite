using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class LayoutBLL
    {
        private CategoryDAO categorydao = new CategoryDAO();
        private SocialMediaDAO socialdao = new SocialMediaDAO();
        private FavDAO favdao = new FavDAO();
        private MetaDAO metadao = new MetaDAO();
        private AddressDAO addressdao = new AddressDAO();
        private PostDAO postdao = new PostDAO();
        public HomeLayoutDTO GetLayoutData()
        {
            HomeLayoutDTO dto = new HomeLayoutDTO();
            dto.Categories = categorydao.GetCategories();
            List<SocialMediaDTO> socialMediaList = new List<SocialMediaDTO>();
            socialMediaList = socialdao.GetSocialMedias();
            dto.Facebook = socialMediaList.First(x => x.Link.Contains("facebook"));
            dto.Twitter = socialMediaList.First(x => x.Link.Contains("twitter"));
            dto.Instagram = socialMediaList.First(x => x.Link.Contains("instagram"));
            dto.Youtube = socialMediaList.First(x => x.Link.Contains("youtube"));
            dto.Linkedin = socialMediaList.First(x => x.Link.Contains("linkedin"));
            dto.FavDTO = favdao.GetFav();
            dto.MetaList = metadao.GetMetaData();
            List<AddressDTO> addressList = addressdao.GetAddress();
            dto.Address = addressList.First();
            dto.HotNews = postdao.GetHotNews();

            return dto;
        }
    }
}
