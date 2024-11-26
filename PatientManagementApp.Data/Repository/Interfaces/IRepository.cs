

using System.Linq.Expressions;

namespace PatientManagementApp.Data.Repository.Interfaces
{
    public interface IRepository<TType, TId>
    {
        TType GetById(TId id);

        Task<TType> GetByIdAsync(TId id);

        TType FirstOrDefault(Func<TType, bool> predicate);

        Task<TType> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate);

        IEnumerable<TType> GetAll();

        Task<IEnumerable<TType>> GetAllAsync();

        IEnumerable<TType> GetAllAttached();


        void Add(TType item);
        Task AddAsync(TType item);
        void AddRange(TType[] items);

        Task AddRangeAsync(TType[] items);

        bool Delete(TType item);

        Task<bool> DeleteAsync(TType item);



        bool Update(TType  item);
        Task<bool> UpdateAsync(TType item);



    }
}
