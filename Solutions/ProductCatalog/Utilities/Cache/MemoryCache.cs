using log4net;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Utilities.Cache {
    public abstract class MemoryCache<TKey, TValue> : IMemoryCache<TKey, TValue> {
        ConcurrentDictionary<TKey, TValue> cacheData;
        string cacheName;
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public MemoryCache(string cacheName) {
            this.cacheName = cacheName;
            cacheData = new ConcurrentDictionary<TKey, TValue>();
        }

        public TValue this[TKey key] {
            get {
                TValue value;
                if (cacheData.TryGetValue(key, out value)) {
                    return value;
                } else {
                    string errorMessage = string.Format("MemoryCache{0}-Key Not found:{1}", this.cacheName, key.ToString());
                    log.ErrorFormat(errorMessage);
                    throw new KeyNotFoundException(errorMessage);
                }
            }
        }
        public ICollection<TKey> Keys {
            get {
                return cacheData.Keys;
            }
        }
        public ICollection<TValue> Values {
            get {
                return cacheData.Values;
            }
        }
        public void Add(TKey key, TValue value) {
            if (cacheData.ContainsKey(key)) {
                cacheData[key] = value;
            } else {
                if (!cacheData.TryAdd(key, value)) {
                    string warningMessage = string.Format("MemoryCache{0}-Key is not added!Key:{1},Value:{2}", this.cacheName, key, value);
                    log.ErrorFormat(warningMessage);
                }
            }
        }

        public void Clear() {
            cacheData.Clear();
        }

        public bool ContainsKey(TKey key) {
            return cacheData.ContainsKey(key);
        }

        public bool ContainsValue(TValue value) {
            return cacheData.Values.Contains(value);
        }
        public void Remove(TKey key) {
            TValue value;
            if (!cacheData.TryRemove(key, out value)) {
                string warningMessage = string.Format("MemoryCache{0}-Key can not be removed!Key:{1}", this.cacheName, key);
                log.Info(warningMessage);
            }
        }
        public bool TryGetValue(TKey key, out TValue value) {
            TValue tmpValue;
            if (!cacheData.TryGetValue(key, out tmpValue)) {
                string warningMessage = string.Format("MemoryCache{0}--Key is not found!Key:{1}", this.cacheName, key);
                log.DebugFormat(warningMessage);
                value = default(TValue);
                return false;
            }
            value = tmpValue;
            return true;
        }

        public void Update(TKey key, TValue value) {
            if (cacheData.ContainsKey(key)) {
                cacheData[key] = value;
            }
            else {
                cacheData.TryAdd(key,value);
            }
        }

        public int Count {
            get {
                return cacheData.Count;
            }
        }
    }
}
