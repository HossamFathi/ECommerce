﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DTO.Entities.Identity
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage ="يجب ادخال البريد الالكترونى")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
