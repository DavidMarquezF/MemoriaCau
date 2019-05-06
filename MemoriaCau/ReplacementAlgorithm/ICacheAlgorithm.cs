using System;
using System.Collections.Generic;

namespace MemoriaCau
{
    public enum ReplacementAlgorithm { FIFO }
    public interface IReplacementAlgorithm<T>
    {
        Dictionary<UInt32, Block<T>> GetSetData();
        CacheEvent<T> Read(UInt32 tag, T value);
        CacheEvent<T> Write(UInt32 tag, T value);
        void Remove(UInt32 tag);
        bool Contains(UInt32 tag);
    }
}