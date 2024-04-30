using CommanClsLibrary.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary.Repository.Classes
{
    public class LocationService : ILocationService
    {
        public double CalculateDistance(double sLatitude, double sLongitude, double eLatitude, double eLongitude)
        {
            var sCoord = new GeoCoordinate(sLatitude, sLongitude);
            var eCoord = new GeoCoordinate(eLatitude, eLongitude);

            return sCoord.GetDistanceTo(eCoord);
        }

    }
}
