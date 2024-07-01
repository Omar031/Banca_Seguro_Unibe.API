using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();


//Variables globales de la captura del appSetting.json

var jwt_key = builder.Configuration.GetSection("JWT:Key").Get<string>();
var jwt_Iuser = builder.Configuration.GetSection("JWT:Issuer").Get<string>();
var jwt_audience = builder.Configuration.GetSection("JWT:Audience").Get<string>();
var jwt_authority = builder.Configuration.GetSection("JWT:Authority").Get<string>();
var jwt_minutes = builder.Configuration.GetSection("JWT:Minutes").Get<string>();

//Captura la key del appsetting para el esquema de authetication del usuario.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true, 
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = jwt_audience,
            ValidIssuer = jwt_Iuser,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt_key)),
            ClockSkew = TimeSpan.Zero //default de los 5 minutos de trazos
        }; 

    });
 

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
 
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication(); // acceder al api con el permiso asumidos (roles)

app.UseAuthorization(); // dar permiso al API (Autorizacion con fecha de expíración) 

 
app.UseEndpoints(endpoint => {

    endpoint.MapControllers();
    endpoint.MapRazorPages();
});

app.UseCors();

app.Run();
