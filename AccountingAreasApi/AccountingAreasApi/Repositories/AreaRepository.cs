using AccountingAreasApi.Data;
using AccountingAreasApi.Interfaces;
using AccountingAreasApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingAreasApi.Repositories;

public class AreaRepository : IAreaRepository
{
    private readonly DataContext _context;

    public AreaRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<Area> Get(int id)
    {
        var area = await _context.Areas
            .Include(a => a.Region)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return area;
    }

    public async Task<List<Area>> GetAll()
    {
        var areas = await _context.Areas.Include(a => a.Region).ToListAsync();
        return areas;
    }

    public async Task<Area> Add(Area area)
    {
        var region = _context.Regions.FirstOrDefault(r => r.Id == area.Region.Id);
        area.Region = region;
        _context.Areas.Add(area);
        await _context.SaveChangesAsync();
        return await _context.Areas.FindAsync(area.Id);
    }

    public async Task<Area> Update(int Id, Area request)
    {
        var area = await _context.Areas.FindAsync(Id);
        if (request is null)
            return null;
        area.Id = request.Id;
        area.Category = request.Category;
        area.Description = request.Description;
        area.Location = request.Location;
        area.Name = request.Name;
        area.Region = request.Region;
        area.StartDate = request.StartDate;
        area.EndDate = request.EndDate;

        await _context.SaveChangesAsync();
        return await _context.Areas.FindAsync(Id);
    }

    public async Task<List<Area>> Delete(int Id)
    {
        var area = await _context.Areas.FindAsync(Id);
        if (area is null)
            return null;

        _context.Areas.Remove(area);
        await _context.SaveChangesAsync();

        return await _context.Areas.ToListAsync();
    }
}