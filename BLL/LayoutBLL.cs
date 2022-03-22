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
        public HomeLayoutDTO GetLayoutData()
        {
            HomeLayoutDTO dto = new HomeLayoutDTO();
            dto.Categories = categorydao.GetCategories();
            return dto;
        }
    }
}
