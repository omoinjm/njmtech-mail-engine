using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.Engine.Service.Shared.Queries
{
    public class RepositoryQuery
    {
        #region Select Queries
        public static string GetMailboxes()
        {
            var query = $@"
                
            SELECT TOP {batchNumber}

                s.Id,
                s.Msisdn,
                s.IsProcessed,
                s.CreatedDate

            FROM MTN_GrantThortonMsisdn s
            
            WHERE
                s.IsProcessed = 0 AND
                s.MSISDN NOT IN (
                    SELECT MSISDN FROM MTN_GrantThortonRecord
                    WHERE
                        -- JM: Not sure if the data updates everyday on MTN (will monitor)
                        CallDate = CONVERT(DATE, GETDATE()) AND
                        BillMonth = (SELECT FORMAT(dbo.GetCurrentBillMonth(), 'yyyy-MM-dd')) -- JM: If Bill Month is not passed use the current Bill Month.
                )
        ";

            return query;
        }

        #endregion

        #region Update Queries

        #endregion

        #region Procedure Execution

        #endregion
    }
}