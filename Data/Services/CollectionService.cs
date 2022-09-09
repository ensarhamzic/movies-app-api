using Movies_API.Data.Models;
using System.Security.Claims;

namespace Movies_API.Data.Services
{
    public class CollectionService
    {
        private IHttpContextAccessor httpContextAccessor;
        private AppDbContext dbContext;

        public CollectionService(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public Collection CreateCollection(string name)
        {
            var userId = GetAuthUserId();
            var foundCollection = dbContext.Collections
                .FirstOrDefault(c => c.Name == name && c.UserId == userId);
            if (foundCollection != null)
                throw new Exception("Cannot have two collections with the same name");
            var newCollection = new Collection()
            {
                Name = name,
                UserId = userId,
            };
            dbContext.Add(newCollection);
            dbContext.SaveChanges();
            return newCollection;
        }

        private int GetAuthUserId()
        {
            return int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid));
        }
    }
}
