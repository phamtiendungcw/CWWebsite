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
    }
}
