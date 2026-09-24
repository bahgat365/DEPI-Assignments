using System;
using System.Collections.Generic;
using System.Text;

namespace Session_09
{
    public static class Mapper
    {
        public static UserDto MapFromModelToDto(User user)
        {
            return new UserDto(user.Id, user.Name, user.Email);
        }
    }
}
