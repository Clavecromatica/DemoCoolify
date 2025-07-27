using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DemoCoolify.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var connectionString = $"Data Source={Environment.GetEnvironmentVariable("SqlserverHost")};" +
                                   $"Initial Catalog={Environment.GetEnvironmentVariable("SqlserverDatabase")};" +
                                   $"User ID={Environment.GetEnvironmentVariable("SqlserverUser")};" +
                                   $"Password={Environment.GetEnvironmentVariable("SqlserverPassword")};" +
                                   $"TrustServerCertificate=True;Encrypt=False;";
            var cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                var query = "SELECT TOP 10 * FROM [dbo].[Users]";
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
