using ShiftType.DbModels;
using ShiftType.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShiftType.Factories;

namespace ShiftType.Tests
{
    public class UserLevelServiceTest
    {
        [Fact]
        public void AddExpTest()
        {
            var user = UserFactory.CreateRandom();
            UserLevelService.AddExp(user, UserLevelService.GetNextLevelExp(user.Level) - user.Exp - 1);
            Assert.Equal(1, UserLevelService.GetNextLevelExp(user.Level) - user.Exp);
        }

        [Fact]
        public void LevelUpTest()
        {
            
            var user = UserFactory.CreateRandom();
            var level = user.Level;
            UserLevelService.AddExp(user, UserLevelService.GetNextLevelExp(user.Level));
            Assert.Equal(level+1, user.Level);

        }

        
    }
}
