using MyBlog.Db;

public class LikeProcessorService : BackgroundService
{
    private readonly IQueueClient _queueClient;
    private readonly BlogDbContext _context;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var messages = await _queueClient.ReceiveMessagesAsync(maxMessages: 100);

            if (messages.Any())
            {
                var groupedMessages = messages.GroupBy(m => m.ArticleId);

                foreach (var group in groupedMessages)
                {
                    var articleId = group.Key;
                    var netLikes = group.Sum(m => m.Action == "like" ? 1 : -1);

                    // Update the database with aggregated likes/unlikes
                    var article = await _context.Articles.FindAsync(articleId);
                    if (article != null)
                    {
                        article.LikeCount += netLikes;
                        _context.Update(article);
                    }
                }

                await _context.SaveChangesAsync();
                await _queueClient.DeleteMessagesAsync(messages); // Remove processed messages
            }

            await Task.Delay(1000); // Throttle processing
        }
    }
}
