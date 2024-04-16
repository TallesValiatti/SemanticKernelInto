using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace App.Plugins;

public class NlpToSqlPlugin()
{
    [KernelFunction]
    [Description("Write SQL queries given a Natural Language description")]
    [return: Description("The generated SQL query ")]
    public async Task<string> ConvertNlpToSqlAsync(
        Kernel kernel,
        [Description("Define the Natural Language input request")] string request)
    {
        var result = await kernel.InvokePromptAsync(
            """
            You are an SQL expert at writing queries. You must write queries to fetch sales data from the database 
            ---
            {{$request}}
            ---

            Create SQL queries that is compatible with Transact-SQL and fulfill the requirements.
            You must use only the following tables:

            CREATE TABLE [dbo].[Products]([Id] [uniqueidentifier] NOT NULL, [Name] [nvarchar](max) NOT NULL, [UnitPrice] [decimal](18, 2) NOT NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
            CREATE TABLE [dbo].[Sales]([Id] [uniqueidentifier] NOT NULL, [CreatedAt] [datetime2](7) NOT NULL) ON [PRIMARY]
            CREATE TABLE [dbo].[SaleItems]([Id] [uniqueidentifier] NOT NULL, [ProductId] [uniqueidentifier] NOT NULL, [SaleId] [uniqueidentifier] NOT NULL, [UnitPrice] [decimal](18, 2) NOT NULL, [Units] [int] NOT NULL) ON [PRIMARY]

            The total amount of sale is equals to SaleItem.UnitPrice * SaleItem.Unit
            
            Use the following format to return the SQL query in Transact-SQL dialect:
            T-SQL: SELECT * FROM table_name;
            T-SQL: 
            """ 
            , new() {
            { "request", request }
        });

        // Return the plan back to the agent
        
        Console.WriteLine($"Query to be executed: {Environment.NewLine}{result}");
        Console.WriteLine();
        
        return result.ToString();
    }
}