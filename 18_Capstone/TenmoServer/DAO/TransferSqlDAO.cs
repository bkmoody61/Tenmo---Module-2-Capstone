using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDAO
    {
        private readonly string connectionString;
        public TransferSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool Transaction(int userId1, int userId2, decimal transferAmount)
        {
            Accounts account1 = new Accounts();
            Accounts account2 = new Accounts();

            //if (transferAmount <= account1.)

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Select account_id, user_id, balance from accounts where user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId1);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        account1.AccountID = Convert.ToInt32(reader["account_id"]);
                        account1.UserID = Convert.ToInt32(reader["user_id"]);
                        account1.Balance = Convert.ToDecimal(reader["balance"]);
                    }
                }
            }
            catch(SqlException)
            {
                throw;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Select account_id, user_id, balance from accounts where user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId2);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        account2.AccountID = Convert.ToInt32(reader["account_id"]);
                        account2.UserID = Convert.ToInt32(reader["user_id"]);
                        account2.Balance = Convert.ToDecimal(reader["balance"]);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            if (transferAmount <= account1.Balance)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand($"Update Accounts set balance = (balance - @transferAmount) where user_id = @userId1 ; Update Accounts set balance = (balance + @transferAmount) where user_id = @userId2", conn);
                        cmd.Parameters.AddWithValue("@userId1", userId1);
                        cmd.Parameters.AddWithValue("@userId2", userId2);
                        cmd.Parameters.AddWithValue("@transferAmount", transferAmount);
                        ////SqlDataReader reader = cmd.ExecuteReader();
                    }
                }
                catch (SqlException)
                {
                    throw;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
