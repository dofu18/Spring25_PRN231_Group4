using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Constants
{
    public class Constants
    {
        public static class Errors
        {
            public const string NOT_EXIST_ERROR = "not exist";
            public const string ALREADY_EXIST_ERROR = "already exist";
            public static string ENUM_NOT_EXIST_ERROR(string Enum, int? EnumNum)
            {
                return $"Please choose the correct {Enum} [1-{EnumNum}]";
            }
        }
        public static class Http
        {
            public const string API_VERSION = "v1";
            public const string CORS = "CORS";
            public const string JSON_CONTENT_TYPE = "application/json";
            public const string USER_POLICY = "User";
        }
        public static class Entities
        {
            public const string BOUGHT_COURSE = "Bought Course ";
            public const string CATEGORY = "Category ";
            public const string COURSE = "Course ";
            public const string COURSE_CATEGORY = "Course Category ";
            public const string ORDER = "Order ";
            public const string ORDER_COURSE = "Order Course ";
            public const string REVIEW = "Review "; 
            public const string SCHEDULE = "Schedule ";
            public const string TRANSACTION_HISTORY = "Transaction History ";
            public const string TUTOR_PROFILE = "Tutor Profile ";
            public const string USER = "User ";
            public const string REFRESH_TOKEN = "Refresh Token ";
        }
    }
}
