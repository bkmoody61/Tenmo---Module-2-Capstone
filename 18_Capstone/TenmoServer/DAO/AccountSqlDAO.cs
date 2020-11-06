using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSqlDAO : IAccountDAO
    {
        private readonly string connectionString;
        public AccountSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Account GetAccounts(int userid)
        {
            Account account = new Account();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT account_id, user_id, balance FROM accounts WHERE user_id = @user_id ", conn);
                    cmd.Parameters.AddWithValue("@user_id", userid);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        account.AccountID = Convert.ToInt32(reader["account_id"]);
                        account.UserID = Convert.ToInt32(reader["user_id"]);
                        account.Balance = Convert.ToDecimal(reader["balance"]);
                        return account;
                    }
                    return null;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

    }

}
