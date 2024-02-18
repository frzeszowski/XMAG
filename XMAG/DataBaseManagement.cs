using MySql.Data.MySqlClient;

public class DatabaseManager
{
    private string connectionString;

    public DatabaseManager(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }

    // Other database management methods...
}
