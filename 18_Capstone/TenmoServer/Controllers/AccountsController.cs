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
    public class AccountsController : Controller
    {
        private IAccountsDAO accountsDAO;

        public AccountsController(IAccountsDAO accountsDAO)
        {
            this.accountsDAO = accountsDAO;
        }

        [HttpGet("/accounts/{userid}")]
        //[Authorize]
        public IActionResult ReturnAccount(int userid)
        {
            Accounts accounts = accountsDAO.GetAccounts(userid);
            return Ok(accounts);
        }
    }
}
