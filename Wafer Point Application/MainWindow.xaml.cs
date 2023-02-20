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


namespace Wafer_Point_Application
{
    public partial class MainWindow : Window
    {
        List<Ellipse> activePlot = null;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            PointCalculator calc = new PointCalculator();
            deletePlot(this.activePlot);
            try
            {
                int rings = calc.validateInput(int.Parse(inputBox.Text));
                if (rings < 0)
                {
                    underText.Text = "Invalid number. Entry must satisfy (8n+1)";
                    resultPoints.Text = "";
                }
                    
                else
                {
                    underText.Text = "Rings generated: " + rings.ToString();
                    List<Point> points = calc.generatePoints(rings);
                    this.activePlot = plotList(points);
                    resultPoints.Text = calc.printList(points);
                }
                
            }
            catch(Exception ex)
            {
                underText.Text = "Invalid input";
                resultPoints.Text = "";
            }
            
        }

        private List<Ellipse> plotList(List<Point> pointList)
        {
            List<Ellipse> plotPoints = new List<Ellipse>();
            foreach (Point point in pointList)
            {
                ToolTip tt = new ToolTip
                {
                    Content = "[" + Math.Round(point.x, 2) + ", " + Math.Round(point.y, 2) + "] "

                };
                Ellipse newEllipse = new Ellipse
                {
                    Width = 7,
                    Height = 7,
                    Fill = Brushes.Red,
                    Stroke = Brushes.Red
                };
                ToolTipService.SetInitialShowDelay(newEllipse, 0);
                ToolTipService.SetBetweenShowDelay(newEllipse, 0);
                newEllipse.ToolTip = tt;
                plotPoints.Add(newEllipse);
                Canvas.SetLeft(newEllipse, 622.5+point.x);
                Canvas.SetTop(newEllipse, 219.5+point.y);
                myCanvas.Children.Add(newEllipse);
            }
            return plotPoints;  

        }

        private void deletePlot(List<Ellipse> activePlot)
        {
            if(activePlot != null)
            {
                foreach (Ellipse plot in activePlot)
                {
                    myCanvas.Children.Remove(plot);
                }
            }

        }
    }
}
