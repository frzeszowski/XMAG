using MySql.Data.MySqlClient;

namespace XMAG
{
    internal class Parts
    {
        public static void InitializeDatabase()
        {
            try
            {
                using (MySqlConnection connection = new DatabaseManager("your_connection_string").GetConnection())
                {
                    connection.Open();
                    string createProductsTableQuery = @"
                CREATE TABLE IF NOT EXISTS products (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    name TEXT NOT NULL,
                    quantity INT NOT NULL,
                    uwagi TEXT
                )";
                    MySqlCommand createProductsTableCommand = new MySqlCommand(createProductsTableQuery, connection);
                    createProductsTableCommand.ExecuteNonQuery();

                    string createWellHeadPartsTableQuery = @"
                CREATE TABLE IF NOT EXISTS well_head_parts (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    name TEXT NOT NULL,
                    quantity INT NOT NULL,
                    description TEXT
                )";
                    MySqlCommand createWellHeadPartsTableCommand = new MySqlCommand(createWellHeadPartsTableQuery, connection);
                    createWellHeadPartsTableCommand.ExecuteNonQuery();
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
        public static void UpdateData()
        {
            try
            {
                using (MySqlConnection connection = new DatabaseManager("your_connection_string").GetConnection())
                {
                    connection.Open();
                    string updateWellHeadPartsTableQuery = @"
                UPDATE well_head_parts
                SET name = 'Dławik M20x1,5'
                WHERE id = 1;

                UPDATE well_head_parts
                SET name = 'Korek M20x1,5'
                WHERE id = 2;

                UPDATE well_head_parts
                SET name = 'Śruba M8x55 inox'
                WHERE id = 3;

                UPDATE well_head_parts
                SET name = 'Śruba M8x25 inox'
                WHERE id = 4;

                UPDATE well_head_parts
                SET name = 'Podkładka M8 duża inox'
                WHERE id = 5;

                UPDATE well_head_parts
                SET name = 'Podkładka M8 mała inox'
                WHERE id = 6;

                UPDATE well_head_parts
                SET name = 'Ucho z gwintem M8 inox'
                WHERE id = 7;

                UPDATE well_head_parts
                SET name = 'Ucho z nakrętką M8 inox'
                WHERE id = 8;

                UPDATE well_head_parts
                SET name = 'Wkładka dwu gwint. M8 na 12 mm dł. 15 mm'
                WHERE id = 9;

                UPDATE well_head_parts
                SET name = 'Wkładka dwu gwint. M10 na 12 mm dł. 20 mm'
                WHERE id = 10;

                UPDATE well_head_parts
                SET name = 'Podkładka M8 mała ocynk'
                WHERE id = 11;

                UPDATE well_head_parts
                SET name = 'Nakrętka M8 ocynk'
                WHERE id = 12;

                UPDATE well_head_parts
                SET name = 'Ucho ze śrubą M8 ocynk'
                WHERE id = 13;

                UPDATE well_head_parts
                SET name = 'Nakrętka z uchem M8 ocynk'
                WHERE id = 14;
                ";

                    MySqlCommand createWellHeadPartsTableCommand = new MySqlCommand(updateWellHeadPartsTableQuery, connection);
                    createWellHeadPartsTableCommand.ExecuteNonQuery();
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
    }
}
