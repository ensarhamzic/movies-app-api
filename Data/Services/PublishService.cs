using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies_API.Data.Models;
using Movies_API.Data.ViewModels;
using System.Security.Claims;

namespace Movies_API.Data.Services
{
    public class PublishService
    {
        private AppDbContext dbContext;
        private IHttpContextAccessor httpContextAccessor;

        public PublishService(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
        }
        public string SetPublishName(PublishVM request)
        {
            var userId = GetAuthUserId();
            var foundPublish = dbContext.Publishes.FirstOrDefault(p => p.UserId == userId);
            var nameTaken = dbContext.Publishes.FirstOrDefault(p => p.Name == request.Name && p.UserId != userId);
            if (nameTaken != null) throw new Exception("Name is already taken");
            if(foundPublish != null)
            {
                foundPublish.Name = request.Name;
                dbContext.SaveChanges();
                return "Name updated";
            } else
            {
                var newPublish = new Publish()
                {
                    Name = request.Name,
                    UserId = userId
                };
                dbContext.Publishes.Add(newPublish);
                dbContext.SaveChanges();
                return "New url saved";
            }
        }

        public Publish? GetUserPublish()
        {
            var userId = GetAuthUserId();
            var foundPublish = dbContext.Publishes.FirstOrDefault(p => p.UserId == userId);
            return foundPublish;
        }

        public string PublishCollections(PublishCollectionsVM request)
        {
            var userId = GetAuthUserId();
            var foundPublish = dbContext.Publishes.FirstOrDefault(p => p.UserId == userId);
            if (foundPublish == null) throw new Exception("You need to set publish Url first");
            var publishedCollections = dbContext.PublishedCollections.Where(pc => pc.PublishId == foundPublish.Id);
            dbContext.PublishedCollections.RemoveRange(publishedCollections);
            foreach(var collectionId in request.CollectionIds)
            {
                var newPublishedCollection = new PublishCollection()
                {
                    PublishId = foundPublish.Id,
                    CollectionId = collectionId
                };
                dbContext.Add(newPublishedCollection);
            }
            dbContext.SaveChanges();
            return "Collections published";
        }


        private int GetAuthUserId()
        {
            return int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid));
        }


    }
}
