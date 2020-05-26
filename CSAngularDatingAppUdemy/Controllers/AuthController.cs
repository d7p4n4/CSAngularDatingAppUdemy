using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSAngularDatingAppUdemy.Data;
using CSAngularDatingAppUdemy.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSAngularDatingAppUdemy.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthRepository Repo;
        public AuthController(IAuthRepository repo)
        {
            Repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Register(string userName, string password)
        {
            //validate request

            userName = userName.ToLower();

            if (await Repo.UserExists(userName))
                return BadRequest("Username already eyists");

            var userToCreate = new User()
            {
                Username = userName
            };

            var createdUser = await Repo.Register(userToCreate, password);

            return StatusCode(201);
        }
    }
}