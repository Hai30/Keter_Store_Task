using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;


namespace YourProjectName.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("{getproduct}")]
        public IActionResult GetProduct(int productCode)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=<path_to_your_mdf_file>;Integrated Security=True";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("GetProduct", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductCode", productCode);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var product = new
                            {
                                ProductCode = reader.GetInt32(0),
                                ProductName = reader.GetString(1),
                                ProductDescription = reader.GetString(2),
                                StartSellingDate = reader.GetDateTime(3)
                            };
                            return Ok(product);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
        }


        [HttpGet("{deleteproduct}")]
        public IActionResult DeleteProduct(int productCode)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=<path_to_your_mdf_file>;Integrated Security=True";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DeleteProduct", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductCode", productCode);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                }
            }
        }
    }
}
