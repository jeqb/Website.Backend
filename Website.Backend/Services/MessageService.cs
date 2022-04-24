﻿using Website.Backend.Domain;
using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.Extensions;
using Website.Backend.Models;
using Website.Backend.Services.Interfaces;

namespace Website.Backend.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<MessageDomain> _messageRepository;

        public MessageService(IRepositoryFactory repositoryFactory)
        {
            _messageRepository = repositoryFactory.CreateMessageRepository();
        }

        public async Task<MessageModel> Create(MessageModel entity)
        {
            MessageDomain createdModel = await _messageRepository.Create(entity.ToDomain());

            return createdModel.ToModel();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MessageModel>> GetAll()
        {
            IEnumerable<MessageDomain> results = await _messageRepository.GetAll();

            return results.Select(
                (message) => message.ToModel()
                );
        }

        public async Task<MessageModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<MessageModel> Update(MessageModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
