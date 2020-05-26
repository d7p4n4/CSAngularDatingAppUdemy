using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSAngularDatingAppUdemy.Data;
using CSAngularDatingAppUdemy.DTOs;
using CSAngularDatingAppUdemy.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSAngularDatingAppUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthRepository Repo;
        public AuthController(IAuthRepository repo)
        {
            Repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO)
        {
            //validate request

            userForRegisterDTO.UserName = userForRegisterDTO.UserName.ToLower();

            if (await Repo.UserExists(userForRegisterDTO.UserName))
                return BadRequest("Username already eyists");

            var userToCreate = new User()
            {
                Username = userForRegisterDTO.UserName
            };

            var createdUser = await Repo.Register(userToCreate, userForRegisterDTO.Password);

            return StatusCode(201);
        }
    }
}