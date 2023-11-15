using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTasker.API.Repositories.Abstract;
using MyTasker.Core.Models;

namespace MyTasker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskModelRepository _taskModelRepository;

    public TaskController(ITaskModelRepository taskModelRepository)
    {
        _taskModelRepository = taskModelRepository;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllTask()
    {
        var result = await _taskModelRepository.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("GetAll/{status}")]
    public async Task<IActionResult> GetAllTask(int status)
    {
        var result = await _taskModelRepository.GetAllAsync(x => (int)x.Status == status);

        return Ok(result);
    }

    [HttpGet("GetAllSearch/{search}")]
    public async Task<IActionResult> GetAllTask(string search)
    {
        search = search.ToLower();
        var result = await _taskModelRepository.GetAllAsync(x => x.Title.ToLower().Contains(search) ||
                                                                 x.Content.ToLower().Contains(search));

        return Ok(result);
    }

    [HttpGet("GetAllWithToday")]
    public async Task<IActionResult> GetAllWithDayTask()
    {
        var result = await _taskModelRepository.GetAllAsync(x => x.TaskDate.Date == DateTime.Now.Date);
        var temp = DateTime.Now.Date.ToString();
        return Ok(result);
    }

    [HttpGet("GetAllWithDay")]
    public async Task<IActionResult> GetAllWithDayTask([FromQuery]string date)
    {
        if (DateTime.TryParse(date, out DateTime dateTime))
        {
            var result = await _taskModelRepository.GetAllAsync(x => x.TaskDate.Date == dateTime.Date);

            return Ok(result);
        }
        
        return BadRequest();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var result = await _taskModelRepository.GetSingleAsync(x => x.Id == id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] TaskModel model)
    {
        var result = await _taskModelRepository.InsertAsync(model);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] TaskModel model)
    {
        var result = await _taskModelRepository.UpdateAsync(model);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var result = await _taskModelRepository.RemoveAsync(id);

        return Ok(result);
    }

    [HttpDelete("DeleteRange")]
    public async Task<IActionResult> DeleteTask(List<int> ids)
    {
        var result = await _taskModelRepository.RemoveRangeAsync(ids);

        return Ok(result);
    }
}
