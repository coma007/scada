using Microsoft.Extensions.Options;
using MongoDB.Driver;
using scada_back.Alarm;
using scada_back.AlarmHistory;
using scada_back.Database;
using scada_back.DriverState;
using scada_back.Exception.Filter;
using scada_back.Tag;
using scada_back.Tag.Model.Converter;
using scada_back.TagHistory;
using scada_back.User;
using scada_back.Validation;

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

builder.Services.AddScoped<IAlarmHistoryRecordRepository, AlarmHistoryRecordRepository>();
builder.Services.AddScoped<IAlarmHistoryRecordService, AlarmHistoryRecordService>();

builder.Services.AddScoped<IDriverStateRepository, DriverStateRepository>();
builder.Services.AddScoped<IDriverStateService, DriverStateService>();


builder.Services.AddScoped<IValidationService, ValidationService>();

builder.Services.AddControllers(options => 
        options.Filters.Add<GlobalExceptionFilter>())
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new TagDtoConverter());
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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