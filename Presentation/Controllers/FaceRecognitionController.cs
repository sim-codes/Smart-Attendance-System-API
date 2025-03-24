using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/faces")]
    [ApiController]
    public class FaceRecognitionController : ControllerBase
    {
        private readonly IServiceManager _service;

        public FaceRecognitionController(IServiceManager service) => _service = service;

        [HttpPost("detect")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DetectFacesFromUrl([FromBody] FaceDetectionDto faceDetection)
        {
            try
            {
                var result = await _service.FaceRecognitionService.DetectFacesFromUrlAsync(faceDetection.ImageUrl);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("verify")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> VerifyFacesFromUrl([FromBody] FaceVerificationDto faceVerification)
        {
            try
            {
                var result = await _service.FaceRecognitionService.VerifyFacesFromUrlAsync(faceVerification.ImageUrl1, faceVerification.ImageUrl2);
                return Ok(new
                {
                    IsIdentical = result.IsIdentical,
                    Confidence = result.Confidence
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
