namespace EntityUpdateQueueExample
{
    public class MyService
    {
        private readonly object _lock = new object();
        private readonly Queue<int> _queue = new Queue<int>();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public async Task UpdateEntityAsync(int id)
        {
            // Kaydın güncellenmesi istendiğinde sıraya alınır
            lock (_lock)
            {
                _queue.Enqueue(id);
            }

            // Kilit mekanizması kullanılarak sıra dışında kalınır
            await _semaphore.WaitAsync();

            try
            {
                // Sıradaki kaydın güncellenmesi gerçekleştirilir
                while (true)
                {
                    int nextId;

                    lock (_lock)
                    {
                        if (_queue.Count == 0)
                        {
                            return;
                        }

                        nextId = _queue.Dequeue();
                    }

                    // Kaydın güncellenmesi gerçekleştirilir
                    using (var context = new MyDbContext())
                    {
                        var entity = await context.MyEntities.FindAsync(nextId);

                        if (entity == null)
                        {
                            continue;
                        }

                        entity.Property1 = "New Value";
                        await context.SaveChangesAsync();
                    }
                }
            }
            finally
            {
                // Kilit serbest bırakılır ve bir sonraki istek için sıra bekleyenlerin işlemleri gerçekleştirilir
                _semaphore.Release();
            }
        }
    }
}
