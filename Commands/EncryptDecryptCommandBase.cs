using BSK1.Algorithms;
using BSK1.Algorithms.BinaryAlgorithms;
using BSK1.Algorithms.TextAlgorithms;
using BSK1.Enums;
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
        protected AlgorithmsFormViewModel _viewModel;

        protected FileService fileService;


        public EncryptDecryptCommandBase(AlgorithmsFormViewModel viewModel)
        {
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

            _viewModel.ValidateKey();


            if (!_viewModel.AlgorithmViewModel.IsKeyValid(_viewModel.KeyInput)) return;


            switch (_viewModel.AlgorithmViewModel.AlgorithmType)
            {
                case AlgorithmType.Text:
                    SetUpForText();
                    break;
                case AlgorithmType.Binary:
                    SetUpForBinary();
                    break;
            }
        }

        public abstract string Translate(string input);

        protected void SetOutput(string output)
        {
            if (_viewModel.IsInputFile)
            {
                fileService.SaveStringToOutputFile(output);
                _viewModel.OutputFilePath = FileService.outputFilePath;
                _viewModel.OutputFileLinkVisible = true;
                MessageBox.Show("Zmodyfikowano plik wyjściowy");
            }
            else _viewModel.OutputText = output;
        }

        protected void SetOutput(byte[] output)
        {
            if (_viewModel.IsInputFile)
            {
                var filePath = fileService.SaveBytesToFile(_viewModel.FilePath, output);
                _viewModel.OutputFilePath = filePath;
                _viewModel.OutputFileLinkVisible = true;
            }
            else _viewModel.OutputText = Encoding.ASCII.GetString(output);
        }

        protected void ClearOutput()
        {
            _viewModel.OutputFileLinkVisible = false;
            _viewModel.OutputText = "";
        }

        private void SetUpForText()
        {
            string inputUnnormalised;
            if (_viewModel.IsInputFile) inputUnnormalised = fileService.GetStringFromFile(_viewModel.FilePath);
            else inputUnnormalised = _viewModel.InputText;

            var input = new string(inputUnnormalised.ToUpper().Where(c => Char.IsLetter(c)).ToArray());

            SetOutput(Translate(input));
        }

        private void SetUpForBinary()
        {
            byte[] input;
            if (_viewModel.IsInputFile) input = fileService.GetBinaryData(_viewModel.FilePath);
            else input = Encoding.ASCII.GetBytes(_viewModel.InputText);

            string output = Translate(ToBinary(input));

            SetOutput(ToByte(output));
        }

        public String ToBinary(Byte[] data)
        {
            return string.Join("", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }

        public byte[] ToByte(string data)
        {
            int numOfBytes = Convert.ToInt32((Math.Ceiling(Convert.ToDouble(data.Length) / 8)));
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(data.Substring(8 * i, 8), 2);
            }
            return bytes;
        }

    }
}
