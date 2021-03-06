﻿using System;
using Rocket.API.Drawing;
using Rocket.API.Commands;
using Rocket.API.DependencyInjection;
using Rocket.API.Logging;
using Rocket.API.User;
using Rocket.Core.Logging;

namespace Rocket.Tests.Mock
{
    public class TestConsole : IConsole
    {
        public IDependencyContainer Container { get; }

        public TestConsole(IDependencyContainer container)
        {
            SessionConnectTime = DateTime.Now;
            BaseLogger.SkipTypeFromLogging(GetType());
            Container = container.CreateChildContainer();
        }

        public string Id => "Console";
        public string Name => "Console";
        public string IdentityType => IdentityTypes.Console;

        public IUserManager UserManager => Container.Resolve<IUserManager>("console");
        public bool IsOnline => true;
        public DateTime SessionConnectTime { get; }
        public DateTime? SessionDisconnectTime => null;
        public DateTime? LastSeen => DateTime.Now;
        public string UserType => "Console";

        public void WriteLine(string format, params object[] bindings)
        {
            Console.WriteLine(format, bindings);
        }

        public void WriteLine(LogLevel level, string format, params object[] bindings)
        {
            Console.WriteLine(format, bindings);
        }

        public void WriteLine(LogLevel level, string format, Color? color = null, params object[] bindings)
        {
            Console.WriteLine(format, bindings);
        }

        public void WriteLine(string format, Color? color = null, params object[] bindings)
        {
            Console.WriteLine(format, bindings);
        }

        public void Write(string format, Color? color = null, params object[] bindings)
        {
            Console.Write(format, bindings);
        }

        public void Write(string format, params object[] bindings)
        {
            Write(format, null, bindings);
        }
    }
}