using System.Text;
using Backend.Graphql.Mutations;
using Backend.Graphql.Query;
using Backend.Identity;
using Domain.Repositories.Implementations;
using Domain.Repositories.Interfaces;
using Domain.Services.Implementations;
using Domain.Services.Interfaces;
using HotChocolate.Authorization;
using HotChocolate.Resolvers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using Model.Configuration;
using Model.Entities;
using IAuthorizationHandler = Microsoft.AspNetCore.Authorization.IAuthorizationHandler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<YDbContext>(
    options => options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection") 
    )
);

builder.Services.AddScoped<IRegexService, RegexService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserFollowsRepository, UserFollowsRepository>();
builder.Services.AddScoped<IYeetRepository, YeetRepository>();
builder.Services.AddScoped<IYommentRepository, YommentRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();

builder.Services.AddScoped<IYeetService, YeetService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IYommentService, YommentService>();

var jwtOptionsSection = builder.Configuration.GetRequiredSection("Jwt");
builder.Services.Configure<JwtOptions>(jwtOptionsSection);
builder.Services.AddTransient<IJwtService, JwtService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>
{
    var configKey = jwtOptionsSection["Key"];
    var key = Encoding.UTF8.GetBytes(configKey);

    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtOptionsSection["Issuer"],
        ValidAudience = jwtOptionsSection["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true
    };
});

/*
builder.Services.AddAuthorization(options =>
{
   options.AddPolicy("AddFollowPolicy", policy =>
        policy.Requirements.Add(new IsUserRequirement("slaveId"))); 
});
*/

//builder.Services.AddTransient<IAuthorizationHandler, IsUserHandler>();

builder.Services.AddControllers();

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddMutationType<Mutation>()
    .AddMutationConventions(applyToAllMutations: true)
    .AddQueryType<Query>();

var app = builder.Build();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

var scope = app.Services.CreateScope();  
using var dbContext = scope.ServiceProvider.GetRequiredService<YDbContext>();
dbContext.Database.Migrate();

app.Run();