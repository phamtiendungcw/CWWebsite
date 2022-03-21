using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        private PostBLL bll = new PostBLL();
        // GET: Admin/Post
        public ActionResult PostList()
        {
            List<PostDTO> postList = new List<PostDTO>();
            postList = bll.GetPosts();
            return View(postList);
        }

        public ActionResult AddPost()
        {
            PostDTO model = new PostDTO();
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPost(PostDTO model)
        {
            if (model.PostImage[0] == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                foreach (var item in model.PostImage)
                {
                    Bitmap image = new Bitmap(item.InputStream);
                    string ext = Path.GetExtension(item.FileName);
                    if (ext != ".png" && ext != ".jpeg" && ext != ".jpg")
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                        model.Categories = CategoryBLL.GetCategoriesForDropdown();
                        return View(model);
                    }
                }
                List<PostImageDTO> imageList = new List<PostImageDTO>();
                foreach (var postedFile in model.PostImage)
                {
                    Bitmap image = new Bitmap(postedFile.InputStream);
                    Bitmap resizeImage = new Bitmap(image, 750, 422);
                    string fileName = "";
                    string uniqueNumber = Guid.NewGuid().ToString();
                    fileName = uniqueNumber + postedFile.FileName;
                    resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/" + fileName));
                    PostImageDTO dto = new PostImageDTO();
                    dto.ImagePath = fileName;
                    imageList.Add(dto);
                }
                model.PostImages = imageList;
                if (bll.AddPost(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new PostDTO();
                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
                ViewBag.ProcessState = General.Messages.EmptyArea;
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            return View(model);
        }

        public ActionResult UpdatePost(int ID)
        {
            PostDTO model = new PostDTO();
            model = bll.GetPostWithID(ID);
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            model.isUpdate = true;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdatePost(PostDTO model)
        {
            IEnumerable<SelectListItem> selectList = CategoryBLL.GetCategoriesForDropdown();
            if (ModelState.IsValid)
            {
                if (model.PostImage[0] != null)
                {
                    foreach (var item in model.PostImage)
                    {
                        Bitmap image = new Bitmap(item.InputStream);
                        string ext = Path.GetExtension(item.FileName);
                        if (ext != ".png" && ext != ".jpeg" && ext != ".jpg")
                        {
                            ViewBag.ProcessState = General.Messages.ExtensionError;
                            model.Categories = CategoryBLL.GetCategoriesForDropdown();
                            return View(model);
                        }
                    }
                    List<PostImageDTO> imageList = new List<PostImageDTO>();
                    foreach (var postedFile in model.PostImage)
                    {
                        Bitmap image = new Bitmap(postedFile.InputStream);
                        Bitmap resizeImage = new Bitmap(image, 750, 422);
                        string fileName = "";
                        string uniqueNumber = Guid.NewGuid().ToString();
                        fileName = uniqueNumber + postedFile.FileName;
                        resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/" + fileName));
                        PostImageDTO dto = new PostImageDTO();
                        dto.ImagePath = fileName;
                        imageList.Add(dto);
                    }
                    model.PostImages = imageList;
                }
                if (bll.UpdatePost(model))
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
                ViewBag.ProcessState = General.Messages.EmptyArea;
            model = bll.GetPostWithID(model.ID);
            model.Categories = selectList;
            model.isUpdate = true;
            return View(model);
        }

        public JsonResult DeletePostImage(int ID)
        {
            string imagePath = bll.DeletePostImage(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + imagePath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + imagePath));
            }
            return Json("");
        }

        public JsonResult DeletePost(int ID)
        {
            List<PostImageDTO> imageList = bll.DeletePost(ID);
            foreach (var item in imageList)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath));
                }
            }
            return Json("");
        }
    }
}