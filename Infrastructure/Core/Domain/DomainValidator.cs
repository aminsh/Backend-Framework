using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ApiResult;
using Core.Domain.Contract;

namespace Core.Domain
{
    public class DomainValidator
    {
        public ICurrent Current { get; set; }
        public IValidationResult ValidationResult { get; set; }
    }
}
