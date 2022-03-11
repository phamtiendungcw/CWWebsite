using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class FavBLL
    {
        private FavDAO dao = new FavDAO();
        public FavDTO GetFav()
        {
            return dao.GetFav();
        }

        public FavDTO UpdateFav(FavDTO model)
        {
            FavDTO dto = new FavDTO();
            dto = dao.UpdateFav(model);
            LogDAO.AddLog(General.ProcessType.IconUpdate, General.TableName.Icon, dto.ID);
            return dto;
        }
    }
}
