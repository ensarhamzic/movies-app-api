using Microsoft.EntityFrameworkCore;
using Movies_API.Data.Models;
using Movies_API.Data.ViewModels;
using System.Reflection.Metadata.Ecma335;
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

        public List<Collection>? GetCollections()
        {
            var userId = GetAuthUserId();
            var collections = dbContext.Collections.Include(c => c.Movies).Where(c => c.UserId == userId).ToList();
            return collections;
        }

        public object AddMovieToCollection(MovieVM request)
        {
            var userId = GetAuthUserId();
            var foundCollection = dbContext.Collections.Include(c => c.Movies).FirstOrDefault(c => c.Id == request.CollectionId);
            if (foundCollection != null)
            {
                var hasPermission = foundCollection.UserId == userId;
                var foundMovie = foundCollection.Movies.FirstOrDefault(m => m.Id == request.Id);
                if (foundMovie != null)
                {
                    dbContext.Remove(foundMovie);
                    dbContext.SaveChanges();
                    return new {message = "Movie removed from collection" };
                }
                    
                if (hasPermission)
                {
                    var newMovie = new Movie()
                    {
                        Id = request.Id,
                        CollectionId = request.CollectionId
                    };
                    dbContext.Add(newMovie);
                    dbContext.SaveChanges();
                    return new {message = "Movie added to collection" };
                }
                throw new Exception("You don't have permission to add to this collection");
            }
            throw new Exception("Invalid collection id");
        }

        public object AddMovieToFavorites(FavoriteVM request)
        {
            var userId = GetAuthUserId();
            var foundFavorite = dbContext.Favorites.FirstOrDefault(f => f.Id == request.Id && f.UserId == userId);
            if (foundFavorite == null)
            {
                var newMovie = new Favorite()
                {
                    Id = request.Id,
                    UserId = userId,
                };
                dbContext.Add(newMovie);
                dbContext.SaveChanges();
                return new { message = "Movie added to favorites" };
            }
            dbContext.Remove(foundFavorite);
            dbContext.SaveChanges();
            return new { message = "Movie removed from favorites" };
        }

        private int GetAuthUserId()
        {
            return int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid));
        }


    }
}
