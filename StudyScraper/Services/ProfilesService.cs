using StudyScraper.Models.Requests;
using StudyScraper.Models.ViewModels;
using System.Data;
using System.Data.SqlClient;

namespace StudyScraper.Services
{
    public class ProfilesService : BaseService
    {
        public Profile SelectById(int id)
        {
            Profile model = new Profile();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Profiles_SelectById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", id);
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        model = Mapper(reader);
                    }
                }
                conn.Close();
            }
            return model;
        }

        public void Insert(UpdateProfileRequest model)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Profiles_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleInitial", model.MiddleInitial);
                    cmd.Parameters.AddWithValue("@LastName", model.LastName);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void ChangePassword(UpdatePasswordRequest model)
        {
            CryptographyService svc = new CryptographyService();
            string salt = svc.GenerateRandomString(16);
            string hashedPassword = svc.Hash(model.NewPassword, salt);

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Users_ChangePassword", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.UserId);
                    cmd.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                    cmd.Parameters.AddWithValue("@Salt", salt);

                    cmd.ExecuteNonQuery();
                };
                conn.Close();
            }
        }

        private Profile Mapper(SqlDataReader reader)
        {
            Profile model = new Profile();
            int index = 0;

            if (!reader.IsDBNull(index))
            {
                model.UserId = reader.GetInt32(index++);
            }
            else
            {
                index++;
            }
            if (!reader.IsDBNull(index))
            {
                model.FirstName = reader.GetString(index++);
            }
            else
            {
                index++;
            }
            if (!reader.IsDBNull(index))
            {
                model.MiddleInitial = reader.GetString(index++);
            }
            else
            {
                index++;
            }
            if (!reader.IsDBNull(index))
            {
                model.LastName = reader.GetString(index++);
            }
            else
            {
                index++;
            }
            return model;
        }
    }
}
