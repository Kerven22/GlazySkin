namespace RepositoryContracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> Find
    }
}
