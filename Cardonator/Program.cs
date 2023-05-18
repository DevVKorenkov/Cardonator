using Cardonator.Data.DataContext;
using Cardonator.Data.Repositories;
using Cardonator.Data.Repositories.Abstrations;
using Cardonator.Data.Services;
using Cardonator.Data.Services.Abstractions;
using Cardonator.Models.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cardonator", Version = "v1" });
});

services.AddControllers();
services.AddDbContext<CardsDataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
});

services
    .AddIdentity<CardonatorUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<CardsDataContext>();

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.Cookie.Name = "CardinatorAuth";
        opt.ExpireTimeSpan = TimeSpan.FromDays(3);
    });

services.AddAuthorization();

services.AddScoped<ICardRepository, CardRepository>();
services.AddScoped<ICardCollectionsRepository, CardCollectionsRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IUserService, UserService>();
services.AddTransient<ISaveContext, SaveContext>();
services.AddTransient<IAuthService, AuthService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cardonator_v1");
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();
