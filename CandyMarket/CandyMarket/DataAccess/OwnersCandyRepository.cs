using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyMarket.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

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

        public CandyWithDateReceived RandomCandyWithExpRecDate(string flavorToSearch)
        {
            var sql = @"select c.*, o.DateReceived
                        from Candy c
                    	JOIN OwnersCandy o 
	                    ON o.CandyId = c.Id
                        where flavor = @flavor
                        order by ExpirationDate,DateReceived                                       
		                        ";

            var parameters = new
            {
                flavor = flavorToSearch
            };

            using (var db = new SqlConnection(ConnectionString))
            {
                var candy = db.QueryFirstOrDefault<CandyWithDateReceived>(sql, parameters);
                return candy;
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

        public OwnersCandy TradesCandy(int userId1, int userId2)
        {
            // User1 and User2 trade their oldest candies
            var sql = @"with TradeCandy1 as (
                            select top(1)
	                            OwnersCandy.*,
	                            [User].Name as OwnerName, [User].Id as UserId,
	                            Candy.Name as CandyName
                            from OwnersCandy
	                            join [User] on [User].Id = OwnersCandy.UserId
	                            join Candy on OwnersCandy.CandyId = Candy.ID
                            order by DateReceived asc
                            )
                            update TradeCandy1
	                            set DateReceived = getdate() 
	                            set UserId = @UserId2
	                            output inserted.*

                        with TradeCandy2 as (
                            select top(1)
	                            OwnersCandy.*,
	                            [User].Name as OwnerName, [User].Id as UserId,
	                            Candy.Name as CandyName
                            from OwnersCandy
	                            join [User] on [User].Id = OwnersCandy.UserId
	                            join Candy on OwnersCandy.CandyId = Candy.ID
                            order by DateReceived asc
                            )
                            update TradeCandy2
	                            set DateReceived = getdate()
	                            set UserId = @UserId1
	                            output inserted.*";

            var parameters = new
            {
                UserId2 = userId2,
                UserId1 = userId1
            };

            using (var db = new SqlConnection(ConnectionString))
            {
                var result = db.QueryFirstOrDefault<OwnersCandy>(sql, parameters);
                return result;
            }
        }
    }
}

