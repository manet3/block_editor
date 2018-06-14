using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleBlockEditor
{
    class EditorViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Commands
        public ICommand NewBlockCommand { get; }
        #endregion

        public ObservableCollection<Block> Blocks { get; } = new ObservableCollection<Block>();

        public EditorViewModel()
        {
            NewBlockCommand = new Command(AddNewBlock);
        }

        private void AddNewBlock(object obj)
        {
            Blocks.Add(new Block());
        }
    }
}
