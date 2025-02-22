using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainLayer.Enums.GeneralEnum;

namespace ApplicationLayer.DTOs.Account
{
    public class AccountDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public UserStatusEnum status { get; set; }
    }
}
