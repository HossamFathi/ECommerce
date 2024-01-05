using DTO.Entities.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threenine.Data.Paging;

namespace ServiceLayer.Message.Helper
{
    public interface IMessage
    {
        Task<IPaginate<MessageDTO>> GetMessages(int index  , int size);
        Task Insert(MessageDTO message);
        Task<MessageDTO> Get(Guid ID);
    }
}
