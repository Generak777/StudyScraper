using StudyScraper.Models.Domain;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace StudyScraper.Services
{
    public class ForgotPasswordService : BaseService
    {
        // check for old token in DB, if one exists delete it
        // insert new token into DB, use email to select corresponding UserId
        // send user an email w/ link to reset password
        public void ForgotPasswordInsert(string email)
        {
            bool validUser = false;
            ForgotPassword model = new ForgotPassword();
            GenerateForgotPasswordTokenService svc = new GenerateForgotPasswordTokenService();
            model.Token = svc.GetRandomValue(128);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Confirmation_Tokens_ForgotPasswordInsertWithEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Token", model.Token);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        validUser = reader.GetBoolean(0);
                    }
                };
                conn.Close();
            }
            if (validUser)
            {
                string url = GenerateUrl(model.Token);
                string html = "<p>Click <a href='" + url + "'>here</a> to reset your password.</p><p>If the link does not work please click " +
                    "<a href='localhost:52505/forgotPassword/'>here</a> to recieve a new email from us.</p><p>Please disregard this email " +
                    "if you did not make this request.</p>";

                Task<string> res = SendEmail(new SingleRecipientEmail
                {
                    ToEmailAddress = email,
                    ToName = email,
                    FromEmailAddress = "licenseplategallery@gmail.com",
                    FromName = "Plate Gallery",
                    Subject = "Reset your password",
                    PlainTextContent = "",
                    HtmlContent = html
                });
            }
        }

        // use this method when url is NOT http://lpgallery.dev/public/forgotPassword/
        // url is parsed, then the token from the url is validated using Confirmation_Tokens_SelectValidToken stored procedure
        // separate from GetUserId method bc the stored proc executed returns 0 or 1 based on if the token exists in the DB
        // did not want to return the UserId in order to keep that DB info away frm the REST Client and Chrome Dev Tools
        // the 1/0 returned is also used in the JavaScript code to determine which div to show on the pg (should not show reset pass form
        // if the token is not found in the DB
        public int GetByToken(string token)
        {
            int found = 0;
            this.DataProvider.ExecuteCmd(
                "Confirmation_Tokens_SelectValidToken",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Token", token);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int index = 0;
                    found = reader.GetSafeInt32(index++);
                }
            );
            return found;
        }

        // Method used to update the user's password and delete the token associated w/ the user, called once the user submits 
        // new pass and confirm pass
        public void GetUserId(string token, string password)
        {
            string salt = GenerateRandomString(15);
            string newPass = HashPass(password, salt, 1);
            this.DataProvider.ExecuteNonQuery(
                "Confirmation_Tokens_SelectAndUpdate",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Token", token);
                    paramCol.AddWithValue("@NewHashedPass", newPass);
                    paramCol.AddWithValue("@Salt", salt);
                }
            );
        }

        private string HashPass(string pass, string salt, int iterCount)
        {
            string hashedPass;
            hashedPass = Hash(pass, salt, iterCount);
            return hashedPass;
        }

        private string Hash(string original, string salt, int iterations = 1)
        {
            const int hashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash

            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] bytes;
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(original, saltBytes))
            {
                pbkdf2.IterationCount = iterations;
                bytes = pbkdf2.GetBytes(hashByteSize);
            }

            return Convert.ToBase64String(bytes);
        }

        private string GenerateRandomString(int length)
        {
            // This will give us approximately the desired length string.
            byte[] bytes = new byte[(int)Math.Floor(length * .75)];

            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return Convert.ToBase64String(bytes);
        }

        private string GenerateUrl(string token)
        {
            string url = "localhost:52505/forgotPassword/" + token;
            return url;
        }

        private async Task<string> SendEmail(SingleRecipientEmail emailModel)
        {
            DevReturnConfig<string> apiKey = _devConfigService.GetValue("SendGrid Auth Token");
            SendGridClient client = new SendGridClient(apiKey.Value);
            SendGridMessage msg = MailHelper.CreateSingleEmail(
                new EmailAddress(emailModel.FromEmailAddress, emailModel.FromName)
                , new EmailAddress(emailModel.ToEmailAddress, emailModel.ToName)
                , emailModel.Subject
                , emailModel.PlainTextContent
                , emailModel.HtmlContent);

            Response response = await client.SendEmailAsync(msg);
            return response.StatusCode.ToString();
        }
    }
}
