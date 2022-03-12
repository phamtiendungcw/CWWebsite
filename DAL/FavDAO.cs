using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FavDAO : PostContext
    {
        public FavDTO GetFav()
        {
            FavLogoTitle fav = db.FavLogoTitles.First();
            FavDTO dto = new FavDTO();
            dto.ID = fav.ID;
            dto.Title = fav.Title;
            dto.Fav = fav.Fav;
            dto.Logo = fav.Logo;
            return dto;
        }

        public FavDTO UpdateFav(FavDTO model)
        {
            try
            {
                FavLogoTitle fav = db.FavLogoTitles.First();
                FavDTO dto = new FavDTO();
                dto.ID = fav.ID;
                dto.Title = fav.Title;
                dto.Fav = fav.Fav;
                dto.Logo = fav.Logo;
                if (model.Logo != null)
                    fav.Logo = model.Logo;
                if (model.Fav != null)
                    fav.Fav = model.Fav;
                db.SaveChanges();
                return dto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
