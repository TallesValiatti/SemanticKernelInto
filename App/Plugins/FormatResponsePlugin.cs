using System.ComponentModel;
using System.Data;
using App.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;

namespace App.Plugins;

public class FormatResponsePlugin
{
    [KernelFunction]
    [Description("Define a friendly response to the user, based on the query result and original request")]
    public async Task<string> ExecuteQueryAsync(
        Kernel kernel,
        [Description("Define the query result")] string queryResult)
    {
        var result = await kernel.InvokePromptAsync(
            """
            The query result was
            ---
            {{$queryResult}}
            ---
            
            Your goal is to create a response to the end user based on original request.
            The response should be formulated based on the information returned from the database and the original user input.
            Don't show any Id on the response. Instead, provide other information as 'CreateAt' and Names
            
            Ex: 
            Response: [{'Item': "Chocolate bar"}]
            Message -> According to the database the most sold item was Chocolate bar.
            
            Response: [{'Sale': "6790c600-7c75-4ef8-97fb-04f3e6f30f8a", 'CreatedAt' : "2024-12-1T12:15:00", 'value' : "34.25"}]
            Message -> According to the data sale with highest value was created at January 12th, with a total value of $34.25.
            
            """ 
            , 
            new() 
            {
                { "queryResult", queryResult }
            });
        
        return result.ToString();   
    }
}