using Microsoft.EntityFrameworkCore;
using MyTasker.Core.Models;

namespace MyTasker.API.Context;

public class MyTaskerContext : DbContext
{
    public MyTaskerContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TaskModel> Tasks { get; set; }
    public DbSet<SettingsModel> Settings { get; set; }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var datas = ChangeTracker.Entries<BaseModel>();
        foreach (var data in datas)
        {
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now,
            };
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
