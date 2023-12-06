

using ShiftType.Services;
using Xunit;

namespace ShiftType.Tests
{
    public class TypeHelperServiceTest
    {
        [Fact]
        public void TestErrors()
        {
            //test for no errors
            var errorTest1 = TypeHelperService.CountErrors("Hello", "Hello");
            Assert.Equal(errorTest1, 0);

            //test for 1 error
            var errorTest2 = TypeHelperService.CountErrors("Hallo", "Hello");
            Assert.Equal(errorTest2, 1);

            //test for different lengths
            var errorTest3 = TypeHelperService.CountErrors("Hello", "Hello World!");
            Assert.Equal(errorTest3, 0);

            //test for input overflow
            Action erroract = () => TypeHelperService.CountErrors("Hello World!", "Hello");
            Assert.ThrowsAny<Exception>(erroract);

        }
        [Fact]
        public void TestWpm()
        {
            //test for no errors
            var wpmTest1 = TypeHelperService.CountWPM("Hello", "Hello", 30);
            Assert.Equal(wpmTest1, (int)(2*(5 / 4.5)));

            //test for 1 error
            var wpmTest2 = TypeHelperService.CountWPM("Hallo", "Hello", 30);
            Assert.Equal(wpmTest2, (int)(2 * (5 / 4.5)-1));

        }
    }
}