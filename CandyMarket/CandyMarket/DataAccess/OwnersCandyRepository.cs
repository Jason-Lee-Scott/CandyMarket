using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyMarket.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace CandyMarket.DataAccess
{
    public class OwnersCandyRepository
    {
        const string ConnectionString = "Server=localhost;Database=CandyMarket;Trusted_Connection=True;";

        public OwnersCandy EatsCandy(int userId, int candyId)
        {
            var sql = @"with EatCandy_view as (
	                        select top(1) * from OwnersCandy
	                        where 
		                        UserId = @UserId 
		                        and CandyId = @CandyId
                                and IsEaten = 0
	                        order by DateReceived
                        )
                        update EatCandy_view
                        set IsEaten = 1, EatenDate = getdate()
                        output inserted.*";

            var parameters = new
            {
                UserId = userId,
                CandyId = candyId
            };

            using (var db = new SqlConnection(ConnectionString))
            {
                var result = db.QueryFirstOrDefault<OwnersCandy>(sql, parameters);
                return result;
            }
        }

        public List<EatenOwnersCandy> EatenCandy(int userId)
        {
            var sql = @"select 
		                    oc.ID as OwnersCandyId, DateReceived, us.Name as UserName, can.Name as CandyName, IsEaten, EatenDate
	                    from OwnersCandy as oc
	                    join [User] as us on us.ID = oc.UserId
	                    join Candy as can on oc.CandyId = can.ID
	                    where 
		                    UserId = @UserId
		                    and IsEaten = 1
	                    order by DateReceived";

            var parameters = new
            {
                UserId = userId
            };

            using (var db = new SqlConnection(ConnectionString))
            {
                var result = db.Query<EatenOwnersCandy>(sql, parameters).ToList();
                return result;
            }
        }
    }
}
