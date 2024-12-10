using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoi3Launcher.Models
{
    public record Mod(string Name, string Path)
    {
        public bool IsEnabled { get; set; }
    }
}
