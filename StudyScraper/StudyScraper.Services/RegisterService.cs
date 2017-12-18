using StudyScraper.Models.Requests;
using System.Data;
using System.Data.SqlClient;

namespace StudyScraper.Services
{
    public class RegisterService : BaseService
    {
        public int RegisterUser(RegisterRequest model)
        {
            int id = 0;
            string salt;
            string hashedPassword;
            string password = model.Password;

            CryptographyService svc = new CryptographyService();
            salt = svc.GenerateRandomString(16);
            hashedPassword = svc.Hash(password, salt);
            model.HashedPassword = hashedPassword;
            model.Salt = salt;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Users_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@HashedPassword", model.HashedPassword);
                    cmd.Parameters.AddWithValue("@Salt", model.Salt);

                    SqlParameter parm = new SqlParameter("@Id", SqlDbType.Int);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);
                    cmd.ExecuteNonQuery();
                    id = (int)cmd.Parameters["@Id"].Value;
                };
                conn.Close();
            }
            return id;
        }
    }
}
