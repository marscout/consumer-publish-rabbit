﻿using Rabbit.Models.Entities;
using Rabbit.Repositories.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace Rabbit.Repositories
{
    public class RabbitMensagemRepository : IRabbitMensagemRepository
    {
        public void SendMensage(RabbitMensagem mensagem)
        {
            var factory = new ConnectionFactory { 
                HostName = "localhost",
                UserName = "admin",
                Password = "123456",
            };
            using var connection = factory.CreateConnection();

            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "rabbitMensagesQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


                var json = JsonSerializer.Serialize(mensagem);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                    routingKey: "rabbitMensagesQueue",
                    basicProperties: null,
                    body: body);
            }
        }
    }
}
