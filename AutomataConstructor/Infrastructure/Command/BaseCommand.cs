﻿using System;
using System.Windows.Input;

namespace AutomataConstructor.Infrastructure.Command
{
    /// <summary>
    /// The base class of command
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }
}