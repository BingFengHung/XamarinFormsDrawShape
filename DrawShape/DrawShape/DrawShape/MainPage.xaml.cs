using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DrawShape
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCanvaViewPaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;

            SKCanvas canvas = surface.Canvas;


            // 由於是使用笛卡爾座標，因此畫面原點原本在左上角，因次要有一個轉換的函式
            Point RelateToOriginalPoint(Point point)
            {
                // 設定中心點
                int x = info.Width / 2;
                int y = info.Height / 2;

                return new Point(point.X + x, -point.Y + y);
            }
        }

        public SKPaint StrokeColor = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Blue,
            StrokeWidth = 4
        };

        public SKPaint TextColor = new SKPaint
        {
            Color = SKColors.Black,
            TextAlign = SKTextAlign.Center,
            TextSize = 40
        };
    }
}
