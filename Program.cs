using NanoCorpWebAppMVCNoAuth.Jobs;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
var supportedCultures = new[] { "en-CA", "it-IT" };

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddLocalization(opt => opt.ResourcesPath = "Resources"); //Resources folder

builder.Services.AddMvc()
    .AddMvcLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    opt.AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures)
        .SetDefaultCulture(supportedCultures[0]);
});


builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var conconcurrentJobKey = new JobKey("ConconcurrentJob");
    q.AddJob<ConconcurrentJob>(opts => opts.WithIdentity(conconcurrentJobKey));
    q.AddTrigger(opts => opts
        .ForJob(conconcurrentJobKey)
        .WithIdentity("ConconcurrentJob-trigger")
        .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(3)
            //.WithRepeatCount(10)
            .RepeatForever()
        )
    );

    var nonConconcurrentJobKey = new JobKey("NonConconcurrentJob");
    q.AddJob<NonConconcurrentJob>(opts => opts.WithIdentity(nonConconcurrentJobKey));
    q.AddTrigger(opts => opts
        .ForJob(nonConconcurrentJobKey)
        .WithIdentity("NonConconcurrentJob-trigger")
        .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(5)
            //.WithRepeatCount(10)
            .RepeatForever()
        )
    );

});

builder.Services.AddQuartzHostedService(
    q => q.WaitForJobsToComplete = true);

builder.Services.AddSignalR(options =>
{
    options.MaximumParallelInvocationsPerClient = 5;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

var localizationOptions = new RequestLocalizationOptions()
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures)
    .SetDefaultCulture(supportedCultures[0]);

app.UseRequestLocalization(localizationOptions);


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<JobsHub>("/hub");
});

//app.MapControllerRoute(
//    name: "defaultLocalized",
//    pattern: "{language=en}-{culture=CA}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
