using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DrawShape
{
    /// <summary>
    /// 計算資料點的位置
    /// </summary>
    internal static class CalcShapePoint
    {
        /// <summary>
        /// 水平線 (頭尾不算點)
        /// </summary>
        /// <param name="lastPoint">前一個位置的點</param>
        /// <param name="count">需要的點的數量</param>
        /// <param name="length">長度</param>
        /// <param name="clocks">+1 往坐標系軸正方向，-1 往坐標系負方向</param>
        /// <param name="points">out 各個點的值</param>
        /// <returns>回傳最後一點的座標</returns>
        public static Point HorizontalPoint(Point lastPoint, int count, double length, int clocks, out List<Point> points)
        {
            points = new List<Point>();

            var space = length / (count + 1);

            for (int i = 0; i < count; i++)
            {
                points.Add(new Point
                {
                    X = lastPoint.X + clocks * space * (i + 1),
                    Y = lastPoint.Y
                });
            }

            return points[points.Count - 1];
        }

        /// <summary>
        /// 垂直線 (頭尾不算點)
        /// </summary>
        /// <param name="lastPoint">前一個位置的點</param>
        /// <param name="count">需要的點的數量</param>
        /// <param name="length">長度</param>
        /// <param name="clocks">+1 往坐標系軸正方向，-1 往坐標系負方向</param>
        /// <param name="points">out 各個點的值</param>
        /// <returns>回傳最後一點的座標</returns>
        public static Point VerticalPoint(Point lastPoint, int count, double length, int clocks, out List<Point> points)
        {
            points = new List<Point>();

            var space = length / (count + 1);

            for (int i = 0; i < count; i++)
            {
                points.Add(new Point
                {
                    X = lastPoint.X,
                    Y = lastPoint.Y + clocks * space * (i + 1)
                });
            }

            return points[points.Count - 1];
        }

        /// <summary>
        /// 圓形
        /// </summary>
        /// <param name="lastPoint">前一個位置的點</param>
        /// <param name="count">點的數量</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="radius">半徑</param>
        /// <param name="points">out 各個點的值</param>
        /// <returns>回傳最後一點的座標</returns>
        public static Point CirclePoint(Point lastPoint, int count, double startAngle, double radius, out List<Point> points)
        {
            points = new List<Point>();

            // 每一度
            double perDegree = 360 / count;

            double x, y;

            for (int i = 0; i < count; i++)
            {
                double degree = i * perDegree + startAngle;
                x = lastPoint.X + radius * Math.Cos(degree * Math.PI / 180);
                y = lastPoint.Y + radius * Math.Sin(degree * Math.PI / 180);

                points.Add(new Point { X = x, Y = y });
            }

            return points[points.Count - 1];
        }

        /// <summary>
        /// 弧形 (包含頭尾)
        /// </summary>
        /// <param name="lastPoint">前一個位置的點</param>
        /// <param name="count">點的數量</param>
        /// <param name="angle">總角度</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="radius">半徑</param>
        /// <param name="points">out 各個點的值</param>
        /// <returns>回傳最後一點的座標</returns>
        public static Point ArcPoint(Point lastPoint, int count, double angle, double startAngle, double radius, out List<Point> points)
        {
            points = new List<Point>();

            // 每一度
            double perDegree = angle / (count - 1);

            double x, y;

            for (int i = 0; i < count - 1; i++)
            {
                double degree = i * perDegree + startAngle;
                x = lastPoint.X + radius * Math.Cos(degree * Math.PI / 180);
                y = lastPoint.Y + radius * Math.Sin(degree * Math.PI / 180);

                points.Add(new Point { X = x, Y = y });
            }

            x = lastPoint.X + radius * Math.Cos((startAngle + angle) * Math.PI / 180);
            y = lastPoint.Y + radius * Math.Sin((startAngle + angle) * Math.PI / 180);
            points.Add(new Point { X = x, Y = y });

            return points[points.Count - 1];
        }
    }
}
