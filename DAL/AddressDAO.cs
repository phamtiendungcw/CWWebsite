using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class AddressDAO : PostContext
    {
        public int AddAddress(Address ads)
        {
            try
            {
                db.Addresses.Add(ads);
                db.SaveChanges();
                return ads.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<AddressDTO> GetAddress()
        {
            List<Address> list = db.Addresses.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
            List<AddressDTO> dtoList = new List<AddressDTO>();
            foreach (var item in list)
            {
                AddressDTO dto = new AddressDTO();
                dto.ID = item.ID;
                dto.AddressContent = item.Address1;
                dto.Email = item.Email;
                dto.Fax = item.Fax;
                dto.LargeMapPath = item.MapPathLarge;
                dto.SmallMapPath = item.MapPathSmall;
                dto.Phone = item.Phone;
                dto.Phone2 = item.Phone2;
                dtoList.Add(dto);
            }
            return dtoList;
        }
    }
}
