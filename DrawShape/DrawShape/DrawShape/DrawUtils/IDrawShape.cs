using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DrawShape
{
    /// <summary>
    /// 取得畫圖點位的介面
    /// </summary>
    interface IDrawShape
    {
        /// <summary>
        /// 用來儲存笛卡爾座標
        /// </summary>
        List<Point> DrawPoints { get; set; }

        /// <summary>
        /// 取得資料點位
        /// </summary>
        void Draw();
    }


    public class RectangleShape : IDrawShape
    {
        public List<Point> DrawPoints { get; set; }

        public void Draw()
        {
            // 產生想要的圖形路徑
            GetShapePoint(180, 500, 500, out List<Point> temp);
            DrawPoints = temp;
        }

        // 這邊用來取得幾個圓弧的角的位置
        private List<Point> GetCornerPoint(double width, double height)
        {
            return new List<Point>
            {
                // 右上角
                new Point(width /2, height /2),

                // 左上角
                new Point(-width/2, height/2),

                // 左下角
                new Point(-width/2, -height/2),

                // 右下角
                new Point(width/2, -height/2)
            };
        }

        /// <summary>
        /// 要繪製的圖形在這邊決定
        /// </summary>
        /// <param name="radius">圖形圓角半徑</param>
        /// <param name="rectangeWidth">圖形寬度</param>
        /// <param name="rectangleHeight">圖形高度</param>
        /// <param name="dataPoint">欲返回的資料點</param>
        private void GetShapePoint(double radius, double rectangleWidth, double rectangleHeight, out List<Point> dataPoint)
        {
            dataPoint = new List<Point>();

            var points = GetCornerPoint(rectangleWidth, rectangleHeight);

            // 右上角
            var lastPoint = ArcPath(points[0], 5, 90, 0, radius, out var pointList);
            dataPoint.AddRange(pointList);

            // 水平線
            HorizontalPath(lastPoint, 5, rectangleWidth, -1, out pointList);
            dataPoint.AddRange(pointList);

            // 左上角
            lastPoint = ArcPath(points[1], 5, 90, 90, radius, out pointList);
            dataPoint.AddRange(pointList);

            // 垂直線
            VerticalPath(lastPoint, 5, rectangleHeight, -1, out pointList);
            dataPoint.AddRange(pointList);

            // 左下角
            lastPoint = ArcPath(points[2], 5, 90, 180, radius, out pointList);
            dataPoint.AddRange(pointList);

            // 水平線
            HorizontalPath(lastPoint, 5, rectangleWidth, 1, out pointList);
            dataPoint.AddRange(pointList);


            // 右下角
            lastPoint = ArcPath(points[3], 5, 90, 270, radius, out pointList);
            dataPoint.AddRange(pointList);

            // 垂直線
            VerticalPath(lastPoint, 5, rectangleWidth, 1, out pointList);
            dataPoint.AddRange(pointList);
        }

        /// <summary>
        /// 用來計算水平線上給定數量點的位置
        /// </summary>
        /// <param name="lastPoint"></param>
        /// <param name="count"></param>
        /// <param name="sideLength"></param>
        /// <param name="clocks"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        private Point HorizontalPath(Point lastPoint, int count, double sideLength, int clocks, out List<Point> points)
        {
            points = new List<Point>();
            var space = sideLength / (count + 1);

            for (int i = 0; i < count; i++)
            {
                points.Add(new Point
                {
                    X = lastPoint.X + (clocks) * space * (i + 1),
                    Y = lastPoint.Y
                });
            }

            return points[points.Count - 1];
        }

        /// <summary>
        /// 用來計算垂直線上給定數量點的位置
        /// </summary>
        /// <param name="lastPoint"></param>
        /// <param name="count"></param>
        /// <param name="sideLength"></param>
        /// <param name="clocks"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        private Point VerticalPath(Point lastPoint, int count, double sideLength, int clocks, out List<Point> points)
        {
            points = new List<Point>();
            var space = sideLength / (count + 1);

            for (int i = 0; i < count; i++)
            {
                points.Add(new Point
                {
                    X = lastPoint.X,
                    Y = lastPoint.Y + (clocks) * space * (i + 1)
                });
            }

            return points[points.Count - 1];
        }

        /// <summary>
        /// 用來計算圓弧上點位的位置
        /// </summary>
        /// <param name="lastPoint"></param>
        /// <param name="count"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="radius"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        private Point ArcPath(Point lastPoint, int count, double angle, double startAngle, double radius, out List<Point> points)
        {
            points = new List<Point>();

            // 每一度
            double perDegree = angle / (count - 1);

            double x, y;

            int lastPointX = lastPoint.X < 0 ? -1 : 1;
            int lastPointY = lastPoint.Y < 0 ? -1 : 1;

            for (int i = 0; i < count - 1; i++)
            {
                double degree = i * perDegree + startAngle;
                x = lastPoint.X + radius * Math.Cos(degree * Math.PI / 180);
                y = lastPoint.Y + radius * Math.Sin(degree * Math.PI / 180);

                lastPointX = lastPoint.X < 0 ? -1 : 1;
                lastPointY = lastPoint.Y < 0 ? -1 : 1;
                points.Add(new Point { X = x, Y = y });
            }

            x = lastPoint.X + radius * Math.Cos((startAngle + angle) * Math.PI / 180);
            y = lastPoint.Y + radius * Math.Sin((startAngle + angle) * Math.PI / 180);
            points.Add(new Point { X = x, Y = y });

            return points[points.Count - 1];
        }

        /// <summary>
        /// 用來計算圓形上點位的位置
        /// </summary>
        /// <param name="lastPoint"></param>
        /// <param name="count"></param>
        /// <param name="startAngle"></param>
        /// <param name="radius"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        private Point CirclePath(Point lastPoint, int count, double startAngle, double radius, out List<Point> points)
        {
            points = new List<Point>();

            // 每一度
            double perDegree = 360 / count;

            double x, y;

            int lastPointX = lastPoint.X < 0 ? -1 : 1;
            int lastPointY = lastPoint.Y < 0 ? -1 : 1;

            for (int i = 0; i < count; i++)
            {
                double degree = i * perDegree + startAngle;
                x = lastPoint.X + radius * Math.Cos(degree * Math.PI / 180);
                y = lastPoint.Y + radius * Math.Sin(degree * Math.PI / 180);

                lastPointX = lastPoint.X < 0 ? -1 : 1;
                lastPointY = lastPoint.Y < 0 ? -1 : 1;
                points.Add(new Point { X = x, Y = y });
            }

            return points[points.Count - 1];
        }
    }
}
