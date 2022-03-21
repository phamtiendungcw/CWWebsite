using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Models.Attributes
{
    public class LoginControl : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserStatic.UserID == 0)
            {
                filterContext.HttpContext.Response.Redirect("/Admin/Login/Index");
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}