using EntityUpdateQueueExample;

var service = new MyService();


List<Task> tasks = new List<Task>();

for (int i = 1; i <= 5; i++)
{
    int entityId = i; // Her döngüde yeni bir değişken oluşturuluyor.

    tasks.Add(Task.Run(async () =>
    {
        await service.UpdateEntityAsync(entityId);
        Console.WriteLine(entityId);                             
    }));
}



int j = 0;



for (int i = 1; i <= 5; i++)
{
    tasks.Add(Task.Run(async () =>
    {
        await service.UpdateEntityAsync(i);
        Console.WriteLine(i);
    }));
}




await Task.WhenAll(tasks);


Console.WriteLine("Press any key to exit...");
Console.ReadKey();
