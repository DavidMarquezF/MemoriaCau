using System;
using System.Collections.Specialized;

namespace MemoriaCau
{
    public class NWayCache<T> : IUbicationAlgorithm<T>
    {
        private IReplacementAlgorithm<T>[] _sets;
        public readonly int cacheSize, nWays, nSets;
        public readonly int bitsTag, bitsSet, bitsOffset;

        public OrderedDictionary AdressDistribution => getOrd();

        private OrderedDictionary getOrd()
        {
            var o = new OrderedDictionary();
            o.Add("Etiqueta", bitsTag);
            o.Add("Conjunt", bitsSet);
            o.Add("Accés byte", bitsOffset);

            return o;
        }

        public NWayCache(int size_bytes, int block_size_bytes, int ways, ReplacementAlgorithm al, WriteAlgorithms wrAl)
        {
            cacheSize = size_bytes;
            nWays = ways;

            nSets = cacheSize / (nWays * block_size_bytes);
            bitsSet = (int)Math.Log(nSets, 2);

            bitsOffset = (int)Math.Log(block_size_bytes, 2);

            bitsTag = 32 - bitsOffset - bitsSet;

            _sets = new IReplacementAlgorithm<T>[nSets];
            for (int i = 0; i < nSets; i++)
            {
                _sets[i] = new FIFO<T>(nWays, new WriteAlgorithm(wrAl));
            }
        }

        public AccessRow<T> Read(UInt32 key, T value)
        {
            var setIndex = getSetIndex(key);

            var specific = new OrderedDictionary();
            specific.Add("Conjunt", setIndex);

            return new AccessRow<T>(_sets[setIndex].Read(getTag(key), value), key, specific);
        }

        public AccessRow<T> Write(UInt32 key, T value)
        {
            var setIndex = getSetIndex(key);

            var specific = new OrderedDictionary();
            specific.Add("Conjunt", setIndex);

            return new AccessRow<T>(_sets[setIndex].Write(getTag(key), value), key, specific);
        }

        private long getSetIndex(UInt32 key)
        {
            return (key >> bitsOffset) & ((long)Math.Pow(2, bitsSet) - 1);
        }

        private UInt32 getTag(UInt32 key)
        {
            return key >> (bitsOffset + bitsSet);
        }
    }
}