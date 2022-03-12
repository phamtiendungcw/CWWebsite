using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
