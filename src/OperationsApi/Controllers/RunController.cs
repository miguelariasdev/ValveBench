using Microsoft.AspNetCore.Mvc;
using OperationsApi.Dtos;
using ValveBench.proto;

namespace OperationsApi.Controllers
{
[ApiController]
[Route("api")]
    public class RunController : ControllerBase
    {
        private readonly Calc.CalcClient _calc;

        public RunController(Calc.CalcClient calc)
        {
            _calc = calc;
        }

        [HttpPost("run")]
        public async Task<ActionResult<ResultDto>> Run([FromBody] ProfileDto dto)
        {
            if (dto.P1Bar < 0 || dto.P2Bar < 0 || dto.Cycles < 1 || dto.TempC < -50 || dto.TempC > 200)
                return BadRequest("Parámetros fuera de rango.");

            var res = await _calc.CalculateAsync(new TestProfile
            {
                P1Bar = dto.P1Bar,
                P2Bar = dto.P2Bar,
                Cycles = dto.Cycles,
                TempC = dto.TempC
            });

            return Ok(new ResultDto
            {
                PeakPressureBar = res.PeakPressureBar,
                PressureDropBar = res.PressureDropBar,
                LeakDetected = res.LeakDetected
            });
        }
    }
}
