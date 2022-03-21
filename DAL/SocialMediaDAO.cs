using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SocialMediaDAO : PostContext
    {
        public int AddSocialMedia(SocialMedia social)
        {
            try
            {
                db.SocialMedias.Add(social);
                db.SaveChanges();
                return social.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<SocialMediaDTO> GetSocialMedias()
        {
            List<SocialMedia> list = db.SocialMedias.Where(x => x.isDeleted == false).ToList();
            List<SocialMediaDTO> dtoList = new List<SocialMediaDTO>();
            foreach (var item in list)
            {
                SocialMediaDTO dto = new SocialMediaDTO();
                dto.Name = item.Name;
                dto.Link = item.Link;
                dto.ImagePath = item.ImagePath;
                dto.ID = item.ID;
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public string DeleteSocialMedia(int ID)
        {
            try
            {
                SocialMedia social = db.SocialMedias.First(x => x.ID == ID);
                string imagePath = social.ImagePath;
                social.isDeleted = true;
                social.DeletedDate = DateTime.Now;
                social.LastUpdateDate = DateTime.Now;
                social.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return imagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public SocialMediaDTO GetSocialMediaWithID(int ID)
        {
            SocialMedia socialMedia = db.SocialMedias.First(x => x.ID == ID);
            SocialMediaDTO dto = new SocialMediaDTO();
            dto.ID = socialMedia.ID;
            dto.Name = socialMedia.Name;
            dto.Link = socialMedia.Link;
            dto.ImagePath = socialMedia.ImagePath;
            return dto;
        }

        public string UpdateSocialMedia(SocialMediaDTO model)
        {
            try
            {
                SocialMedia social = db.SocialMedias.First(x => x.ID == model.ID);
                string oldImagePath = social.ImagePath;
                social.Name = model.Name;
                social.Link = model.Link;
                if (model.ImagePath != null)
                    social.ImagePath = model.ImagePath;
                social.LastUpdateDate = DateTime.Now;
                social.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return oldImagePath;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
