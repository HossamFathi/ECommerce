
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DTO.Entities.Identity
{
    public class RegiesterViewModel
    {


        [Required(ErrorMessage ="يجب ادخال البريد الالكترونى")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "يجب ان يكون البريد الالكترونى اقل من 50 حرف")]

        [EmailAddress]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "تأكد من ادخال كلمة المرور")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "يجب ان تكون كلمة المرور اكثر من 5 حروف واقل من 50 حرف")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="تأكد من ادخال تاكيد كلمة المرور")]
        [Compare(nameof(Password), ErrorMessage ="تأكد من ادخال تأكيد كلمة المرور مشابهة لكلمة المرور")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "يجب ان تكون كلمة المرور اكثر من 5 حروف واقل من 50 حرف")]

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
