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
    public class BlockViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<bool> SelectedChabged;

        #region ICommands
        public ICommand MouseMoveCommand { get; set; }
        public ICommand MouseDownCommand { get; set; }
        public ICommand MouseUpCommand { get; set; }
        #endregion

        #region property_fields
        private bool _isSelected;
        private bool _isDragged;
        private Point _position;
        #endregion

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
                SelectedChabged?.Invoke(this, _isSelected);
            }
        }

        
        private bool IsDragged
        {
            get => _isDragged;
            set
            {
                _isDragged = value;
                (MouseMoveCommand as Command)?.ReiseExecuteChanged();
            }
        }

        public double X { get => _position.X; set { _position.X = value; OnPropertyChanged("X"); } }

        public double Y { get => _position.Y; set{ _position.Y = value; OnPropertyChanged("Y"); } }

        #region logical fields
        private Point _lastPosition;
        #endregion


        public BlockViewModel()
        {      
            MouseDownCommand = new Command(OnMouseClick);
            MouseUpCommand = new Command(OnMouseUp);
            MouseMoveCommand = new Command(OnMouseMove, () => IsDragged);
        }

        private void OnMouseClick(object obj)
        {
            IsDragged = true;
            _lastPosition = GetMousePosition((MouseArguments)obj);
            IsSelected = !IsSelected;
        }

        private void OnMouseMove(object obj)
        {
            var position = GetMousePosition((MouseArguments)obj);
            var dX = position.X - _lastPosition.X;
            var dY = position.Y - _lastPosition.Y;
            X += dX;
            Y += dY;
        }

        private void OnMouseUp(object obj)
        {
            IsDragged = false;
        }

        private Point GetMousePosition(MouseArguments arguments)
        {
            var sender = arguments.Sender as FrameworkElement;
            var args = arguments.Args as MouseEventArgs 
                ?? arguments.Args as MouseButtonEventArgs;
            return new Point(args.GetPosition(sender).X, 
                args.GetPosition(sender).Y);
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        
    }
}
