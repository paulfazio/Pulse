using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI.Popups;

namespace PulseApp.Common
{
    public class StatusManager
    {
        public enum RouteType
        {
            Driving,

            Walking,

            PublicT
        };


        public async Task<Geopoint> GetMyLocation()
        {
            // Get current location.
            Geolocator myGeolocator = new Geolocator() { DesiredAccuracy = PositionAccuracy.High };
            var geopositionTask = myGeolocator.GetGeopositionAsync(
                maximumAge: TimeSpan.FromMinutes(5),
                timeout: TimeSpan.FromSeconds(10));

            Geoposition position = await geopositionTask;

            return position.Coordinate.Point;
        }

        public async Task<double> CalculateETA(RouteType routeType, Geopoint startPoint, Geopoint endPoint)
        {
            var route = await this.GetRoute(routeType, startPoint, endPoint);

            return route.Route.EstimatedDuration.TotalMinutes;
        }

        public async Task<MapRouteFinderResult> GetRoute(RouteType routeType, Geopoint startPoint, Geopoint endPoint)
        {
            switch (routeType)
            {
                case RouteType.Driving:
                    return await this.CalculateDrivingRoute(startPoint, endPoint);
                case RouteType.Walking:
                    return await this.CalculateWalkingRoute(startPoint, endPoint);
                default:
                    return null;
            }
        }

        public async Task<MapRouteFinderResult> CalculateDrivingRoute(Geopoint startPoint, Geopoint endPoint)
        {
            // Get the route between the points.
            var finderTask = MapRouteFinder.GetDrivingRouteAsync(
                startPoint,
                endPoint,
                MapRouteOptimization.Time,
                MapRouteRestrictions.None,
                290);

            MapRouteFinderResult routeResult = await finderTask;
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                return routeResult;
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog("Error calculating route information", "Error calculating route");
                await messageDialog.ShowAsync();
            }

            return null;
        }

        public async Task<MapRouteFinderResult> CalculateWalkingRoute(Geopoint startPoint, Geopoint endPoint)
        {
            var finderTask = MapRouteFinder.GetWalkingRouteAsync(
                startPoint,
                endPoint);

            MapRouteFinderResult routeResult = await finderTask;
            return routeResult;
        }


        public async Task<Geopoint> GetGeoCodeFromAddress(string address)
        {
            // Get the user's current location so it can be used as a starting point 
            // for the location search below.
            Geolocator geolocator = new Geolocator();
            Geoposition currentPosition = await geolocator.GetGeopositionAsync();

            // Geocodes the specified address, using the current location
            // as a reference point. Returns no more than 5 results.
            MapLocationFinderResult result =
                await MapLocationFinder.FindLocationsAsync(
                                    address,
                                    currentPosition.Coordinate.Point,
                                    5);

            // list to hold search results
            List<string> locations = new List<string>();

            // If the query returns results then bind it to the list view.
            if (result.Status == MapLocationFinderStatus.Success)
            {
                return new Geopoint(result.Locations[0].Point.Position);
                //foreach (MapLocation mapLocation in result.Locations)
                //{ 
                //    // create a display string of the map location
                //    string display = mapLocation.Address.StreetNumber + " " +
                //        mapLocation.Address.Street + Environment.NewLine +
                //        mapLocation.Address.Town + ", " +
                //        mapLocation.Address.RegionCode + "  " +
                //        mapLocation.Address.PostCode + Environment.NewLine +
                //        mapLocation.Address.CountryCode;

                //    // add the display string to the location list
                //    locations.Add(display);
                //}

                //// bind the location list to the ListView control
                //lvLocations.ItemsSource = locations;

            }
            else
            {
                return null;
                // Tell the user to try again.
                //string message = this.resourceLoader.GetString("NoAddress");
                //MessageDialog messageDialog = new MessageDialog("NoAddress");
                //await messageDialog.ShowAsync();
            }

            //if (locations.Count == 0)
            //{
            //    // Tell the user to try again.
            //    //string message = this.resourceLoader.GetString("NoAddress");
            //    //string title = this.resourceLoader.GetString("NoAddressTitle");
            //    MessageDialog messageDialog = new MessageDialog("NoAddress", "NoAddressTitle");
            //    await messageDialog.ShowAsync();
            //}
        }
    }
}
