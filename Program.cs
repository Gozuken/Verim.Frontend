using Verim.Frontend.Clients;
using Verim.Frontend.Components;

var builder = WebApplication.CreateBuilder(args);

var VerimApiUrl = builder.Configuration["VerimApiUrl"] ?? 
                  throw new Exception("Verim backend URL is not set...");

Console.WriteLine("API Url : " + VerimApiUrl);
// Add services to the container.
builder.Services.AddHttpClient<AssetsClient>(
    client => client.BaseAddress = new Uri(VerimApiUrl));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<AssetsClient>();
builder.Services.AddSingleton<CredentialsClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

    app.Run();
