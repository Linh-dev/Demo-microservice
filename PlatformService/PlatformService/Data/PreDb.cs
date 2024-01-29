using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PreDb
    {
        public static void PrePopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }

        }
        private static void SeedData(AppDbContext context)
        {
            if(!context.Platforms.Any())
            {
                Console.WriteLine("--> seeding data...");

                context.Platforms.AddRange(
                    new Platform() { Name="Dot net", Publisher="Microsoft", Cost="Free"},
                    new Platform() { Name="Java", Publisher="Oracel", Cost="Free"},
                    new Platform() { Name="Python", Publisher="Other", Cost="Free"}
                    );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }

    }
}
