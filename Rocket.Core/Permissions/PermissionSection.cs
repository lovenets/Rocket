﻿using System;
using Rocket.API.Configuration;
using Rocket.Core.Configuration;

namespace Rocket.Core.Permissions
{
    public class PermissionSection
    {
        protected IConfigurationElement element;

        public PermissionSection(string id, IConfigurationElement element)
        {
            this.element = element;
            Id = id;
            Permissions = new string[0];
        }

        public PermissionSection() { }

        public virtual string Id { get; set; }

        [ConfigArray(ElementName = "Permission")]
        public virtual string[] Permissions { get; set; } = new string[0];

        public virtual void Save() => throw new NotSupportedException();

        public void SetConfigElement(IConfigurationElement element)
        {
            this.element = element;
        }

        public virtual string[] GetGroups() => throw new NotSupportedException();
        public virtual void SetGroups(string[] groups) => throw new NotSupportedException();
    }
}