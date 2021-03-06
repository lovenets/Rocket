﻿using Rocket.API.Eventing;
using Rocket.API.Plugins;

namespace Rocket.Core.Plugins.Events
{
    public class PluginLoadEvent : PluginEvent, ICancellableEvent
    {
        public PluginLoadEvent(IPluginManager pluginManager, IPlugin plugin) : base(pluginManager, plugin) { }

        public PluginLoadEvent(IPluginManager pluginManager, IPlugin plugin, bool global = true) : base(
            pluginManager,
            plugin, global) { }

        public PluginLoadEvent(IPluginManager pluginManager, IPlugin plugin,
                                   EventExecutionTargetContext executionTarget = EventExecutionTargetContext.Sync,
                                   bool global = true) : base(pluginManager, plugin, executionTarget, global) { }

        public bool IsCancelled { get; set; }
    }
}