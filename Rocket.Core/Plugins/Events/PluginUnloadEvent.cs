﻿using Rocket.API.Eventing;
using Rocket.API.Plugins;

namespace Rocket.Core.Plugins.Events
{
    public class PluginUnloadEvent : PluginEvent
    {
        public PluginUnloadEvent(IPluginManager plugnManager, IPlugin plugin, bool global = true) : base(
            plugnManager,
            plugin, global) { }

        public PluginUnloadEvent(IPluginManager plugnManager, IPlugin plugin,
                                     EventExecutionTargetContext executionTarget = EventExecutionTargetContext.Sync,
                                     bool global = true) : base(plugnManager, plugin, executionTarget, global) { }

        public PluginUnloadEvent(IPluginManager plugnManager, IPlugin plugin) : base(plugnManager, plugin) { }
    }
}