using System;
using System.Collections.ObjectModel;
using System.Linq;

using ModelBoilerPlateGenerator.Models;

namespace ModelBoilerPlateGenerator.ViewModels
{
    public class EnumerationViewModel : SubViewModel
    {
        protected Enumeration enumeration;
        
        public string Name {
            get { return this.enumeration.Name; }
            set {
                this.enumeration.Name = value;

                OnPropertyChanged("Name");

                this.modelBoilerPlateViewModel.RefreshTree();
            }
        }

        public EnumerationViewModel(ModelBoilerPlateViewModel modelBoilerPlateViewModel, Enumeration enumeration)
            : base(modelBoilerPlateViewModel)
        {
            this.enumeration = enumeration;
        }
    }
}
