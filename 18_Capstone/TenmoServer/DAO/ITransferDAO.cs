using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        bool TransferFunds(Transfers transfer);
    }
}