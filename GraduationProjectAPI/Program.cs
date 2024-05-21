using GraduationProjectAPI.DAL.Database;
using Microsoft.EntityFrameworkCore;
using GraduationProjectAPI.BL.AutoMapper;
using Microsoft.AspNetCore.Identity;
using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.BL.Repos;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Hangfire.SqlServer;
using GraduationProjectAPI.BL.BackgroindJob;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextPool<DataContext>(opt =>
{

    opt.UseSqlServer(builder.Configuration.GetConnectionString("GraduationProject"));
    



});



// Add Hangfire server and dashboard (optional)

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<DataContext>();
builder.Services.AddMvc()
        .ConfigureApiBehaviorOptions(options => {
            options.SuppressModelStateInvalidFilter = true;
        }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

builder.Services.AddAutoMapper(opt => opt.AddProfile(new MyProfile()));
builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    opt.SerializerSettings.MaxDepth = 86;
    opt.SerializerSettings.NullValueHandling=Newtonsoft.Json.NullValueHandling.Ignore;
});
//var options = new SqlServerStorageOptions
//{
//    InvisibilityTimeout = TimeSpan.FromDays(100) // default value
//};


//builder.Services.AddHangfire(con =>
//{

//    con.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
//    con.UseSimpleAssemblyNameTypeSerializer();
//    con.UseRecommendedSerializerSettings();
//    con.UseSqlServerStorage("Server=db4689.public.databaseasp.net; Database=db4689; User Id=db4689; Password=bB_2Y#7r+pJ9; Encrypt=False; MultipleActiveResultSets=True;");
    

//});



;

// Add the processing server as IHostedService
//builder.Services.AddHangfireServer();

builder.Services.AddScoped<IPatient, PatientRepo>();
builder.Services.AddScoped<IMedicine, MedicineRepo>();
builder.Services.AddScoped<IPrescription, PrescriptionRepo>();
builder.Services.AddScoped<IDoctor, DoctorRepo>();
builder.Services.AddScoped<IPharmacist, PharmacistRepo>();
builder.Services.AddScoped<IOrderHistories, OrderHistoryRepo>();

builder.Services.AddScoped<IMedicineOfPrescription, MedicineOfPrescriptionRepo>();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(opt =>
    {
        opt.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddHostedService<SendNotificationJob>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
//app.UseHangfireDashboard("/Dashboard");
//app.UseHangfireServer();
//RecurringJob.AddOrUpdate<SendNotificationJob>((opt) =>opt.SendNotification() , Cron.Minutely);

app.MapControllers();

app.Run();
