using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        bool TransferFunds(Transfer transfer);
        List<Transfer> ViewTransfer(int userId);
        Transfer ViewTransferDetails(int transferId);


    }
}