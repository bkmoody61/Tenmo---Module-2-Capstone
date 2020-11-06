using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDAO userDAO;
        public UserController(IUserDAO userDao)
        {
            this.userDAO = userDao;
        }
        [HttpGet]
        public List<User> ListOfUsers()
        {
            return userDAO.GetUserList();
        }
    }
}
