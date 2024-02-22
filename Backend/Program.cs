using System.Text;
using Backend.Graphql;
using Backend.Graphql.Mutations;
using Backend.Graphql.Query;
using Domain.Graphql.Types;
using Domain.Mapper;
using Domain.Repositories.Implementations;
using Domain.Repositories.Interfaces;
using Domain.Services.Implementations;
using Domain.Services.Interfaces;
using Domain.Services.ValueTypes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<YDbContext>(
    options => options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection") 
    )
);

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserFollowsRepository, UserFollowsRepository>();
builder.Services.AddTransient<ITagRepository, TagRepository>();
builder.Services.AddTransient<IYeetRepository, YeetRepository>();
/*
builder.Services.AddScoped<IYommentRepository, YommentRepository>();
*/

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<IYeetService, YeetService>();

/*
builder.Services.AddScoped<IYommentService, YommentService>();
*/

builder.Services.AddTransient<FromMapper<Model.Entities.User, User>, MapUser>();
builder.Services.AddTransient<FromMapper<Model.Entities.Yeet, Yeet>, MapYeet>();

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

builder.Services.AddControllers();

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment())
    .AddMutationConventions(applyToAllMutations: true)
    .RegisterService<IUserService>()
    .RegisterService<ITagService>()
    .RegisterService<IYeetService>()
    .AddHttpRequestInterceptor<GetUserIdInterceptor>()
    .AddMutationType<Mutation>()
    .AddQueryType<Query>();

var app = builder.Build();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

var scope = app.Services.CreateScope();  
using var dbContext = scope.ServiceProvider.GetRequiredService<IDbContextFactory<YDbContext>>().CreateDbContext();
dbContext.Database.Migrate();

app.Run();