﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ControlsLibrary.ViewModel.Base;
using ControlsLibrary.Model;
using System.Windows.Input;
using ControlsLibrary.Infrastructure.Command;
using QuickGraph;
using System.Linq;

namespace ControlsLibrary.Controls.Executor
{
    public class ExecutorViewModel : BaseViewModel
    {

        private FiniteAutomata FA;
        private BidirectionalGraph<NodeViewModel, EdgeViewModel> graph;
        public BidirectionalGraph<NodeViewModel, EdgeViewModel> Graph
        {
            get => graph;
            set
            {
                graph = value;
                //OnPropertyChanged();
            }
        }
        

        public ICommand SetAutomataCommand { get; }
        private void OnSetAutomataCommandExecuted(object p)
        {
            FA = FiniteAutomata.ConvertGraphToAutomata(Graph.Edges.ToList(), Graph.Vertices.ToList());
            ActualStates = FA.GetCurrentStates();
        }
        private bool CanSetAutomataCommandExecute(object p) => true;

        public ICommand SetStringCommand { get; }
        private void OnSetStringCommandExecuted(object p)
        {
            FA.SetStr(InputStr);
            ActualStates = FA.GetCurrentStates();
        }
        private bool CanSetStringCommandExecute(object p)
        {
            return (InputStr != null);
        }
        public ICommand StepInCommand { get; }
        private void OnStepInCommandExecuted(object p)
        {
            FA.SingleStep();
            ActualStates = FA.GetCurrentStates();
        }
        private bool CanStepInCommandExecute(object p)
        {
            if (FA == null)
            {
                return false;
            }
            return FA.CanDoStep();
        }
        public ICommand IsAcceptCommand { get; }
        private void OnIsAcceptCommandExecuted(object p)
        {
            Result = FA.DoAllTransitions(InputStr);
            ActualStates = FA.GetCurrentStates();
        }
        private bool CanIsAcceptCommandExecute(object p)
        {
            return (FA != null);
        }

        private string _InputStr;
        public string InputStr
        {
            get => _InputStr;
            set => Set(ref _InputStr, value);
        }

        private List<int> _ActualStates;
        public List<int> ActualStates
        {
            get => _ActualStates;
            set
            {
                Set(ref _ActualStates, value);
                string x = "";
                foreach (int state in _ActualStates)
                {
                    x += state.ToString();
                }
                States = x;
            }
        }
        private string _States;
        public string States
        {
            get => _States;
            set => Set(ref _States, value);
        }
        private bool _Result;
        public bool Result
        {
            get => _Result;
            set => Set(ref _Result, value);
        }

        public ExecutorViewModel()
        {
            #region Commands
            SetAutomataCommand = new RelayCommand(OnSetAutomataCommandExecuted, CanSetAutomataCommandExecute);
            SetStringCommand = new RelayCommand(OnSetStringCommandExecuted, CanSetStringCommandExecute);
            StepInCommand = new RelayCommand(OnStepInCommandExecuted, CanStepInCommandExecute);
            IsAcceptCommand = new RelayCommand(OnIsAcceptCommandExecuted, CanIsAcceptCommandExecute);
            #endregion
        }

    }
}
