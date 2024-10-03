using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;

namespace Tiknas.EntityFrameworkCore.GlobalFilters;

public class TiknasCompiledQueryCacheKeyGenerator : ICompiledQueryCacheKeyGenerator
{
    protected ICompiledQueryCacheKeyGenerator InnerCompiledQueryCacheKeyGenerator { get; }
    protected ICurrentDbContext CurrentContext { get; }

    public TiknasCompiledQueryCacheKeyGenerator(
        ICompiledQueryCacheKeyGenerator innerCompiledQueryCacheKeyGenerator,
        ICurrentDbContext currentContext)
    {
        InnerCompiledQueryCacheKeyGenerator = innerCompiledQueryCacheKeyGenerator;
        CurrentContext = currentContext;
    }

    public virtual object GenerateCacheKey(Expression query, bool async)
    {
        var cacheKey = InnerCompiledQueryCacheKeyGenerator.GenerateCacheKey(query, async);
        if (CurrentContext.Context is ITiknasEfCoreDbFunctionContext tiknasEfCoreDbFunctionContext)
        {
            return new TiknasCompiledQueryCacheKey(cacheKey, tiknasEfCoreDbFunctionContext.GetCompiledQueryCacheKey());
        }

        return cacheKey;
    }

    private readonly struct TiknasCompiledQueryCacheKey : IEquatable<TiknasCompiledQueryCacheKey>
    {
        private readonly object _compiledQueryCacheKey;
        private readonly string _currentFilterCacheKey;

        public TiknasCompiledQueryCacheKey(object compiledQueryCacheKey, string currentFilterCacheKey)
        {
            _compiledQueryCacheKey = compiledQueryCacheKey;
            _currentFilterCacheKey = currentFilterCacheKey;
        }

        public override bool Equals(object? obj)
        {
            return obj is TiknasCompiledQueryCacheKey key && Equals(key);
        }

        public bool Equals(TiknasCompiledQueryCacheKey other)
        {
            return _compiledQueryCacheKey.Equals(other._compiledQueryCacheKey) &&
                   _currentFilterCacheKey == other._currentFilterCacheKey;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_compiledQueryCacheKey, _currentFilterCacheKey);
        }
    }
}
