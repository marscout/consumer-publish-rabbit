using Rabbit.Models.Entities;
using Rabbit.Repositories.Interfaces;
using Rabbit.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Services
{
    public class RabbitMensagemService : IRabbitMensagemService
    {
        private readonly IRabbitMensagemRepository _repository;

        public RabbitMensagemService(IRabbitMensagemRepository repository)
        {
            _repository = repository;
        }
        public void SendMensage(RabbitMensagem mensagem)
        {
            _repository.SendMensage(mensagem);
        }
    }
}
