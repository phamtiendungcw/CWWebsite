using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class MetaController : Controller
    {
        // GET: Admin/Meta
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddMeta()
        {
            MetaDTO dto = new MetaDTO();
            return View(dto);
        }
    }
}