using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelBoilerPlateGenerator.ViewModels
{
    public class TreeViewModel : ViewModel
    {
        protected ObservableCollection<TreeViewModel> children;
        protected string name;
        protected object representing;

        public ObservableCollection<TreeViewModel> Children { get { return this.children; } set { this.children = value;  OnPropertyChanged("Children"); } }
        public string Name { get { return this.name; } set { this.name = value; OnPropertyChanged("Name"); } }
        public object Representing { get { return this.representing; } set { this.representing = value;  OnPropertyChanged("Representing"); } }

        public TreeViewModel(object representing)
            : this(representing, null)
        {
        }

        public TreeViewModel(string name)
            : this(null, name)
        {
        }

        public TreeViewModel(object representing, string name)
        {
            this.name = name;
            this.representing = representing;

            Children = new ObservableCollection<TreeViewModel>();
        }

        public TreeViewModel AddChild(object child, string name)
        {
            TreeViewModel childTreeViewModel = new TreeViewModel(child, name);

            this.children.Add(childTreeViewModel);

            return childTreeViewModel;
        }
    }
}
