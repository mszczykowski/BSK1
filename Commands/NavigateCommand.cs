﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSK1.Services;

namespace BSK1.Commands
{
    internal class NavigateCommand : CommandBase
    {
        private readonly NavigationService _navigationService;

        public NavigateCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
