using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface IDataSet<T>
    {
        IQueryable<T> AsQueryable { get; }

        bool Any();
        Task AddRangeAsync(IEnumerable<T> dataSet);
        IQueryable<T> Where(Func<T, bool> func);
        void Add(T value);
        ValueTask<T> FindAsync(int id);
        IIncludableQueryable<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> func);
        Task<List<T>> ToListAsync(CancellationToken cancellationToken);
    }
}
