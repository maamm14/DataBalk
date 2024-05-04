using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserTaskApi.Models.Domain;
using UserTaskApi.Models.DTO.Tasks;
using UserTaskApi.Repositories.TaskRepo;

namespace UserTaskApi.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public TaskController(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<TaskDomain> tasks = await _taskRepository.GetAllAsync();

            List<TasksDto> tasksDtos = _mapper.Map<List<TasksDto>>(tasks);

            if (tasksDtos.Count == 0)
            {
                return NotFound();
            }

            return Ok(tasksDtos);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }

            TaskDomain? task = await _taskRepository.GetByIdAsync(Id);

            if (task is null)
            {
                return NotFound();
            }

            TasksDto tasksDto = _mapper.Map<TasksDto>(task);

            return Ok(tasksDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequestDto createTask)
        {
            //Map DTO to Domain Model
            TaskDomain taskDomain = _mapper.Map<TaskDomain>(createTask);
            //Use Domain Model to create walk
            taskDomain = await _taskRepository.CreateAsync(taskDomain);
            //Map Domain Model to DTO
            TasksDto tasksDto = _mapper.Map<TasksDto>(taskDomain);
            //Return Created walk to Client
            return CreatedAtAction(nameof(GetById), new { id = tasksDto.Id }, tasksDto);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] UpdateTaskRequestDto updateTask)
        {
            //Map DTO to Domain Model
            TaskDomain? taskDomain = _mapper.Map<TaskDomain>(updateTask);
            //Use Domain Model to update walk
            taskDomain = await _taskRepository.UpdateAsync(Id, taskDomain);
            //Check if walk doesnt exist and return response
            if (taskDomain == null)
            {
                return NotFound();

            }
            //Map Domain Model to DTO
            TasksDto tasksDto = _mapper.Map<TasksDto>(taskDomain);
            //Return Updated walk to Client
            return Ok(tasksDto);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            //fetching a existing walk from Repository and delete it
            TaskDomain? taskDomain = await _taskRepository.DeleteAsync(Id);
            //Check if region doesnt exist and return response
            if (taskDomain == null)
            {
                return NotFound();
            }
            //Return Deleted walk to Client
            return Ok(taskDomain); //Return Deleted walk to Client
        }
    }
}