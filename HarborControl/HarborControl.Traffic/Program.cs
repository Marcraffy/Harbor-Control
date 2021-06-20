using HarborControl.Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HarborControl.Traffic
{
    class Program
    {
        static async Task Main()
        {
            var client = new TrafficClient();
            var random = new Random();
            var vessels = new List<string>(); //Vessel Names
            var multiplier = await client.GetMultiplierAsync();

            while (true)
            {
                //Wait for a random period up to 30 seconds
                var waitTime = Convert.ToInt32((random.Next() % 30 / (double)multiplier) * 1000);
                Thread.Sleep(waitTime);

                //Randomly select between arrival and departure
                if(random.Next() % 3 != 0)
                {
                    var arrivingVessel = GetShipName(random);
                    if (vessels.Any(name => string.Equals(name, arrivingVessel, StringComparison.OrdinalIgnoreCase)))
                    {
                        continue;
                    }

                    var locations = new Location[] { Location.Harbor, Location.Perimeter };
                    var types = Enum.GetValues(typeof(VesselType)) as VesselType[];

                    var location = locations[random.Next() % locations.Length];
                    var type = types[random.Next() % types.Length];

                    Console.WriteLine($"Adding vessel with name: {arrivingVessel}, location: {location}, and type: {type}");
                    await client.SendVessalArrivalAsync(arrivingVessel, location, type);
                    vessels.Add(arrivingVessel);
                    multiplier = await client.GetMultiplierAsync();
                }
                else
                { 
                    if (vessels.Count == 0)
                    {
                        continue;
                    }

                    var departingVessel = vessels[random.Next() % vessels.Count];
                    Console.WriteLine($"Removing vessel with name: {departingVessel}");
                    await client.SendVessalDepartureAsync(departingVessel);
                    vessels.Remove(departingVessel);
                    multiplier = await client.GetMultiplierAsync();
                }
            }
        }

        private static string GetShipName(Random random)
        {
            var headings = new string[] { "SS", "HMS", "RMS", "MS", "CS" };
            var name = Faker.Name.First();
            return $"{headings[random.Next() % headings.Length]} {name}";
        }
    }
}
