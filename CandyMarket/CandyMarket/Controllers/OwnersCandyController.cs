using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CandyMarket.Models;
using CandyMarket.DataAccess;

namespace CandyMarket.Controllers
{
    [Route("api/ownerscandy")]
    [ApiController]
    public class OwnersCandyController : ControllerBase
    {

        OwnersCandyRepository _repository;

        public OwnersCandyController(OwnersCandyRepository repository)
        {
            _repository = repository;
        }

        CandyRepository  _candyRepository;
        

        [HttpPost("eatcandy/{candyId}/user/{userId}")]
        public IActionResult EatsCandy(int userId, int candyId)
        {
            var updatedOwnerCandy = _repository.EatsCandy(userId, candyId);
            return Ok(updatedOwnerCandy);
        }

        [HttpGet("flavor/{flavor}/candy")]
        public IActionResult RandomCandyWithExpRecDate(string flavor)
        {
            var candy = _repository.RandomCandyWithExpRecDate(flavor);
            return Ok(candy);
        }

        [HttpGet("viewcandy/eaten/user/{userId}")]
        public IActionResult EatenCandy(int userId)
        {
            var candies = _repository.EatenCandy(userId);
            return Ok(candies);
        }

        [HttpPut("tradecandy/user/{userId1}/user/{userId2}")]
        public IActionResult TradesCandy(int userId1, int userId2)
        {
            Candy user1Candy = _repository.GetOldestCandyForUser(userId1);
            Candy user2Candy = _repository.GetOldestCandyForUser(userId2);
            var candies = _repository.TradesCandy(userId1, user1Candy.ID, userId2, user2Candy.ID);
            return Ok(candies);
        }
    }
}