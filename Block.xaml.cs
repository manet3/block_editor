﻿using System;
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
    public partial class Block : UserControl
    {
        public DependencyProperty ConnectLocationProperty;
        public Block()
        {
            //ConnectLocationProperty = DependencyProperty.Register("ConnectorLocation", typeof(Point), typeof(Block), new Fra)
            InitializeComponent();
        }
    }
}
