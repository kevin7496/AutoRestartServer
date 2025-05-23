﻿using System;
using System.Linq;
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

            if (Plugin.plugin.Config.RestartTimer)
            {
                response = "Таймер уже запущен";
                return false;
            }

            if (arguments.IsEmpty()) 
            {
                response = "Абодончик, сука забыл время указать";
                return false; 
            }
            Plugin.plugin.Config.RestartTimer = true;
            string arg = arguments.ElementAt(0);
            int conv = int.Parse(arg) * 60;
            ushort time = (ushort)conv;
            response = $"Рестрарт сервер через {time}м";
            Map.ClearBroadcasts();
            Map.Broadcast(
                duration: time,
                message: $"<color=#FF4500><b>ВНИМАНИЕ ТЕХ. РЕСТРАТ СЕРВЕРА ЧЕРЕЗ</color> {arg}м"
                );
            Timing.CallDelayed(time, () => 
            {
                Plugin.plugin.Restart();
            });
            return true;
        }
    }
}
