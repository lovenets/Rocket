﻿using System;
using System.Collections.Generic;
using System.Linq;
using Rocket.API.Collections;
using Rocket.API.Commands;
using Rocket.API.Providers;
using Rocket.API.Providers.Plugins;
using Rocket.API.Providers.Commands;

namespace Rocket.Plugins.Native
{
    public class NativeRocketCommandProvider : IRocketCommandProvider
    {
        public NativeRocketCommandProvider(IRocketPluginProvider manager)
        {
            Commands = new List<IRocketCommand>();
        }

        public List<IRocketCommand> Commands { get; }
        public void AddCommands(IEnumerable<IRocketCommand> commands)
        {
            Commands.AddRange(commands.AsEnumerable());
        }

        public void Load(bool isReload = false)
        {

        }

        public void Unload()
        {

        }
    }
}