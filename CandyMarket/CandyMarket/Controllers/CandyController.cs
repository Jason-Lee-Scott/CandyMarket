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
    [Route("api/candy")]
    [ApiController]
    public class CandyController : ControllerBase
    {
        CandyRepository _repository;

        public CandyController(CandyRepository repository)
        {
            _repository = repository;
        }

        //GET: api/candy/candyByDate
        [HttpGet("candyByDate")]
        public IActionResult GetCandyByDate()
        {
            
            var candies = _repository.GetCandyByDates();

            if (!candies.Any())
            {
                return NotFound();
            }
            return Ok(candies);
        }
    }
}