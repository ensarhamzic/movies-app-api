using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Movies_API.Data.Models;
using Movies_API.Data.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Movies_API.Data.Services
{
    public class UserService
    {
        private AppDbContext dbContext;
        private IConfiguration configuration;

        public UserService(AppDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }
        public object RegisterUser(UserRegisterVM request)
        {
            var userExists = dbContext.Users.Any(u => u.Email == request.Email);
            if (userExists)
                throw new Exception("This user already exists");
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newUser = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            dbContext.Add(newUser);
            dbContext.SaveChanges();
            string token = CreateToken(newUser);
            UserVM user = (UserVM)newUser;
            return new {user, token};
        }

        public object LoginUser(UserLoginVM request)
        {
            var errorMessage = "Check your credentials and try again!";
            var foundUser = dbContext.Users.FirstOrDefault(u => u.Email == request.Email);
            if (foundUser == null)
                throw new Exception(errorMessage);
            var passwordCorrect = VerifyPasswordHash(request.Password, foundUser.PasswordHash, foundUser.PasswordSalt);
            if(!passwordCorrect)
                throw new Exception(errorMessage);
            var token = CreateToken(foundUser);
            UserVM user = (UserVM)foundUser;
            return new { user, token };
        }

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
                GetBytes(configuration.GetSection("JWT:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
