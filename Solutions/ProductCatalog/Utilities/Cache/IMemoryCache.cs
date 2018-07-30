using System.Collections.Generic;

namespace Utilities.Cache
{
    public interface IMemoryCache<TKey, TValue>
    {
        void Add(TKey key, TValue value);
        void Clear();
        void Remove(TKey key);
        void Update(TKey key, TValue value);
        bool ContainsKey(TKey key);
        bool ContainsValue(TValue value);
        ICollection<TKey> Keys { get; }
        ICollection<TValue> Values { get; }
        TValue this[TKey key] { get; }
        bool TryGetValue(TKey key, out TValue value);
        int Count { get; }
    }
}
