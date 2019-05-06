using Xunit;
using MemoriaCau;

namespace MemoriaCau.Tests
{
    public class FIFOTest
    {
        [Fact]
        public void Read_FIFO_FirstFail()
        {
            var cache = new FIFO<int>(4, new WriteAlgorithm(WriteAlgorithms.CopyBack));

            var result = cache.Read(0, 0);

            Assert.False(result.success);
            Assert.False(result.replacedNode);
            Assert.Equal(0U, result.block.Tag);
            Assert.Equal(0, result.block.Val);
        }

        [Fact]
        public void Read_FIFO_SecondSameSuccess()
        {
            var cache = new FIFO<int>(4, new WriteAlgorithm(WriteAlgorithms.CopyBack));

            cache.Read(0, 5);
            var result = cache.Read(0, 0);

            Assert.True(result.success);
            Assert.False(result.replacedNode);
            Assert.Equal(0U, result.block.Tag);
            Assert.Equal(0, result.block.Val);
        }

        [Fact]
        public void Read_FIFO_SecondDifFail()
        {
            var cache = new FIFO<int>(4, new WriteAlgorithm(WriteAlgorithms.CopyBack));

            cache.Read(1, 5);
            var result = cache.Read(0, 0);

            Assert.False(result.success);
            Assert.False(result.replacedNode);
            Assert.Equal(0U, result.block.Tag);
            Assert.Equal(0, result.block.Val);
        }

        [Fact]
        public void Read_FIFO_ReplaceFirst()
        {
            var cache = new FIFO<int>(2, new WriteAlgorithm(WriteAlgorithms.CopyBack));

            cache.Read(1, 5);
            cache.Read(2, 6);
            var result = cache.Read(0, 0);

            Assert.False(result.success);
            Assert.True(result.replacedNode);
            Assert.Equal(1U, result.replacedNodeTag);
            Assert.Equal(0U, result.block.Tag);
            Assert.Equal(0, result.block.Val);
        }

        [Fact]
        public void Read_FIFO_ReplaceSecond()
        {
            var cache = new FIFO<int>(2, new WriteAlgorithm(WriteAlgorithms.CopyBack));

            cache.Read(1, 5);
            cache.Read(2, 6);
            cache.Read(3, 6);
            var result = cache.Read(0, 0);

            Assert.False(result.success);
            Assert.True(result.replacedNode);
            Assert.Equal(2U, result.replacedNodeTag);
            Assert.Equal(0U, result.block.Tag);
            Assert.Equal(0, result.block.Val);
        }


        [Fact]
        public void Read_FIFO_ReplacedSuccess()
        {
            var cache = new FIFO<int>(2, new WriteAlgorithm(WriteAlgorithms.CopyBack));

            cache.Read(1, 5);
            cache.Read(0, 6);
            cache.Read(3, 6);
            var result = cache.Read(0, 0);

            Assert.True(result.success);
            Assert.False(result.replacedNode);
            Assert.Equal(0U, result.block.Tag);
            Assert.Equal(0, result.block.Val);
        }

        [Fact]
        public void Write_FIFO_CopyBack_FirstFail()
        {
            var cache = new FIFO<int>(4, new WriteAlgorithm(WriteAlgorithms.CopyBack));

            var result = cache.Write(0, 0);

            Assert.False(result.success);
            Assert.False(result.replacedNode);
        }

        [Fact]
        public void Write_FIFO_CopyBack_SecondSameSuccess()
        {
            var cache = new FIFO<int>(4, new WriteAlgorithm(WriteAlgorithms.CopyBack));

            cache.Write(0, 5);
            var result = cache.Write(0, 0);

            Assert.True(result.success);
            Assert.False(result.replacedNode);
        }

        [Fact]
        public void ReadWrite_FIFO_CopyBack_SecondSuccess()
        {
            var cache = new FIFO<int>(4, new WriteAlgorithm(WriteAlgorithms.CopyBack));

            cache.Read(0, 5);
            var result = cache.Write(0, 0);

            Assert.True(result.success);
            Assert.False(result.replacedNode);
            Assert.Equal(0U, result.block.Tag);
            Assert.Equal(0, result.block.Val);
        }

        [Fact]
        public void Write_FIFO_CopyBack_ReplaceFirst()
        {
            var cache = new FIFO<int>(2, new WriteAlgorithm(WriteAlgorithms.CopyBack));

            cache.Write(1, 5);
            cache.Write(2, 6);
            var result = cache.Write(0, 0);

            Assert.False(result.success);
            Assert.True(result.replacedNode);
            Assert.Equal(1U, result.replacedNodeTag);
            Assert.Equal(0U, result.block.Tag);
            Assert.Equal(0, result.block.Val);
        }

        [Fact]
        public void Write_FIFO_WriteThroughAssign_FirstFail()
        {
            var cache = new FIFO<int>(4, new WriteAlgorithm(WriteAlgorithms.WriteThroughWithAssignementOnWrite));

            var result = cache.Write(0, 0);

            Assert.False(result.success);
            Assert.False(result.replacedNode);
        }

        [Fact]
        public void Write_FIFO_WriteThroughAssign_SecondSameSuccess()
        {
            var cache = new FIFO<int>(4, new WriteAlgorithm(WriteAlgorithms.WriteThroughWithAssignementOnWrite));

            cache.Write(0, 5);
            var result = cache.Write(0, 0);

            Assert.True(result.success);
            Assert.False(result.replacedNode);
        }

        [Fact]
        public void ReadWrite_FIFO_WriteThroughAssign_SecondSuccess()
        {
            var cache = new FIFO<int>(4, new WriteAlgorithm(WriteAlgorithms.WriteThroughWithAssignementOnWrite));

            cache.Read(0, 5);
            var result = cache.Write(0, 0);

            Assert.True(result.success);
            Assert.False(result.replacedNode);
            Assert.Equal(0U, result.block.Tag);
            Assert.Equal(0, result.block.Val);
        }

        [Fact]
        public void Write_FIFO_WriteThroughAssign_ReplaceFirst()
        {
            var cache = new FIFO<int>(2, new WriteAlgorithm(WriteAlgorithms.WriteThroughWithAssignementOnWrite));

            cache.Write(1, 5);
            cache.Write(2, 6);
            var result = cache.Write(0, 0);

            Assert.False(result.success);
            Assert.True(result.replacedNode);
            Assert.Equal(1U, result.replacedNodeTag);
            Assert.Equal(0U, result.block.Tag);
            Assert.Equal(0, result.block.Val);
        }

        [Fact]
        public void Write_FIFO_WriteThroughNoAssign_FirstFail()
        {
            var cache = new FIFO<int>(4, new WriteAlgorithm(WriteAlgorithms.WriteThroughWithoutAssignementOnWrite));

            var result = cache.Write(0, 0);

            Assert.False(result.success);
            Assert.False(result.replacedNode);
        }

        [Fact]
        public void Write_FIFO_WriteThroughNoAssign_SecondSameFail()
        {
            var cache = new FIFO<int>(4, new WriteAlgorithm(WriteAlgorithms.WriteThroughWithoutAssignementOnWrite));

            cache.Write(0, 5);
            var result = cache.Write(0, 0);

            Assert.False(result.success);
            Assert.False(result.replacedNode);
        }

        [Fact]
        public void Write_FIFO_WriteThroughNoAssign_SecondSuccess()
        {
            var cache = new FIFO<int>(4, new WriteAlgorithm(WriteAlgorithms.WriteThroughWithoutAssignementOnWrite));

            cache.Read(0, 5);
            var result = cache.Write(0, 0);

            Assert.True(result.success);
            Assert.False(result.replacedNode);
            Assert.Equal(0U, result.block.Tag);
            Assert.Equal(0, result.block.Val);
        }

        [Fact]
        public void Write_FIFO_WriteThroughNoAssign_DontReplaceFirst()
        {
            var cache = new FIFO<int>(2, new WriteAlgorithm(WriteAlgorithms.WriteThroughWithoutAssignementOnWrite));

            cache.Write(1, 5);
            cache.Write(2, 6);
            var result = cache.Write(0, 0);

            Assert.False(result.success);
            Assert.False(result.replacedNode);
        }


    }
}