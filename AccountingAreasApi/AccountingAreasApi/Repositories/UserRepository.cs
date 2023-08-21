using AccountingAreasApi.Data;
using AccountingAreasApi.Dto;
using AccountingAreasApi.Interfaces;
using AccountingAreasApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingAreasApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<User> Get(int Id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);
        return user;
    }

    public async Task<List<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> Add(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> Update(int Id, User request)
    {
        var user = await _context.Users.FindAsync(Id);
        user.Id = request.Id;
        user.Username = request.Username;
        user.PasswordHash = request.PasswordHash;
        user.PasswordSalt = request.PasswordSalt;

        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<List<User>> Delete(int Id)
    {
        var user = await _context.Users.FindAsync(Id);
        _context.Users.Remove(user);
        return await _context.Users.ToListAsync();
    }

    public bool UserExists(UserDto user)
    {
        return _context.Users.Any(x => x.Username == user.Username);
    }

    public User GetUserByUsername(UserDto user)
    {
        var _user = _context.Users.FirstOrDefault(x => x.Username == user.Username);
        return _user;
    }
}