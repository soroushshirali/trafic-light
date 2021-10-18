using System;
using traffic_light;
using traffic_light.Models;
using traffic_light.StateMachines;
using Xunit;

namespace traffic_light_tests
{
    public class StateMachineTests
    {
        [Fact]
        public void When_TrafficLoad_is_High_Should_Keep_NorthSouth_40Seconds_Green()
        {
            TrafficStateMachine trafficStateMachine = new TrafficStateMachine(new DateTimeProvider(new DateTime(2021, 07, 20,9,40,10)));

            //get initial status
            var lightsStatus = trafficStateMachine.SecondClockPulse();
            Assert.Equal(LightColor.green, lightsStatus.North);
            Assert.Equal(LightColor.red, lightsStatus.West);

            //should be the same light status after 10 second
            lightsStatus = TriggerStateMachineClockPulse_N_Times(trafficStateMachine, 10);
            Assert.Equal(LightColor.green, lightsStatus.North);
            Assert.Equal(LightColor.red, lightsStatus.West);

            //should change north-south to yellow after 40 second
            lightsStatus = TriggerStateMachineClockPulse_N_Times(trafficStateMachine, 30);
            Assert.Equal(LightColor.yellow, lightsStatus.North);
            Assert.Equal(LightColor.red, lightsStatus.West);

            //should change north-south to red after 5 second
            lightsStatus = TriggerStateMachineClockPulse_N_Times(trafficStateMachine, 5);
            Assert.Equal(LightColor.red, lightsStatus.North);
            Assert.Equal(LightColor.green, lightsStatus.West);

            //should change west-east to yellow after 10 second
            lightsStatus = TriggerStateMachineClockPulse_N_Times(trafficStateMachine, 10);
            Assert.Equal(LightColor.red, lightsStatus.North);
            Assert.Equal(LightColor.yellow, lightsStatus.West);

            //should change west-east to red after 5 second
            lightsStatus = TriggerStateMachineClockPulse_N_Times(trafficStateMachine, 5);
            Assert.Equal(LightColor.green, lightsStatus.North);
            Assert.Equal(LightColor.red, lightsStatus.West);
        }

        [Fact]
        public void When_TrafficLoad_is_Low_Should_Keep_NorthSouth_20Seconds_Green()
        {
            TrafficStateMachine trafficStateMachine = new TrafficStateMachine(new DateTimeProvider(new DateTime(2021, 07, 20, 13, 40, 10)));

            //get initial status
            var lightsStatus = trafficStateMachine.SecondClockPulse();
            Assert.Equal(LightColor.green, lightsStatus.North);
            Assert.Equal(LightColor.red, lightsStatus.West);

            //should change north-south to yellow after 20 second
            lightsStatus = TriggerStateMachineClockPulse_N_Times(trafficStateMachine, 20);
            Assert.Equal(LightColor.yellow, lightsStatus.North);
            Assert.Equal(LightColor.red, lightsStatus.West);

            //should change north-south to red after 5 second
            lightsStatus = TriggerStateMachineClockPulse_N_Times(trafficStateMachine, 5);
            Assert.Equal(LightColor.red, lightsStatus.North);
            Assert.Equal(LightColor.green, lightsStatus.West);

            //should change west-east to yellow after 10 second
            lightsStatus = TriggerStateMachineClockPulse_N_Times(trafficStateMachine, 20);
            Assert.Equal(LightColor.red, lightsStatus.North);
            Assert.Equal(LightColor.yellow, lightsStatus.West);

            //should change west-east to red after 5 second
            lightsStatus = TriggerStateMachineClockPulse_N_Times(trafficStateMachine, 5);
            Assert.Equal(LightColor.green, lightsStatus.North);
            Assert.Equal(LightColor.red, lightsStatus.West);
        }

        private LightsStatus TriggerStateMachineClockPulse_N_Times(TrafficStateMachine trafficStateMachine, int N)
        {
            LightsStatus lastResult = null;

            for (int i = 0; i < N; i++)
            {
                lastResult = trafficStateMachine.SecondClockPulse();
            }

            return lastResult;
        }
    }
}
