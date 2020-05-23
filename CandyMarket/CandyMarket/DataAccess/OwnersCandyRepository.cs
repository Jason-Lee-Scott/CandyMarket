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

    }
}

