//------------------------------------------------------------------------------
// <copyright file="ModelBoilerPlateVisualStudioWindowControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using Microsoft.VisualStudio.LanguageServices;

using ModelBoilerPlateGenerator.Models;
using ModelBoilerPlateGenerator.ViewModels;

namespace ModelBoilerPlateGenerator
{
    /// <summary>
    /// Interaction logic for ModelBoilerPlateVisualStudioWindowControl.
    /// </summary>
    public partial class ModelBoilerPlateVisualStudioWindowControl : UserControl
    {
        protected ModelBoilerPlateViewModel modelBoilerPlateViewModel;
        protected VisualStudioWorkspace workspace;

        public ModelBoilerPlateViewModel ModelBoilerPlateViewModel { get { return this.modelBoilerPlateViewModel; } set { this.modelBoilerPlateViewModel = value; DataContext = this.modelBoilerPlateViewModel; } }

        public ModelBoilerPlateVisualStudioWindowControl(VisualStudioWorkspace workspace, ModelBoilerPlateViewModel modelBoilerPlateViewModel)
        {
            this.workspace = workspace;
            this.workspace.WorkspaceChanged += Workspace_WorkspaceChanged;

            ModelBoilerPlateViewModel = modelBoilerPlateViewModel;

            Workspace_WorkspaceChanged(null, null);

            this.InitializeComponent();
        }

        private void Workspace_WorkspaceChanged(object sender, Microsoft.CodeAnalysis.WorkspaceChangeEventArgs e)
        {
            string solutionFolderPath = null;
            string solutionName = null;

            if (!string.IsNullOrWhiteSpace(this.workspace.CurrentSolution.FilePath))
            {
                int index = this.workspace.CurrentSolution.FilePath.LastIndexOf("\\");
                int extensionIndex = this.workspace.CurrentSolution.FilePath.LastIndexOf(".");

                solutionFolderPath = this.workspace.CurrentSolution.FilePath.Substring(0, index);
                solutionName = this.workspace.CurrentSolution.FilePath.Substring(index + 1, extensionIndex - index - 1);
            }

            this.modelBoilerPlateViewModel.SolutionFolderPath = solutionFolderPath;
            this.modelBoilerPlateViewModel.SolutionName = solutionName;
            this.modelBoilerPlateViewModel.Load();
        }

        private void generateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.modelBoilerPlateViewModel.IsValidSolution)
            {
            }
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e == null && e.NewValue == null || !(e.NewValue is TreeViewModel))
            {
                ModelBoilerPlateViewModel.SelectedItem = null;

                return;
            }

            TreeViewModel treeViewModel = (TreeViewModel)e.NewValue;

            ModelBoilerPlateViewModel.SelectedItem = treeViewModel.Representing;
        }
    }
}