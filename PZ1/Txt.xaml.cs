using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PZ1
{

    public partial class Txt : Window
    {

        //DRAWING VARS

        public static bool editFlag; // ecllipse editing
        public static TextBox txt = null; //text addon

        //DRAWING VARS

        public Txt()
        {
            InitializeComponent();
            editFlag = false;
            cmb_text_color.ItemsSource = typeof(Colors).GetProperties();
        }

        public Txt(TextBox txt_editing)
        {
            InitializeComponent();
            editFlag = true;
            txt = txt_editing;

            txt_text.IsReadOnly = true;
            txt_text.Text = txt_editing.Text;
            txt_text_size.Text = txt_editing.FontSize.ToString();
            cmb_text_color.ItemsSource = typeof(Colors).GetProperties();
        }

        private static TextBox CreateText(string text, double fontSize, Color foreground)
        {
            TextBox textBox = new TextBox();
            textBox.Text = text;
            textBox.FontSize = fontSize;
            textBox.Foreground = new SolidColorBrush(foreground);

            return textBox;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_draw_Click(object sender, RoutedEventArgs e)
        {
            //validacija polja
            if (txt_text.Text.Trim().Equals("") || txt_text_size.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please be sure to fill in some text and its size with non zero/empty values!", string.Empty, MessageBoxButton.OK);
                return;

            }
            else if (Double.Parse(txt_text_size.Text.Trim()) == 0)
            {
                MessageBox.Show("Please be sure to fill in the text size with non zero value!", string.Empty, MessageBoxButton.OK);
                return;
            }
            else if (cmb_text_color.SelectedItem as PropertyInfo == null)
            {
                MessageBox.Show("Please be sure to select the color of your Text!", string.Empty, MessageBoxButton.OK);
                return;
            }

            if (editFlag == true)
            {
                txt.Text = txt_text.Text;
                txt.FontSize = Double.Parse(txt_text_size.Text.Trim());

                Color foreground = (Color)(cmb_text_color.SelectedItem as PropertyInfo).GetValue(1, null);
                txt.Foreground = new SolidColorBrush(foreground);

            }
            else
            {
                Color foreground = (Color)(cmb_text_color.SelectedItem as PropertyInfo).GetValue(1, null);
                txt = CreateText(txt_text.Text, Double.Parse(txt_text_size.Text.Trim()), foreground);
            }

            this.Close();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            editFlag = false;
            this.Close();
        }
    }
}
