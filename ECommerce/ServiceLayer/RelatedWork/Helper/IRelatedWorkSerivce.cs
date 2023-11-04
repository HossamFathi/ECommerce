using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.RelatedWorks.Helper
{
    public interface IRelatedWorkSerivce
    {
        Task<IEnumerable<RelatedWorkDTO>> getAll(int index, int size);
        Task<IEnumerable<RelatedWorkDTO>> getAll(int productID);
        Task<RelatedWorkDTO> get(int RelatedWorkID);
        Task<bool> Update(int RelatedWorkID, RelatedWorkDTO RelatedWork);
        Task<bool> Delete(int RelatedWorkID);
        Task Insert(RelatedWorkDTO RelatedWork);
    }
}
