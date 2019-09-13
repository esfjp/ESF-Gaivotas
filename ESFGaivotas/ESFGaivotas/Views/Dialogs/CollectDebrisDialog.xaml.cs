using ESFGaivotas.Enumerations;
using ESFGaivotas.Model.Element;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESFGaivotas.Views.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectDebrisDialog : BaseDialog
    {
        public CollectDebrisDialog()
        {
            InitializeComponent();

            foreach (var debrisType in (EDebrisType[])Enum.GetValues(typeof(EDebrisType)))
                DebrisTypeList.Add(new DebrisElement(debrisType, 1));
        }

        #region Bindable Properties

        public List<DebrisElement> DebrisTypeList { get; } = new List<DebrisElement>();

        #endregion

        #region Commanding

        public DelegateCommand<object> SelectDebrisType => _selectDebrisType ?? (_selectDebrisType = new DelegateCommand<object>(ExecuteSelectDebrisType));
        private DelegateCommand<object> _selectDebrisType;
        void ExecuteSelectDebrisType(object parameter)
        {
            var selectedDebrisType = (DebrisElement)parameter;

            foreach (var debrisType in DebrisTypeList)
                if (debrisType.IsSelected == true && debrisType.DebrisType != selectedDebrisType.DebrisType)
                    debrisType.IsSelected = false;

            selectedDebrisType.IsSelected = !selectedDebrisType.IsSelected;
        }

        #endregion
    }
}