﻿using Rocket.API.Eventing;
using Rocket.API.Plugins;

namespace Rocket.Core.Plugins.Events
{
    public class PluginUnloadedEvent : PluginEvent
    {
        public PluginUnloadedEvent(IPluginManager pluginManager, IPlugin plugin, bool global = true) : base(
            pluginManager, plugin, global) { }

        public PluginUnloadedEvent(IPluginManager pluginManager, IPlugin plugin,
                                      EventExecutionTargetContext executionTarget = EventExecutionTargetContext.Sync,
                                      bool global = true) : base(pluginManager, plugin, executionTarget, global) { }

        public PluginUnloadedEvent(IPluginManager pluginManager, IPlugin plugin) : base(pluginManager, plugin) { }
    }
}