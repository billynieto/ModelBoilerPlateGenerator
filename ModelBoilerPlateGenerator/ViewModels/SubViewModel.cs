using System;
using System.Collections.ObjectModel;
using System.Linq;

using ModelBoilerPlateGenerator.Models;

namespace ModelBoilerPlateGenerator.ViewModels
{
    public class SubViewModel : ViewModel
    {
        protected ModelBoilerPlateViewModel modelBoilerPlateViewModel;

        public SubViewModel(ModelBoilerPlateViewModel modelBoilerPlateViewModel)
        {
            this.modelBoilerPlateViewModel = modelBoilerPlateViewModel;
        }
    }
}
