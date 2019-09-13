using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ESFGaivotas.Services
{
    public interface ITrackingService
    {
        Task StartTracking(Action<Position> actionPosition);
        Task StopTracking();
    }

    public class TrackingService : ITrackingService
    {
        private Action<Position> updatePosition;
        private Position lastPosition = new Position();

        public async Task StartTracking(Action<Position> actionPosition)
        {
            updatePosition = actionPosition;

            if (CrossGeolocator.Current.IsListening) return;

            CrossGeolocator.Current.PositionChanged += PositionChanged;

            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 2, true);
            Gyroscope.Start(SensorSpeed.UI);
        }

        public async Task StopTracking()
        {
            if (!CrossGeolocator.Current.IsListening) return;

            await CrossGeolocator.Current.StopListeningAsync();
            Gyroscope.Stop();

            CrossGeolocator.Current.PositionChanged -= PositionChanged;

            updatePosition = null;
        }

        private void PositionChanged(object sender, PositionEventArgs e)
        {
            Gyroscope.Stop();

            if (updatePosition != null && lastPosition.Latitude != e.Position.Latitude && lastPosition.Longitude != e.Position.Longitude)
                updatePosition(e.Position);

            Gyroscope.Start(SensorSpeed.UI);
        }
    }
}
