using System.ComponentModel;
using System.Data;
using App.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;

namespace App.Plugins;

public class QueryDbPlugin(AppDbContext context)
{
    [KernelFunction]
    [Description("Execute a SQL query on database")]
    public async Task<List<Dictionary<string, object>>> ExecuteQueryAsync(
        Kernel kernel,
        [Description("Query to be execute on database")] string query)
    {
        query = query.Replace("\n", " ");

        var result = new List<Dictionary<string, object>>();

        await using (SqlConnection connection =
                     new(context.Database.GetConnectionString()))
        {  
            // Create the Command and Parameter objects.
            SqlCommand command = new(query, connection);

            // Open the connection in a try/catch block.
            // Create and execute the DataReader, writing the result
            // set to the console window.
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                var item = new Dictionary<string, Object>();
                foreach (var column in reader.GetColumnSchema())
                {
                    item.Add(column.ColumnName, reader[column.ColumnName]);
                }

                if (item.Any())
                {
                    result.Add(item);
                }
            }
            reader.Close();
        }
        
        return result;
    }
}