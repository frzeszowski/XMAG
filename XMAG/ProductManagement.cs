using MySql.Data.MySqlClient;

namespace XMAG
{
    public static class ProductManagement
    {
        public static void InitializeDatabase()
        {
            try
            {
                using (MySqlConnection connection = new DatabaseManager("Server=sql11.freemysqlhosting.net;Port=3306;Database=sql11684411;Uid=sql11684411;Pwd=LBLperHwjK;").GetConnection())
                {
                    connection.Open();
                    // SQL command to create the "products" table
                    string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS products (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    name TEXT NOT NULL,
                    quantity INT NOT NULL,
                    uwagi TEXT
                )";
                    MySqlCommand command = new MySqlCommand(createTableQuery, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("MySQL Error: " + ex.Message);
                throw; // Re-throw the exception to propagate it up the call stack
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }
        // Method to add a new product to the database
        public static void DodajNowyProdukt(string name, int quantity, string uwagi)
        {
            try
            {
                using (MySqlConnection connection = new DatabaseManager("Server=sql11.freemysqlhosting.net;Port=3306;Database=sql11684411;Uid=sql11684411;Pwd=LBLperHwjK;").GetConnection())
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO products (name, quantity, uwagi) VALUES (@name, @quantity, @uwagi)";
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@uwagi", uwagi);
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("MySQL Error: " + ex.Message);
                // You can log the error or handle it appropriately
                throw; // Re-throw the exception to propagate it up the call stack
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Handle other types of exceptions here
                throw;
            }
        }


        // Method to delete a selected product from the database
        public static void UsunWybranyProdukt(int productId)
        {
            using (MySqlConnection connection = new DatabaseManager("Server=sql11.freemysqlhosting.net;Port=3306;Database=sql11684411;Uid=sql11684411;Pwd=LBLperHwjK;").GetConnection())
            {
                try
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM products WHERE id = @id";
                    MySqlCommand command = new MySqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@id", productId);
                    command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("MySQL Error: " + ex.Message);
                    // You can log the error or handle it appropriately
                    throw; // Re-throw the exception to propagate it up the call stack
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    // Handle other types of exceptions here
                    throw;
                }
            }
        }

        // Method to update a product in the database
        public static void EdytujProdukt(int productId, string newName, int newQuantity, string newUwagi)
        {
            using (MySqlConnection connection = new DatabaseManager("Server=sql11.freemysqlhosting.net;Port=3306;Database=sql11684411;Uid=sql11684411;Pwd=LBLperHwjK;").GetConnection())
            {
                try
                {
                    connection.Open();
                    string updateQuery = "UPDATE products SET name = @name, quantity = @quantity, uwagi = @uwagi WHERE id = @id";
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@id", productId);
                    command.Parameters.AddWithValue("@name", newName);
                    command.Parameters.AddWithValue("@quantity", newQuantity);
                    command.Parameters.AddWithValue("@uwagi", newUwagi);
                    command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("MySQL Error: " + ex.Message);
                    // You can log the error or handle it appropriately
                    throw; // Re-throw the exception to propagate it up the call stack
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    // Handle other types of exceptions here
                    throw;
                }
            }
        }

        // Method to retrieve products from the database
        public static List<Product> PokazProdukty()
        {
            List<Product> products = new List<Product>();
            using (MySqlConnection connection = new DatabaseManager("Server=sql11.freemysqlhosting.net;Port=3306;Database=sql11684411;Uid=sql11684411;Pwd=LBLperHwjK;").GetConnection())
            {

                try
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM products";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Name = Convert.ToString(reader["name"]),
                                Quantity = Convert.ToInt32(reader["quantity"]),
                                Uwagi = Convert.ToString(reader["uwagi"])
                            };
                            products.Add(product);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("MySQL Error: " + ex.Message);
                    // You can log the error or handle it appropriately
                    throw; // Re-throw the exception to propagate it up the call stack
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    // Handle other types of exceptions here
                    throw;
                }
            }
            return products;
        }
    }
}
