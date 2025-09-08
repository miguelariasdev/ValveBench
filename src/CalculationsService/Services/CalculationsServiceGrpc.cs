using Grpc.Core;
using ValveBench.proto;

namespace CalculationsService.Services;

public class CalculationsServiceGrpc : Calc.CalcBase
{
    public override Task<TestResult> Calculate(TestProfile req, ServerCallContext ctx)
    {
        var peak = Math.Max(req.P1Bar, req.P2Bar);

        var drop = (peak * 0.0005) * req.Cycles + Math.Max(0, (req.TempC - 60) * 0.001 * peak);

        var leak = drop > peak * 0.02;

        return Task.FromResult(new TestResult
        {
            PeakPressureBar = Math.Round(peak, 2),
            PressureDropBar = Math.Round(drop, 3),
            LeakDetected = leak
        });
    }
}
