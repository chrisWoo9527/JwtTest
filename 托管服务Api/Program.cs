using Identity.Sql.Data;
using Identity.Sql.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using 托管服务Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<HostedServiceTest1>();
builder.Services.AddHostedService<SechemServices>();

#region 注入SQLServer 服务

builder.Services.AddDbContext<MirDbContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("default");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new ArgumentException("配置信息错误,请联系管理员核查~~~");
    }

    opt.UseSqlServer(connectionString);

});

#endregion

#region  注入 Identity 的服务

builder.Services.AddDataProtection();
builder.Services.AddIdentityCore<User>(opt =>
{
    opt.Password.RequireUppercase = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 6;
    opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
});

IdentityBuilder identityBuilder = new IdentityBuilder(typeof(User), typeof(Role), builder.Services);
identityBuilder.AddEntityFrameworkStores<MirDbContext>().
    AddDefaultTokenProviders().
    AddRoleManager<RoleManager<Role>>().
    AddUserManager<UserManager<User>>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
