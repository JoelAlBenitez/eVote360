namespace eVote360.Core.Domain.Contracts.BaseRepository
{
    public interface IBaseRepository<TEntitie, Tkey> where TEntitie : class
    {
        Task<bool> CreateEntiteAsync(TEntitie entitie);
        Task<bool> UpdateEntitieAsync (TEntitie entitie);
        Task<TEntitie> GetByIdEntitie(Tkey tkey);
        Task<bool> DesactiveEntitie(Tkey tkey);
    }
}
