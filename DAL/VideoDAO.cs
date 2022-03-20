using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class VideoDAO : PostContext
    {
        public int AddVideo(Video video)
        {
            try
            {
                db.Videos.Add(video);
                db.SaveChanges();
                return video.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<VideoDTO> GetVideos()
        {
            List<Video> list = db.Videos.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).ToList();
            List<VideoDTO> dtoList = new List<VideoDTO>();
            foreach (var item in list)
            {
                VideoDTO dto = new VideoDTO();
                dto.Title = item.Title;
                dto.VideoPath = item.VideoPath;
                dto.OriginalVideoPath = item.OriginalVideoPath;
                dto.ID = item.ID;
                dto.AddDate = item.AddDate;
                dtoList.Add(dto);
            }
            return dtoList;
        }

        public VideoDTO GetVideoWithID(int ID)
        {
            Video video = db.Videos.First(x => x.ID == ID);
            VideoDTO dto = new VideoDTO();
            dto.ID = video.ID;
            dto.OriginalVideoPath = video.OriginalVideoPath;
            dto.Title = video.Title;
            return dto;
        }

        public void UpdateVideo(VideoDTO model)
        {
            try
            {
                Video video = db.Videos.First(x => x.ID == model.ID);
                video.VideoPath = model.VideoPath;
                video.OriginalVideoPath = model.OriginalVideoPath;
                video.Title = model.Title;
                video.LastUpdateDate = DateTime.Now;
                video.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void DeleteVideo(int ID)
        {
            try
            {
                Video video = db.Videos.First(x => x.ID == ID);
                video.isDeleted = true;
                video.DeletedDate = DateTime.Now;
                video.LastUpdateDate = DateTime.Now;
                video.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
