using System;
using System.Windows;
using Shared;

namespace SharedSourceFailFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void DoSomething() => Console.WriteLine(MyEnum.One);
    }
}
