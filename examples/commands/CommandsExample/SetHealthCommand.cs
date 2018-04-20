﻿using System.Collections.Generic;
using Rocket.API.Commands;
using Rocket.API.Player;

namespace Rocket.Examples.CommandsPlugin
{
    public class SetHealthCommand : ICommand
    {
        public string Name => "SetHealth";
        public string Description => "Set health of players";
        public string Permission => "Rocket.Examples.SetHealth";
        public string Syntax => "<target player> <health>";
        public ISubCommand[] ChildCommands => null;
        public string[] Aliases => new[] {"sh"};
        public bool SupportsCaller(ICommandCaller caller)
        {
            return true; //supports all callers (e.g. Console, Player, etc...)
        }

        public void Execute(ICommandContext context)
        {
            IOnlinePlayer target = context.Parameters.Get<IOnlinePlayer>(0); // target player is first parameter
            double health = context.Parameters.Get<double>(1); // health is second parameter

            if (target is ILivingEntity entity)
                entity.Health = health;
            else // the game likely does not support killing players (e.g. Eco)
                context.Caller.SendMessage("Target could not be killed :(");
        }
    }
}