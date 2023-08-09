namespace AccountingAreasApi.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
     Task<TEntity> Get(int Id);
     Task<List<TEntity>> GetAll();
     Task<TEntity> Add(TEntity entity);
     Task<TEntity> Update(int Id, TEntity entity);
     Task<List<TEntity>> Delete(int Id);
}