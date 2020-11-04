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

        [HttpGet("{account_id}")]
        [Authorize]
        public Accounts ReturnAccount(int accountid)
        {
            return accountsDAO.GetAccounts(accountid);
        }
    }
}
