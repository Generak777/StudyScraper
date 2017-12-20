using StudyScraper.Models.Domain;
using StudyScraper.Models.Requests;
using System.Data;
using System.Data.SqlClient;

namespace StudyScraper.Services
{
    public class LoginService : BaseService
    {
        public LoginData Login(LoginRequest model)
        {
            LoginData res = new LoginData();
            res.IsLoggedIn = false;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Users_SelectByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        LoginRequest responseModel = Mapper(reader);
                        res.Id = responseModel.Id;
                        res.Email = responseModel.Email;

                        int multOf4 = responseModel.Salt.Length % 4;
                        if (multOf4 > 0)
                        {
                            responseModel.Salt += new string('=', 4 - multOf4);
                        }
                        CryptographyService cryptSvc = new CryptographyService();
                        string passwordHash = cryptSvc.Hash(model.Password, responseModel.Salt);

                        if (passwordHash == responseModel.EncryptedPass)
                        {
                            res.IsLoggedIn = true;
                        }
                    }
                }
                conn.Close();
            }
            if (res.IsLoggedIn == false)
            {
                res.Id = 0;
                res.Email = "Failed to login";
                return res;
            }
            return res;
        }

        private LoginRequest Mapper(SqlDataReader reader)
        {
            LoginRequest model = new LoginRequest();
            int index = 0;

            model.Id = reader.GetInt32(index++);
            model.Email = reader.GetString(index++);
            model.EncryptedPass = reader.GetString(index++);
            model.Salt = reader.GetString(index++);
            return model;
        }
    }
}