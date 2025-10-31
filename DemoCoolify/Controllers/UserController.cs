using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace DemoCoolify.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var connectionString = $"SSL Mode=Require;Host={Environment.GetEnvironmentVariable("sqlhost")};Port=5432;" +
                                   $"Database={Environment.GetEnvironmentVariable("sqldatabase")};" +
                                   $"username={Environment.GetEnvironmentVariable("sqluser")};" +
                                   $"Password={Environment.GetEnvironmentVariable("sqlpassword")};";
            var cnn = new NpgsqlConnection(connectionString);
            try
            {
                cnn.Open();
                var query = "SELECT * FROM users";
                var res = await cnn.QueryAsync(query);
                return Ok(res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }
    }
}
