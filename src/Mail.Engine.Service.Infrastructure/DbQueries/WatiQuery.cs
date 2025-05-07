namespace Mail.Engine.Service.Infrastructure.DbQueries
{
    public class WatiQuery
    {
        #region Select Queries
        public static string GetWatiConfigQuery()
        {
            var query = $@"
                
                SELECT

                    ""RecordId"",
                    ""Bearer"",
                    ""BaseUrl"",
                    ""ClientID"" AS ClientId

                FROM ""WatiConfig""

            ";

            return query;
        }
        #endregion
    }
}
