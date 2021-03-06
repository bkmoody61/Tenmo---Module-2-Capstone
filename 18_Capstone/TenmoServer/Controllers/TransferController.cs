﻿using System;
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
        private IAccountDAO accountDAO;
        private ITransferDAO transferDAO;

        public TransferController(IAccountDAO accountDAO, ITransferDAO transferDAO)
        {
            this.accountDAO = accountDAO;
            this.transferDAO = transferDAO;
        }

        [HttpPost("sendMoney")]
        public IActionResult SendMoney(Transfer transfer)
        {
            int userId = GetUserId();
            Account account = accountDAO.GetAccounts(userId);
            if(account.Balance < transfer.Amount)
            {
                return BadRequest("You do not have enough money.");
            }
            transfer.AccountFrom = account.AccountID;
            transferDAO.TransferFunds(transfer);
            return Ok();

        }
        [HttpGet("/transfer/{userId}")]
        public List<Transfer> ViewTransfer(int userId)
        {
            return transferDAO.ViewTransfer(userId);
        }

        [HttpGet("/transfer/detail/{transferId}")]
        public Transfer ViewTransferDetails(int transferId)
        {
            return transferDAO.ViewTransferDetails(transferId);
        }



        private int GetUserId()
        {
            string strUserId = User.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
            return String.IsNullOrEmpty(strUserId) ? 0 : Convert.ToInt32(strUserId);
        }


    }
}
