using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private UserBLL userbll = new UserBLL();
        // GET: Admin/Login
        public ActionResult Index()
        {
            UserDTO dto = new UserDTO();
            return View(dto);
        }

        [HttpPost]
        public ActionResult Index(UserDTO model)
        {
            if (model.Username != null && model.Password != null)
            {
                UserDTO user = userbll.GetUserWithUsernameAndPassword(model);
                if (user.ID != 0)
                {
                    UserStatic.UserID = user.ID;
                    UserStatic.IsAdmin = user.IsAdmin;
                    UserStatic.NameSurname = user.Name;
                    UserStatic.ImagePath = user.ImagePath;
                    LogBLL.AddLog(General.ProcessType.Login, General.TableName.Login, 12);
                    return RedirectToAction("Index", "Post");
                }
                else
                    return View(model);
            }
            else
                return View(model);
        }
    }
}