using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

string secretKey = builder.Configuration.GetValue<string>("Jwt:secretKey") ?? string.Empty;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<SiteProvider>();

builder.Services.AddCors(p=>p.AddPolicy("MyCors", 
    builder =>{
        builder.WithOrigins("http://localhost:3000", "http://103.27.236.138:5160", "http://103.27.236.138")
               .AllowAnyMethod()
               .AllowAnyHeader()
               //.SetIsOriginAllowed(origin => true) : cho phép tất cả các nguồn truy cập vào server
               .AllowCredentials();
    }
));
builder.Services.AddAuthentication(p=>{
    p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(p=>{
    p.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

app.MapDefaultControllerRoute();
app.MapControllers();
app.UseRouting();
app.UseCors("MyCors");
app.UseStaticFiles();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();