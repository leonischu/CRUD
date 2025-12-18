using CollegeApp.Configurations;
using CollegeApp.Data;
using CollegeApp.Data.Repository;
using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);






// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMyLogger, LogToFile>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient(typeof(ICollegeRepository<>), typeof(CollegeRepository<>));
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddDbContext<CollegeDBContext>(options =>
{ 

    options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDBConnection"));
}

);

builder.Services.AddCors(option =>
{

    option.AddDefaultPolicy( policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();


    });



    //option.AddPolicy("AllowAll", policy =>
    //{
    //    // allow all origins
    //    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

    //    // Allow only few origins
    //    //policy.WithOrigins("http://localhost:4200");

    //});
    option.AddPolicy("AllowOnlyLocalhost", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

      
    });
    option.AddPolicy("AllowOnlyGoogle", policy =>
    {
        policy.WithOrigins("http://google.com","http://gmail.com","http://gmail.com");

      
    });
    option.AddPolicy("AllowOnlyMicrosoft", policy =>
    {
        policy.WithOrigins("https://Microsoft.com","http://Onedrive.com","http://outlook.com");

      
    });




});
var keyJWTSecretforGoogle = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretforGoogle"));
var keyJWTSecretforMicrosoft = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretforMicrosoft"));
var keyJWTSecretforLocal = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretforLocal"));





// JWT Authentication Configuration .Yo halena vane [Authorize] le kaam gardaina 
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("LoginForGoogleUsers",options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()

    {
        ValidateIssuerSigningKey = true,    // this validates the signing key
        IssuerSigningKey = new SymmetricSecurityKey(keyJWTSecretforGoogle),
        ValidateIssuer = false,
        ValidateAudience = false,
    };


}).AddJwtBearer("LoginForMicrosoftUsers", options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()

    {
        ValidateIssuerSigningKey = true,    // this validates the signing key
        IssuerSigningKey = new SymmetricSecurityKey(keyJWTSecretforMicrosoft),
        ValidateIssuer = false,
        ValidateAudience = false,
    };


}).AddJwtBearer("LoginForLocalUsers", options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()

    {
        ValidateIssuerSigningKey = true,    // this validates the signing key
        IssuerSigningKey = new SymmetricSecurityKey(keyJWTSecretforLocal),
        ValidateIssuer = false,
        ValidateAudience = false,
    };


});



var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors();      // property for using a CORS

    app.UseAuthorization();

    app.MapControllers();

    app.Run();


app.UseEndpoints(endpoints =>
{

    endpoints.MapGet("api/testendpoint2",
        context => context.Response.WriteAsync(builder.Configuration.GetValue<string>("JWTSecret")));
});