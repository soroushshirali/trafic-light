using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace traffic_light.Models
{
    public class LightsStatus
    {
        public LightColor North { get; set; }

        public LightColor South { get; set; }

        public LightColor East { get; set; }

        public LightColor West { get; set; }
    }

    public class LightsStatusDto
    {
        public LightsStatusDto(LightsStatus lightsStatus)
        {
            this.North = lightsStatus.North.ToString();
            this.South = lightsStatus.South.ToString();
            this.West = lightsStatus.West.ToString();
            this.East = lightsStatus.East.ToString();
        }

        public string North { get; set; }

        public string South { get; set; }

        public string East { get; set; }

        public string West { get; set; }
    }
}