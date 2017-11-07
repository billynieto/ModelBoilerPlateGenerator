using System;
using System.Linq;
using System.IO;
using System.Windows.Input;
using System.Xml.Serialization;

using ModelBoilerPlateGenerator.Models;
using System.Collections.Generic;

namespace ModelBoilerPlateGenerator.ViewModels
{
    public class ModelBoilerPlateViewModel : ViewModel
    {
        protected const string defaultSolutionName = "Solution";
        protected const string seperator = ".";
        protected const string defaultSpecificationFileName = "models.xml";

        protected Dictionary<string, TreeViewModel> enumerationTreeViewModels;
        protected ICommand newEnumerationCommand;
        protected ICommand newModelCommand;
        protected Dictionary<string, TreeViewModel> modelTreeViewModels;
        protected ICommand openCommand;
        protected TreeViewModel rootEnumerationsTreeViewModel;
        protected TreeViewModel rootModelsTreeViewModel;
        protected TreeViewModel rootTreeViewModel;
        protected ICommand saveCommand;
        protected object selectedItem;
        protected string solutionFolderPath;
        protected string solutionName;
        protected Specification specification;
        protected SubViewModel subViewModel;
        protected FileInfo specificationFile;
        protected TreeViewModel specificationTreeViewModel;
        protected TreeViewModel settingsTreeViewModel;

        public bool IsValidSolution { get { return !string.IsNullOrWhiteSpace(SolutionName); } }
        public ICommand NewEnumerationCommand { get { return this.newEnumerationCommand; } set { this.newEnumerationCommand = value; } }
        public ICommand NewModelCommand { get { return this.newModelCommand; } set { this.newModelCommand = value; } }
        public ICommand OpenCommand { get { return this.openCommand; } set { this.openCommand = value; } }
        public TreeViewModel RootTreeViewModel { get { return this.rootTreeViewModel; } set { this.rootTreeViewModel = value; OnPropertyChanged("RootTreeViewModel"); } }
        public ICommand SaveCommand { get { return this.saveCommand; } set { this.saveCommand = value; } }
        public string SolutionFolderPath { get { return this.solutionFolderPath; } set { this.solutionFolderPath = value; OnPropertyChanged("SolutionFolderPath"); } }
        public string SolutionName { get { return this.solutionName; } set { this.solutionName = value; OnPropertyChanged("SolutionName"); } }
        public SubViewModel SubViewModel { get { return this.subViewModel; } set { this.subViewModel = value; OnPropertyChanged("SubViewModel"); } }

        public object SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;

                    if (this.selectedItem == null)
                        SubViewModel = null;
                    else if (this.selectedItem is Models.Enumeration)
                        SubViewModel = new EnumerationViewModel(this, (Models.Enumeration)this.selectedItem);
                    else if (this.selectedItem is Models.Model)
                        SubViewModel = new ModelViewModel(this, (Models.Model)this.selectedItem);
                    else if (this.selectedItem is Models.Settings)
                        SubViewModel = new SettingsViewModel(this, (Models.Settings)this.selectedItem);
                    else
                        SubViewModel = null;
                }
            }
        }
        public Specification Specification
        {
            get { return this.specification; }
            set
            {
                this.specification = value;

                OnPropertyChanged("Specification");

                InitializeTreeViewModel();
            }
        }

        public ModelBoilerPlateViewModel(Specification specification)
        {
            Specification = specification;

            this.newEnumerationCommand = new RelayCommand(
                param =>
                {
                    Enumeration enumeration = new Enumeration() { Name = "New Enumeration" };
                    enumeration.Items.Add(new EnumerationItem("Default1", 0));
                    enumeration.Items.Add(new EnumerationItem("Default2", 1));

                    Specification.Enumerations.Add(enumeration);

                    OnPropertyChanged("Specification");
                },
                param => { return IsValidSolution; });

            this.newModelCommand = new RelayCommand(
                param =>
                {
                    Models.Key key = new Models.Key();
                    Model model = new Model(key) { Name = "New Model" };

                    Specification.Models.Add(model);
                    
                    OnPropertyChanged("Specification");
                },
                param => { return IsValidSolution; });

            this.openCommand = new RelayCommand(
                param => { Load(); },
                param => { return IsValidSolution; });

            this.saveCommand = new RelayCommand(
                param => { Save(); },
                param => { return IsValidSolution; });
        }

        public void InitializeTreeViewModel()
        {
            TreeViewModel treeViewModel = new TreeViewModel(null, null);
            this.specificationTreeViewModel = treeViewModel.AddChild(this.specification, "Specification");
            this.rootEnumerationsTreeViewModel = this.specificationTreeViewModel.AddChild(this.specification == null ? null : this.specification.Enumerations, "Enumerations");
            this.rootModelsTreeViewModel = this.specificationTreeViewModel.AddChild(this.specification == null ? null : this.specification.Models, "Models");
            this.settingsTreeViewModel = this.specificationTreeViewModel.AddChild(this.specification == null ? null : this.specification.Settings, "Settings");

            RefreshTree();

            RootTreeViewModel = treeViewModel;
        }

        public void Load()
        {
            if (IsValidSolution)
            {
                this.specificationFile = new FileInfo(Path.Combine(SolutionFolderPath, defaultSpecificationFileName));

                if (this.specificationFile.Exists)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Specification));

                    using (FileStream fileStream = new FileStream(this.specificationFile.FullName, FileMode.Open))
                    {
                        Specification = (Specification)serializer.Deserialize(fileStream);
                    }
                }
            }
            else
            {
                Specification = new Specification();
            }

            SetupDefaults();

            SelectedItem = null;
        }

        public void RefreshTree()
        {
            foreach (TreeViewModel enumerationTreeViewModel in this.rootEnumerationsTreeViewModel.Children.Where(_treeItem => !this.specification.Enumerations.Any(_enumeration => _enumeration.Name == _treeItem.Name)).ToList())
                this.rootEnumerationsTreeViewModel.Children.Remove(enumerationTreeViewModel);
            foreach (Enumeration enumeration in this.specification.Enumerations)
            {
                TreeViewModel enumerationTreeViewModel = this.rootEnumerationsTreeViewModel.AddChild(enumeration, enumeration.Name);

                foreach(EnumerationItem item in enumeration.Items)
                    enumerationTreeViewModel.AddChild(item, item.Name);

                this.enumerationTreeViewModels[enumerationTreeViewModel.Name] = enumerationTreeViewModel;
            }

            foreach (TreeViewModel modelTreeViewModel in this.rootModelsTreeViewModel.Children.Where(_treeItem => !this.specification.Models.Any(_model => _model.Name == _treeItem.Name)).ToList())
                this.rootModelsTreeViewModel.Children.Remove(modelTreeViewModel);
            foreach (Model model in this.specification.Models)
            {
                TreeViewModel modelTreeViewModel = this.rootModelsTreeViewModel.AddChild(model, model.Name);

                //foreach (EnumerationItem item in enumeration.Items)
                //    enumerationTreeViewModel.AddChild(item, item.Name);

                this.enumerationTreeViewModels[modelTreeViewModel.Name] = modelTreeViewModel;
            }

            OnPropertyChanged("RootTreeViewModel");
        }

        public void Save()
        {
            if (IsValidSolution)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Specification));

                using (TextWriter writer = new StreamWriter(this.specificationFile.FullName))
                {
                    serializer.Serialize(writer, Specification);

                    writer.Close();
                }
            }
        }

        public void SetupDefaults()
        {
            if (Specification != null && Specification.Settings != null)
            {
                if (string.IsNullOrWhiteSpace(SolutionFolderPath) || string.IsNullOrWhiteSpace(SolutionName))
                {
                    Specification.Settings.FrameworkProjectName = new System.Text.StringBuilder(defaultSolutionName).Append(seperator).Append("Framework").ToString();
                    Specification.Settings.ModelsProjectName = new System.Text.StringBuilder(defaultSolutionName).ToString();
                    Specification.Settings.RepositoriesProjectName = new System.Text.StringBuilder(defaultSolutionName).Append(seperator).Append("Server").ToString();
                    Specification.Settings.TestProjectName = new System.Text.StringBuilder(defaultSolutionName).Append(seperator).Append("Test").ToString();

                    Specification.Settings.IntegrationTestsProjectName = new System.Text.StringBuilder(Specification.Settings.TestProjectName).Append(seperator).Append("Integration").ToString();
                    Specification.Settings.UnitTestsProjectName = new System.Text.StringBuilder(Specification.Settings.TestProjectName).Append(seperator).Append("Unit").ToString();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Specification.Settings.FrameworkProjectName))
                        Specification.Settings.FrameworkProjectName = new System.Text.StringBuilder(SolutionName).Append(seperator).Append("Framework").ToString();

                    if (string.IsNullOrWhiteSpace(Specification.Settings.ModelsProjectName))
                        Specification.Settings.ModelsProjectName = new System.Text.StringBuilder(SolutionName).ToString();

                    if (string.IsNullOrWhiteSpace(Specification.Settings.RepositoriesProjectName))
                        Specification.Settings.RepositoriesProjectName = new System.Text.StringBuilder(SolutionName).Append(seperator).Append("Server").ToString();

                    if (string.IsNullOrWhiteSpace(Specification.Settings.TestProjectName))
                        Specification.Settings.TestProjectName = new System.Text.StringBuilder(SolutionName).Append(seperator).Append("Test").ToString();



                    if (string.IsNullOrWhiteSpace(Specification.Settings.IntegrationTestsProjectName))
                        Specification.Settings.IntegrationTestsProjectName = new System.Text.StringBuilder(Specification.Settings.TestProjectName).Append(seperator).Append("Integration").ToString();

                    if (string.IsNullOrWhiteSpace(Specification.Settings.UnitTestsProjectName))
                        Specification.Settings.UnitTestsProjectName = new System.Text.StringBuilder(Specification.Settings.TestProjectName).Append(seperator).Append("Unit").ToString();
                }
            }
        }
    }
}
