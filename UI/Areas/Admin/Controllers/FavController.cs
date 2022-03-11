using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class FavController : Controller
    {
        // GET: Admin/Fav
        private FavBLL bll = new FavBLL();
        public ActionResult UpdateFav()
        {
            FavDTO dto = new FavDTO();
            dto = bll.GetFav();
            return View(dto);
        }

        [HttpPost]
        public ActionResult UpdateFav(FavDTO model)
        {
            if (!ModelState.IsValid)
                ViewBag.ProcessState = General.Messages.EmptyArea;
            else
            {
                if (model.FavImage != null)
                {
                    string favName = "";
                    HttpPostedFileBase postedFileFav = model.FavImage;
                    Bitmap FavImage = new Bitmap(postedFileFav.InputStream);
                    Bitmap resizeFavImage = new Bitmap(FavImage, 100, 100);
                    string ext = Path.GetExtension(postedFileFav.FileName);
                    if (ext == ".ico" || ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    {
                        string favUinique = Guid.NewGuid().ToString();
                        favName = favUinique + postedFileFav.FileName;
                        resizeFavImage.Save(Server.MapPath("~/Areas/Admin/Content/FavImage/" + favName));
                        model.Fav = favName;
                    }
                    else
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                }

                if (model.LogoImage != null)
                {
                    string logoName = "";
                    HttpPostedFileBase postedFileLogo = model.FavImage;
                    Bitmap LogoImage = new Bitmap(postedFileLogo.InputStream);
                    Bitmap resizeLogoImage = new Bitmap(LogoImage, 100, 100);
                    string ext = Path.GetExtension(postedFileLogo.FileName);
                    if (ext == ".ico" || ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    {
                        string logoUinique = Guid.NewGuid().ToString();
                        logoName = logoUinique + postedFileLogo.FileName;
                        resizeLogoImage.Save(Server.MapPath("~/Areas/Admin/Content/FavImage/" + logoName));
                        model.Logo = logoName;
                    }
                    else
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                }
                FavDTO returndto = new FavDTO();
                returndto = bll.UpdateFav(model);
                if (model.FavImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Fav)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Fav));
                    }
                }
                if (model.LogoImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Logo)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Logo));
                    }
                }
                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }
            return View(model);
        }
    }
}