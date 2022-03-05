using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MetaDAO : PostContext
    {
        public int AddMeta(Meta meta)
        {
            try
            {
                db.Metas.Add(meta);
                db.SaveChanges();
                return meta.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<MetaDTO> GetMetaData()
        {
            List<MetaDTO> metalist = new List<MetaDTO>();
            List<Meta> list = db.Metas.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
            foreach (var item in list)
            {
                MetaDTO dto = new MetaDTO();
                dto.MetaID = item.ID;
                dto.Name = item.Name;
                dto.MetaContent = item.MetaContent;
                metalist.Add(dto);
            }
            return metalist;
        }
    }
}
