﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
        public List<Transfer> ViewTransfer(int userId)
        {
            List<Transfer> viewTransferList = new List<Transfer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    string sql =
@"select transfer_id, ufrom.username as fromusername, uto.username as tousername, amount
from transfers t
join accounts afrom on t.account_from = afrom.user_id
join accounts ato on t.account_to = ato.account_id
join users ufrom on afrom.user_id = ufrom.user_id
join users uto on ato.user_id = uto.user_id
where t.account_from = (select account_id from accounts where user_id = @userId)
or t.account_to = (select account_id from accounts where user_id = @userId)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {



                        while (reader.Read())
                        {
                            Transfer viewTransfer = new Transfer();

                            viewTransfer.TransferID = Convert.ToInt32(reader["transfer_id"]);
                            viewTransfer.AccountFromName = Convert.ToString(reader["fromusername"]);
                            viewTransfer.AccountToName = Convert.ToString(reader["tousername"]);
                            viewTransfer.Amount = Convert.ToDecimal(reader["amount"]);

                            viewTransferList.Add(viewTransfer);

                        }
                    }
                    return viewTransferList;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
        public Transfer ViewTransferDetails(int transferId)
        {
            Transfer viewTransfer = new Transfer();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    string sql =
@"select transfer_id, ufrom.username as fromusername, uto.username as tousername, ttypes.transfer_type_desc as ttypename, tstatuses.transfer_status_desc as tstatusname, amount
from transfers t
join transfer_types ttypes ON t.transfer_type_id = ttypes.transfer_type_id
join transfer_statuses tstatuses On t.transfer_status_id = tstatuses.transfer_status_id
join accounts afrom on t.account_from = afrom.user_id
join accounts ato on t.account_to = ato.account_id
join users ufrom on afrom.user_id = ufrom.user_id
join users uto on ato.user_id = uto.user_id
where t.transfer_id = @transferId";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@transferId", transferId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {



                        if (reader.Read())
                        {
                            //Transfer viewTransfer = new Transfer();

                            viewTransfer.TransferID = Convert.ToInt32(reader["transfer_id"]);
                            viewTransfer.AccountFromName = Convert.ToString(reader["fromusername"]);
                            viewTransfer.AccountToName = Convert.ToString(reader["tousername"]);
                            viewTransfer.TypeName = Convert.ToString(reader["ttypename"]);
                            viewTransfer.StatusName = Convert.ToString(reader["tstatusname"]);
                            viewTransfer.Amount = Convert.ToDecimal(reader["amount"]);

                            //viewTransferList.Add(viewTransfer);

                        }
                    }
                    return viewTransfer;
                }
            }
            catch (SqlException)
            {
                throw;
            }

        }
    }
}

