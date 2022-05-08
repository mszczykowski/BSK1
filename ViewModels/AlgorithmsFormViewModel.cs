using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BSK1.Algorithms;
using BSK1.Algorithms.BinaryAlgorithms;
using BSK1.Algorithms.KeyGenerators;
using BSK1.Algorithms.TextAlgorithms;
using BSK1.Commands;
using BSK1.Commands.KeyGeneratorCommands;
using BSK1.Enums;
using BSK1.Services;
using Shamork.Util;

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

        private string _outputFilePath;
        public string OutputFilePath
        {
            get => _outputFilePath;
            set
            {
                _outputFilePath = value;
                OnPropertyChanged(nameof(OutputFilePath));
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

        private string _keyInput;

        public string KeyInput
        {
            get => _keyInput;
            set
            {
                _keyInput = value;
                
                ValidateKey();
                UpdatePolynominal();
                OnPropertyChanged(nameof(KeyInput));
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

        private string _binaryKey;
        public string BinaryKey
        {
            get => _binaryKey;
            set
            {
                _binaryKey = value;
                OnPropertyChanged(nameof(BinaryKey));
            }
        }

        private string _polynominal;
        public string Polynominal
        {
            get => _polynominal;
            set
            {
                _polynominal = value;
                ClearGeneratedKeyWhenPolynominalChanged();
                OnPropertyChanged(nameof(Polynominal));
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private bool _isKeyGeneratorVisible;
        public bool IsKeyGeneratorVisible
        {
            get => _isKeyGeneratorVisible;
            set
            {
                _isKeyGeneratorVisible = value;
                OnPropertyChanged(nameof(IsKeyGeneratorVisible));
            }
        }
        
        private string _seed;
        public string Seed
        {
            get => _seed;
            set
            {
                _seed = value;
                ValidateSeed();
                OnPropertyChanged(nameof(Seed));
            }
        }


        private ICollection<AlgorithmViewModel> _algorithmsList;

        public ICollection<AlgorithmViewModel> AlgorithmsList => _algorithmsList;


        public ICommand EncryptCommand { get; }

        public ICommand DecryptCommand { get; }

        public ICommand ChooseFileCommand { get; }

        public ICommand OpenOutputFileCommand { get; }

        public ICommand CopyOutputToInputCommand { get; }

        public ICommand GenerateKeyCommand { get; }

        public ICommand StopGeneratingKeyCommand { get; }


        public AlgorithmsFormViewModel()
        {
            GenerateKeyCommand = new GenerateKeyCommand(this);

            StopGeneratingKeyCommand = new StopGeneratingKeyCommand(this);



            ChooseFileCommand = new ChooseFileCommand(this);

            OpenOutputFileCommand = new OpenOutputFileCommand(this);

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
                new AlgorithmViewModel
                {
                    Name = "DES",
                    Algorithm = new DES(this),
                    KeyErrorMessage = "Klucz musi być w postaci binarnej i mieć 64 bity",
                    KeyName = "Klucz",
                    AlgorithmType = AlgorithmType.Binary
                },
                new AlgorithmViewModel
                {
                    Name = "Szyfr strumieniowy",
                    Algorithm = new StreamCipher(this),
                    KeyErrorMessage = "Potęgi muszą być w formacie: \"1, 2, 3, 4...\"",
                    KeyName = "Potęgi",
                    AlgorithmType = AlgorithmType.Binary,
                    KeyGenerator = new LFSR(UpdateGeneratedKey, this)

                },
                new AlgorithmViewModel
                {
                    Name = "Rail fence",
                    Algorithm = new RailFenceAlgorithm(this),
                    KeyErrorMessage = "Podaj liczbę całkowitą większą od 0",
                    KeyName = "N",
                    AlgorithmType = AlgorithmType.Text
                },
                new AlgorithmViewModel
                {
                    Name = "Przestawienie macierzowe A",
                    Algorithm = new TranspositionAAlgorithm(this),
                    KeyErrorMessage = "Klucz musi byc w formacie 1-2-3-4",
                    KeyName = "Klucz",
                    AlgorithmType = AlgorithmType.Text
                },
                new AlgorithmViewModel
                {
                    Name = "Przestawienie macierzowe B",
                    Algorithm = new TranspositionBAlgorithm(this),
                    KeyErrorMessage = "Klucz musi składać się z samych liter",
                    KeyName = "Klucz",
                    AlgorithmType = AlgorithmType.Text
                },
                new AlgorithmViewModel
                {
                    Name = "Przestawienie macierzowe C",
                    Algorithm = new TranspositionCAlgorithm(this),
                    KeyErrorMessage = "Klucz musi składać się z samych liter",
                    KeyName = "Klucz",
                    AlgorithmType = AlgorithmType.Text
                },
                new AlgorithmViewModel
                {
                    Name = "Szyfr Cezara",
                    Algorithm = new CaesarCipherAlgorithm(this),
                    KeyErrorMessage = "Podaj liczbę całkowitą większą od 0",
                    KeyName = "K",
                    AlgorithmType = AlgorithmType.Text
                },
                 new AlgorithmViewModel
                {
                    Name = "Szyfrowanie Vigenere'a",
                    Algorithm = new VigenereCipherAlgorithm(this),
                    KeyErrorMessage = "Klucz musi składać się z samych liter",
                    KeyName = "Klucz",
                    AlgorithmType = AlgorithmType.Text
                }
            };
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AlgorithmViewModel))
            {
                Initialise();
                ClearKeyInputValidation();
                ClearOutput();
            }
        }

        public void ClearOutput()
        {
            OutputFileLinkVisible = false;
            OutputText = "";
        }

        private void Initialise()
        {
            FilePath = "";
            KeyInput = "";
            Polynominal = "";
            ParametersLabel = _algorithmViewModel.KeyName + ":";


            if (_algorithmViewModel.KeyGenerator != null) IsKeyGeneratorVisible = true;
            else IsKeyGeneratorVisible = false;

            _algorithmsList.ToList().ForEach(algorithm =>
            {
                if (algorithm.KeyGenerator != null)
                {
                    algorithm.KeyGenerator.ClearKey();
                }
            });
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
            _errorsViewModel.ClearErrors(nameof(KeyInput));

            if (String.IsNullOrEmpty(_keyInput)) _errorsViewModel.AddError(nameof(KeyInput), "Klucz nie może być pusty");

            else if(!_algorithmViewModel.IsKeyValid(_keyInput)) _errorsViewModel.AddError(nameof(KeyInput), _algorithmViewModel.KeyErrorMessage);
        }

        public void ValidateSeed()
        {
            _errorsViewModel.ClearErrors(nameof(Seed));

            if (String.IsNullOrEmpty(_seed)) _errorsViewModel.AddError(nameof(Seed), "Ziarno nie może być puste przy deszyfrowaniu");
            else
            {
                var steramCipher = _algorithmViewModel.Algorithm as StreamCipher;
                if (!steramCipher.IsSeedValid(_seed)) _errorsViewModel.AddError(nameof(Seed), "Ziarno musi mieć postać binarną");
            }
        }

        public void UpdatePolynominal()
        {
            if (AlgorithmViewModel.KeyGenerator == null) return;
            if (!_algorithmViewModel.IsKeyValid(_keyInput)) return;

            string[] stringPowers = _keyInput.Replace(" ", string.Empty).Split(",");
            int[] powers = Array.ConvertAll(stringPowers, s => int.TryParse(s, out var x) ? x : -1).OrderBy(x => x).ToArray();

            string text = $"1 + x";
            for (int i = 0; i < powers.Length; i++)
            {
                text += StringSubSupExtension.ToSuperscripts(powers[i].ToString());

                if (i < powers.Length - 1)
                    text += " + x";
            }

            Polynominal = text;
        }

        private void ClearKeyInputValidation()
        {
            _errorsViewModel.ClearErrors(nameof(KeyInput));
            _errorsViewModel.ClearErrors(nameof(Seed));
        }

        public static void UiInvoke(Action a)
        {
            if (Application.Current != null) Application.Current.Dispatcher.Invoke(a);
        }

        private void UpdateGeneratedKey(object sender, EventArgs arguments)
        {
            UiInvoke(() =>
            {
                var generator = sender as IKeyGenerator;
                BinaryKey = generator.Key;
            });
            
        }

        private void ClearGeneratedKeyWhenPolynominalChanged()
        {
            if(_algorithmViewModel.KeyGenerator != null)
            {
                _algorithmViewModel.KeyGenerator.ClearKey();
                _errorsViewModel.ClearErrors(nameof(Seed));
            }
        }
    }
}
