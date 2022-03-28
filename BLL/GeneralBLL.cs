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
        public GeneralDTO GetAllPosts()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.SliderPost = dao.GetSliderPosts();
            return dto;
        }
    }
}
