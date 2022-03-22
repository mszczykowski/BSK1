using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BSK1.Algorithms;
using BSK1.Commands;
using BSK1.Enums;
using BSK1.Services;

namespace BSK1.ViewModels
{
    internal class AlgorithmsFormViewModel : ViewModelBase
    {
        private string _inputText;
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

        private int _n;

        public int N
        {
            get => _n;
            set
            {
                _n = value;
                OnPropertyChanged(nameof(N));
            }
        }

        private string _key;
        public string Key
        {
            get => _key;
            set
            {
                _key = value;
                OnPropertyChanged(nameof(Key));
            }
        }

        private int _k;
        public int K
        {
            get => _k;
            set
            {
                _k = value;
                OnPropertyChanged(nameof(K));
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

        private bool _isNVisible;
        public bool IsNVisible
        {
            get => _isNVisible;
            set
            {
                _isNVisible = value;
                OnPropertyChanged(nameof(IsNVisible));
            }
        }
        private bool _isKeyVisible;
        public bool IsKeyVisible
        {
            get => _isKeyVisible;
            set
            {
                _isKeyVisible = value;
                OnPropertyChanged(nameof(IsKeyVisible));
            }
        }
        private bool _isKVisible;
        public bool IsKVisible
        {
            get => _isKVisible;
            set
            {
                _isKVisible = value;
                OnPropertyChanged(nameof(IsKVisible));
            }
        }

        private AlgorithmViewModel _algorithm;
        public AlgorithmViewModel AlgorithmViewModel
        {
            get => _algorithm;
            set
            {
                _algorithm = value;
                OnPropertyChanged(nameof(AlgorithmViewModel));
            }
        }

        private ICollection<AlgorithmViewModel> _algorithmsList;
        public ICollection<AlgorithmViewModel> AlgorithmsList => _algorithmsList;


        public ICommand EncryptCommand { get; }

        public ICommand DecryptCommand { get; }

        public ICommand ExitCommand { get; }

        public ICommand ChooseFileCommand { get; }

        public ICommand OpenOutputFileCommand { get; }

        public ICommand CopyOutputToInputCommand { get; }

        public AlgorithmsFormViewModel()
        {
            ExitCommand = new ExitCommand();

            ChooseFileCommand = new ChooseFileCommand(this);

            OpenOutputFileCommand = new OpenOutputFileCommand();

            CopyOutputToInputCommand = new CopyOutputToInputCommand(this);

            EncryptCommand = new EncryptCommand(this);

            DecryptCommand = new DecryptCommand(this);

            InitialiseAlgorithmsList();

            PropertyChanged += OnViewModelPropertyChanged;
        }

        private void InitialiseAlgorithmsList()
        {
            _algorithmsList = new List<AlgorithmViewModel>
            {
                new AlgorithmViewModel("Rail fence", new RailFenceAlgorithm(this), RequiredForm.N),
                new AlgorithmViewModel("Przestawienie macierzowe A", new TranspositionAAlgorithm(this), RequiredForm.Key),
                new AlgorithmViewModel("Przestawienie macierzowe B", new TranspositionBAlgorithm(this), RequiredForm.Key),
                new AlgorithmViewModel("Przestawienie macierzowe C", new TranspositionCAlgorithm(this), RequiredForm.Key),
                new AlgorithmViewModel("Szyfr Cezara", new CaesarCipherAlgorithm(this), RequiredForm.K),
                new AlgorithmViewModel("Szyfrowanie Vigenere'a", new VigenereCipherAlgorithm(this), RequiredForm.Key)
            };
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(AlgorithmViewModel))
            {
                IsNVisible = IsKeyVisible = IsKVisible = false;

                switch(_algorithm.RequiredForm)
                {
                    case RequiredForm.K:
                        ParametersLabel = "K:";
                        IsKVisible = true;
                        break;
                    case RequiredForm.N:
                        ParametersLabel = "N:";
                        IsNVisible = true;
                        break;
                    case RequiredForm.Key:
                        ParametersLabel = "Klucz:";
                        IsKeyVisible = true;
                        break;
                }
            }
        }
    }
}
