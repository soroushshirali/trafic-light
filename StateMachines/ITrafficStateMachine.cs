using traffic_light.Models;

namespace traffic_light.StateMachines
{
    public interface ITrafficStateMachine
    {
        LightsStatus SecondClockPulse();
    }
}