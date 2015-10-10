using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Command;
using Core.Domain.Contract;
using Newtonsoft.Json.Linq;
using Utility;

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

        public static CommnadMessage Deserialize(string message)
        {
            var token = JObject.Parse(message);
            var typeName = token["CommandName"].ToString();
            var current = ObjectExtention.Deserialize<CurrentForBus>(token["Current"].ToString());

            AppDomain.CurrentDomain.Load("Commands");
            var type =
                AppDomain.CurrentDomain.GetAssemblies()
                    .First(a => a.GetName().Name == "Commands")
                    .GetTypes()
                    .First(t => t.Name == typeName);

            var command = ObjectExtention.Deserialize(token["Command"].ToString(), type).As<ICommand>();

            return new CommnadMessage(command, typeName, current);
        }
    }
}