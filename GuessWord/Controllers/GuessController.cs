using GuessWord.Models;
using GuessWord.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessWord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuessController : ControllerBase
    {
        private readonly GuessService _guessService;

        public GuessController(GuessService guessService)
        {
            _guessService = guessService;
        }

        [HttpPost]
        public IActionResult GuessWord([FromBody]GuessWordRequest request)
        {
            return Ok(_guessService.GuessWord(request.Guess));
        }

        [HttpGet("history")]
        public IActionResult GetHistory()
        {
            return Ok(_guessService.GetHistory());
        }
    }
}
