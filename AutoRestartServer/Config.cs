using System.Collections.Generic;
using Exiled.API.Interfaces;

namespace AutoRestartServer
{
    public class Config : IConfig
    {
        public bool IsEnabled {  get; set; } = true;
        public bool Debug {  get; set; } = false;
    }
}
