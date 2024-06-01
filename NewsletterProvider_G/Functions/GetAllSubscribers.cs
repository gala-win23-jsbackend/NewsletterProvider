

using Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NewsletterProvider_G.Functions;

public class GetAllSubscribers(ILogger<GetAllSubscribers> logger, DataContext context)
{
    private readonly ILogger<GetAllSubscribers> _logger = logger;
    private readonly DataContext _context = context;

    [Function("GetSubscribers")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        var subscribers = _context.Subscribers.ToList();
        return new OkObjectResult(subscribers);
    }
}
