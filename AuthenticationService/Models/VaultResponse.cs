using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Npgsql;

public class VaultResponse
{
    public Data? data { get; set; }

    public class Data
    {
        public string? username { get; set; }
        public string? password { get; set; }
    }
}

public class Settings
{
    public VaultSettings? VaultSettings { get; set; }
    public PostgresSettings? PostgresSettings { get; set; }
}

public class VaultSettings
{
    public string? VaultUrl { get; set; }
    public string? VaultToken { get; set; }
}

public class PostgresSettings
{
    public string? Host { get; set; }
    public int? Port { get; set; }
    public string? Database { get; set; }
}

class Program
{
    private static readonly HttpClient client = new HttpClient();

    // Refactored function to accept table_name and limit as parameters
    static async Task QueryPostgres(string table_name, int limit)
    {
        try
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            // Bind the VaultSettings from appsettings.json
            var settings = config.GetSection("Settings").Get<Settings>();

            // Step 1: Make the GET request to Vault
            client.DefaultRequestHeaders.Add("X-Vault-Token", settings.VaultSettings.VaultToken);
            var response = await client.GetStringAsync(settings.VaultSettings.VaultUrl);

            // Step 2: Deserialize the response
            var vaultResponse = JsonConvert.DeserializeObject<VaultResponse>(response);
            if (vaultResponse?.data != null) // Null check before accessing data
            {
                var username = vaultResponse.data.username;
                var password = vaultResponse.data.password;

                // Step 3: Connect to PostgreSQL using the retrieved credentials
                string connectionString = $"Host={settings.PostgresSettings.Host};Port={settings.PostgresSettings.Port};Username={username};Password={password};Database={settings.PostgresSettings.Database}";

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    // Step 4: Perform a query (example query)
                    string query = $"SELECT * FROM {table_name} LIMIT {limit};";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write($"{reader.GetName(i)}: {reader.GetValue(i)} ");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Vault response data is missing.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static async Task Main(string[] args)
    {
        // Example usage of the QueryPostgres function with table_name and limit as parameters
        await QueryPostgres("test_table", 10);
    }
}
