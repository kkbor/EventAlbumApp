using Amazon.S3;
using EventAlbumApp.Connetion;
using EventAlbumApp.Services.Implementation;
using EventAlbumApp.Services.Implementations;
using EventAlbumApp.Services.Interface;
using EventAlbumApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Controllers + CORS

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

#endregion

#region DB (EF Core SQL Server)

builder.Services.AddDbContext<AppdbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

#endregion

#region Services (DI)

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<R2UrlService>();

#endregion

#region S3 / Cloudflare R2 (WORKING CONFIG)

builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();

    var s3Config = new AmazonS3Config
    {
        ServiceURL = config["S3:ServiceUrl"],
        ForcePathStyle = true
    };

    return new AmazonS3Client(
        config["S3:AccessKey"],
        config["S3:SecretKey"],
        s3Config
    );
});

#endregion

#region JWT AUTH

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = jwtSettings["Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key!)
        ),

        NameClaimType = ClaimTypes.NameIdentifier
    };
});

builder.Services.AddAuthorization();

#endregion

var app = builder.Build();

#region Middleware pipeline

app.UseCors("AllowReact");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();