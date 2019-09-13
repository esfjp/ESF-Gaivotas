using ESFGaivotas.Enumerations;
using ESFGaivotas.Model.Repository;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ESFGaivotas.Model.Element
{
    public class ReportElement : BindableBase
    {

        #region Root Property

        public ReportModel Report
        {
            get { return _report; }
            set { SetProperty(ref _report, value); ReportChanged(); }
        }
        private ReportModel _report;

        #endregion

        #region Bindable Properties

        public ObservableCollection<DebrisElement> DebrisCollection
        {
            get { return _debrisCollection; }
            set { SetProperty(ref _debrisCollection, value); }
        }
        private ObservableCollection<DebrisElement> _debrisCollection = new ObservableCollection<DebrisElement>();

        public double Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }
        private double _weight;

        #endregion

        #region Property Changed Events

        private void ReportChanged()
        {
            var debrisTypes = (EDebrisType[])Enum.GetValues(typeof(EDebrisType));

            DebrisCollection.Clear();
            foreach (var debrisType in debrisTypes)
            {
                var quantity = Report.Debris.Where(l => l.DebrisType == debrisType).Count();
                DebrisCollection.Add(new DebrisElement(debrisType, quantity));
            }

            Weight = Report.Weight;
        }

        #endregion

    }
}
