using Blazored.LocalStorage;
using CodeBuilder.Areas.Identity;
using CodeBuilder.Data;
using CodeBuilder.Services;
using CodeBuilder.Services.Modal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContextFactory<ApplicationDbContext>(
    options => options.UseInMemoryDatabase("CodeBuilderDb"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();

builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

// Services - local db/server side blazor
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<DisplaySettingsService>();
builder.Services.AddScoped<ModalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


// Ensure the database is created and seed data is added
using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
