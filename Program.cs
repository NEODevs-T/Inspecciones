using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;

using Microsoft.EntityFrameworkCore;

using Blazored.LocalStorage;

using Inspecciones.Data;
using Inspecciones.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.AddOptions();  
builder.Services.AddAuthorizationCore();

builder.Services.AddDbContext<DbNeoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDbNeo")),ServiceLifetime.Transient
);

builder.Services.AddScoped<IDataInspeccion,DataInspeccion>();
builder.Services.AddScoped<IDataPregunta,DataPregunta>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// builder.Services.AddDbContext<DbNeoContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDbNeo")),ServiceLifetime.Transient
// );

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
