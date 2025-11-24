using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkoutPlannerMVC.Data;
using WorkoutPlannerMVC.Services;
using WorkoutPlannerMVC.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WorkoutPlannerMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WorkoutPlannerMVCContext") ?? throw new InvalidOperationException("Connection string 'WorkoutPlannerMVCContext' not found.")));

builder.Services.AddHealthChecks()
    .AddCheck<DbCheck>("DbCheck");



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IWorkoutRepCounterService, WorkoutRepCounterService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();


var app = builder.Build();

app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    ResponseWriter = DbCheck.WriteResponse
});



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<WorkoutPlannerMVCContext>();


    // Seed data
    //SeedData.Initialize(context);
}


app.Run();
