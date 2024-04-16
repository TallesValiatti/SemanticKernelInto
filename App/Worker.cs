using App.Data.SeedData;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace App;

public class Worker(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        
        SeedManager seedManager =
            scope.ServiceProvider.GetRequiredService<SeedManager>();

        await seedManager.SeedAsync();
        
        var kernel = scope.ServiceProvider.GetRequiredService<Kernel>();
        
        IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
        
        // Create the chat history
        ChatHistory chatMessages = new ChatHistory("""
            You are an expert at writing SQL queries regarding sales.
            If the request is a query to be executed in the database, before return a answer to the user with the data, create a friendly response message.
            Query the database to any additional information about the entities
            """);
      
        while (!stoppingToken.IsCancellationRequested)
        {
            System.Console.Write("User > ");
            chatMessages.AddUserMessage(Console.ReadLine()!);

            OpenAIPromptExecutionSettings openAiPromptExecutionSettings = new()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };
            var result = chatCompletionService.GetStreamingChatMessageContentsAsync(
                chatMessages,
                executionSettings: openAiPromptExecutionSettings,
                kernel: kernel);

            System.Console.Write("Assistant > ");
            
            string fullMessage = "";
            await foreach (var content in result)
            {
                System.Console.Write(content.Content);
                fullMessage += content.Content;
            }
            System.Console.WriteLine();

            chatMessages.AddAssistantMessage(fullMessage);
        }
    }
}