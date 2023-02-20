using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Wafer_Point_Application
{
    internal class Point
    {
        public double x { get; set; }
        public double y { get; set; }

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    internal class PointCalculator
    {
        //number of slices = 8
        //first point always at [0,0]

        public int validateInput(int input)
        {
            if ((input - 1) % 8 != 0 || input < 1)
                return -1;
            else
                return ((input - 1) / 8);
        }

        //input validation is done outside this function, takes in number of full rings to generate
        public List<Point> generatePoints(int rings)
        {
            List<Point> pointList = new List<Point>();
            pointList.Add(new Point(0, 0));
            double ringStep = (150 / ((double)rings + 1)); //Distance between points on the slice
            double distance = ringStep;

            for (int i = 0; i < rings; i++)
            {
                double angle = Math.PI / 4;
                for (int j = 0; j < 8; j++)
                {
                    double x = distance * Math.Sin(angle);
                    double y = distance * Math.Cos(angle);
                    pointList.Add(new Point(x, y));
                    angle += Math.PI / 4;
                }
                distance += ringStep;
            }
            return pointList;
        }

        public string printList(List<Point> pointList)
        {
            string pointString = "Ring: 0\n";
            int i = 0;
            int ring = 1;

            foreach (Point point in pointList)
            {
                if ((i-1) % 8 == 0)
                {
                    pointString += "Ring: " + ring.ToString() + "\n";
                    ring++;
                }
                    
                pointString += "[" + (Math.Round(point.x, 2) + 0) + ", " + (Math.Round(point.y, 2) + 0) + "] ";
                if (i % 4 == 0)
                {
                    pointString += "\n";
                    if (i % 8 == 0)
                        pointString += "\n";
                }

                i++;
            }
            return pointString;
        }
    }
}
