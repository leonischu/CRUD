using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProductManager.Models;

namespace ProductManager.Controllers
{
    public class ProductController : Controller
    {
        private readonly string? _connectionString;
        public ProductController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Index()
        {

            var sql = "SELECT * FROM PRODUCTS";
            var products = new List<Product>();

            // Now Create a sql connection 

            using(var connection = new SqlConnection(_connectionString))
            {
                //Now open this Connections 
                connection.Open();

                //now create the sql command in this we should pass two parameters sql query and sql connection ))

                using (var cmd = new SqlCommand(sql, connection))
                {
                    //now execute this command
                    using (var reader = cmd.ExecuteReader()) { 
                    
                        //now iterate through the rerader and creating new products object 
                        while(reader.Read())
                        {
                            var product = new Product
                            {
                                ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                                ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
                                CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                                UnitsInStock = reader.GetInt32(reader.GetOrdinal("UnitsInStock")),
                                UnitsOnOrder = reader.GetInt32(reader.GetOrdinal("UnitsOnOrder")),
                                Discontinued = reader.GetBoolean(reader.GetOrdinal("Discontinued"))
                                //DiscontinuedDate = reader.GetDateTime(reader.GetOrdinal("DiscontinuedDate"))

                            };
                            products.Add(product); 
                        }


                    
                    
                    
                    }

                }

            }


            return View(products);
        }
    }
}
