﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rocket.API.Eventing;

namespace Rocket.Tests.Eventing
{
    [TestClass]
    public class DefaultEventingTests : EventingTestBase
    {
        protected override IEventManager GetEventManager() => Runtime.Container.Resolve<IEventManager>();
    }
}