using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        // Read user credentials from the JSON file
        var users = ReadUsersFromFile("Json/b370733c8c8d4b45879b8db578ab7a4b.json");

        var user = users.FirstOrDefault(u => u.Username == login.Username);

        if (user != null)
        {
            if (user.VerifyPassword(login.Password))
            {
                var token = GenerateToken();
                return Ok(new { token });
            }
            else
            {
                // Password is incorrect
                return BadRequest("Password is incorrect.");
            }
        }

        // User not found
        return BadRequest("User not found.");
    }

    private List<UserModel> ReadUsersFromFile(string filePath)
    {
        try
        {
            var json = System.IO.File.ReadAllText(filePath);
            var users = JsonConvert.DeserializeObject<List<UserModel>>(json);
            return users;
        }
        catch (Exception ex)
        {
            // Handle file read or deserialization errors
            return new List<UserModel>();
        }
    }

    private bool IsValidCredentials(List<UserModel> users, string username, string password)
    {
        return users.Any(user => user.Username == username && user.Password == password);
    }

    private string GenerateToken()
    {
         var appSettings =  new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JWT");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings["Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
       
        var token = new JwtSecurityToken(
            appSettings["ValidIssuer"],
             appSettings["ValidAudience"],
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
