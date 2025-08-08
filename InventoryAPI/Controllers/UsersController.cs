using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{
    // localhost:xxxx/api/users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        //contect to db using dbcontext through constructor injection
        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
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
                Password = addUserDto.Password,
                Level = addUserDto.Level
            };

            dbContext.Users.Add(userEntity);
            dbContext.SaveChanges();

            return Ok(userEntity);
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

            user.Password = updateUserDto.Password;
            if (updateUserDto.Level.HasValue)
            {
                user.Level = updateUserDto.Level.Value;
            }

            dbContext.SaveChanges();
            return Ok(user);

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
}
