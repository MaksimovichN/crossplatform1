using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Maksimovich.Models;

namespace Lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("token")]
        public string GetToken()
        {
            return AuthOptions.GenerateToken();
        }
        [HttpGet("token/secret")]
        public string GetAdminToken()
        {
            return AuthOptions.GenerateToken(true);
        }
    }
}