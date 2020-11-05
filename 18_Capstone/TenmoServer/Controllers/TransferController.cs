using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private IAccountsDAO accountsDAO;
        private ITransferDAO transferDAO;

        public TransferController(IAccountsDAO accountsDAO, ITransferDAO transferDAO)
        {
            this.accountsDAO = accountsDAO;
            this.transferDAO = transferDAO;
        }

        [HttpPost("sendMoney")]
        public IActionResult SendMoney(Transfers transfer)
        {
            int userid = GetUserId();
            Accounts account = accountsDAO.GetAccounts(userid);
            if(account.Balance < transfer.Amount)
            {
                return BadRequest("You do not have enough money.");
            }
            transfer.AccountFrom = account.AccountID;
            transferDAO.TransferFunds(transfer);
            return Ok();

        }
        private int GetUserId()
        {
            string strUserId = User.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
            return String.IsNullOrEmpty(strUserId) ? 0 : Convert.ToInt32(strUserId);
        }


    }
}
