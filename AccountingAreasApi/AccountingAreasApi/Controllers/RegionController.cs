using AccountingAreasApi.Dto;
using AccountingAreasApi.Interfaces;
using AccountingAreasApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountingAreasApi.Controllers;
[Route("api/region")]
[ApiController]
public class RegionController : ControllerBase
{
    private readonly IRegionRepositroy _regionRepositroy;
    private readonly IMapper _mapper;

    public RegionController(IRegionRepositroy regionRepositroy, IMapper mapper)
    {
        _regionRepositroy = regionRepositroy;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<Region>>> GetAllRegions()
    {
        var regions = _mapper.Map<RegionDto>(await _regionRepositroy.GetAll());
        return Ok(regions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Region>> GetRegion(int id)
    {
        var result = _mapper.Map<RegionDto>(await _regionRepositroy.Get(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Region>> AddRegion([FromBody] RegionDto region)
    {
        var regionMap = _mapper.Map<Region>(region);
        var result = await _regionRepositroy.Add(regionMap);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Region>> UpdateRegion(int id,[FromBody] RegionDto request)
    {
        var regionMap = _mapper.Map<Region>(request);
        var result = await _regionRepositroy.Update(id, regionMap);

        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult<Region>> DeleteRegion(int Id)
    {
        var result = await _regionRepositroy.Delete(Id);

        return Ok(result);
    }
}