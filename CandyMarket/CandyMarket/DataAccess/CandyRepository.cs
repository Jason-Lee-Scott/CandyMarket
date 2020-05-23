﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyMarket.Models;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CandyMarket.DataAccess
{
    public class CandyRepository
    {
        const string ConnectionString = "Server=localhost;Database=CandyMarket;Trusted_Connection=True;";

        public IEnumerable<CandyWithEarliestDate> GetCandyByDates()
        {
            var sql = @"SELECT OwnersCandy.ID, Candy.Name, MIN(CASE WHEN (Candy.ExpirationDate < OwnersCandy.DateReceived)
		                        THEN Candy.ExpirationDate ELSE OwnersCandy.DateReceived END) AS EarliestDate
                            FROM Candy
                            JOIN OwnersCandy 
                            ON OwnersCandy.CandyId = Candy.ID
                            GROUP BY OwnersCandy.ID, Candy.Name
                            ORDER BY EarliestDate ASC;";

           

            using (var db = new SqlConnection(ConnectionString))
            {
                var result = db.Query<CandyWithEarliestDate>(sql);
                
                return result;
            }
        }
    }
}
