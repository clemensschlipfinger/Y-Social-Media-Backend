using System.Text;
using Backend;
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
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Model.Configuration;

var builder = WebApplication.CreateBuilder(args);

var postgresOptions = builder.Configuration.GetRequiredSection("Postgres");
var postgresConnectionString = $"Host={postgresOptions.GetValue<string>("Server")};Database={postgresOptions.GetValue<string>("Database")};Username={postgresOptions.GetValue<string>("Username")};Password={postgresOptions.GetValue<string>("Password")}";

builder.Services.AddPooledDbContextFactory<YDbContext>(
    options => options.UseNpgsql(postgresConnectionString,
    optionsPg => optionsPg.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
    )
);

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserFollowsRepository, UserFollowsRepository>();
builder.Services.AddTransient<ITagRepository, TagRepository>();
builder.Services.AddTransient<IYeetRepository, YeetRepository>();
builder.Services.AddTransient<IYommentRepository, YommentRepository>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<IYeetService, YeetService>(); 
builder.Services.AddTransient<IYommentService, YommentService>();

builder.Services.AddTransient<FromMapper<Model.Entities.User, User>, MapUser>();
builder.Services.AddTransient<FromMapper<Model.Entities.Yeet, Yeet>, MapYeet>();
builder.Services.AddTransient<FromMapper<Model.Entities.Yomment, Yomment>, MapYomment>();

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
        ValidateIssuerSigningKey = true,
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
    .RegisterService<IYommentService>()
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