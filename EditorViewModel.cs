using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleBlockEditor
{

    class EditorViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Commands
        public ICommand NewBlockCommand { get; }
        public ICommand DrawConnectorCommand { get; }
        public ICommand RemoveCommand { get; set; }
        #endregion

        public Visibility RemoveButtonVis
        {
            get => _selectedBlocks.Count == 0
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        public Visibility ConnectButtonVis
        {
            get => _selectedBlocks.Count == 2
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private ObservableCollection<UserControl> _canvasElements;
        public ObservableCollection<UserControl> CanvasElements
        {
            get => _canvasElements;
            set{
                _canvasElements = value;
                OnPropertyChanged(nameof(CanvasElements));
            }
        }

        private List<BlockViewModel> _selectedBlocks = new List<BlockViewModel>();

        public EditorViewModel()
        {
            CanvasElements  = new ObservableCollection<UserControl>();

            #region Commands
            NewBlockCommand = new Command(AddNewBlock);
            DrawConnectorCommand = new Command(DrawConnector);
            RemoveCommand = new Command(RemoveBlocks);
            #endregion
        }

        private void RemoveBlocks(object obj)
        {
            _selectedBlocks.Clear();

            var notSelected = CanvasElements.Where(b => !(b is Block) 
            || !((BlockViewModel)((Block)b).DataContext).IsSelected);
            CanvasElements = new ObservableCollection<UserControl>(notSelected);
        }

        private void DrawConnector(object obj)
        {
            //called only for _selectedBlocks.Count == 2
            var connector = new Connector(_selectedBlocks[0], _selectedBlocks[1]);
            CanvasElements.Add(connector);
        }

        private void AddNewBlock(object obj)
        {
            var block = new Block();
            ((BlockViewModel)block.DataContext).SelectedChabged += BlockSelectedChanged;
            CanvasElements.Add(block);
        }

        private void BlockSelectedChanged(object sender, bool selected)
        {
            if (!selected)
                _selectedBlocks.Remove((BlockViewModel)sender);
            else _selectedBlocks.Add((BlockViewModel)sender);

            OnPropertyChanged(nameof(RemoveButtonVis));
            OnPropertyChanged(nameof(ConnectButtonVis));
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
