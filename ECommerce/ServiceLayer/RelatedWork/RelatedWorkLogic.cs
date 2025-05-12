using AutoMapper;
using DataBaseLayer;
using DataBaseLayer.models;
using DTO;
using DTO.Entities.Category;
using DTO.Entities.RelatedWork;
using DTO.Enums;
using ServiceLayer.RelatedWorks.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threenine.Data.Paging;

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
         RelatedWork work = await _related.SingleOrDefaultAsync(re => re.ID == RelatedWorkID);
            if (work == null)
            {
                return null; 
            }
          return  ConvertToRelatedWorkDTO(work);
        }
        public async Task<IPaginate<RelatedWorkDTO>> getAll(int index, int size)
        {
            IPaginate<RelatedWork> Works = await _related.GetAll( index: index, size: size);
            return Paginate.From(Works, ConvertToRelatedWorkDTO);
        }
        public async Task<IPaginate<RelatedWorkDTO>> getAll(int index, int size, LanguageCode code = LanguageCode.en)
        {
            IPaginate<RelatedWork> Works = await _related.GetAll(index: index, size: size);
            return Translate(code, Works);
        }

        public async Task Insert(AddRelatedWorkDTO RelatedWork)
        {
          RelatedWork work =  ConvertToRelatedWork(RelatedWork);
            if (!string.IsNullOrEmpty(RelatedWork.GetPhotoUrl()))
            {
                work.Photo = RelatedWork.GetPhotoUrl();
            }
                await  _related.InsertEntityAsync(work);
        }
        public async Task<bool> Update(int RelatedWorkID, AddRelatedWorkDTO RelatedWorkDTO)
        {
            RelatedWork relatedWork = await _related.SingleOrDefaultAsync(re => re.ID == RelatedWorkID);
            if (relatedWork == null)
                return false;
            if (string.IsNullOrEmpty(RelatedWorkDTO.GetPhotoUrl()))
            {
                string PhotoPath = relatedWork.Photo;
                _mapper.Map(RelatedWorkDTO, relatedWork, typeof(RelatedWorkDTO), typeof(RelatedWork));
                relatedWork.Photo = PhotoPath;
            }
            else
            {
                _mapper.Map(RelatedWorkDTO, relatedWork, typeof(RelatedWorkDTO), typeof(RelatedWork));
            }
           
            return await _related.update(relatedWork);
        }
        public async Task<IPaginate<RelatedWorkDTO>> getAll(int productID)
        {
            IPaginate<RelatedWorkDTO> Works = await _related.GetAll( re => ConvertToRelatedWorkDTO(re), wo=>wo.ProductId == productID);
            return Works;
        }
        public async Task<IPaginate<RelatedWorkDTO>> getAll(int productID , LanguageCode code =LanguageCode.en)
        {
            IPaginate<RelatedWork> Works = await _related.GetAll( wo => wo.ProductId == productID);

            return Translate(code, Works);


        }

        #region Helper
        private IPaginate<RelatedWorkDTO> Translate(LanguageCode code , IPaginate<RelatedWork> Works)
        {
            return code == LanguageCode.en ?
               Paginate.From(Works, ConvertToRelatedWorkDTO) :
               Paginate.From(Works, ConvertToArabicRelatedWorkDto);
        }
      
        private RelatedWorkDTO ConvertToRelatedWorkDTO(RelatedWork work)
        {
            return _mapper.Map<RelatedWorkDTO>(work);
        }
        private RelatedWorkDTO ConvertToRelatedWorkDTO_Arabic(RelatedWork work)
        {
            return new RelatedWorkDTO
            {
                ID = work.ID,
                Describtion = work.ArabicDescribtion,
                Name = work.ArabicName,
                Photo = work.Photo,
                ProductId = work.ProductId
            };
        }
        private RelatedWork ConvertToRelatedWork(RelatedWorkDTO relatedWork)
        {
            return _mapper.Map<RelatedWork>(relatedWork);
        }
        private RelatedWork ConvertToRelatedWork(AddRelatedWorkDTO relatedWork)
        {
            return _mapper.Map<RelatedWork>(relatedWork);
        }
        private AddRelatedWorkDTO ConvertToAddRelatedWorkDto(RelatedWork relatedWork)
        {
            return _mapper.Map<AddRelatedWorkDTO>(relatedWork);
        }
        private IEnumerable<AddRelatedWorkDTO> ConvertToAddRelatedWorkDto(IEnumerable<RelatedWork> relatedWork)
        {
            List<AddRelatedWorkDTO> relatedWorkDTOs = new List<AddRelatedWorkDTO>();
            foreach(RelatedWork relatedWorkItem in relatedWork)
            {
                relatedWorkDTOs.Add(ConvertToAddRelatedWorkDto(relatedWorkItem));
            }

            return relatedWorkDTOs;
        }

        private IEnumerable<RelatedWorkDTO> ConvertToRelatedWorkDTO(IEnumerable<RelatedWork> relatedWork)
        {
            List<RelatedWorkDTO> relatedWorkDTOs = new List<RelatedWorkDTO>();
            foreach (RelatedWork relatedWorkItem in relatedWork)
            {
                relatedWorkDTOs.Add(ConvertToRelatedWorkDTO(relatedWorkItem));
            }

            return relatedWorkDTOs;
        }
        private IEnumerable<RelatedWorkDTO> ConvertToArabicRelatedWorkDto(IEnumerable<RelatedWork> relatedWork)
        {
            List<RelatedWorkDTO> relatedWorkDTOs = new List<RelatedWorkDTO>();
            foreach (RelatedWork relatedWorkItem in relatedWork)
            {
                relatedWorkDTOs.Add(ConvertToRelatedWorkDTO_Arabic(relatedWorkItem));
            }

            return relatedWorkDTOs;
        }
        #endregion
    }
}
