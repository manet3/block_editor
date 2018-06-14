using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimpleBlockEditor
{
    class BlockViewModel:INotifyPropertyChanged
    {
        #region ICommands
        public ICommand MouseMoveCommand { get; set; }
        public ICommand MouseDownCommand { get; set; }
        public ICommand MouseUpCommand { get; set; }
        #endregion

        private bool _isDragged;
        private bool IsDragged
        {
            get => _isDragged;
            set
            {
                _isDragged = value;
                (MouseMoveCommand as Command)?.ReiseExecuteChanged();
            }
        }

        private double _x;
        public double X { get => _x; set { _x = value; OnPropertyChanged("X"); } }

        private double _y;
        public double Y { get => _y; set{ _y = value; OnPropertyChanged("Y"); } }

        public BlockViewModel()
        {      
            MouseDownCommand = new Command(OnMouseClick);
            MouseUpCommand = new Command(OnMouseUp);
            MouseMoveCommand = new Command(OnMouseMove, () => IsDragged);
        }

        private void OnMouseClick(object obj)
        {
            IsDragged = true;
        }

        private void OnMouseMove(object obj)
        {
            var arguments = (MouseArguments)obj;
            var sender = arguments.Sender as FrameworkElement;
            var args = arguments.Args as MouseEventArgs;
            X = args.GetPosition(sender).X;
            Y = args.GetPosition(sender).Y;
        }

        private void OnMouseUp(object obj)
        {
            IsDragged = false;
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
