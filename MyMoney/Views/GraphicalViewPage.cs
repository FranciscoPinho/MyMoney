using System.Collections.Generic;
using OxyPlot;
using Xamarin.Forms;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;
using System;
using System.Diagnostics;

namespace MyMoney
{
    public class GraphicalViewPage : ContentPage
    {
        public GraphicalViewPage(List<Tuple<double, string>> values)
        {
            PlotModel modelP1 = new PlotModel { Title = "Conversion to " + MyMoney.MainPage.conversion_target };
            int count = 0;
            dynamic seriesP1 = new PieSeries { StrokeThickness = 2.0, InsideLabelPosition = 0.5, AngleSpan = 360, StartAngle = 0 };
            if (values != null)
                foreach (Tuple<double,string> val in values)
                {
                    if(count==0)
                    seriesP1.Slices.Add(new PieSlice(val.Item2.Substring(0,3)+":" +val.Item1+MyMoney.MainPage.conversion_target.Substring(0, 3), val.Item1) { IsExploded = false, Fill = OxyColors.PaleVioletRed });
                    else seriesP1.Slices.Add(new PieSlice(val.Item2.Substring(0, 3) + ":" + val.Item1 + MyMoney.MainPage.conversion_target.Substring(0, 3), val.Item1) { IsExploded = false});
                    count++;
                }

            modelP1.Series.Add(seriesP1);
            var opv = new PlotView
            {
                Model = modelP1,
                WidthRequest = 450,
                HeightRequest = 300,
                BackgroundColor = Color.LightCyan,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            Content = opv;
        }
       
    }
}