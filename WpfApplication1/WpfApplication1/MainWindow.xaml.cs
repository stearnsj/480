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
using System.Text.RegularExpressions;
using System.Collections;



namespace WpfApplication1
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = "";
            EquationViewer_Copy.Text = "";
        }

        public void solve_click(object sender, RoutedEventArgs e)
        {

            //reversePolishNotation.ReversePolishNotation myFeed = new reversePolishNotation.ReversePolishNotation();
            reversePolishNotation.ReversePolishNotation myFeed = new reversePolishNotation.ReversePolishNotation();
            try
            {
                myFeed.ParseString(EquationViewer.Text);
                EquationViewer_Copy.Text = myFeed.Evaluate().ToString();
                if (EquationViewer_Copy.Text.Equals("Infinity"))
                {
                    EquationViewer_Copy.Text = "Overflow Error!";
                }
                EquationViewer.Text = "";
            
            }
            catch (Exception exc)
            {
                EquationViewer_Copy.Text = exc.Message;
                EquationViewer.Text = "";
            }
        }

        private void add_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "+";
        }

        private void pow_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "^";
        }

        private void Par_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "(";
        }

        private void div_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "/";
        }

        private void times_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "*";
        }

        private void sub_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "-";
        }

        private void zero_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "0";
        }

        private void one_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "1";
        }

        private void two_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "2";
        }

        private void three_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "3";
        }

        private void dot_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + ".";
        }

        private void four_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "4";
        }

        private void five_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "5";
        }

        private void six_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "6";
        }

        private void seven_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "7";
        }

        private void eight_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "8";
        }

        private void nine_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "9";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void TextBox_TextChanged2(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void RPar_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + ")";
        }

        private void LPar_click(object sender, RoutedEventArgs e)
        {
            EquationViewer.Text = EquationViewer.Text + "(";
        }
    }
}
