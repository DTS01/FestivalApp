using FestivalApp.Server.Authentication;
using FestivalApp.Server.Repository;
using FestivalApp.Shared.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB
var mongoDbConnectionString = "mongodb+srv://eaa23tsd:HofteUdhus30@cluster0.cha8yhm.mongodb.net/";
var mongoClient = new MongoClient(mongoDbConnectionString);
var database = mongoClient.GetDatabase("MusikDB");

// Register MongoDB Services
builder.Services.AddSingleton<IMongoClient>(mongoClient);
builder.Services.AddSingleton(database);

// Register Repository Services
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IShiftRepository, ShiftRepository>();

// DbContext as a singleton service
builder.Services.AddSingleton<DataStorage>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<UserAccountService>();


builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtAuthenticationManager.JWT_SECURITY_kEY)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:7224", "http://localhost:5229")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseCors("MyCorsPolicy");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
