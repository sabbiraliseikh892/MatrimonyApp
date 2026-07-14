using Matrimony.Application.Features.UserInterests;
using Matrimony.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserInterestController : ControllerBase
    {
        private readonly IUserInterestService _interestService;
        private readonly ICurrentUserService _currentUserService;

        public UserInterestController(
            IUserInterestService interestService,
            ICurrentUserService currentUserService)
        {
            _interestService = interestService;
            _currentUserService = currentUserService;
        }
        //-----------------------------------------------------
        // Send Interest
        //-----------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> SendInterest(
            CreateUserInterestRequest request)
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            await _interestService.SendInterestAsync(
                currentUserId,
                request);

            return Ok(new
            {
                Message = "Interest sent successfully."
            });
        }
        //-----------------------------------------------------
        // Accept / Reject
        //-----------------------------------------------------

        [HttpPut("{interestId}")]
        public async Task<IActionResult> RespondInterest(
            Guid interestId,
            RespondUserInterestRequest request)
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            await _interestService.UpdateInterestStatusAsync(
                currentUserId,
                interestId,
                request);

            return Ok(new
            {
                Message = "Interest updated successfully."
            });
        }
        //-----------------------------------------------------
        // Received
        //-----------------------------------------------------

        [HttpGet("received")]
        public async Task<IActionResult> GetReceived()
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            var result =
                await _interestService.GetReceivedAsync(
                    currentUserId);

            return Ok(result);
        }
        //-----------------------------------------------------
        // Sent
        //-----------------------------------------------------

        [HttpGet("sent")]
        public async Task<IActionResult> GetSent()
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            var result =
                await _interestService.GetSentAsync(
                    currentUserId);

            return Ok(result);
        }
        //-----------------------------------------------------
        // Details
        //-----------------------------------------------------

        [HttpGet("{interestId}")]
        public async Task<IActionResult> GetById(
            Guid interestId)
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            var result =
                await _interestService.GetByIdAsync(
                    interestId,
                    currentUserId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
