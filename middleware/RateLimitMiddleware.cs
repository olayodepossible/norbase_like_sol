using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using System.Threading.Tasks;

public class RateLimitMiddleware
{
    private static readonly ConcurrentDictionary<string, (int Count, DateTime Timestamp)> _rateLimits = new();

    private readonly RequestDelegate _next;
    private readonly int _maxRequests;
    private readonly TimeSpan _timeSpan;

    public RateLimitMiddleware(RequestDelegate next, int maxRequests, TimeSpan timeSpan)
    {
        _next = next;
        _maxRequests = maxRequests;
        _timeSpan = timeSpan;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userId = context.User.FindFirst("UserId")?.Value;

            var now = DateTime.UtcNow;
            var key = $"RateLimit_{userId}";

            var (count, timestamp) = _rateLimits.GetOrAdd(key, _ => (0, now));

            if (timestamp + _timeSpan > now)
            {
                if (count >= _maxRequests)
                {
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.Response.WriteAsync("Rate limit exceeded. Please try again later.");
                    return;
                }
                _rateLimits[key] = (count + 1, timestamp);
            }
            else
            {
                _rateLimits[key] = (1, now);
            }
        }
        
        await _next(context);
    }
}
