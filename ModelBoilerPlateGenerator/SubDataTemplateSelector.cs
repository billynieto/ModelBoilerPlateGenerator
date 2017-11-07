using System;
using System.Windows;
using System.Windows.Controls;

using ModelBoilerPlateGenerator.ViewModels;

namespace ModelBoilerPlateGenerator
{
    public class SubDataTemplateSelector : DataTemplateSelector
    {
        protected DataTemplate enumerationDataTemplate;
        protected DataTemplate modelDataTemplate;
        protected DataTemplate settingsDataTemplate;

        public DataTemplate EnumerationDataTemplate { get { return this.enumerationDataTemplate; }set { this.enumerationDataTemplate = value; } }
        public DataTemplate ModelDataTemplate { get { return this.modelDataTemplate; } set { this.modelDataTemplate = value; } }
        public DataTemplate SettingsDataTemplate { get { return this.settingsDataTemplate; } set { this.settingsDataTemplate = value; } }

        public SubDataTemplateSelector()
        {
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
                return null;

            if (item is EnumerationViewModel)
                return EnumerationDataTemplate;
            if (item is ModelViewModel)
                return ModelDataTemplate;
            if (item is SettingsViewModel)
                return SettingsDataTemplate;
            else
                throw new NotSupportedException("ViewModel Data Template Selection: " + item.ToString());
        }
    }
}
