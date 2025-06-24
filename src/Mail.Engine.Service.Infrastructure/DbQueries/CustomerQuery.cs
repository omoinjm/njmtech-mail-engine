using Mail.Engine.Service.Core.Helpers.Constants;

namespace Mail.Engine.Service.Infrastructure.DbQueries
{
    public static class CustomerQuery
    {
        public static string CustomerIdListQuery()
        {
            var query = $@"

                SELECT 

                    u.""Id"" AS CustomerId,
                    u.""Name"" AS FirstName,
                    u.""Surname"",
                    u.""AccountCreationDate"",
                    u.""IsAccountActive"",
                    u.""PhoneNumber"",

                    u.""CountryId"",
                    c.""CountryName"",
                    c.""Alpha3Code"",
                    c.""MobileCode"",
                    c.""MobileRegex"",
                    c.""TimeZone""

                FROM ""AspNetUsers"" u
                LEFT JOIN ""AspNetUserRoles"" ur ON u.""Id"" = ur.""UserId""
                LEFT JOIN ""AspNetRoles"" r ON ur.""RoleId"" = r.""Id""
                LEFT JOIN ""Countries"" c ON u.""CountryId"" = c.""CountryId""

                WHERE r.""Id"" = '{RoleConstants.CUSTOMER_ID}' OR r.""RoleCode"" = '{RoleConstants.CUSTOMER_CODE}'

            ";

            return query;
        }

        public static string GetCustomerSessionQuery()
        {
            var query = $@"
                SELECT
                
                ""Id"",
                ""WalletyUserId"" AS CustomerId,
                ""SessionToken"",
                ""BearerSessionToken"",
                ""StartTime"",
                ""EndTime"",
                ""SessionHashKey"",
                ""LastActiveTime"",
                ""IsActive"",
                ""IsAutoLogout""

                FROM ""UserSessions"" WHERE ""WalletyUserId"" = @CustomerId
            ";

            return query;
        }

        public static string GetLoginKeyQuery()
        {
            var query = $@"
                SELECT * FROM ""LoginKeys"" WHERE ""WalletyUserId"" = @CustomerId
            ";

            return query;
        }

        public static string RemoveLoginKeyQuery()
        {
            var query = $@"
                DELETE FROM ""LoginKeys"" WHERE ""Key"" = @Key
            ";

            return query;
        }

        public static string UpdateUserSessionQuery()
        {
            var query = $@"
                
                UPDATE ""UserSessions"" SET

                    ""IsActive"" = FALSE,
                    ""EndTime"" = NOW(),
                    ""IsAutoLogout"" = TRUE

                WHERE ""Id"" = @UserSessionId

            ";

            return query;
        }
    }
}
