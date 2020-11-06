using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDAO : ITransferDAO
    {
        private readonly string connectionString;
        public TransferSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool TransferFunds(Transfer transfer)

        {
          
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql =
@"Begin Transaction;
Insert into transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount)
Values (@transferType, @transferStatus, @accountFrom, @accountTo, @transferAmount);
Update Accounts set balance = (balance - @transferAmount) where account_id = @accountFrom; 
Update Accounts set balance = (balance + @transferAmount) where account_id = @accountTo;
Commit Transaction;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@transferType", TransferType.Send);
                    cmd.Parameters.AddWithValue("@transferStatus", TransferStatus.Approved);
                    cmd.Parameters.AddWithValue("@accountFrom", transfer.AccountFrom);
                    cmd.Parameters.AddWithValue("@accountTo", transfer.AccountTo);
                    cmd.Parameters.AddWithValue("@transferAmount", transfer.Amount);
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (SqlException)
            {
                throw;
            }


        }
    }
}

