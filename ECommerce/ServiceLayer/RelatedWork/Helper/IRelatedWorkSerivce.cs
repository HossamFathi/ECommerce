using DTO;
using DTO.Entities.RelatedWork;
using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threenine.Data.Paging;

namespace ServiceLayer.RelatedWorks.Helper
{
    public interface IRelatedWorkSerivce
    {
        Task<IPaginate<RelatedWorkDTO>> getAll(int index, int size);
        Task<IPaginate<RelatedWorkDTO>> getAll(int productID);
        Task<RelatedWorkDTO> get(int RelatedWorkID);
        Task<bool> Update(int RelatedWorkID, AddRelatedWorkDTO RelatedWork);
        Task<bool> Delete(int RelatedWorkID);
        Task Insert(AddRelatedWorkDTO RelatedWork);
        Task<IPaginate<RelatedWorkDTO>> getAll(int productID, LanguageCode code = LanguageCode.en);
        Task<IPaginate<RelatedWorkDTO>> getAll(int index, int size, LanguageCode code = LanguageCode.en);
    }
}
