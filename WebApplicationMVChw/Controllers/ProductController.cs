using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace WebApplicationMVChw.Controllers
{
    public class ProductController : Controller
    {
        private readonly string _connectionString;

        public ProductController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Index()
        {
            List<object> products = new List<object>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Products", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new
                    {
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = (decimal)reader["Price"]
                    });
                }
            }

            return Json(products);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name, string description, decimal price)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(
                    "INSERT INTO Products (Name, Description, Price) VALUES (@Name, @Description, @Price)",
                    connection);

                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Price", price);

                command.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search(string keyword)
        {
            List<object> products = new List<object>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(
                    "SELECT * FROM Products WHERE Name LIKE @Keyword",
                    connection);

                command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = (decimal)reader["Price"]
                    });
                }
            }

            return Json(products);
        }

        public IActionResult Details(int id)
        {
            object product = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(
                    "SELECT * FROM Products WHERE Id = @Id",
                    connection);

                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    product = new
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = (decimal)reader["Price"]
                    };
                }
            }

            if (product == null)
                return NotFound();

            return Json(product);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            object deletedProduct = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand selectCommand = new SqlCommand(
                    "SELECT * FROM Products WHERE Id = @Id",
                    connection);

                selectCommand.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = selectCommand.ExecuteReader();

                if (reader.Read())
                {
                    deletedProduct = new
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = (decimal)reader["Price"]
                    };
                }

                reader.Close();

                if (deletedProduct != null)
                {
                    SqlCommand deleteCommand = new SqlCommand(
                        "DELETE FROM Products WHERE Id = @Id",
                        connection);

                    deleteCommand.Parameters.AddWithValue("@Id", id);
                    deleteCommand.ExecuteNonQuery();
                }
            }

            if (deletedProduct == null)
                return NotFound();

            return Json(deletedProduct);
        }
    }
}
