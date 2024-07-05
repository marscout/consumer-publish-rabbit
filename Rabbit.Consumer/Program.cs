using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;
using Rabbit.Models;
using Rabbit.Models.Entities;

var factory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "admin",
    Password = "123456",
};
using var connection = factory.CreateConnection();

using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "rabbitMensagesQueue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);


    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        RabbitMensagem mensagem = JsonSerializer.Deserialize<RabbitMensagem>(message ?? "");
        Console.WriteLine($" Titulo: {mensagem.Titulo}; Text: {mensagem.Texto}");
    };
    channel.BasicConsume(queue: "rabbitMensagesQueue",
                         autoAck: true,
                         consumer: consumer);

    Console.WriteLine("Enter");
    Console.ReadLine();
}