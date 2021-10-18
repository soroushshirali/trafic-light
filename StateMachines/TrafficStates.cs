using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traffic_light.StateMachines
{
    public enum TrafficStates
    {
        NSgreen_WEred,
        NSyellow_WEred,
        NSred_WEgreen,
        NSred_WEyellow,
    }
}
