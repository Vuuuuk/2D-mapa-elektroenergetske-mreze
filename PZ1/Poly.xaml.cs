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

    public partial class Poly : Window
    {

        //DRAWING VARS

        public static bool editFlag; // polygon editing
        public static Polygon polygon;
        public static TextBlock txt = null; //text addon

        //DRAWING VARS

        public Poly()
        {
            InitializeComponent();
            editFlag = false;
            txt_polygon_text_size.IsEnabled = false;
            cmb_border_color.ItemsSource = typeof(Colors).GetProperties();
            cmb_polygon_color.ItemsSource = typeof(Colors).GetProperties();
            cmb_text_color.ItemsSource = typeof(Colors).GetProperties();
        }

        public Poly(Polygon obj, TextBlock txt_editing)
        {
            InitializeComponent();
            editFlag = true;
            polygon = obj;

            txt_border_thickness.Text = obj.StrokeThickness.ToString();

            cmb_border_color.ItemsSource = typeof(Colors).GetProperties();
            cmb_polygon_color.ItemsSource = typeof(Colors).GetProperties();
            cmb_text_color.ItemsSource = typeof(Colors).GetProperties();

            txt = txt_editing;
            if (txt_editing == null)
            {
                txt_polygon_text.IsReadOnly = true;
                cmb_polygon_color.IsEnabled = false;
                txt_polygon_text_size.IsEnabled = false;
            }
            else
            {
                txt_polygon_text.Text = txt_editing.Text;
                txt_polygon_text.IsReadOnly = true;
                if (!txt_polygon_text_size.Text.Trim().Equals(""))
                    txt.FontSize = int.Parse(txt_polygon_text_size.Text.Trim());
            }
        }

        public static Polygon CreatePolygon(double borderThickness, Color borderColor, Color fillColor, List<Point> points)
        {
            Polygon polygon = new Polygon();
            polygon.StrokeThickness = borderThickness;
            polygon.Stroke = new SolidColorBrush(borderColor);
            polygon.Fill = new SolidColorBrush(fillColor);

            foreach (var item in points)
                polygon.Points.Add(item);

            return polygon;

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            editFlag = false;
            this.Close();
        }

        private void btn_draw_Click(object sender, RoutedEventArgs e)
        {
            //VALIDATION

            if (txt_border_thickness.Text.Trim().Equals("") || Double.Parse(txt_border_thickness.Text.Trim()) == 0)
            {
                MessageBox.Show("Please be sure to fill in border thickness field with non zero values!", string.Empty, MessageBoxButton.OK);
                return;
            }
            else if (cmb_border_color.SelectedItem as PropertyInfo == null || cmb_polygon_color.SelectedItem as PropertyInfo == null)
            {
                MessageBox.Show("Please be sure to select the color of the Polygon!", string.Empty, MessageBoxButton.OK);
                return;
            }
            else if (!txt_polygon_text.Text.Trim().Equals("") && cmb_text_color.SelectedItem as PropertyInfo == null)
            {
                MessageBox.Show("Please be sure to select the color of your Text!", string.Empty, MessageBoxButton.OK);
                return;
            }
            else if (txt_polygon_text.Text.Trim().Equals("") && cmb_text_color.IsEnabled == true && cmb_text_color.SelectedItem as PropertyInfo != null && txt_polygon_text.IsReadOnly == false)
            {
                MessageBox.Show("Please be sure to write some text!", string.Empty, MessageBoxButton.OK);
                return;
            }

            if (editFlag == true)
            {
                polygon.StrokeThickness = Double.Parse(txt_border_thickness.Text.Trim());

                Color borderColor = (Color)(cmb_border_color.SelectedItem as PropertyInfo).GetValue(1, null);
                Color fillColor = (Color)(cmb_polygon_color.SelectedItem as PropertyInfo).GetValue(1, null);

                polygon.Stroke = new SolidColorBrush(borderColor);
                polygon.Fill = new SolidColorBrush(fillColor);

                if (txt != null)
                {
                    Color cmbcolor = (Color)(cmb_polygon_color.SelectedItem as PropertyInfo).GetValue(1, null);
                    txt.Foreground = new SolidColorBrush(cmbcolor);
                    if (!txt_polygon_text_size.Text.Trim().Equals(""))
                        txt.FontSize = int.Parse(txt_polygon_text_size.Text.Trim());
                }
            }
            else
            {
                Color borderColor = (Color)(cmb_border_color.SelectedItem as PropertyInfo).GetValue(1, null);
                Color fillColor = (Color)(cmb_polygon_color.SelectedItem as PropertyInfo).GetValue(1, null);

                if (txt_polygon_text.Text != "")
                {
                    txt = new TextBlock();

                    Color cmbcolor = (Color)(cmb_text_color.SelectedItem as PropertyInfo).GetValue(1, null);

                    txt.Foreground = new SolidColorBrush(cmbcolor);
                    txt.FontSize = 18;
                    txt.Text = txt_polygon_text.Text;

                }

                polygon = CreatePolygon(Double.Parse(txt_border_thickness.Text.Trim()), borderColor, fillColor, MainWindow.points);

            }

            this.Close();
        }
    }
}
