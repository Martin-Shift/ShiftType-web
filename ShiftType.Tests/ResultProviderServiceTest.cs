using ShiftType.DbModels;
using ShiftType.Factories;
using ShiftType.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftType.Tests
{
    public class ResultProviderServiceTest
    {
        [Fact]
        public static void TestGetBestTimeResult()
        {   
            //test for list
            var results = ResultFactory.CreateSome(5);
            var max = ResultProviderService.GetBestTimeResult(results,15);           
            Assert.Equal(max.Wpm,results.MaxBy(x=> x.Wpm).Wpm);

            //test for empty
            var empty = new List<Result>();
            var max2 = ResultProviderService.GetBestTimeResult(empty,15);
            Assert.Equal(max2.Wpm, 0);
        }
        [Fact]
        public static void TestGetBestWordResult()
        {
            //test for list
            var results = ResultFactory.CreateSome(5);
            var max = ResultProviderService.GetBestWordResult(results, 10);
            Assert.NotNull(max);

            //test for empty
            var empty = new List<Result>();
            var max2 = ResultProviderService.GetBestWordResult(empty, 10);
            Assert.NotNull(max2);
        }
        [Fact]
        public static void TestAverageBy()
        {
            //test for list
            var results = ResultFactory.CreateSome(5);
            var max = ResultProviderService.AverageBy(results, x=> x.Wpm);
            Assert.NotNull(max);

            //test for empty
            var empty = new List<Result>();
            var max2 = ResultProviderService.AverageBy(empty, x => x.Wpm);
            Assert.Equal(max2, 0);


            //test for null
            List<Result>? Null = null;
            var max3 = ResultProviderService.AverageBy(Null, x => x.Wpm);
            Assert.Equal(max3, 0);
        }
        [Fact]
        public static void TestMaxBy()
        {
            //test for list
            var results = ResultFactory.CreateSome(5);
            var max = ResultProviderService.MaxBy(results, x => x.Wpm);
            Assert.NotNull(max);

            //test for empty
            var empty = new List<Result>();
            var max2 = ResultProviderService.MaxBy(empty, x => x.Wpm);
            Assert.Equal(max2, 0);


            //test for null
            List<Result>? Null = null;
            var max3 = ResultProviderService.MaxBy(Null, x => x.Wpm);
            Assert.Equal(max3, 0);
        }
    }
}
