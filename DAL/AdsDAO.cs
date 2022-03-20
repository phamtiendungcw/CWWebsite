using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class AdsDAO : PostContext
    {
        public int AddAds(Ad ads)
        {
            try
            {
                db.Ads.Add(ads);
                db.SaveChanges();
                return ads.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<AdsDTO> GetAds()
        {
            List<Ad> list = db.Ads.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
            List<AdsDTO> dtoList = new List<AdsDTO>();
            foreach (var item in list)
            {
                AdsDTO dto = new AdsDTO();
                dto.ID = item.ID;
                dto.Name = item.Name;
                dto.Link = item.Link;
                dto.ImagePath = item.ImagePath;
                dtoList.Add(dto);
            }
            return dtoList;
        }

        public AdsDTO GetADsWithID(int ID)
        {
            Ad ads =db.Ads.First(x => x.ID == ID);
            AdsDTO dto = new AdsDTO();
            dto.ID = ads.ID;
            dto.Name = ads.Name;
            dto.Link = ads.Link;
            dto.ImagePath = ads.ImagePath;
            dto.ImageSize = ads.Size;
            return dto;
        }

        public string DeleteAds(int ID)
        {
            try
            {
                Ad ads = db.Ads.First(x => x.ID == ID);
                string imagePath = ads.ImagePath;
                ads.isDeleted = true;
                ads.DeletedDate = DateTime.Now;
                ads.LastUpdateDate = DateTime.Now;
                ads.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return imagePath;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string UpdateAds(AdsDTO model)
        {
            try
            {
                Ad ads = db.Ads.First(x => x.ID == model.ID);
                string oldImagePath = ads.ImagePath;
                ads.Name = model.Name;
                ads.Link = model.Link;
                if (model.ImagePath != null)
                    ads.ImagePath = model.ImagePath;
                ads.Size = model.ImageSize;
                ads.LastUpdateDate = DateTime.Now;
                ads.LastUpdateUserID = UserStatic.UserID;
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
