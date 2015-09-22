using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Command;
using Core.Domain.Contract;

namespace Core.Bus
{
    public class CommnadMessage
    {
        public CommnadMessage(ICommand command, string commandName, ICurrent current)
        {
            Command = command;
            CommandName = commandName;
            Current = current;
        }
        public ICommand Command { get; set; }
        public string CommandName { get; set; }
        public ICurrent Current { get; set; }
    }
}
