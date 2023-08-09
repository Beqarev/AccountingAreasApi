using AccountingAreasApi.Data;
using AccountingAreasApi.Interfaces;
using AccountingAreasApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingAreasApi.Repositories;

public class RegionRepository : IRegionRepositroy
{
    private readonly DataContext _context;

    public RegionRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<Region> Get(int Id)
    {
        var region = await _context.Regions.FindAsync(Id);
        if (region is null)
            return null;
        return region;
    }

    public async Task<List<Region>> GetAll()
    {
        var regions = await _context.Regions.ToListAsync();
        return regions;
    }

    public async Task<Region> Add(Region region)
    {
        _context.Regions.Add(region);
        await _context.SaveChangesAsync();
        return await _context.Regions.FindAsync(region.Id);
    }

    public async Task<Region> Update(int Id, Region request)
    {
        var region = await _context.Regions.FindAsync(Id);
        if (request is null)
            return null;


        region.Id = request.Id;
        region.Name = request.Name;

        await _context.SaveChangesAsync();
        return await _context.Regions.FindAsync(Id);
    }

    public async Task<List<Region>> Delete(int Id)
    {
        var region = await _context.Regions.FindAsync(Id);
        if (region is null)
            return null;

        _context.Regions.Remove(region);
        await _context.SaveChangesAsync();

        return await _context.Regions.ToListAsync();
    }
}