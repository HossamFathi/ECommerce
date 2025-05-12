using AutoMapper;
using DataBaseLayer;
using DataBaseLayer.models;
using DTO.Entities.Message;
using ServiceLayer.Message.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threenine.Data.Paging;

namespace ServiceLayer.Message
{
    internal class MessageLogic : IMessage
    {
        private readonly IRepository<Messages> _Message;
        private readonly IMapper _Mapper;
        public MessageLogic(IRepository<Messages> message, IMapper mapper)
        {
            _Message = message;
            _Mapper = mapper;
        }
        public async Task<IPaginate<MessageDTO>> GetMessages(int index, int size)
        {
          IPaginate<Messages> Messages = await _Message.GetAll( mess=> mess.IsVerify ==false,index:index, size:size);
            return Paginate.From(Messages, ConvertToMessageDto);
        }

        public async Task Insert(MessageDTO message)
        {
            message.ID =  Guid.NewGuid();
             await _Message.InsertEntityAsync(ConvertToMessage(message));
        }

        public async Task<MessageDTO> Get(Guid ID)
        {
          Messages message = await _Message.SingleOrDefaultAsync(x => x.ID == ID);
            return ConvertToMessageDto(message);
        }

        public async Task Verify(Guid ID)
        {
            Messages message = await _Message.SingleOrDefaultAsync(x => x.ID == ID);
            if (message == null)
                return;
            message.IsVerify = true;
            message.VerifyTime = DateTime.Now;
          await  _Message.update(message);
            return;

        }
        #region Helper
        private MessageDTO ConvertToMessageDto(Messages message) {

           return _Mapper.Map<MessageDTO>(message);
        }
        private IEnumerable<MessageDTO> ConvertToMessageDto(IEnumerable<Messages> message) {
        
            return message.Select(ConvertToMessageDto);
        
        }
        private Messages ConvertToMessage(MessageDTO message) {
            return _Mapper.Map<Messages>(message);
        }

       



        #endregion
    }
}
