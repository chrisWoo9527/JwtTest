using Identity.Sql.Data;
using Identity.Sql.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using �йܷ���Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<HostedServiceTest1>();
builder.Services.AddHostedService<SechemServices>();

#region ע��SQLServer ����

builder.Services.AddDbContext<MirDbContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("default");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new ArgumentException("������Ϣ����,����ϵ����Ա�˲�~~~");
    }

    opt.UseSqlServer(connectionString);

});

#endregion

#region  ע�� Identity �ķ���

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
