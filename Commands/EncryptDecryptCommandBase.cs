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
    internal abstract class EncryptDecryptCommandBase : AsyncCommandBase
    {
        protected AlgorithmsFormViewModel _viewModel;

        protected FileService fileService;

        protected string input;

        protected string output;

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
            bool isInputEmpty = true;

            if (_viewModel.IsInputFile) isInputEmpty = String.IsNullOrEmpty(_viewModel.FilePath);

            else isInputEmpty = String.IsNullOrEmpty(_viewModel.InputText);

            return !isInputEmpty && !_viewModel.IsLoading && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _viewModel.ClearOutput();

            _viewModel.AlgorithmViewModel.KeyGenerator.ClearKey();

            _viewModel.ValidateKey();


            if (!_viewModel.AlgorithmViewModel.IsKeyValid(_viewModel.KeyInput)) return;

            _viewModel.IsLoading = true;

            switch (_viewModel.AlgorithmViewModel.AlgorithmType)
            {
                case AlgorithmType.Text:
                    SetInputForText();
                    await Translate();
                    SetOutputText();
                    break;
                case AlgorithmType.Binary:
                    SetInputForBinary();
                    await Translate();
                    SetOutputBinary();
                    break;
            }
            _viewModel.IsLoading = false;
        }

        public abstract Task Translate();

        protected void SetOutputText()
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

        protected void SetOutputBinary()
        {
            byte[] outputByte = ToByte(output);
            
            if (_viewModel.IsInputFile)
            {
                var filePath = fileService.SaveBytesToFile(_viewModel.FilePath, outputByte);
                _viewModel.OutputFilePath = filePath;
                _viewModel.OutputFileLinkVisible = true;
            }
            else _viewModel.OutputText = Encoding.Unicode.GetString(outputByte);
        }

        private void SetInputForText()
        {
            string inputUnnormalised;
            if (_viewModel.IsInputFile) inputUnnormalised = fileService.GetStringFromFile(_viewModel.FilePath);
            else inputUnnormalised = _viewModel.InputText;

            input = new string(inputUnnormalised.ToUpper().Where(c => Char.IsLetter(c)).ToArray());
        }

        private void SetInputForBinary()
        {
            byte[] byteInput;
            if (_viewModel.IsInputFile) byteInput = fileService.GetBinaryData(_viewModel.FilePath);
            else byteInput = Encoding.Unicode.GetBytes(_viewModel.InputText);

            input = ToBinary(byteInput);
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
