namespace AccountingAreasApi.Interfaces;

public interface IAuthenticationRepository
{
    string GenerateToken(string username);
}