using Xunit;
using IIG.FileWorker;
using IIG.BinaryFlag;

namespace IntegrationTesting
{
    public class FileWorkerTest
    {
        [Fact]
        public void Write_ReadAll_Flag()
        {
            var flag = new MultipleBinaryFlag(2);
            var flagTxt = flag.ToString();

            BaseFileWorker.Write(flagTxt, "File1.txt");
            string res = BaseFileWorker.ReadAll("File1.txt");

            Assert.Equal(flagTxt, res);
        }

        [Fact]
        public void Write_ReadAll_Boolean()
        {
            var flag = new MultipleBinaryFlag(2, false);
            var flagTxt = flag.GetFlag().ToString();

            BaseFileWorker.Write(flagTxt, "File2.txt");
            string res = BaseFileWorker.ReadAll("File2.txt");

            Assert.Equal(flagTxt, res);
        }

        [Fact]
        public void Write_ReadAll_Null()
        {
            var flag = new MultipleBinaryFlag(2);
            flag.Dispose();
            var flagTxt = flag.ToString();

            BaseFileWorker.Write(flagTxt, "File3.txt");
            string res = BaseFileWorker.ReadAll("File3.txt");

            Assert.Equal("", res);
        }

        [Fact]
        public void Write_ReadLines()
        {
            var flag = new MultipleBinaryFlag(2);
            flag.ResetFlag(1);
            var flagTxt = flag.ToString();

            BaseFileWorker.Write(flagTxt, "File4.txt");
            string[] res = BaseFileWorker.ReadLines("File4.txt");

            Assert.Equal(flagTxt, res[0]);
        }

        [Fact]
        public void TryWrite_ReadAll()
        {
            var flag = new MultipleBinaryFlag(2, false);
            flag.SetFlag(1);
            var flagTxt = flag.ToString();

            BaseFileWorker.TryWrite(flagTxt, "File5.txt");
            string res = BaseFileWorker.ReadAll("File5.txt");

            Assert.Equal(flagTxt, res);
        }

        [Fact]
        public void TryCopy_ReadAll()
        {
            var flag = new MultipleBinaryFlag(2, false);
            var flagTxt = flag.ToString();

            BaseFileWorker.Write(flagTxt, "File6.txt");
            BaseFileWorker.TryCopy("File6.txt", "File7.txt", true);
            string res = BaseFileWorker.ReadAll("File7.txt");

            Assert.Equal(flagTxt, res);
        }
    }
}

