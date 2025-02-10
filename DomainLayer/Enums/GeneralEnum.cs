using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public class GeneralEnum
    {
        public enum OrderEnum
        {
            Pending,
            Processing,
            Cancelled,
            Failed,
            Successfully
        }
        public enum IdType
        {
            [Description("Id")]
            Id = 1,
            [Description("User Id")]
            UserId = 2,
            [Description("Order Id")]
            OrderId = 3
        }
        public enum UserStatusEnum
        {
            NotVerified,
            Active,
            Disabled,
            // status = 0: Inactive, 1: Active, 2: Disabled
        }
        public enum UserRoleEnum
        {
            Admin,
            Staff,
            Parent,
            Tutor,
            Kid,
        }
        public enum TutorProfileEnum
        {
            Draft,
            Pending,
            Publish,
            Disable,
        }
        public enum ScheduleStatusEnum
        {
            Active,
            InActive,
        }
        public enum CourseStatusEnum
        {
            Draft,
            Pending,
            Publish,
            Disable,
        }
    }
}
