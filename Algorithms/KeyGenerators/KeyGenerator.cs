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
        public string Key => key;

        protected string key;

        private static Thread thread;
        private static bool isRunning;

        public event EventHandler KeyUpdated;

        protected AlgorithmsFormViewModel _viewModel;


        public KeyGenerator(EventHandler KeyUpdatedEventHandler, AlgorithmsFormViewModel viewModel)
        {
            isRunning = false;

            KeyUpdated += KeyUpdatedEventHandler;
            _viewModel = viewModel;
        }

        public void StartGeneratingKey()
        {
            if(thread == null || !thread.IsAlive)
            {
                thread = new Thread(new ThreadStart(Run));

                thread.IsBackground = true;

                isRunning = true;
                thread.Start();
            }
        }

        public void StopGeneratingKey()
        {
            isRunning = false;
            OnKeyUpdate();
        }

        public void ClearKey()
        {
            key = "";
            OnKeyUpdate();
        }

        private void Run()
        {
            while(isRunning)
            {
                GenerateKeyElement();
                
                Thread.Sleep(100);

                OnKeyUpdate();
            }
            thread.Interrupt();
        }

        private void OnKeyUpdate()
        {
            KeyUpdated?.Invoke(this, EventArgs.Empty);
        }

        public abstract void GenerateKeyElement();

        public abstract string GenerateKey(int keyLenght);
    }
}
