using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private IAccountDAO accountDAO;

        public AccountController(IAccountDAO accountDAO)
        {
            this.accountDAO = accountDAO;
        }

        [HttpGet("/accounts/{userid}")]
        //[Authorize]
  
        public IActionResult ReturnAccount(int userid)
        {
            Account accounts = accountDAO.GetAccounts(userid);
            return Ok(accounts);
        }

     

    }
}
