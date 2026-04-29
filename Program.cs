using Task_Day_2_ASP.Data.Dbcontext;
using Microsoft.EntityFrameworkCore;
using Task_Day_2_ASP.Models.Reposiotoriey;
using Microsoft.Extensions.Options;
using Microsoft.SqlServer.Server;

namespace Task_Day_2_ASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllersWithViews();
            
            builder.Services.AddScoped<IstudentRepo, StudentRepo>(); // ? ?????? ???
                                                                     // خلي AddDbContext مرة واحدة بس، والـ connection string صح:
            builder.Services.AddDbContext<LearningDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));

            var app = builder.Build();

            #region HTTP request pipeline
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
            #endregion
        }
    }
}
