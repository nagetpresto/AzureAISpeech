using AzureAISpeech.Client.Components;
using AzureAISpeech.Server.Repositories.Master.Interfaces;
using AzureAISpeech.Server.Repositories.Master;
using AzureAISpeech.Server.Services.Master.Interfaces;
using AzureAISpeech.Server.Services.Master;
using AzureAISpeech.Server.Repositories.Speech.Interfaces;
using AzureAISpeech.Server.Repositories.Speech;
using AzureAISpeech.Server.Services.Speech.Interfaces;
using AzureAISpeech.Server.Services.Speech;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// master
builder.Services.AddScoped<IMasterLanguagesRepository, MasterLanguagesRepository>();
builder.Services.AddScoped<IMasterLanguagesService, MasterLanguagesService>();

//speech
builder.Services.AddScoped<ITTSRepository, TTSRepository>();
builder.Services.AddScoped<ITTSService, TTSService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
