using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleBlockEditor
{
    /// <summary>
    /// Interaction logic for Connector.xaml
    /// </summary>
    public partial class Connector : UserControl
    {
        //TO FIND NORMAL SOLUTION
        public BlockViewModel Block1 { get; set; }
        public BlockViewModel Block2 { get; set; }
        public Connector(BlockViewModel b1, BlockViewModel b2)
        {
            Block1 = b1;
            Block2 = b2;
            InitializeComponent();
        }
    }
}
