using System.Collections.Specialized;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using scada_back.Api.ApiKey;
using scada_back.Api.WebSocket;
using scada_back.Infrastructure.Database;
using scada_back.Infrastructure.Exception.Filter;
using scada_back.Infrastructure.Feature.Alarm;
using scada_back.Infrastructure.Feature.AlarmHistory;
using scada_back.Infrastructure.Feature.DriverState;
using scada_back.Infrastructure.Feature.Tag;
using scada_back.Infrastructure.Feature.Tag.Model.Converter;
using scada_back.Infrastructure.Feature.TagHistory;
using scada_back.Infrastructure.Feature.User;
using scada_back.Infrastructure.Validation;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ScadaDatabaseSettings>(builder.Configuration.GetSection(nameof(ScadaDatabaseSettings)));
builder.Services.AddSingleton<IScadaDatabaseSettings>(
    s => s.GetRequiredService<IOptions<ScadaDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(_ =>
    new MongoClient(builder.Configuration.GetValue<string>("ScadaDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddScoped<ITagHistoryRepository, TagHistoryRepository>();
builder.Services.AddScoped<ITagHistoryService, TagHistoryService>();

builder.Services.AddScoped<IAlarmRepository, AlarmRepository>();
builder.Services.AddScoped<IAlarmService, AlarmService>();

builder.Services.AddScoped<IAlarmHistoryRepository, AlarmHistoryRepository>();
builder.Services.AddScoped<IAlarmHistoryService, AlarmHistoryService>();
builder.Services.AddScoped<IAlarmHistoryLogger, AlarmHistoryLogger>();

builder.Services.AddScoped<IDriverStateRepository, DriverStateRepository>();
builder.Services.AddScoped<IDriverStateService, DriverStateService>();

builder.Services.AddScoped<IValidationService, ValidationService>();

builder.Services.AddScoped<IWebSocketServer, WebSocketServer>();

builder.Services.AddControllers(options => 
        options.Filters.Add<GlobalExceptionFilter>())
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new TagDtoConverter());
    });
builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(RequireApiKeyAttribute));
});
builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme(\"Bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("Authorization:Token").Value)),
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

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseRouting();

app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();