using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using traffic_light.Hubs;
using traffic_light.Hubs.Clients;
using traffic_light.Models;
using traffic_light.StateMachines;

namespace traffic_light
{
    public class BackgroundTrafficLight: IHostedService, IDisposable
    {
        private Timer timer;
        private readonly ILogger<BackgroundTrafficLight> _logger;
        private readonly IHubContext<TrafficHub, ITrafficClient> _trafficHub;
        private readonly ITrafficStateMachine _trafficStateMachine;
        
        private TimerCallback _timerCallback;
        private LightsStatus _previousLightStatus = null;

        public BackgroundTrafficLight(ILogger<BackgroundTrafficLight> logger,
            IHubContext<TrafficHub, ITrafficClient> trafficHub, 
            ITrafficStateMachine trafficStateMachine)
        {
            _logger = logger;
            _trafficHub = trafficHub;
            _trafficStateMachine = trafficStateMachine;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timerCallback += TriggerStateMachineAndUpdateLightsStatus;
            timer = new Timer(_timerCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private void TriggerStateMachineAndUpdateLightsStatus(object state)
        {
            var lightStatus = _trafficStateMachine.SecondClockPulse();

            if (_previousLightStatus == null 
                || _previousLightStatus.North != lightStatus.North
                || _previousLightStatus.West != lightStatus.West)
            {
                _trafficHub.Clients.All.ReceiveNewStatus(new LightsStatusDto(lightsStatus: lightStatus));
            }

            _previousLightStatus = lightStatus;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //todo: logging
            return Task.CompletedTask;
        }
    }
}
