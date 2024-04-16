using App;
using App.Data;
using App.Data.SeedData;
using App.Plugins;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddScoped<SeedManager>();
builder.Services.AddScoped<NlpToSqlPlugin>();
builder.Services.AddScoped<FormatResponsePlugin>();
builder.Services.AddScoped<QueryDbPlugin>(sp => 
    new QueryDbPlugin(sp.GetRequiredService<AppDbContext>()));

builder.Services.AddSingleton<IChatCompletionService>(sp => 
    new AzureOpenAIChatCompletionService(
        builder.Configuration["AzureOpenAi:Deployment"]!, 
        builder.Configuration["AzureOpenAi:Endpoint"]!, 
        builder.Configuration["AzureOpenAi:Key"]!));

builder.Services.AddScoped<Kernel>((sp) =>
{
    Kernel k = new(sp);
    k.Plugins.AddFromType<QueryDbPlugin>(serviceProvider: sp);
    k.Plugins.AddFromType<NlpToSqlPlugin>(serviceProvider: sp);
    k.Plugins.AddFromType<FormatResponsePlugin>(serviceProvider: sp);
    return k;
});

var host = builder.Build();
host.Run();
