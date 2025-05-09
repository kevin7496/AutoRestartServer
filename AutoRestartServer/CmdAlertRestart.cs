using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MEC;

namespace AutoRestartServer
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class CmdAlertRestart : ICommand
    {
        string ICommand.Command { get; } = "technicalrr";
        string[] ICommand.Aliases { get; } = { "tech" };
        string ICommand.Description { get; } = "Оповещение о рестрате сервера";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player pl = Player.Get(sender);

            if (!pl.CheckPermission("allertrr"))
            {
                response = "У вас нет прав.";
                return false;
            }
            if (arguments.IsEmpty()) 
            {
                response = "Абодончик, сука забыл время указать";
                return false; 
            }
            int conv = Convert.ToInt32(arguments.Array[0]) * 60;
            ushort time = (ushort)conv;
            response = $"Рестрарт сервер через {time}м";
            Map.Broadcast(
                duration: time,
                message: "<color=#FF4500><b>ВНИМАНИЕ ТЕХ. РЕСТРАТ СЕРВЕРА ЧЕРЕЗ {time}м"
                );
            Timing.CallDelayed(time, () => 
            {
                Plugin.plugin.Restart();
            });
            return true;
        }
    }
}
