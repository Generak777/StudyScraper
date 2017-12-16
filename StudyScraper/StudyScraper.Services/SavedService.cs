using StudyScraper.Models.Requests;
using StudyScraper.Models.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StudyScraper.Services
{
    public class SavedService : BaseService
    {
        public List<SavedPost> GetAll()
        {
            List<SavedPost> postList = new List<SavedPost>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("RedditPosts_SelectAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        SavedPost model = Mapper(reader);
                        postList.Add(model);
                    }
                }

                conn.Close();
            }
            return postList;
        }

        public SavedPost SelectById(int id)
        {
            SavedPost model = new SavedPost();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("RedditPosts_SelectById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        model = Mapper(reader);
                    }
                }
                conn.Close();
            }
            return model;
        }

        public void Update(UpdatePostRequest model)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("RedditPosts_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Url", model.Url);
                    cmd.Parameters.AddWithValue("@Notes", model.Notes);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("RedditPosts_Delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        private SavedPost Mapper(SqlDataReader reader)
        {
            SavedPost model = new SavedPost();
            int index = 0;

            model.Id = reader.GetInt32(index++);
            model.Title = reader.GetString(index++);
            model.Url = reader.GetString(index++);
            if(!reader.IsDBNull(index))
            {
                model.Notes = reader.GetString(index++);
            } else
            {
                index++;
            }
            model.CreatedDate = reader.GetDateTime(index);

            return model;
        }
    }
}
