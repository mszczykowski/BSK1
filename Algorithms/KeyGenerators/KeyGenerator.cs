using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BSK1.Algorithms.KeyGenerators
{
    internal abstract class KeyGenerator: IKeyGenerator
    {
        public string Key => _key;
        public bool IsRunning => _isRunning;

        protected string _key;

        private static Thread thread;
        private static bool _isRunning;

        public event EventHandler KeyUpdated;

        protected AlgorithmsFormViewModel _viewModel;


        public KeyGenerator(EventHandler KeyUpdatedEventHandler, AlgorithmsFormViewModel viewModel)
        {
            _isRunning = false;

            KeyUpdated += KeyUpdatedEventHandler;
            _viewModel = viewModel;
        }

        public void StartGeneratingKey()
        {
            ClearKey();

            if(thread == null || !thread.IsAlive)
            {
                thread = new Thread(new ThreadStart(Run));

                thread.IsBackground = true;

                _isRunning = true;
                thread.Start();
            }
        }

        public void StopGeneratingKey()
        {
            _isRunning = false;
            OnKeyUpdate();
        }

        public virtual void ClearKey()
        {
            _isRunning = false;
            _key = "";
            OnKeyUpdate();
        }

        private void Run()
        {
            while(_isRunning)
            {
                GenerateKeyElement();
                
                Thread.Sleep(100);

                OnKeyUpdate();
            }
            thread.Interrupt();
        }

        protected void OnKeyUpdate()
        {
            KeyUpdated?.Invoke(this, EventArgs.Empty);
        }

        public abstract void GenerateKeyElement();

        public abstract string GenerateKey(int keyLenght);
    }
}
