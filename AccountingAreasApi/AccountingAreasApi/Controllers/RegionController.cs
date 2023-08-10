using AccountingAreasApi.Interfaces;
using AccountingAreasApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountingAreasApi.Controllers;
[Route("api/region")]
[ApiController]
public class RegionController : ControllerBase
{
    private readonly IRegionRepositroy _regionRepositroy;

    public RegionController(IRegionRepositroy regionRepositroy)
    {
        _regionRepositroy = regionRepositroy;
    }

    [HttpGet]
    public async Task<ActionResult<List<Region>>> GetAllRegions()
    {
        return await _regionRepositroy.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Region>> GetRegion(int id)
    {
        var result = await _regionRepositroy.Get(id);
        if (result is null)
            return null;

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Region>> AddRegion(Region region)
    {
        var result = await _regionRepositroy.Add(region);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Region>> UpdateRegion(int id, Region request)
    {
        var result = await _regionRepositroy.Update(id, request);

        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult<Region>> DeleteRegion(int Id)
    {
        var result = await _regionRepositroy.Delete(Id);

        return Ok(result);
    }
}