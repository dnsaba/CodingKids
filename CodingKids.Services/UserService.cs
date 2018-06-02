using CodingKids.Models.Domain;
using CodingKids.Services.Cryptography;
using CodingKids.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodingKids.Services
{
    public class UserService
    {
        private IAuthenticationService _authenticationService;
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["AuthContext"].ConnectionString;
        CryptographyService cryptsvc = new CryptographyService();
        int RAND_LENGTH = 15;
        int HASH_ITERATION_COUNT = 1;

        public int Create(NewUser model)
        {
            // return to later if I have time to check for already registered emails
            //LoginUser loginModel = GetSalt(userModel.Email);
            //if (loginModel == null)

            int id = 0;
            string salt;
            string passwordHash;

            string password = model.Password;

            salt = cryptsvc.GenerateRandomString(RAND_LENGTH);
            passwordHash = cryptsvc.Hash(password, salt, HASH_ITERATION_COUNT);
            model.Salt = salt;
            model.EncryptedPass = passwordHash;

            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Users_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", model.UserName);
                    cmd.Parameters.AddWithValue("@Salt", model.Salt);
                    cmd.Parameters.AddWithValue("@HashPassword", model.EncryptedPass);

                    SqlParameter param = new SqlParameter("@Id", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();

                    id = (int)cmd.Parameters["@Id"].Value;
                }
                conn.Close();
            }
            return id;
        }

        public bool LogIn(string userName, string password)
        {
            bool isSuccessful = false;

            LoginUser userData = GetInfo(userName);

            if (userData != null && !String.IsNullOrEmpty(userData.Salt))
            {
                int multOf4 = userData.Salt.Length % 4;
                if (multOf4 > 0)
                {
                    userData.Salt += new string('=', 4 - multOf4);
                }

                string passwordHash = cryptsvc.Hash(password, userData.Salt, HASH_ITERATION_COUNT);

                IUserAuthData response = new UserBase
                {
                    Id = userData.UserId,
                    UserName = userData.UserName,
                    Roles = new[] { "User" },
                    RoleId = userData.RoleId.ToString()
                };

                Claim emailClaim = new Claim(userData.UserName.ToString(), "CodingKids");
                _authenticationService.LogIn(response, new Claim[] { emailClaim });



                if (userName == userData.UserName && passwordHash == userData.HashPassword)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;

        }

        private LoginUser GetInfo(string userName)
        {
            LoginUser model = null;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Users_SelectByUserName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        model = new LoginUser();
                        int index = 0;
                        model.UserId = reader.GetInt32(index++);
                        model.UserName = reader.GetString(index++);
                        model.Salt = reader.GetString(index++);
                        model.HashPassword = reader.GetString(index++);
                        model.CreatedDate = reader.GetDateTime(index++);
                        model.RoleId = reader.GetInt32(index);
                    }

                }
                conn.Close();
            }
            return model;
        }
    }
}
