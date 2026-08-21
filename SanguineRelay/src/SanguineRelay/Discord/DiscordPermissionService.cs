using Discord.WebSocket;
using SanguineRelay.Core;

namespace SanguineRelay.Discord;

internal enum RelayPermission
{
    ViewStatus,
    ViewPlayer,
    Announce,
    Administer
}

internal sealed class DiscordPermissionService
{
    private readonly ulong _guildId;
    private readonly PermissionOptions _permissions;

    public DiscordPermissionService(RelayOptions options)
        : this(options.Discord.GuildId, options.Permissions)
    {
    }

    internal DiscordPermissionService(ulong guildId, PermissionOptions permissions)
    {
        _guildId = guildId;
        _permissions = permissions;
    }

    public bool CanExecute(SocketSlashCommand command, RelayPermission permission)
    {
        if (command.GuildId != _guildId || command.User is not SocketGuildUser guildUser)
        {
            return false;
        }

        return CanExecute(command.GuildId, guildUser.Roles.Select(role => role.Id), permission);
    }

    internal bool CanExecute(ulong? guildId, IEnumerable<ulong> roleIds, RelayPermission permission)
    {
        if (guildId != _guildId)
        {
            return false;
        }

        if (permission == RelayPermission.ViewStatus)
        {
            return true;
        }

        var roles = roleIds as IReadOnlyCollection<ulong> ?? roleIds.ToArray();
        var isAdmin = roles.Any(_permissions.AdminRoleIds.Contains);
        if (permission == RelayPermission.Administer)
        {
            return isAdmin;
        }

        return isAdmin || roles.Any(_permissions.ModeratorRoleIds.Contains);
    }
}
