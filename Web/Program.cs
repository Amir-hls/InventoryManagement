using Application;
using Application.IRepository;
using Application.IServices;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repository;
using Mapster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
TypeAdapterConfig.GlobalSettings.Scan(typeof(MapsterConfiguration).Assembly);
var defaultConnectionString = builder.Configuration
    .GetConnectionString("DefaultConnectionString");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(defaultConnectionString)
    .UseSnakeCaseNamingConvention();
        
});

builder.Services.AddSingleton<ISqlConnectionFactory>(provider
    => new SqlConnectionFactory(defaultConnectionString));
builder.Services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
builder.Services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
