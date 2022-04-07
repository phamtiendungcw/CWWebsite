using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DTO;

namespace DAL
{
    public class PostDAO
    {
        public int AddPost(Post post)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.Posts.Add(post);
                    db.SaveChanges();
                }

                return post.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int AddImage(PostImage item)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.PostImages.Add(item);
                    db.SaveChanges();
                }

                return item.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int AddTag(PostTag item)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                db.PostTags.Add(item);
                db.SaveChanges();
            }

            return item.ID;
        }

        public List<PostDTO> GetPosts()
        {
            List<PostDTO> dtoList = new List<PostDTO>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                var postList = (from p in db.Posts.Where(x => x.isDeleted == false)
                                join c in db.Categories on p.CategoryID equals c.ID
                                select new
                                {
                                    ID = p.ID,
                                    Title = p.Title,
                                    categoryName = c.CategoryName,
                                    AddDate = p.AddDate
                                }).OrderByDescending(x => x.AddDate).ToList();
                foreach (var item in postList)
                {
                    PostDTO dto = new PostDTO();
                    dto.Title = item.Title;
                    dto.ID = item.ID;
                    dto.CategoryName = item.categoryName;
                    dto.AddDate = item.AddDate;
                    dtoList.Add(dto);
                }
            }

            return dtoList;
        }

        public List<PostDTO> GetHotNews()
        {
            List<PostDTO> dtoList = new List<PostDTO>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                var postList = (from p in db.Posts.Where(x => x.isDeleted == false && x.Area1 == true)
                                join c in db.Categories on p.CategoryID equals c.ID
                                select new
                                {
                                    ID = p.ID,
                                    Title = p.Title,
                                    categoryName = c.CategoryName,
                                    AddDate = p.AddDate,
                                    seolink = p.SeoLink
                                }).OrderByDescending(x => x.AddDate).Take(8).ToList();
                foreach (var item in postList)
                {
                    PostDTO dto = new PostDTO();
                    dto.Title = item.Title;
                    dto.ID = item.ID;
                    dto.CategoryName = item.categoryName;
                    dto.AddDate = item.AddDate;
                    dto.SeoLink = item.seolink;
                    dtoList.Add(dto);
                }
            }

            return dtoList;
        }

        public PostDTO GetPostWithID(int ID)
        {
            PostDTO dto = new PostDTO();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Post post = db.Posts.First(x => x.ID == ID);
                dto.ID = post.ID;
                dto.Title = post.Title;
                dto.ShortContent = post.ShortContent;
                dto.PostContent = post.PostContent;
                dto.Language = post.LanguageName;
                dto.Notification = post.Notification;
                dto.SeoLink = post.SeoLink;
                dto.Slider = post.Slider;
                dto.Area1 = post.Area1;
                dto.Area2 = post.Area2;
                dto.Area3 = post.Area3;
                dto.CategoryID = post.CategoryID;
            }

            return dto;
        }

        public List<PostImageDTO> GetPostImageWithPostID(int postID)
        {
            List<PostImageDTO> dtoList = new List<PostImageDTO>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostImage> list = db.PostImages.Where(x => x.isDeleted == false && x.PostID == postID).ToList();
                foreach (var item in list)
                {
                    PostImageDTO dto = new PostImageDTO();
                    dto.ID = item.ID;
                    dto.ImagePath = item.ImagePath;
                    dtoList.Add(dto);
                }
            }

            return dtoList;
        }

        public List<PostTag> GetPostTagWithPostID(int postID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                return db.PostTags.Where(x => x.isDeleted == false && x.PostID == postID).ToList();
            }
        }

        public void UpdatePost(PostDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Post post = db.Posts.First(x => x.ID == model.ID);
                post.Title = model.Title;
                post.Area1 = model.Area1;
                post.Area2 = model.Area2;
                post.Area3 = model.Area3;
                post.CategoryID = model.CategoryID;
                post.LanguageName = model.Language;
                post.LastUpdateDate = DateTime.Now;
                post.LastUpdateUserID = UserStatic.UserID;
                post.Notification = model.Notification;
                post.PostContent = model.PostContent;
                post.SeoLink = model.SeoLink;
                post.ShortContent = model.ShortContent;
                post.Slider = model.Slider;
                db.SaveChanges();
            }
        }

        public void DeleteTags(int postID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostTag> list = db.PostTags.Where(x => x.isDeleted == false && x.PostID == postID).ToList();
                foreach (var item in list)
                {
                    item.isDeleted = true;
                    item.DeletedDate = DateTime.Now;
                    item.LastUpdateDate = DateTime.Now;
                    item.LastUpdateUserID = UserStatic.UserID;
                }
                db.SaveChanges();
            }
        }

        public string DeletePostImage(int ID)
        {
            try
            {
                string imagePath = "";
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    PostImage img = db.PostImages.First(x => x.ID == ID);
                    imagePath = img.ImagePath;
                    img.isDeleted = true;
                    img.DeletedDate = DateTime.Now;
                    img.LastUpdate = DateTime.Now;
                    img.LastUpdateUserID = UserStatic.UserID;
                    db.SaveChanges();
                }
                return imagePath;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<PostImageDTO> DeletePost(int ID)
        {
            List<PostImageDTO> dtoList = new List<PostImageDTO>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Post post = db.Posts.First(x => x.ID == ID);
                post.isDeleted = true;
                post.DeleteDate = DateTime.Now;
                post.LastUpdateDate = DateTime.Now;
                post.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                List<PostImage> imageList = db.PostImages.Where(x => x.PostID == ID).ToList();
                foreach (var item in imageList)
                {
                    PostImageDTO dto = new PostImageDTO();
                    dto.ImagePath = item.ImagePath;
                    item.isDeleted = true;
                    item.DeletedDate = DateTime.Now;
                    item.LastUpdate = DateTime.Now;
                    item.LastUpdateUserID = UserStatic.UserID;
                    dtoList.Add(dto);
                }
                db.SaveChanges();
            }

            return dtoList;
        }

        public void AddComment(Comment comment)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<CommentDTO> GetComments()
        {
            List<CommentDTO> dtolist = new List<CommentDTO>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                var list = (from c in db.Comments.Where(x => x.isDeleted == false && x.isApproved == false)
                            join p in db.Posts on c.PostID equals p.ID
                            select new
                            {
                                ID = c.ID,
                                PostTitle = p.Title,
                                Email = c.Email,
                                Content = c.CommentContent,
                                AddDate = c.AddDate
                            }).OrderBy(x => x.AddDate).ToList();
                foreach (var item in list)
                {
                    CommentDTO dto = new CommentDTO();
                    dto.ID = item.ID;
                    dto.PostTitle = item.PostTitle;
                    dto.Email = item.Email;
                    dto.CommentContent = item.Content;
                    dto.AddDate = item.AddDate;
                    dtolist.Add(dto);
                }
            }

            return dtolist;
        }

        public void ApproveComment(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Comment cmt = db.Comments.First(x => x.ID == ID);
                cmt.isApproved = true;
                cmt.ApproveUserID = UserStatic.UserID;
                cmt.ApproveDate = DateTime.Now;
                cmt.LastUpdateDate = DateTime.Now;
                cmt.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
        }

        public void DeleteComment(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Comment cmt = db.Comments.First(x => x.ID == ID);
                cmt.isDeleted = true;
                cmt.DeletedDate = DateTime.Now;
                cmt.LastUpdateUserID = UserStatic.UserID;
                cmt.LastUpdateDate = DateTime.Now;
                db.SaveChanges();
            }
        }

        public List<CommentDTO> GetAllComments()
        {
            List<CommentDTO> dtolist = new List<CommentDTO>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                var list = (from c in db.Comments.Where(x => x.isDeleted == false)
                            join p in db.Posts on c.PostID equals p.ID
                            select new
                            {
                                ID = c.ID,
                                PostTitle = p.Title,
                                Email = c.Email,
                                Content = c.CommentContent,
                                AddDate = c.AddDate,
                                IsApproved = c.isApproved
                            }).OrderBy(x => x.AddDate).ToList();
                foreach (var item in list)
                {
                    CommentDTO dto = new CommentDTO();
                    dto.ID = item.ID;
                    dto.PostTitle = item.PostTitle;
                    dto.Email = item.Email;
                    dto.CommentContent = item.Content;
                    dto.AddDate = item.AddDate;
                    dto.isApproved = item.IsApproved;
                    dtolist.Add(dto);
                }
            }

            return dtolist;
        }

        public int GetMessageCount()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                return db.Contacts.Where(x => x.isDeleted == false && x.isRead == false).Count();
            }
        }

        public int GetCommentCount()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                return db.Comments.Where(x => x.isDeleted == false && x.isApproved == false).Count();
            }
        }

        public CountDTO GetAllCounts()
        {
            CountDTO dto = new CountDTO();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                dto.PostCount = db.Posts.Where(x => x.isDeleted == false).Count();
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false).Count();
                dto.MessageCount = db.Contacts.Where(x => x.isDeleted == false).Count();
                dto.ViewCount = db.Posts.Where(x => x.isDeleted == false).Sum(x => x.ViewCount);
            }

            return dto;
        }
    }
}
