using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ApiResult;
using Core.Bus;

namespace Core.Command
{
    public interface ISendCommandToValidator
    {
        IValidationResult Validate(CommnadMessage message);
    }
}
