using ShiftType.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftType.Tests
{
    public class UserInfoServiceTest
    {
        [Fact]
        public void TestGetFirstPartOfEmail()
        {
            //test for valid
            var correctEmail = UserInfoService.GetFirstPartOfEmail("test@test.com");
            Assert.Equal("test", correctEmail);

            //test for invalid
            var incorrectEmail = UserInfoService.GetFirstPartOfEmail("test1234");
            Assert.Equal("", incorrectEmail);
        }
    }
}
