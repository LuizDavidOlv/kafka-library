using data_worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<KafkaConsumerWorker>();

var host = builder.Build();
host.Run();
