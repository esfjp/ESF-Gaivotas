using ESFGaivotas.Enumerations;
using ESFGaivotas.Model.Repository;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESFGaivotas.Model.Element
{
    public class DebrisElement : BindableBase
    {

        public DebrisElement(EDebrisType debrisType, int quantity)
        {
            DebrisType = debrisType;
            Quantity = quantity;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        #region Bindable Properties

        public EDebrisType DebrisType
        {
            get { return _debrisType; }
            set { SetProperty(ref _debrisType, value); }
        }
        private EDebrisType _debrisType;

        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }
        private int _quantity;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
        private bool _isSelected;

        public bool IsDrawn
        {
            get { return _isDrawn; }
            set { SetProperty(ref _isDrawn, value); }
        }
        private bool _isDrawn = false;

        #endregion

    }
}