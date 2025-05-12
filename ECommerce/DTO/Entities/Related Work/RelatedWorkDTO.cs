using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Entities.RelatedWork
{
    public class RelatedWorkDTO : BaseRelatedWorkDTO
    {
        public string Describtion { get; set; }
        public string ArabicDescribtion { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Photo { get; set; }


    }
}
