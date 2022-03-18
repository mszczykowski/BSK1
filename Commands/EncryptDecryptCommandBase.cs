﻿using BSK1.Algorithms;
using BSK1.Services;
using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BSK1.Commands
{
    internal abstract class EncryptDecryptCommandBase : CommandBase
    {
        protected IAlgorithm _algorithm;

        protected ModuleBaseViewModel _viewModel;

        protected FileService fileService;

        protected string input;

        public EncryptDecryptCommandBase(IAlgorithm algorithm, ModuleBaseViewModel viewModel)
        {
            _algorithm = algorithm;
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;

            fileService = new FileService();
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            if (_viewModel.IsInputFile) return !String.IsNullOrEmpty(_viewModel.FilePath);

            else return !String.IsNullOrEmpty(_viewModel.InputText);
        }

        public override void Execute(object? parameter)
        {
            ClearOutput();
            if(_viewModel.IsInputFile) input = fileService.GetStringFromFile(_viewModel.FilePath);
            else input = _viewModel.InputText;
        }

        protected void SetOutput(string output)
        {
            if (_viewModel.IsInputFile)
            {
                fileService.SaveStringToOutputFile(output);
                _viewModel.OutputFileLinkVisible = true;
                MessageBox.Show("Zmodyfikowano plik wyjściowy");
            }
            else _viewModel.OutputText = output;
        }

        protected void ClearOutput()
        {
            _viewModel.OutputFileLinkVisible = false;
            _viewModel.OutputText = "";
        }
    }
}
