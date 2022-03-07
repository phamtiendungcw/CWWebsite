using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class SocialMediaController : Controller
    {
        SocialMediaBLL bll = new SocialMediaBLL();
        // GET: Admin/AddSocialMedia
        public ActionResult AddSocialMedia()
        {
            SocialMediaDTO model = new SocialMediaDTO();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddSocialMedia(SocialMediaDTO model)
        {
            if (model.SocialImage == null)
                ViewBag.ProcessState = General.Messages.ImageMissing;
            else if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = model.SocialImage;
                Bitmap socialMedia = new Bitmap(postedFile.InputStream);
                var ext = Path.GetExtension(postedFile.FileName);
                var fileName = "";
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    var uniqueNumber = Guid.NewGuid().ToString();
                    fileName = uniqueNumber + postedFile.FileName;
                    socialMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + fileName));
                    model.ImagePath = fileName;
                    if (bll.AddSocialMedia(model))
                    {
                        ViewBag.ProcessState = General.Messages.AddSuccess;
                        model = new SocialMediaDTO();
                        ModelState.Clear();
                    }
                    else
                        ViewBag.ProcessState = General.Messages.GeneralError;
                }
                else
                    ViewBag.ProcessState = General.Messages.ExtensionError;
            }
            else
                ViewBag.ProcessState = General.Messages.EmptyArea;
            return View(model);
        }
    }
}