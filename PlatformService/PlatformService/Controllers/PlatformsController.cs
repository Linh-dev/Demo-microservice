using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController(IPlatformRepo repo, IMapper mapper, ICommandDataClient commandDataClient)
        {
            _repo = repo;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            try
            {
                var data = _repo.GetAll();
                var res = _mapper.Map<IEnumerable<PlatformReadDto>>(data);
                return Ok(res);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById")]
        public ActionResult<PlatformReadDto> GetById(int id)
        {
            try
            {
                var info = _repo.GetById(id);
                if (info == null) return NotFound();
                var res = _mapper.Map<PlatformReadDto>(info);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<PlatformReadDto>> Create([FromBody] PlatformCreateDto req)
        {
            try
            {
                var info = _mapper.Map<Platform>(req);
                _repo.Create(info);
                _repo.SaveChange();
                
                var readInfo = _mapper.Map<PlatformReadDto>(info);

                try
                {
                    await _commandDataClient.SendPlatformToCommand(readInfo);
                }catch (Exception ex)
                {
                    Console.WriteLine($"--> could not send synchronously: {ex.Message}");
                }

                return CreatedAtAction("GetById", new {Id = readInfo.Id}, readInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
