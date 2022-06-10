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
    public partial class Ecllipse : Window
    {
        //DRAWING VARS

        public static bool editFlag; // ecllipse editing
        public static Ellipse ecllipse; 
        public static TextBlock txt = null; //text addon

        //DRAWING VARS

        public Ecllipse()
        {
            InitializeComponent();
            editFlag = false;
            txt_ellipse_text_size.IsEnabled = false;

            //COLOR INIT

            cmb_border_color.ItemsSource = typeof(Colors).GetProperties();
            cmb_ellipse_color.ItemsSource = typeof(Colors).GetProperties();
            cmb_text_color.ItemsSource = typeof(Colors).GetProperties();

            //COLOR INIT
        }

        //Const for editing

        public Ecllipse(Ellipse obj, TextBlock txt_editing)
        {
            InitializeComponent();
            ecllipse = obj;
            editFlag = true;
            txt = txt_editing;

            txt_ellipse_width.Text = obj.Width.ToString();
            txt_ellipse_height.Text = obj.Height.ToString();
            txt_border_thickness.Text = obj.StrokeThickness.ToString();


            txt_ellipse_height.IsReadOnly = true;
            txt_ellipse_width.IsReadOnly = true;

            cmb_border_color.ItemsSource = typeof(Colors).GetProperties();
            cmb_ellipse_color.ItemsSource = typeof(Colors).GetProperties();
            cmb_text_color.ItemsSource = typeof(Colors).GetProperties();

            if (txt_editing == null)
            {
                txt_ellipse_text.IsReadOnly = true;
                cmb_text_color.IsEnabled = false;
                txt_ellipse_text_size.IsEnabled = false;
            }
            else
            {
                txt_ellipse_text.Text = txt_editing.Text;
                txt_ellipse_text.IsReadOnly = true;
                if (!txt_ellipse_text_size.Text.Trim().Equals(""))
                    txt_ellipse_text.FontSize = int.Parse(txt_ellipse_text_size.Text.Trim());
            }
        }

        private static Ellipse CreateEllipse(double width, double height, double borderThickness, Color borderColor, Color fillColor)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = width;
            ellipse.Height = height;
            ellipse.StrokeThickness = borderThickness;
            ellipse.Stroke = new SolidColorBrush(borderColor);
            ellipse.Fill = new SolidColorBrush(fillColor);

            return ellipse;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_draw_Click(object sender, RoutedEventArgs e)
        {
            
            //VALIDATION

            if (txt_ellipse_width.Text.Trim().Equals("") || txt_ellipse_height.Text.Trim().Equals("") || txt_border_thickness.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please be sure to fill in width/height and border thickness fields with non zero values!", string.Empty, MessageBoxButton.OK);
                return;
            }
            else if (Double.Parse(txt_ellipse_width.Text.Trim()) == 0 || Double.Parse(txt_ellipse_height.Text.Trim()) == 0 || Double.Parse(txt_ellipse_height.Text.Trim()) == 0)
            {
                MessageBox.Show("Please be sure to fill in width/height and border thickness fields with non zero values!", string.Empty, MessageBoxButton.OK);
                return;
            }
            else if (cmb_border_color.SelectedItem as PropertyInfo == null || cmb_ellipse_color.SelectedItem as PropertyInfo == null)
            {
                MessageBox.Show("Please be sure to select the color of the Ellipse!", string.Empty, MessageBoxButton.OK);
                return;
            }
            else if (!txt_ellipse_text.Text.Trim().Equals("") && cmb_text_color.SelectedItem as PropertyInfo == null)
            {
                MessageBox.Show("Please be sure to select the color of your Text!", string.Empty, MessageBoxButton.OK);
                return;
            }
            else if (txt_ellipse_text.Text.Trim().Equals("") && cmb_text_color.IsEnabled == true && cmb_text_color.SelectedItem as PropertyInfo != null && txt_ellipse_text.IsReadOnly == false)
            {
                MessageBox.Show("Please be sure to write some text!", string.Empty, MessageBoxButton.OK);
                return;
            }

            //VALIDATION

            if (editFlag == true)
            {

                ecllipse.StrokeThickness = Double.Parse(txt_border_thickness.Text.Trim());

                Color borderColor = (Color)(cmb_border_color.SelectedItem as PropertyInfo).GetValue(1, null);
                Color fillColor = (Color)(cmb_ellipse_color.SelectedItem as PropertyInfo).GetValue(1, null);

                ecllipse.Stroke = new SolidColorBrush(borderColor);
                ecllipse.Fill = new SolidColorBrush(fillColor);

                if (txt != null)
                {
                    Color cmbcolor = (Color)(cmb_text_color.SelectedItem as PropertyInfo).GetValue(1, null);
                    txt.Foreground = new SolidColorBrush(cmbcolor);
                    if(!txt_ellipse_text_size.Text.Trim().Equals(""))
                        txt.FontSize = int.Parse(txt_ellipse_text_size.Text.Trim());
                }

            }

            else
            {
                Color borderColor = (Color)(cmb_border_color.SelectedItem as PropertyInfo).GetValue(1, null);
                Color fillColor = (Color)(cmb_ellipse_color.SelectedItem as PropertyInfo).GetValue(1, null);

                if (txt_ellipse_text.Text != "")
                {
                    txt = new TextBlock();

                    Color cmbcolor = (Color)(cmb_text_color.SelectedItem as PropertyInfo).GetValue(1, null);

                    txt.Foreground = new SolidColorBrush(cmbcolor);
                    txt.FontSize = 18;
                    txt.Text = txt_ellipse_text.Text;

                }

                ecllipse = CreateEllipse(Double.Parse(txt_ellipse_width.Text.Trim()), Double.Parse(txt_ellipse_height.Text.Trim()), Double.Parse(txt_border_thickness.Text.Trim()), borderColor, fillColor);

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
