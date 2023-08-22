using AutoMapper;
using Commander.Data;
using Commander.DTOs;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandItem = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandItem);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandItem);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        //PUT
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandOldModel = _repository.GetCommandById(id);
            if (commandOldModel == null)
            {
                return NotFound();
            }

            //AutoMapper updates the object
            //And it remains to SaveChanges()
            _mapper.Map(commandUpdateDto, commandOldModel);
            //This method does nothing
            _repository.UpdateCommand(commandOldModel);

            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH
        [HttpPatch("{id}")]
        public ActionResult PartialCommanUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandOldModel = _repository.GetCommandById(id);
            if (commandOldModel == null)
            {
                return NotFound();
            }

            var commandUpdateDto = _mapper.Map<CommandUpdateDto>(commandOldModel);

            patchDoc.ApplyTo(commandUpdateDto, ModelState);
            if (!TryValidateModel(commandUpdateDto))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandUpdateDto, commandOldModel);
            //This method does nothing
            _repository.UpdateCommand(commandOldModel);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModel = _repository.GetCommandById(id);
            if (commandModel == null)
            {
                return NotFound();
            }

            _repository.DeleteCommand(commandModel);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
