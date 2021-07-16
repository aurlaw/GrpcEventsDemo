using System;
namespace GrpcEventsHost.Models
{
	public class WeatherResult
	{
        public DateTime Updated { get; set; }
        public int Temp { get; set; }
        public string Summary { get; set; }

        public int TempF => (Temp * 9 / 5) + 32;

	}
}
