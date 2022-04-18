using DigitalSkynet.DotnetCore.DataStructures.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DigitalSkynet.DotnetCore.DataAccess.Transactions;

public static class DbContextExtensions
{
    /// <summary>
    /// Sets the created and updated dates for ITimeStamped
    /// </summary>
    /// <param name="context"></param>
    public static void OnBeforeSaving(this DbContext context)
    {
        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is ITimestamped baseEntity)
            {
                var now = DateTime.UtcNow;

                switch (entry.State)
                {
                    case EntityState.Modified:
                        baseEntity.UpdatedDate = now;
                        break;

                    case EntityState.Added:
                        baseEntity.CreatedDate = now;
                        baseEntity.UpdatedDate = now;
                        break;
                }
            }
        }
    }
}