using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class LogDAO : PostContext
    {
        public static void AddLog(int ProcessType, string TableName, int ProcessID)
        {
            Log_Table log = new Log_Table();
            log.UserID = UserStatic.UserID;
            log.ProcessType= ProcessType;
            log.ProcessID = ProcessID;
            log.ProcessCategoryType = TableName;
            log.ProcessDate = DateTime.Now;
            log.IPAddress = HttpContext.Current.Request.UserHostAddress;
            db.Log_Table.Add(log);
            db.SaveChanges();

        }
    }
}
