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
    public class SocialMediaController : BaseController
    {
        private SocialMediaBLL bll = new SocialMediaBLL();

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

        public ActionResult SocialMediaList()
        {
            List<SocialMediaDTO> dtoList = new List<SocialMediaDTO>();
            dtoList = bll.GetSocialMedias();
            return View(dtoList);
        }

        public ActionResult UpdateSocialMedia(int ID)
        {
            SocialMediaDTO dto = bll.GetSocialMediaWithID(ID);
            return View(dto);
        }
        [HttpPost]
        public ActionResult UpdateSocialMedia(SocialMediaDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.SocialImage != null)
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
                    }
                }
                string oldImagePath = bll.UpdateSocialMedia(model);
                if (model.SocialImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + oldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + oldImagePath));
                    }
                }
                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }
            return View(model);
        }

        public JsonResult DeleteSocialMedia(int ID)
        {
            string imagePath = bll.DeleteSocialMedia(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + imagePath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + imagePath));
            }
            return Json("");
        }
    }
}