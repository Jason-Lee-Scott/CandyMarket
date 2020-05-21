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
        OwnersCandyRepository _repository = new OwnersCandyRepository();

        [HttpPost("eatcandy/{candyId}/user/{userId}")]
        public IActionResult EatsCandy(int userId, int candyId)
        {
            var updatedOwnerCandy = _repository.EatsCandy(userId, candyId);
            return Ok(updatedOwnerCandy);
        }
    }
}