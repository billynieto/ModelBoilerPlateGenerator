using System;
using System.Collections.ObjectModel;
using System.Linq;

using ModelBoilerPlateGenerator.Models;

namespace ModelBoilerPlateGenerator.ViewModels
{
    public class ModelViewModel : SubViewModel
    {
        protected Model model;
        
        public Model Model { get { return this.model; } set { this.model = value; OnPropertyChanged("Model"); } }

        public ModelViewModel(ModelBoilerPlateViewModel modelBoilerPlateViewModel, Model model)
            : base(modelBoilerPlateViewModel)
        {
            Model = model;
        }
    }
}
