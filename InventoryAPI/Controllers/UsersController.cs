using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{
    // localhost:xxxx/api/users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasher;

        //contect to db using dbcontext through constructor injection
        public UsersController(ApplicationDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var allUsers = dbContext.Users.ToList();

            return Ok(allUsers);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetUserById(Guid id)
        {
          var user =  dbContext.Users.Find(id);
            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser(AddUserDto addUserDto)
        {
           
            var userEntity = new User()
            {
                Username = addUserDto.Username,
                Password = "",
                Level = addUserDto.Level
            };

            string passwordhassed = passwordHasher.HashPassword(userEntity, addUserDto.Password);
            userEntity.Password = passwordhassed;

            dbContext.Users.Add(userEntity);
            dbContext.SaveChanges();

            return Ok(new {userEntity.Id,userEntity.Username, userEntity.Level});
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            var user = dbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }


            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                string passwordHassed = passwordHasher.HashPassword(user, updateUserDto.Password);
                user.Password = passwordHassed;
            }
            if (updateUserDto.Level.HasValue)
            {
                user.Level = updateUserDto.Level.Value;
            }

            dbContext.SaveChanges();
            return Ok(new {user.Id});

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteUser(Guid id)
        {
           var user = dbContext.Users.Find(id);

            if(user == null)
            {
                return NotFound();  
            }

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
            return Ok(user);
        }



    }

    internal interface IPasswordHasher
    {
    }
}
