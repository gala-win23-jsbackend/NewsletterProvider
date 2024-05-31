using Data.Contexts;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace NewsletterProvider_G.Functions;

public class Unsubscribe(ILogger<Unsubscribe> logger, DataContext context)
{
    private readonly ILogger<Unsubscribe> _logger = logger;
    private readonly DataContext _context = context;

    [Function("Unsubscribe")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        if (!string.IsNullOrEmpty(body))
        {
            var SubscribeEntity = JsonConvert.DeserializeObject<SubscribeEntity>(body);
            if (SubscribeEntity != null)
            {
                var existingSubscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Email == SubscribeEntity.Email);
                if (existingSubscriber != null)
                {
                    _context.Remove(existingSubscriber);
                    await _context.SaveChangesAsync();
                    return new OkObjectResult(new { Status = 200, Message = "Subscriber wat unsubscribed." });
                }

            }
        }
        return new BadRequestObjectResult(new { Status = 400, Message = "Invalid request." });
    }
}
