using AccountingAreasApi.Dto;
using AccountingAreasApi.Interfaces;
using AccountingAreasApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingAreasApi.Controllers;

[Route("api/area")]
[ApiController]
public class AreaController : ControllerBase
{
    private readonly IAreaRepository _areaRepository;
    private readonly IMapper _mapper;

    public AreaController(IAreaRepository areaRepository, IMapper mapper)
    {
        _areaRepository = areaRepository;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<ActionResult<List<AreaDto>>> GetAllAreas()
    {
        var areas = await _areaRepository.GetAll();
        var areaDtos = _mapper.Map<List<AreaDto>>(areas);
        return Ok(areaDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AreaDto>> GetArea(int id)
    {
        var result = _mapper.Map<AreaDto>(await _areaRepository.Get(id));

        return Ok(result);
    }


    [HttpPost]
    public async Task<ActionResult<Area>> AddArea([FromBody] AreaDto area)
    {
        var areaMap = _mapper.Map<Area>(area);
        var result = await _areaRepository.Add(areaMap);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Area>> UpdateArea(int id, [FromBody] AreaDto request)
    {
        var areaMap = _mapper.Map<Area>(request);
        var result = await _areaRepository.Update(id, areaMap);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Area>> DeleteArea(int id)
    {
        var result = await _areaRepository.Delete(id);
        
        return Ok(result);
    }
}