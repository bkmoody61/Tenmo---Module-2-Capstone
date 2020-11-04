using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountsSqlDAO : IAccountsDAO
    {
        private readonly string connectionString;
        public AccountsSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Accounts GetAccounts(int accountid)
        {
            Accounts account = new Accounts();
            //User returnUser = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT account_id, user_id, balance FROM accounts WHERE @account_id ", conn);
                    cmd.Parameters.AddWithValue("@account_id", accountid);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        account.AccountID = Convert.ToInt32(reader["account_id"]);
                        account.UserID = Convert.ToInt32(reader["user_id"]);
                        account.Balance = Convert.ToDecimal(reader["balance"]);
                    }
                    return account;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

    }

}
