using PiperchatService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PiperchatService.Contract
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class UserService : IUserService
    {

        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PiperChatDb;Integrated Security=True";
        public string InsertUserRecord(User user)
        {
            string response = "";
            try
            {
                string query = "INSERT INTO User (UserId,Name,Email,Password) values(@UserId, @Name, @Email, @Password)";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(query,con);
                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                response = "Inserted Successfully";

            }catch(Exception e)
            {
                response = "Error " + e;
            }
            return response;
        }

        public string UpdateUserRecord(User user)
        {
            string result = "";
            string Query = "UPDATE User SET Name=@Name,Email=@Email,Password=@Password WHERE UserId=@UserId";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            result = "Updated Successfully !";
            return result;
        }

        public string DeleteUserRecord(User user)
        {
            string Query = "DELETE FROM User Where UserId=@UserId";
            string result = "";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@UserID", user.UserId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            result = "Deleted Successfully!";
            return result;
        }

        public DataSet GetAllUserRecords()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                string Query = "SELECT * FROM User";

                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }

            return ds;
        }

        public DataSet SearchUserRecord(User user)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                string Query = "SELECT * FROM tblEmployee WHERE UserId=@UserId";

                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                sda.SelectCommand.Parameters.AddWithValue("@UserId", user.UserId);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex);
            }
            return ds;
           
        }
       
        
    }
}
