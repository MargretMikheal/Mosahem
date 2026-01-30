using Microsoft.EntityFrameworkCore;
using mosahem.Domain.Enums;
using Mosahem.Application.Interfaces.Security;

namespace mosahem.Persistence.Seeds
{
    public static class ContextSeed
    {
        public static async Task SeedAsync(MosahmDbContext context, IPasswordHasher passwordHasher)
        {
            try
            {
                if (context.Database.IsSqlServer())
                {
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            if (!await context.Users.AnyAsync(u => u.Role == UserRole.Admin))
            {
                var admin = SeedHelper.GetAdminUser(passwordHasher);
                await context.Users.AddAsync(admin);
                await context.SaveChangesAsync();
            }

            if (!await context.Fields.AnyAsync())
            {
                var fields = SeedHelper.GetFields();
                await context.Fields.AddRangeAsync(fields);
                await context.SaveChangesAsync();
            }

            if (!await context.Skills.AnyAsync())
            {
                var skills = SeedHelper.GetSkills();
                await context.Skills.AddRangeAsync(skills);
                await context.SaveChangesAsync();
            }

            if (!await context.Governorates.AnyAsync())
            {
                var governorates = GovernorateData.GetGovernoratesWithCities();

                await context.Governorates.AddRangeAsync(governorates);
                await context.SaveChangesAsync();
            }
        }
    }
}