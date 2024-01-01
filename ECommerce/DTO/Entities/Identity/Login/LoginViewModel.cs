
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DTO.Entities.Identity.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="يجب ادخال البريد الالكترونى")]
        [StringLength(50,ErrorMessage ="البريد الاكترونى لا يتعدى 50 حرف")]
        [EmailAddress(ErrorMessage ="تاكد من ادخال البريد الاكترونى بشكل صحيح")]
        
        public string Email { get; set; }
        [Required(ErrorMessage ="يجب ادخال كلمة المرور")]
        [DataType(DataType.Password)]
     
        public string Password { get; set; }
    }
}
