using Point = System.Windows.Point;
using PZ1.Common;
using PZ1.Models;
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

namespace PZ1
{
    public partial class MainWindow : Window
    {


        Data data;
        Ecllipse newEclipse;
        Poly newPolygon;
        Txt text;


        //DRAWING VARS

        string selectedObjectOrOperation; // what object to draw on canvas or undo/redo
        Point point; // (X,Y) coords
        public static List<Point> points = new List<Point>(); // Points list for rect drawing
        bool cls = false; // clear screen on true
        int numOfObjects;
        List<List<UIElement>> redo = new List<List<UIElement>>();

        //DRAWING VARS


        //DATA VARS

        PowerEntity[,] matrix = new PowerEntity[160, 160];
        List<Line> allLines = new List<Line>();

        //DATA VARS

        public MainWindow()
        {
            InitializeComponent();
            data = new Data();
            DrawData();
            DrawLines();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to quit?", String.Empty, MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result.Equals(MessageBoxResult.Yes))
                this.Close();
        }

        private void btn_elipse_Click(object sender, RoutedEventArgs e)
        {
            selectedObjectOrOperation = "Ellipse";
        }

        private void btn_polygon_Click(object sender, RoutedEventArgs e)
        {
            selectedObjectOrOperation = "Polygon";
        }

        private void btn_text_Click(object sender, RoutedEventArgs e)
        {
            selectedObjectOrOperation = "Text";
        }

        private void btn_undo_Click(object sender, RoutedEventArgs e)
        {
            selectedObjectOrOperation = "Undo";

            //if (cls == true)
            //{
            //    for (int i = 0; i < numOfObjects; i++)
            //    {
            //        if (redo[redo.Count - 1][1] != null)
            //        {
            //            MainCanvas.Children.Add(redo[redo.Count - 1][0]);
            //            MainCanvas.Children.Add(redo[redo.Count - 1][1]);
            //            data.canvasObjects.Add(new List<UIElement>() { redo[redo.Count - 1][0], redo[redo.Count - 1][1] });
            //        }
            //        else
            //        {
            //            MainCanvas.Children.Add(redo[redo.Count - 1][0]);
            //            data.canvasObjects.Add(new List<UIElement>() { redo[redo.Count - 1][0], null });
            //        }

            //        redo.Remove(redo[redo.Count - 1]);
            //    }
            //    cls = false;
            //    return;
            //}

            //if (data.canvasObjects.Count > 0)
            //{
            //    if (data.canvasObjects[data.canvasObjects.Count - 1][1] != null)
            //    {
            //        redo.Add(new List<UIElement>() { MainCanvas.Children[MainCanvas.Children.Count - 2], MainCanvas.Children[MainCanvas.Children.Count - 1] });
            //        data.canvasObjects.Remove(data.canvasObjects[data.canvasObjects.Count - 1]);
            //        MainCanvas.Children.Remove(MainCanvas.Children[MainCanvas.Children.Count - 1]);
            //        MainCanvas.Children.Remove(MainCanvas.Children[MainCanvas.Children.Count - 1]);
            //    }
            //    else
            //    {
            //        redo.Add(new List<UIElement>() { MainCanvas.Children[MainCanvas.Children.Count - 1], null });
            //        MainCanvas.Children.Remove(MainCanvas.Children[MainCanvas.Children.Count - 1]);
            //        data.canvasObjects.Remove(data.canvasObjects[data.canvasObjects.Count - 1]);
            //    }
            //}
        }

        public void DrawData()
        {
            foreach (NodeEntity node in data.Nodes.Values)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 2;
                ellipse.Height = 2;
                ellipse.Fill = Brushes.Blue;
                ellipse.Stroke = Brushes.Black;
                ellipse.StrokeThickness = 0.1;
                ellipse.ToolTip = "Id: " + node.Id + Environment.NewLine + "Name: " + node.Name;
                ellipse.Uid = node.Id.ToString(); //ID FOR EASIER IDENTIFICATION LATER 

                MapAndDraw(ellipse, node);
            }



            foreach (SubstationEntity sub in data.Substations.Values)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 2;
                ellipse.Height = 2;
                ellipse.Fill = Brushes.Red;
                ellipse.Stroke = Brushes.Yellow;
                ellipse.StrokeThickness = 0.1;
                ellipse.ToolTip = "Id: " + sub.Id + Environment.NewLine + "Name: " + sub.Name;
                ellipse.Uid = sub.Id.ToString();

                MapAndDraw(ellipse, sub);
            }

            foreach (SwitchEntity sw in data.Switches.Values)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 2;
                ellipse.Height = 2;
                ellipse.Fill = Brushes.Green;
                ellipse.Stroke = Brushes.MediumTurquoise;
                ellipse.StrokeThickness = 0.1;
                ellipse.ToolTip = "Id: " + sw.Id + Environment.NewLine + "Name: " + sw.Name + Environment.NewLine + "State: " + sw.Status;
                ellipse.Uid = sw.Id.ToString();

                MapAndDraw(ellipse, sw);
            }
        }

        public void MapAndDraw(Ellipse ellipse, PowerEntity entity)
        {
            //800x800
            int heightCanvas = (int)MainCanvas.Height;
            int widthCanvas = (int)MainCanvas.Width;
            int distancePoint = 5; // SIZE 5x5px
                                   // 800/5 = 160 -> 160x160 parts


            int i = Convert.ToInt32(entity.CanvasPoint.CanvasX / distancePoint); //WHICH PART OF THE MATRIX - X
            int j = Convert.ToInt32(entity.CanvasPoint.CanvasY / distancePoint); //WHICH PART OF THE MATRIX - Y

            if (i == 160)
                i = 159;
            if (j == 160)
                j = 159;

            int distance;
            //Looking for the next free spot (5x5) total (160x160)
            if (matrix[i, j] != null)
            {
                distance = 1;//SHORTEST DISTANCE
                //WHILE WE ARE IN 160x160
                while (distance != 161)
                {
                    if (i + distance < 160 && matrix[i + distance, j] == null)
                    {
                        matrix[i + distance, j] = entity;

                        i = i + distance;
                        break;
                    }
                    else if (i - distance > 0 && matrix[i - distance, j] == null)
                    {
                        matrix[i - distance, j] = entity;

                        i = i - distance;
                        break;
                    }
                    else if (j + distance < 160 && matrix[i, j + distance] == null)
                    {
                        matrix[i, j + distance] = entity;

                        j = j + distance;
                        break;
                    }
                    else if (j - distance > 0 && matrix[i, j - distance] == null)
                    {
                        matrix[i, j - distance] = entity;

                        j = j - distance;
                        break;
                    }
                    else if (j - distance > 0 && i - distance > 0 && matrix[i - distance, j - distance] == null)
                    {
                        matrix[i - distance, j - distance] = entity;

                        i = i - distance;
                        j = j - distance;
                        break;
                    }
                    else if (j + distance < 160 && i + distance < 160 && matrix[i + distance, j + distance] == null)
                    {
                        matrix[i + distance, j + distance] = entity;

                        i = i + distance;
                        j = j + distance;
                        break;
                    }
                    else if (i + distance < 160 && j - distance > 0 && matrix[i + distance, j - distance] == null)
                    {
                        matrix[i + distance, j - distance] = entity;

                        i = i + distance;
                        j = j - distance;
                        break;
                    }
                    else if (i - distance > 0 && j + distance < 160 && matrix[i - distance, j + distance] == null)
                    {
                        matrix[i - distance, j + distance] = entity;

                        i = i - distance;
                        j = j + distance;
                        break;
                    }
                    distance++; // IF EVERYTHING AROUND IS TAKEN + 1 to each side/corner
                }


            }
            // PLACE IT ON THE FREE SPOT
            else
            {
                matrix[i, j] = entity;
            }

            Canvas.SetLeft(ellipse, i * distancePoint);
            Canvas.SetTop(ellipse, j * distancePoint); //crtamo na canvas posto je svaki podeok 5x5 a takih podeoka ima 160x160 onda treba 
                                                       //da ga nacramo na canvasu i to tako sto cemo pomoziti mesto na kom se nalazi podeok sa 5

            MainCanvas.Children.Add(ellipse);

            entity.CanvasPoint.GridX = i * distancePoint;//grid vrednost
            entity.CanvasPoint.GridY = j * distancePoint;

        }



        private void btn_redo_Click(object sender, RoutedEventArgs e)
        {
            selectedObjectOrOperation = "Redo";
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            cls = true;
            numOfObjects = data.canvasObjects.Count; //So that we dont use the value when me modify it with deleting someting
            for (int i = 0; i < numOfObjects; i++)
            {
                if (data.canvasObjects[data.canvasObjects.Count - 1][1] != null)
                {
                    MainCanvas.Children.RemoveAt(MainCanvas.Children.Count - 1);
                    MainCanvas.Children.RemoveAt(MainCanvas.Children.Count - 1);
                }
                else
                    MainCanvas.Children.RemoveAt(MainCanvas.Children.Count - 1);

                data.canvasObjects.Remove(data.canvasObjects[data.canvasObjects.Count - 1]);
            }

            selectedObjectOrOperation = string.Empty;
        }

        private void DrawLines()
        {

            //FOR EACH LINE
            foreach (LineEntity line in data.Lines.Values) 
            {

                CanvasPoint canvasPoint = null;
                CanvasPoint canvasPoint2 = null;

                if (data.Nodes.ContainsKey(line.FirstEnd)) //ONLY FIRST & END NODE
                    canvasPoint = data.Nodes[line.FirstEnd].CanvasPoint;
                if (data.Nodes.ContainsKey(line.SecondEnd))
                    canvasPoint2 = data.Nodes[line.SecondEnd].CanvasPoint;

                if (data.Substations.ContainsKey(line.FirstEnd))
                    canvasPoint = data.Substations[line.FirstEnd].CanvasPoint;
                if (data.Substations.ContainsKey(line.SecondEnd))
                    canvasPoint2 = data.Substations[line.SecondEnd].CanvasPoint;

                if (data.Switches.ContainsKey(line.FirstEnd))
                    canvasPoint = data.Switches[line.FirstEnd].CanvasPoint;
                if (data.Switches.ContainsKey(line.SecondEnd))
                    canvasPoint2 = data.Switches[line.SecondEnd].CanvasPoint;

                if (canvasPoint == null || canvasPoint2 == null)
                    continue;

                Line drawingLine = new Line();

                Line drawingLine2 = new Line();

                drawingLine.Stroke = Brushes.DarkOrange;
                drawingLine.StrokeThickness = 1;
                drawingLine.ToolTip = $"ID:{line.Id}  Name:{line.Name}"; //Line tooltip
                drawingLine.Uid = line.Id.ToString() + ":" + line.FirstEnd.ToString() + ":" + line.SecondEnd.ToString();


                //NOT WORKING IDK WHY 
                drawingLine2.Stroke = Brushes.DarkOrange;
                drawingLine2.StrokeThickness = 1;
                drawingLine2.ToolTip = $"ID:{line.Id}  Name:{line.Name}"; 
                drawingLine2.Uid = line.Id.ToString() + ":" + line.FirstEnd.ToString() + ":" + line.SecondEnd.ToString();

                if (canvasPoint.GridX == canvasPoint2.GridX)
                {

                    drawingLine.X1 = canvasPoint.GridX + 1;
                    drawingLine.Y1 = canvasPoint.GridY + 1;
                    drawingLine.X2 = canvasPoint.GridX + 1;
                    drawingLine.Y2 = canvasPoint.GridY + 1;

                    allLines.Add(drawingLine);
                    MainCanvas.Children.Add(drawingLine);

                }
                else if (canvasPoint.GridY == canvasPoint2.GridY)
                {

                    drawingLine.X1 = canvasPoint.GridX + 1;
                    drawingLine.Y1 = canvasPoint.GridY + 1;
                    drawingLine.X2 = canvasPoint2.GridX + 1;
                    drawingLine.Y2 = canvasPoint.GridY + 1;

                    allLines.Add(drawingLine);
                    MainCanvas.Children.Add(drawingLine);

                }
                else
                {
                    drawingLine.X1 = canvasPoint.GridX + 1;
                    drawingLine.Y1 = canvasPoint.GridY + 1;
                    drawingLine.X2 = canvasPoint2.GridX + 1;
                    drawingLine.Y2 = canvasPoint.GridY + 1;

                    allLines.Add(drawingLine);
                    MainCanvas.Children.Add(drawingLine);

                    drawingLine2.X1 = canvasPoint2.GridX + 1;
                    drawingLine2.Y1 = canvasPoint.GridY + 1;
                    drawingLine2.X2 = canvasPoint2.GridX + 1;
                    drawingLine2.Y2 = canvasPoint2.GridY + 1;

                    allLines.Add(drawingLine2);
                    MainCanvas.Children.Add(drawingLine2);

                }
            }
        }

        private void MainCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            point = Mouse.GetPosition(MainCanvas); // current mouse (X, Y) coords

            switch(selectedObjectOrOperation)
            {
                case "Ellipse":
                    newEclipse = new Ecllipse();
                    newEclipse.ShowDialog();

                    //ELLIPSE LEFT MB DOWN EVENT CREATION FOR EDITING
                    Ecllipse.ecllipse.MouseLeftButtonDown += new MouseButtonEventHandler(Ellipse_MouseLeftButtonDown);

                    MainCanvas.Children.Add(Ecllipse.ecllipse);
                    Canvas.SetLeft(Ecllipse.ecllipse, point.X);
                    Canvas.SetTop(Ecllipse.ecllipse, point.Y);
                    if (Ecllipse.txt != null)
                    {
                        MainCanvas.Children.Add(Ecllipse.txt);

                        //CENTERED TEXT IF THERE IS ANY
                        Canvas.SetLeft(Ecllipse.txt, point.X + Ecllipse.ecllipse.Width / 2);
                        Canvas.SetTop(Ecllipse.txt, point.Y + Ecllipse.ecllipse.Height / 2);
                    }

                    data.canvasObjects.Add(new List<UIElement>() { Ecllipse.ecllipse, Ecllipse.txt });

                    Ecllipse.txt = null;
                    selectedObjectOrOperation = null;
                    return;

                case "Polygon":
                    point = Mouse.GetPosition(MainCanvas);

                    //DOT DRAWING FOR EASIER VISUAL REPRESENTATION

                    Ellipse dot = new Ellipse
                    {
                        Width = 5,
                        Height = 5,
                        Fill = Brushes.MediumPurple,
                        Uid = "PolyDots" //FOR EASIER REMOVAL ON DRAW ~ NOT IMPLEMENTED
                    };
                    Canvas.SetTop(dot, point.Y);
                    Canvas.SetLeft(dot, point.X);
                    MainCanvas.Children.Add(dot);

                    data.canvasObjects.Add(new List<UIElement>() { dot, null }); //FOR EASIER REMOVAL ON CLEAR

                    points.Add(point);

                    return;
                case "Text":
                    text = new Txt();
                    text.ShowDialog();

                    //DISABLE ANY ON SCREEN CHANGE
                    Txt.txt.IsReadOnly = true;
                    Txt.txt.Background = GetColorFromHexa("#424242");

                    //TEXT LEFT MB DOWN EVENT CREATION FOR EDITING
                    Txt.txt.AddHandler(TextBox.MouseLeftButtonDownEvent, new MouseButtonEventHandler(Text_MouseLeftButtonDown), true);
                    MainCanvas.Children.Add(Txt.txt);
                    Canvas.SetLeft(Txt.txt, point.X);
                    Canvas.SetTop(Txt.txt, point.Y);

                    data.canvasObjects.Add(new List<UIElement>() { Txt.txt, null });

                    selectedObjectOrOperation = null; //Not working when there is no reset for textBox
                    return;
            }
        }

        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedObjectOrOperation == "Polygon")
            {
                if (points.Count >= 3)
                {
                    newPolygon = new Poly();
                    newPolygon.ShowDialog();

                    Poly.polygon.MouseLeftButtonDown += new MouseButtonEventHandler(Polygon_MouseLeftButtonDown);
                    MainCanvas.Children.Add(Poly.polygon);

                    if (Poly.txt != null)
                    {
                        MainCanvas.Children.Add(Poly.txt);
                        double X, Y;
                        GetCenterOfPolygon(points, out X, out Y); //CENTERED TEXT IF THERE IS ANY

                        Canvas.SetLeft(Poly.txt, X);
                        Canvas.SetTop(Poly.txt, Y);
                    }

                    data.canvasObjects.Add(new List<UIElement>() { Poly.polygon, Poly.txt });

                    Poly.txt = null;

                    points.Clear();

                }

            }
        }

        //STACK OVERFLOW
        public static void GetCenterOfPolygon(List<Point> poly, out double x, out double y)
        {
            double accumulatedArea = 0.0f;
            double centerX = 0.0f;
            double centerY = 0.0f;

            for (int i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
            {
                double temp = poly[i].X * poly[j].Y - poly[j].X * poly[i].Y;
                accumulatedArea += temp;
                centerX += (poly[i].X + poly[j].X) * temp;
                centerY += (poly[i].Y + poly[j].Y) * temp;
            }

            accumulatedArea *= 3f;
            x = centerX / accumulatedArea;
            y = centerY / accumulatedArea;
            return;
        }


        private void Ellipse_MouseLeftButtonDown(object obj, MouseButtonEventArgs e)
        {
            TextBlock editTxt = null;

            foreach (var item in data.canvasObjects)
                if (item[0] == (Ellipse)obj)
                    editTxt = (TextBlock)item[1];
   
            Ecllipse editEcllipseWindow = new Ecllipse((Ellipse)obj, editTxt); //Using edit const
            editEcllipseWindow.ShowDialog();
            Ecllipse.txt = null;
        }

        private void Polygon_MouseLeftButtonDown(object obj, MouseButtonEventArgs e)
        {
            TextBlock editTxt = null;

            foreach (var item in data.canvasObjects)
                if (item[0] == (Polygon)obj)
                    editTxt = (TextBlock)item[1];

            Poly editPolygonWindow = new Poly((Polygon)obj, editTxt); //Using edit const
            editPolygonWindow.ShowDialog();
            Poly.txt = null;
        }

        void Text_MouseLeftButtonDown(object obj, MouseButtonEventArgs e)
        {
            Txt editTextWindow = new Txt((TextBox)obj); //Using edit const, second param is none
            editTextWindow.ShowDialog();
        }

        //HEX CONVERTER HELPER FROM STACK OVERFLOW
        SolidColorBrush GetColorFromHexa(string hexaColor)
        {
            byte r = Convert.ToByte(hexaColor.Substring(1, 2), 16);
            byte g = Convert.ToByte(hexaColor.Substring(3, 2), 16);
            byte b = Convert.ToByte(hexaColor.Substring(5, 2), 16);
            SolidColorBrush soliColorBrush = new SolidColorBrush(Color.FromArgb(0xFF, r, g, b));
            return soliColorBrush;
        }
    }
}
