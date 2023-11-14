using MyTasker.API.Context;
using MyTasker.API.Repositories.Abstract;
using MyTasker.Core.Models;

namespace MyTasker.API.Repositories.Concrete;

public class SettingsRepository : GenericRepository<SettingsModel>, ISettingsRepository
{
    public SettingsRepository(MyTaskerContext context) : base(context)
    {
    }
}
