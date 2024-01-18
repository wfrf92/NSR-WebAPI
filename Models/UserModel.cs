using System.Security.Cryptography;
using System.Text;

public class UserModel
{
    public string Username { get; set; }
    public string Password { get; set; }
     public string Salt { get; set; }

    public bool VerifyPassword(string password)
    {
        var hashedPassword = HashPassword(password, Salt);
        return Password == hashedPassword;
    }
    private static string HashPassword(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            var combinedBytes = Encoding.UTF8.GetBytes(password + salt);
            var hashBytes = sha256.ComputeHash(combinedBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
