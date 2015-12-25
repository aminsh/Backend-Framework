using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStorm.Application.Domain
{
    public enum Status
    {
        [Display(Name = "انجام نشده")]
        ToDo = 100,
        [Display(Name = "در حال انجام")]
        Doing = 200,
        [Display(Name = "تمام شده")]
        Done = 300
    }
}
