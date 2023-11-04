using AutoMapper;
using DataBaseLayer;
using DataBaseLayer.models;
using DTO;
using ServiceLayer.RelatedWorks.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.RelatedWorks
{
    internal class RelatedWorkLogic : IRelatedWorkSerivce
    {
        private readonly IRepository<RelatedWork> _related;
        private readonly IMapper _mapper;
        public RelatedWorkLogic(IRepository<RelatedWork> related, IMapper mapper)
        {
            _related = related;
            _mapper = mapper;
        }
        public async Task<bool> Delete(int RelatedWorkID)
        {
          return await  _related.Delete(RelatedWorkID);
        }

        public async Task<RelatedWorkDTO> get(int RelatedWorkID)
        {
         RelatedWork work = await _related.singleOrDefault(re => re.ID == RelatedWorkID);
            if (work == null)
            {
                return null; 
            }
          return  ConvertToRelatedWorkDTO(work);
        }

        private RelatedWorkDTO ConvertToRelatedWorkDTO(RelatedWork work)
        {
          return  _mapper.Map<RelatedWorkDTO>(work);
        }

        public async Task<IEnumerable<RelatedWorkDTO>> getAll(int index, int size)
        {
          IEnumerable<RelatedWork> Works =  await _related.getAll();
            return Works.Select(ConvertToRelatedWorkDTO);
        }

        public async Task Insert(RelatedWorkDTO RelatedWork)
        {
          RelatedWork work =  ConvertToRelatedWork(RelatedWork);
         await  _related.InsertEntityAsync(work);
        }

        private RelatedWork ConvertToRelatedWork(RelatedWorkDTO relatedWork)
        {
            return _mapper.Map<RelatedWork>(relatedWork);
        }

        public async Task<bool> Update(int RelatedWorkID, RelatedWorkDTO RelatedWorkDTO)
        {
            RelatedWork relatedWork = await _related.singleOrDefault(re => re.ID == RelatedWorkID);
            if (relatedWork == null)
                return false;
            relatedWork.Name = RelatedWorkDTO.Name;
            relatedWork.Describtion = RelatedWorkDTO.Describtion;
            relatedWork.Photo = RelatedWorkDTO.Photo;
            relatedWork.ProductId = RelatedWorkDTO.ProductId;
            return await _related.update(relatedWork);
        }

        public async Task<IEnumerable<RelatedWorkDTO>> getAll(int productID)
        {
            IEnumerable<RelatedWork> Works = await _related.getAll(wo=>wo.ProductId == productID);
            return Works.Select(ConvertToRelatedWorkDTO);
        }
    }
}
