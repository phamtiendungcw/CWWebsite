﻿using System;
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
        SocialMediaDAO socialdao = new SocialMediaDAO();
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
            return dto;
        }
    }
}