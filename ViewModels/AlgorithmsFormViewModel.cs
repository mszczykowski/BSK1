using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BSK1.Algorithms;
using BSK1.Commands;
using BSK1.Services;

namespace BSK1.ViewModels
{
    internal class AlgorithmsFormViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private string _inputText;
        private readonly ErrorsViewModel _errorsViewModel;

        public string InputText
        {
            get => _inputText; 
            set
            {
                _inputText = value;
                OnPropertyChanged(nameof(InputText));
            } 
        }

        private string _outputText;
        public string OutputText
        {
            get => _outputText;
            set
            {
                _outputText = value;
                OnPropertyChanged(nameof(OutputText));
            }
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        private bool _outputFileLinkVisible;
        public bool OutputFileLinkVisible
        {
            get => _outputFileLinkVisible;
            set
            {
                _outputFileLinkVisible = value;
                OnPropertyChanged(nameof(OutputFileLinkVisible));
            }
        }

        private bool _isInputFile;
        public bool IsInputFile
        {
            get => _isInputFile;
            set
            {
                _isInputFile = value;
                OnPropertyChanged(nameof(IsInputFile));
            }
        }

        private string _key;

        public string Key
        {
            get => _key;
            set
            {
                _key = value;
                
                ValidateKey();
             
                OnPropertyChanged(nameof(Key));
            }
        }

        private string _parametersLabel;
        public string ParametersLabel
        {
            get => _parametersLabel;
            set
            {
                _parametersLabel = value;
                OnPropertyChanged(nameof(ParametersLabel));
            }
        }

        private AlgorithmViewModel _algorithmViewModel;
        public AlgorithmViewModel AlgorithmViewModel
        {
            get => _algorithmViewModel;
            set
            {
                _algorithmViewModel = value;
                OnPropertyChanged(nameof(AlgorithmViewModel));
            }
        }

        private ICollection<AlgorithmViewModel> _algorithmsList;

        public ICollection<AlgorithmViewModel> AlgorithmsList => _algorithmsList;


        public ICommand EncryptCommand { get; }

        public ICommand DecryptCommand { get; }

        public ICommand ChooseFileCommand { get; }

        public ICommand OpenOutputFileCommand { get; }

        public ICommand CopyOutputToInputCommand { get; }

        public AlgorithmsFormViewModel()
        {

            ChooseFileCommand = new ChooseFileCommand(this);

            OpenOutputFileCommand = new OpenOutputFileCommand();

            CopyOutputToInputCommand = new CopyOutputToInputCommand(this);

            EncryptCommand = new EncryptCommand(this);

            DecryptCommand = new DecryptCommand(this);

            InitialiseAlgorithmsList();

            PropertyChanged += OnViewModelPropertyChanged;

            _errorsViewModel = new ErrorsViewModel();

            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
        }

        private void InitialiseAlgorithmsList()
        {
            _algorithmsList = new List<AlgorithmViewModel>
            {
                new AlgorithmViewModel("Rail fence", new RailFenceAlgorithm(this), "Podaj liczbę całkowitą większą od 0", "N"),
                new AlgorithmViewModel("Przestawienie macierzowe A", new TranspositionAAlgorithm(this), "Klucz musi byc w formacie 1-2-3-4", "Klucz"),
                new AlgorithmViewModel("Przestawienie macierzowe B", new TranspositionBAlgorithm(this), "Klucz musi składać się z samych liter", "Klucz"),
                new AlgorithmViewModel("Przestawienie macierzowe C", new TranspositionCAlgorithm(this), "Klucz musi składać się z samych liter", "Klucz"),
                new AlgorithmViewModel("Szyfr Cezara", new CaesarCipherAlgorithm(this), "Podaj liczbę całkowitą większą od 0", "K"),
                new AlgorithmViewModel("Szyfrowanie Vigenere'a", new VigenereCipherAlgorithm(this), "Klucz musi składać się z samych liter", "Klucz")
            };
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AlgorithmViewModel))
            {
                ParametersLabel = _algorithmViewModel.KeyName + ":";
                ClearKeyInputValidation();
            }
        }


        //validation

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors => _errorsViewModel.HasErrors;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsViewModel.GetErrors(propertyName);
        }

        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }

        public void ValidateKey()
        {
            _errorsViewModel.ClearErrors(nameof(Key));

            if (String.IsNullOrEmpty(_key)) _errorsViewModel.AddError(nameof(Key), "Klucz nie może być pusty");

            else if(!_algorithmViewModel.IsKeyValid(_key)) _errorsViewModel.AddError(nameof(Key), _algorithmViewModel.KeyErrorMessage);
        }

        private void ClearKeyInputValidation()
        {
            _errorsViewModel.ClearErrors(nameof(Key));
        }
    }
}
