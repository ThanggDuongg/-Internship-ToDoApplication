using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Entities;
using TodoApplication.Models.DTOs.Requests;

namespace TodoApplication.Test.MockData
{
    public class UserMockData
    {
        public static List<UserEntity> GetUserEntities()
        {
            return new List<UserEntity>
            {
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Username = "Thang01",
                    Password = "Thang01"
                },
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Username = "Thang02",
                    Password = "Thang02"
                },
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Username = "Thang03",
                    Password = "Thang03"
                }
            };
        }

        public static UserAuthRequest NewUserAuthenRequest()
        {
            return new UserAuthRequest
            {
                //Id = Guid.NewGuid(),
                Username = "Thang04",
                Password = "Thang04"
            };
        }

        public static UserAuthRequest NewUserAuthenDTO_EmptyParams()
        {
            return new UserAuthRequest
            {
                Username = "Thang"
            };
        }
    }
}
