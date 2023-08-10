using AccountingAreasApi.Interfaces;
using AccountingAreasApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountingAreasApi.Controllers;

[Route("api/area")]
[ApiController]
public class AreaController : ControllerBase
{
    private readonly IAreaRepository _areaRepository;

    public AreaController(IAreaRepository areaRepository)
    {
        _areaRepository = areaRepository;
    }


    [HttpGet]
    public async Task<ActionResult<List<Area>>> GetAllAreas()
    {
        return await _areaRepository.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Area>> GetArea(int id)
    {
        var result = await _areaRepository.Get(id);

        return Ok(result);
    }


    [HttpPost]
    public async Task<ActionResult<Area>> AddArea(Area area)
    {
        var result = await _areaRepository.Add(area);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Area>> UpdateArea(int id, Area request)
    {
        var result = await _areaRepository.Update(id, request);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Area>> DeleteArea(int id)
    {
        var result = await _areaRepository.Delete(id);

        return Ok(result);
    }
}