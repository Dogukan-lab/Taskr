using Microsoft.AspNetCore.Mvc;

namespace TaskrIntegration;

public static class TestUtil
{
    public static void SafetyChecks(IActionResult result)
    {
        SafetyChecks<OkObjectResult>(result);
    }
    
    public static void SafetyChecks<T>(IActionResult result)
    {
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<T>(), () => (result as ObjectResult)?.Value?.ToString());
    }
}