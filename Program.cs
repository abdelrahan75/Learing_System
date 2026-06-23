using Task_Day_2_ASP.Data.Dbcontext;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Options;
using Microsoft.SqlServer.Server;
using Task_Day_2_ASP.Models.Reposiotoriey.RepoTeachers;
using Task_Day_2_ASP.Models.Reposiotoriey.RepoStudent;
using Task_Day_2_ASP.Models.Reposiotoriey.RepoCourses;
using Task_Day_2_ASP.Models.Reposiotoriey.RepoDepaatment;
using Task_Day_2_ASP.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Task_Day_2_ASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllersWithViews();

            // ── Repositories ─────────────────────────────────────────────────
            builder.Services.AddScoped<IstudentRepo, StudentRepo>();
            builder.Services.AddScoped<ICourseRepo, CoursesRepo>();
            builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            builder.Services.AddScoped<ITeacherRepo, TeacherRepo>();

            // ── Database ─────────────────────────────────────────────────────
            builder.Services.AddDbContext<LearningDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));

            // ── Session & Cookies ─────────────────────────────────────────────
            // AddDistributedMemoryCache is REQUIRED before AddSession
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;   // Not accessible via JS
                options.Cookie.IsEssential = true;   // Required even without consent
                options.Cookie.Name = ".LearningSystem.Session";
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // ── HttpContextAccessor (needed to access Session outside controllers)
            builder.Services.AddHttpContextAccessor();

            // ── Antiforgery Token (CSRF protection) ───────────────────────────
            builder.Services.AddAntiforgery(options =>
            {
                options.Cookie.Name = ".LearningSystem.Antiforgery";
                options.Cookie.HttpOnly = true;
            });

            builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<LearningDbContext>();

            var app = builder.Build();

            // ── Middleware Pipeline ───────────────────────────────────────────
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();   // Must be before UseSession
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}
