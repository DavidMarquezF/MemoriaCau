using System;
using System.Collections.Specialized;

namespace MemoriaCau
{
    public enum UbicationAlgorithm {Direct, Associative, SetAssociative}
    public interface IUbicationAlgorithm<T>
    {
        OrderedDictionary AdressDistribution { get; }
        AccessRow <T> Read(UInt32 key, T value);
        AccessRow <T> Write(UInt32 key, T value);
    }
}