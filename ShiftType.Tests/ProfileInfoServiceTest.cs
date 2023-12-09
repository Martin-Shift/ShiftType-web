using Microsoft.EntityFrameworkCore;
using ShiftType.DbModels;
using ShiftType.Factories;
using ShiftType.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace ShiftType.Tests
{
    
    public class ProfileInfoServiceTest
    {
        [Fact]
        public void TestProfile()
        {
            var user = UserFactory.CreateRandom();
            var profile = ProfileInfoService.GenerateInfo(user);
            Assert.NotNull(profile);
            Assert.NotNull(profile.Results);
        }
    }

}
