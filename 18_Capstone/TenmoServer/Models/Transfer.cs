using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int TransferID { get; set; }
        public TransferType TransferTypeID { get; set; }
        public TransferStatus TransferStatusID { get; set; }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public decimal Amount { get; set; }
    }

    public enum TransferType
    {
       Request = 1, 
       Send
    }

    public enum TransferStatus
    {
        Pending = 1,
        Approved,
        Rejected
    }
}
