using ESFGaivotas.Enumerations;
using ESFGaivotas.Model.Element;
using ESFGaivotas.Services;
using ESFGaivotas.ViewModel;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace ESFGaivotas.Controls
{
    public class ESFMap : Map
    {
        public ESFMap()
        {
            base.InitialCameraUpdate = CameraUpdateFactory.NewPositionZoom(
                new Position(-7.123732, -34.827489), 19D);
        }

        #region Bindable Properties

        public Position Position
        {
            get { return (Position)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }
        public static readonly BindableProperty PositionProperty = BindableProperty.Create(nameof(Position), typeof(Position), typeof(ESFMap), default(Position), propertyChanged: CenterInPosition);

        public double Heading
        {
            get { return (double)GetValue(HeadingProperty); }
            set { SetValue(HeadingProperty, value); }
        }
        public static readonly BindableProperty HeadingProperty = BindableProperty.Create(nameof(Heading), typeof(double), typeof(ESFMap), default(double));

        public IList<DebrisElement> MapDebrisCollection
        {
            get { return (IList<DebrisElement>)GetValue(MapDebrisCollectionProperty); }
            set { SetValue(MapDebrisCollectionProperty, value); }
        }
        public static readonly BindableProperty MapDebrisCollectionProperty = BindableProperty.Create(nameof(MapDebrisCollection), typeof(IList<DebrisElement>), typeof(ESFMap), new ObservableCollection<DebrisElement>());

        #endregion

        #region Property Changed Events

        private static void CenterInPosition(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ESFMap)bindable;
            
            control.MoveCamera(CameraUpdateFactory.NewCameraPosition(
                new CameraPosition(
                    new Position(control.Position.Latitude, control.Position.Longitude),
                    19D,                // Camera Zoom
                    control.Heading,    // Camera Heading
                    80D)));             // Camera Tilt
        }

        private void AddMapDebris(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var debrisElement in this.MapDebrisCollection)
            {
                if (!debrisElement.IsDrawn)
                {
                    Pin marker = new Pin()
                    {
                        Label = $"{debrisElement.DebrisType.ToString()} Debris",
                        Position = this.Position,
                        Icon = BitmapDescriptorFactory.FromBundle(debrisElement.DebrisType.ToString()),
                    };
                    this.Pins.Add(marker);
                    debrisElement.IsDrawn = true;
                }
            }
        }

        #endregion

        #region Property Changed Helpers

        protected override void OnPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(MapDebrisCollection))
            {
                if (MapDebrisCollection != null && MapDebrisCollection is INotifyCollectionChanged collection)
                {
                    collection.CollectionChanged += AddMapDebris;
                }
            }

            base.OnPropertyChanged(propertyName);
        }

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(MapDebrisCollection))
            {
                if (MapDebrisCollection != null && MapDebrisCollection is INotifyCollectionChanged collection)
                {
                    collection.CollectionChanged -= AddMapDebris;
                }

            }
            base.OnPropertyChanging(propertyName);
        }

        #endregion
    }
}