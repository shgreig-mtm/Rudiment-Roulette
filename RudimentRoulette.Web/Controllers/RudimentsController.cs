using Microsoft.AspNetCore.Mvc;
using RudimentRoulette.Web.Data;
using RudimentRoulette.Web.Models;

namespace RudimentRoulette.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RudimentsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Rudiment>> GetRudiments([FromQuery] int? difficulty = null)
    {
        var rudiments = RudimentStore.Rudiments;

        if (difficulty.HasValue)
        {
            // Filter to include all rudiments at or below the specified difficulty level
            rudiments = rudiments.Where(r => (int)r.Difficulty <= difficulty.Value).ToList();
        }

        return Ok(rudiments);
    }

    [HttpGet("random")]
    public ActionResult<Rudiment> GetRandomRudiment([FromQuery] int? difficulty = null)
    {
        var rudiments = RudimentStore.Rudiments;

        if (difficulty.HasValue)
        {
            // Filter to include all rudiments at or below the specified difficulty level
            rudiments = rudiments.Where(r => (int)r.Difficulty <= difficulty.Value).ToList();
        }

        if (rudiments.Count == 0)
        {
            return NotFound("No rudiments found for the specified difficulty level.");
        }

        var random = new Random();
        var randomIndex = random.Next(rudiments.Count);
        return Ok(rudiments[randomIndex]);
    }
}
