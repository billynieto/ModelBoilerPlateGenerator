using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using ModelBoilerPlateGenerator.Models;

namespace ModelBoilerPlateGenerator.ViewModels
{
    public class SettingsViewModel : SubViewModel
    {
        protected Settings settings;

        public Settings Settings { get { return this.settings; } set { this.settings = value; OnPropertyChanged("Settings"); } }

        public SettingsViewModel(ModelBoilerPlateViewModel modelBoilerPlateViewModel, Settings settings)
            : base(modelBoilerPlateViewModel)
        {
            Settings = settings;
        }
    }
}
