using System;
using MemoriaCau;
using Xunit;

namespace MemoriaCau.Tests
{
    public class NWayCacheTest
    {



        [Theory]
        [InlineData(1024, 4, 2, 128)]
        [InlineData(1024, 2, 4, 128)]
        [InlineData(2048, 16, 8, 16)]
        [InlineData(2048, 16, 4, 32)]
        public void Constructor_NWayCache_NumberSets(int size_cache_bytes, int bytes_block, int ways, int expected)
        {
            var cache = new NWayCache<int>(size_cache_bytes, bytes_block, ways, ReplacementAlgorithm.FIFO, WriteAlgorithms.CopyBack);

            var result = cache.nSets;

            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData(1024, 4, 2, 7)]
        [InlineData(1024, 2, 4, 7)]
        [InlineData(2048, 16, 8, 4)]
        [InlineData(2048, 16, 4, 5)]
        public void NWayCache_BitsSet(int size_cache_bytes, int bytes_block, int ways, int expected)
        {
            var cache = new NWayCache<int>(size_cache_bytes, bytes_block, ways, ReplacementAlgorithm.FIFO, WriteAlgorithms.CopyBack);

            var result = cache.bitsSet;

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1024, 4, 2, 2)]
        [InlineData(1024, 2, 4, 1)]
        [InlineData(2048, 16, 8, 4)]
        [InlineData(2048, 16, 4, 4)]
        public void NWayCache_BitsOffset(int size_cache_bytes, int bytes_block, int ways, int expected)
        {
            var cache = new NWayCache<int>(size_cache_bytes, bytes_block, ways, ReplacementAlgorithm.FIFO, WriteAlgorithms.CopyBack);

            var result = cache.bitsOffset;

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1024, 4, 2, 23)]
        [InlineData(1024, 2, 4, 24)]
        [InlineData(2048, 16, 8, 24)]
        [InlineData(2048, 16, 4, 23)]
        public void NWayCache_BitsTag(int size_cache_bytes, int bytes_block, int ways, int expected)
        {
            var cache = new NWayCache<int>(size_cache_bytes, bytes_block, ways, ReplacementAlgorithm.FIFO, WriteAlgorithms.CopyBack);

            var result = cache.bitsTag;

            Assert.Equal(expected, result);
        }
    }
}
