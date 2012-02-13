using System;
using System.Collections.Generic;

namespace Fusion.Core.Types
{
    public class KillLog
    {
        public IList<Attacker> Attackers;
        public long Id;
        public long MoonId;
        public long SolarSystemId;
        public DateTime Time;
        public Victim Victim;
    }
}