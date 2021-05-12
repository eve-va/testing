using System;
using IIG.BinaryFlag;
using Xunit;

namespace BlackBoxTesting
{
    public class MultipleBinaryFlagTest
    {
        [Fact]
        public void CreateMultipleBinaryFlag_InRange()
        {
            var flagMin = new MultipleBinaryFlag(2);
            var flagMidF = new MultipleBinaryFlag(3, false);
            var flagMidT = new MultipleBinaryFlag(17179868703);
            var flagMax = new MultipleBinaryFlag(17179868704, false);

            var expectedMin = "TT";
            var expectedMidT = "FFF";
            
            Assert.Equal(expectedMin, flagMin.ToString());
            Assert.Equal(expectedMidT, flagMidF.ToString());
            Assert.IsType<MultipleBinaryFlag>(flagMidT);
            Assert.IsType<MultipleBinaryFlag>(flagMax);
        }

        [Fact]
        public void CreateMultipleBinaryFlag_OutOfRangeBelow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(1));
        }

        [Fact]
        public void CreateMultipleBinaryFlag_OutOfRangeAbove()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(17179868705, false));
        }

        [Fact]
        public void ResetFlag_InRange()
        {
            var flag = new MultipleBinaryFlag(333);

            flag.ResetFlag(0);
            flag.ResetFlag(1);
            flag.ResetFlag(332);


            Assert.Equal('F', flag.ToString()[0]);
            Assert.Equal('F', flag.ToString()[1]);
            Assert.Equal('F', flag.ToString()[332]);
        }

        [Fact]
        public void ResetFlag_OutOfRange()
        {
            var flag = new MultipleBinaryFlag(333);
            Assert.Throws<ArgumentOutOfRangeException>(() => flag.ResetFlag(333));
            Assert.Throws<ArgumentOutOfRangeException>(() => flag.ResetFlag(334));
        }

        [Fact]
        public void SetFlag_InRange()
        {
            var flag = new MultipleBinaryFlag(333, false);

            flag.SetFlag(0);
            flag.SetFlag(1);
            flag.SetFlag(332);

            Assert.Equal('T', flag.ToString()[0]);
            Assert.Equal('T', flag.ToString()[1]);
            Assert.Equal('T', flag.ToString()[332]);
        }

        [Fact]
        public void SetFlag_OutOfRange()
        {
            var flag = new MultipleBinaryFlag(333, false);
            Assert.Throws<ArgumentOutOfRangeException>(() => flag.SetFlag(333));
            Assert.Throws<ArgumentOutOfRangeException>(() => flag.SetFlag(334));
        }

        [Fact]
        public void GetFlag_True()
        {
            var flag = new MultipleBinaryFlag(22);

            Assert.True(flag.GetFlag());
        }

        [Fact]
        public void GetFlag_False()
        {
            var flag = new MultipleBinaryFlag(22);

            flag.ResetFlag(1);

            Assert.False(flag.GetFlag());
        }

        [Fact]
        public void Dispose()
        {
            var flag = new MultipleBinaryFlag(777);

            flag.Dispose();

            Assert.Null(flag.ToString());
        }

        [Fact]
        public void MethodsAfterDispose()
        {
            var flagSet = new MultipleBinaryFlag(33, false);
            var flagReset = new MultipleBinaryFlag(44);
            var flag = new MultipleBinaryFlag(55);

            flagSet.Dispose();
            flagSet.SetFlag(9);
            flagReset.Dispose();
            flagReset.ResetFlag(2);
            flag.Dispose();

            Assert.Null(flagSet.ToString());
            Assert.Null(flagReset.ToString());
            Assert.Null(flag.GetFlag());
        }
    }
}
