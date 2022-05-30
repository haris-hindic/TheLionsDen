using System.Net.Http.Headers;
using System.Text;

namespace TheLionsDen.Auth
{
    public class CredentialsHelper
    {
        public static Credentials extractCredentials(HttpRequest request)
        {
            var authHeader = AuthenticationHeaderValue.Parse(request.Headers["Authorization"]);
            var credetialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credetialBytes).Split(":");

            var username = credentials[0];
            var password = credentials[1];
            return new Credentials() { Username = username, Password = password };
        }

        public class Credentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
