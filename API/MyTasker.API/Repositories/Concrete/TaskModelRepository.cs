using MyTasker.API.Context;
using MyTasker.API.Repositories.Abstract;
using MyTasker.Core.Models;

namespace MyTasker.API.Repositories.Concrete;

public class TaskModelRepository : GenericRepository<TaskModel>, ITaskModelRepository
{
    public TaskModelRepository(MyTaskerContext context) : base(context)
    {
    }
}
