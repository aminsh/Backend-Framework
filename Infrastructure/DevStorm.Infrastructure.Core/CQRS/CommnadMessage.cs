using System;
using System.Linq;
using DevStorm.Infrastructure.Core.Api;
using DevStorm.Infrastructure.Core.Domain;
using DevStorm.Infrastructure.Utility;
using Newtonsoft.Json.Linq;


namespace DevStorm.Infrastructure.Core.CQRS
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

            var type = TypeService.GetType(e => e.Name == typeName);

            var command = ObjectExtention.Deserialize(token["Command"].ToString(), type).As<ICommand>();

            return new CommnadMessage(command, typeName, current);
        }
    }
}