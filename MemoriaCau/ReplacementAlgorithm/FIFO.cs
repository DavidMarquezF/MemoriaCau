using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoriaCau
{
    public class FIFO<T> : IReplacementAlgorithm<T>
    {
        private Dictionary<UInt32, Block<T>> _setData = new Dictionary<UInt32, Block<T>>();
        private WriteAlgorithm _writeAlg;
        private int _size;
        public FIFO(int size, WriteAlgorithm writeAlgorithm)
        {
            _size = size;
            _writeAlg = writeAlgorithm;
        }

        public bool Contains(UInt32 tag)
        {
            return _setData.ContainsKey(tag);
        }

        public CacheEvent<T> Read(UInt32 tag, T value)
        {
            return _handleBlock(tag, value, false);
        }

        public CacheEvent<T> Write(UInt32 tag, T value)
        {
            return _handleBlock(tag, value, true);
        }

        public Dictionary<UInt32, Block<T>> GetSetData()
        {
            return _setData;
        }

        

        public void Remove(UInt32 key)
        {
            _setData.Remove(key);
        }

        private CacheEvent<T> _handleBlock(UInt32 tag, T value, bool isWrite)
        {

            if (_setData.TryGetValue(tag, out Block<T> d)){
                d.Val = value;
                return new CacheEvent<T>(true, isWrite ? AccessType.Write : AccessType.Read, d);
            }
                

            if (!isWrite || isWrite && _writeAlg.ShouldBringBlockOnWriteFail)
            {
                var createdNode = new Block<T>(tag, value);
                if (_setData.Count < _size)
                {
                    _setData.Add(tag, createdNode);
                    return new CacheEvent<T>(false, isWrite ? AccessType.Write : AccessType.Read, createdNode);
                }
                var replaced = _oldest();
                Remove(replaced);
                _setData.Add(tag, createdNode);
                return new CacheEvent<T>(false, isWrite ? AccessType.Write : AccessType.Read, createdNode, replaced);
            }

            return new CacheEvent<T>(false, isWrite ? AccessType.Write : AccessType.Read);
        }
        private UInt32 _oldest()
        {
            if (_setData == null || _setData.Count < 1) throw new KeyNotFoundException();

            return _setData.Where(x => x.Value.Added != null).OrderBy(x => x.Value.Added).First().Key;
        }
    }
}