using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using traffic_light.Models;

namespace traffic_light.StateMachines
{
    public class TrafficStateMachine : ITrafficStateMachine
    {
        //todo: read  these values form the config
        private readonly int _highTraffic_NorthSouth_TrafficDuration = 40;
        private readonly int _lowTraffic_NorthSouth_TrafficDuration = 20;
        private readonly int _highTraffic_WestEast_TrafficDuration = 10;
        private readonly int _lowTraffic_WestEast_TrafficDuration = 20;

        private TrafficStates _currentState;
        private int _timerCounter;
        private DateTimeProvider _dateTimeProvider;

        public TrafficStateMachine(DateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;

            //initial state
            _currentState = TrafficStates.NSgreen_WEred;
            _timerCounter = getTrafficLoad() == TrafficLoad.High
                ? _highTraffic_NorthSouth_TrafficDuration
                : _lowTraffic_NorthSouth_TrafficDuration;
        }

        public LightsStatus SecondClockPulse()
        {
            if (_timerCounter > 0) this._timerCounter--;

            switch (this._currentState)
            {
                case TrafficStates.NSgreen_WEred:
                    if (this._timerCounter == 0)
                    {
                        this._currentState = TrafficStates.NSyellow_WEred;
                        this._timerCounter = 5;
                    }
                    break;
                case TrafficStates.NSyellow_WEred:
                    if (this._timerCounter == 0)
                    {
                        this._currentState = TrafficStates.NSred_WEgreen;
                        this._timerCounter = (getTrafficLoad() == TrafficLoad.High)
                               ? _highTraffic_WestEast_TrafficDuration
                               : _lowTraffic_WestEast_TrafficDuration;
                    }
                    break;
                case TrafficStates.NSred_WEgreen:
                    if (this._timerCounter == 0)
                    {
                        this._currentState = TrafficStates.NSred_WEyellow;
                        this._timerCounter = 5;
                    }
                    break;
                case TrafficStates.NSred_WEyellow:
                    if (this._timerCounter == 0)
                    {
                        this._currentState = TrafficStates.NSgreen_WEred;
                        this._timerCounter = (getTrafficLoad() == TrafficLoad.High)
                               ? _highTraffic_NorthSouth_TrafficDuration
                               : _lowTraffic_NorthSouth_TrafficDuration;
                    }
                    break;
            }

            return getLightStatus();
        }

        private LightsStatus getLightStatus()
        {
            switch (this._currentState)
            {
                case TrafficStates.NSgreen_WEred:
                    return new LightsStatus { North = LightColor.green, South = LightColor.green, East = LightColor.red, West = LightColor.red };
                    break;
                case TrafficStates.NSyellow_WEred:
                    return new LightsStatus { North = LightColor.yellow, South = LightColor.yellow, East = LightColor.red, West = LightColor.red };
                    break;
                case TrafficStates.NSred_WEgreen:
                    return new LightsStatus { North = LightColor.red, South = LightColor.red, East = LightColor.green, West = LightColor.green };
                    break;
                case TrafficStates.NSred_WEyellow:
                    return new LightsStatus { North = LightColor.red, South = LightColor.red, East = LightColor.yellow, West = LightColor.yellow };
                    break;
                default:
                    return new LightsStatus { North = LightColor.green, South = LightColor.green, East = LightColor.red, West = LightColor.red };
                    break;
            }
        }

        private TrafficLoad getTrafficLoad()
        {
            if ((8 < _dateTimeProvider.Now.Hour  && _dateTimeProvider.Now.Hour < 10) || (17 < _dateTimeProvider.Now.Hour && _dateTimeProvider.Now.Hour < 19))
            {
                return TrafficLoad.High;
            }
            else
            {
                return TrafficLoad.Low;
            }
        }
    }
}
