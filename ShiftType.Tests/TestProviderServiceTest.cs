using ShiftType.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftType.Tests
{
    public class TestProviderServiceTest
    {
        [Fact]
        public void GetWordsTest()
        {
            string validLanguage = "English"; 

            var words = TestProviderService.GetWords(validLanguage);

            Assert.NotNull(words);
            Assert.True(words.Length > 0);
        }
    }
}
