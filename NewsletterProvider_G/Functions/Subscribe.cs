using Data.Contexts;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace NewsletterProvider_G.Functions;

public class Subscribe(ILogger<Subscribe> logger, DataContext context)
{
    private readonly ILogger<Subscribe> _logger = logger;
    private readonly DataContext _context = context;

    [Function("Subscribe")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        try
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            if (!string.IsNullOrWhiteSpace(body))
            {
                var subscriber = JsonConvert.DeserializeObject<SubscribeToNewsletter>(body);
                if (subscriber != null && subscriber.Email != null)
                {
                    var existingSubscriber = await _context.Subscribers.FirstOrDefaultAsync(s => s.Email == subscriber.Email);
                    if (existingSubscriber != null)
                    {
                        if (subscriber.PreferredEmail != null)
                        {
                            existingSubscriber.PreferredEmail = subscriber.PreferredEmail;
                            _context.Update(existingSubscriber);
                        }
                        else
                        {
                            existingSubscriber.Circle1 = subscriber.Circle1;
                            existingSubscriber.Circle2 = subscriber.Circle2;
                            existingSubscriber.Circle3 = subscriber.Circle3;
                            existingSubscriber.Circle4 = subscriber.Circle4;
                            existingSubscriber.Circle5 = subscriber.Circle5;
                            existingSubscriber.Circle6 = subscriber.Circle6;
                        }

                        await _context.SaveChangesAsync();
                        return new OkObjectResult(new { Status = 200, Message = "Subscriber was updated" });
                    }
                    if (subscriber.PreferredEmail != null)
                    {
                        SubscribeEntity subscribeEntity = new SubscribeEntity
                        {
                            Email = subscriber.Email,
                            PreferredEmail = subscriber.PreferredEmail,
                            Circle1 = subscriber.Circle1,
                            Circle2 = subscriber.Circle2,
                            Circle3 = subscriber.Circle3,
                            Circle4 = subscriber.Circle4,
                            Circle5 = subscriber.Circle5,
                            Circle6 = subscriber.Circle6
                        };
                        _context.Subscribers.Add(subscribeEntity);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        SubscribeEntity subscribeEntity = new SubscribeEntity
                        {
                            Email = subscriber.Email,
                            Circle1 = subscriber.Circle1,
                            Circle2 = subscriber.Circle2,
                            Circle3 = subscriber.Circle3,
                            Circle4 = subscriber.Circle4,
                            Circle5 = subscriber.Circle5,
                            Circle6 = subscriber.Circle6,
                        };
                        _context.Subscribers.Add(subscribeEntity);
                        await _context.SaveChangesAsync();
                    }

                    return new OkObjectResult(new { Status = 200, Message = "Subscribed sucessfully" });
                }

            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Unable to subscribe");
            return new BadRequestObjectResult(new { Status = 400, Message = "Unable to subscribe right now." });
        }
        return new BadRequestObjectResult(new { Status = 400, Message = "Unable to subscribe right now." });
    }
}

public class SubscribeToNewsletter
{
    public string Email { get; set; } = null!;
    public string? PreferredEmail { get; set; }
    public bool Circle1 { get; set; }
    public bool Circle2 { get; set; }
    public bool Circle3 { get; set; }
    public bool Circle4 { get; set; }
    public bool Circle5 { get; set; }
    public bool Circle6 { get; set; }
}

