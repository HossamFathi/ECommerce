using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTO.Entities.Identity
{
    public class ChangePasswordViewModel
    {
        public string ID { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
