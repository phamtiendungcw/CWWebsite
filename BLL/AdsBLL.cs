using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class AdsBLL
    {
        private AdsDAO dao = new AdsDAO();
        public void AddAds(AdsDTO model)
        {
            Ad ads = new Ad();
            ads.Name = model.Name;
            ads.Link = model.Link;
            ads.ImagePath = model.ImagePath;
            ads.Size = model.ImageSize;
            ads.AddDate = DateTime.Now;
            ads.LastUpdateUserID = UserStatic.UserID;
            ads.LastUpdateDate = DateTime.Now;
            int ID = dao.AddAds(ads);
            LogDAO.AddLog(General.ProcessType.AdsAdd, General.TableName.Ads, ID);
        }

        public List<AdsDTO> GetAds()
        {
            return dao.GetAds();
        }

        public AdsDTO GetAdsWithID(int ID)
        {
            return dao.GetAdsWithID(ID);
        }

        public string UpdateAds(AdsDTO model)
        {
            string oldImagePath = dao.UpdateAds(model);
            LogDAO.AddLog(General.ProcessType.AdsUpdate, General.TableName.Ads, model.ID);
            return oldImagePath;
        }

        public string DeleteAds(int ID)
        {
            string imagePath = dao.DeleteAds(ID);
            LogDAO.AddLog(General.ProcessType.AdsDelete, General.TableName.Ads, ID);
            return imagePath;
        }
    }
}
