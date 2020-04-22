using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConsoleApp1
{
    public class Tests
    {
        string[] results;
        public Tests()
        {
            var items = Program.LoadCsv();
            Program.LoadValuesToDic(items);
            results = Program.Suggest("ne");
        }
        [Fact]
        public void Test_ne_Should_Return_4_Items()
        {
            Assert.Equal(4, results.Length);
        }

        [Fact]
        public void Test_ne_Should_Return_These_4_Items_In_Order()
        {
            Assert.Equal("next", results[0]);
            Assert.Equal("neck", results[1]);
            Assert.Equal("necessary", results[2]);
            Assert.Equal("neighborly", results[3]);
        }
    }
}
