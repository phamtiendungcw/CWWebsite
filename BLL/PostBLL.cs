using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class PostBLL
    {
        private PostDAO dao = new PostDAO();
        public bool AddPost(PostDTO model)
        {
            Post post = new Post();
            post.Title = model.Title;
            post.PostContent = model.PostContent;
            post.ShortContent = model.ShortContent;
            post.Slider = model.Slider;
            post.Area1 = model.Area1;
            post.Area2 = model.Area2;
            post.Area3 = model.Area3;
            post.Notification = model.Notification;
            post.CategoryID = model.CategoryID;
            post.SeoLink = SeoLink.GenerateUrl(model.Title);
            post.LanguageName = model.Language;
            post.AddDate = DateTime.Now;
            post.AddUserID = UserStatic.UserID;
            post.LastUpdateUserID = UserStatic.UserID;
            post.LastUpdateDate = DateTime.Now;
            int ID = dao.AddPost(post);
            LogDAO.AddLog(General.ProcessType.PostAdd, General.TableName.Post, ID);
            SavePostImage(model.PostImages, ID);
            AddTag(model.TagText, ID);
            return true;
        }

        private void AddTag(string tagText, int PostID)
        {
            string[] tags;
            tags = tagText.Split(',');
            List<PostTag> tagList = new List<PostTag>();
            foreach (var item in tags)
            {
                PostTag tag = new PostTag();
                tag.PostID = PostID;
                tag.TagContent = item;
                tag.AddDate = DateTime.Now;
                tag.LastUpdateUserID = UserStatic.UserID;
                tag.LastUpdateDate = DateTime.Now;
                tagList.Add(tag);
            }

            foreach (var item in tagList)
            {
                int tagID = dao.AddTag(item);
                LogDAO.AddLog(General.ProcessType.TagAdd, General.TableName.Tag, tagID);
            }
        }

        void SavePostImage(List<PostImageDTO> list, int PostID)
        {
            List<PostImage> imageList = new List<PostImage>();
            foreach (var item in list)
            {
                PostImage image = new PostImage();
                image.PostID = PostID;
                image.ImagePath = item.ImagePath;
                image.AddDate = DateTime.Now;
                image.LastUpdate = DateTime.Now;
                image.LastUpdateUserID = UserStatic.UserID;
                imageList.Add(image);
            }

            foreach (var item in imageList)
            {
                int imageID = dao.AddImage(item);
                LogDAO.AddLog(General.ProcessType.ImageAdd, General.TableName.Image, imageID);
            }
        }

        public List<PostDTO> GetPosts()
        {
            return dao.GetPosts();
        }

        public PostDTO GetPostWithID(int ID)
        {
            PostDTO dto = new PostDTO();
            dto = dao.GetPostWithID(ID);
            dto.PostImages = dao.GetPostImageWithPostID(ID);
            List<PostTag> tagList = dao.GetPostTagWithPostID(ID);
            string tagValue = "";
            foreach (var item in tagList)
            {
                tagValue += item.TagContent;
                tagValue += ",";
            }
            dto.TagText = tagValue;
            return dto;
        }

        public bool UpdatePost(PostDTO model)
        {
            model.SeoLink = SeoLink.GenerateUrl(model.Title);
            dao.UpdatePost(model);
            LogDAO.AddLog(General.ProcessType.PostUpdate, General.TableName.Post, model.ID);
            if (model.PostImages != null)
                SavePostImage(model.PostImages, model.ID);
            dao.DeleteTags(model.ID);
            AddTag(model.TagText, model.ID);
            return true;
        }

        public string DeletePostImage(int ID)
        {
            string imagePath = dao.DeletePostImage(ID);
            LogDAO.AddLog(General.ProcessType.ImageDelete, General.TableName.Image, ID);
            return imagePath;
        }

        public List<PostImageDTO> DeletePost(int ID)
        {
            List<PostImageDTO> imageList = dao.DeletePost(ID);
            LogDAO.AddLog(General.ProcessType.PostDelete, General.TableName.Post, ID);
            return imageList;
        }

        public bool AddComment(GeneralDTO model)
        {
            Comment comment = new Comment();
            comment.PostID = model.PostID;
            comment.NameSurname = model.Name;
            comment.Email = model.Email;
            comment.CommentContent = model.Message;
            comment.AddDate = DateTime.Now;
            comment.LastUpdateUserID = 1;
            comment.LastUpdateDate = DateTime.Now;
            comment.isDeleted = false;
            comment.isApproved = false;
            dao.AddComment(comment);
            return true;
        }

        public List<CommentDTO> GetComments()
        {
            return dao.GetComments();
        }

        public void ApproveComment(int ID)
        {
            dao.ApproveComment(ID);
            LogDAO.AddLog(General.ProcessType.CommentApprove, General.TableName.Comment, ID);
        }

        public void DeleteComment(int ID)
        {
            dao.DeleteComment(ID);
            LogDAO.AddLog(General.ProcessType.CommentDelete, General.TableName.Comment, ID);
        }

        public List<CommentDTO> GetAllComments()
        {
            return dao.GetAllComments();
        }

        public CountDTO GetCounts()
        {
            CountDTO dto = new CountDTO();
            dto.MessageCount = dao.GetMessageCount();
            dto.CommentCount = dao.GetCommentCount();
            return dto;
        }
    }
}
