using System;
using Exiled.API.Features;
using UnityEngine;

namespace AutoRestartServer
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "AutoRestartServer";
        public override string Author => "kevin";
        public override string Prefix => "AutoRestartServer";
        public override Version Version => new Version(1, 0, 0);

        public static Plugin plugin;
        public override void OnEnabled()
        {
            plugin = this;
            Map.ClearBroadcasts();
            Map.Broadcast(
                duration: 20,
                message: $"<#00FF00><b>Внимание рестрат сервера был успешно произведён.</color>\n<size=60><b>Удачной игры</b></size>"
                );
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            plugin = null;
            base.OnDisabled();
        }

        public void Restart()
        {
            Server.Restart();
        }
    }
}      
