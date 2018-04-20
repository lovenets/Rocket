﻿using System;
using System.Collections.Generic;
using Rocket.API.Commands;
using Rocket.API.Permissions;
using Rocket.API.Player;
using Rocket.Core.Permissions;

namespace Rocket.Core.Commands.RocketCommands
{
    public class CommandPermission : ICommand
    {
        public string Name => "PermissionGroup";
        public string Permission => "Rocket.Permissions.ManagePermissions";
        public string Syntax => "<add/remove/reload>";
        public string Description => "Manage rocket permissions";

        public ISubCommand[] ChildCommands => new ISubCommand[] { new PermissionSubCommandAdd(), new PermissionSubCommandRemove(), new PermissionSubCommandReload() };
        public string[] Aliases => new[] {"P"};

        public void Execute(ICommandContext context)
        {
            throw new CommandWrongUsageException();
        }

        public bool SupportsCaller(ICommandCaller caller)
        {
            return true;
        }
    }
    public abstract class PermissionSubCommandUpdate : ISubCommand<CommandPermission>
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Permission { get; }
        public string Syntax => "<[p]layer/[g]roup> [target] [permission]";

        public ISubCommand[] ChildCommands => null;
        public abstract string[] Aliases { get; }

        public bool SupportsCaller(ICommandCaller caller)
        {
            return true;
        }

        public void Execute(ICommandContext context)
        {
            if (context.Parameters.Length != 3)
                throw new CommandWrongUsageException();

            IPermissible target;
            string permission;
            string permissionFailMessage;

            var type = context.Parameters.Get<string>(0).ToLower();
            var targetName = context.Parameters.Get<string>(1);
            var permissionToUpdate = context.Parameters.Get<string>(2);

            var permissions = context.Container.Get<IPermissionProvider>("default_permissions");

            switch (type)
            {
                case "g":
                case "group":
                    permission = "Rocket.Permissions.ManageGroups." + targetName;
                    permissionFailMessage = "You don't have permissions to manage this group.";

                    target = permissions.GetGroup(targetName);
                    if (target == null)
                    {
                        context.Caller.SendMessage($"Group \"{targetName}\" was not found.", ConsoleColor.Red);
                        return;
                    }
                    break;

                case "p":
                case "player":
                    permission = "Rocket.Permissions.ManagePlayers";
                    permissionFailMessage = "You don't have permissions to manage permissions of players.";
                    target = context.Parameters.Get<IPlayer>(2);
                    break;

                default:
                    throw new CommandWrongUsageException();
            }

            if (permissions.CheckPermission(context.Caller, permission) != PermissionResult.Grant)
                throw new NotEnoughPermissionsException(context.Caller, permission, permissionFailMessage);

            UpdatePermission(context.Caller, permissions, target, permissionToUpdate);
        }

        protected abstract void UpdatePermission(ICommandCaller caller, IPermissionProvider permissions, IPermissible target, string permissionToUpdate);
    }

    public class PermissionSubCommandAdd : PermissionSubCommandUpdate
    {
        public override string Name => "Add";
        public override string Description => "Add a permission to a group or player";
        public override string Permission => "Rocket.Permissions.ManagePermissions.Add";
        public override string[] Aliases => new[] {"a", "+"};

        protected override void UpdatePermission(ICommandCaller caller, IPermissionProvider permissions, IPermissible target, string permissionToUpdate)
        {
            if (permissions.AddPermission(target, permissionToUpdate))
            {
                caller.SendMessage($"Successfully added \"{permissionToUpdate}\" to \"{target.Name}\"!", ConsoleColor.DarkGreen);
            }
            else
            {
                caller.SendMessage($"Failed to add \"{permissionToUpdate}\" to \"{target.Name}\"!", ConsoleColor.Red);
            }
        }
    }

    public class PermissionSubCommandRemove : PermissionSubCommandUpdate
    {
        public override string Name => "Remove";
        public override string Description => "Remove permission to a group or player";
        public override string Permission => "Rocket.Permissions.ManagePermissions.Remove";
        public override string[] Aliases => new[] {"r", "-"};

        protected override void UpdatePermission(ICommandCaller caller, IPermissionProvider permissions, IPermissible target, string permissionToUpdate)
        {
            if (permissions.RemovePermission(target, permissionToUpdate))
            {
                caller.SendMessage($"Successfully removed \"{permissionToUpdate}\" from \"{target.Name}\"!", ConsoleColor.DarkGreen);
            }
            else
            {
                caller.SendMessage($"Failed to remove \"{permissionToUpdate}\" from \"{target.Name}\"!", ConsoleColor.Red);
            }
        }
    }

    public class PermissionSubCommandReload : ISubCommand<CommandPermission>
    {
        public string Name => "Reload";
        public string Description => "Reload permissions";
        public string Permission => "Rocket.Permissions.ManagePermissions.Reload";
        public string Syntax => "";
        public ISubCommand[] ChildCommands => null;
        public string[] Aliases => new[] {"R"};
        public bool SupportsCaller(ICommandCaller caller)
        {
            return true;
        }

        public void Execute(ICommandContext context)
        {
            var permissions = context.Container.Get<IPermissionProvider>();
            permissions.Reload();
            context.Caller.SendMessage("Permissions have been reloaded.", ConsoleColor.DarkGreen);
        }
    }
}