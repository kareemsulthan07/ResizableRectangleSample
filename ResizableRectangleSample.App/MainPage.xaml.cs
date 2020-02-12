using ResizableRectangleSample.App.CustomControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ResizableRectangleSample.App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ResizableRectangle currentRect;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void mainCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                mainCanvas.CapturePointer(e.Pointer);
                var ptrPoint = e.GetCurrentPoint(mainCanvas);
                var point1 = ptrPoint.Position;

                mainCanvas.Children.OfType<ResizableRectangle>()
                    .ToList()
                    .ForEach(r => r.IsSelected = false);

                currentRect = new ResizableRectangle(mainCanvas, point1);
                mainCanvas.Children.Add(currentRect);

                e.Handled = true;
            }
            catch (Exception)
            {
            }
        }

        private void mainCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                var ptrPoint = e.GetCurrentPoint(mainCanvas);
                if (ptrPoint.Properties.IsLeftButtonPressed)
                {
                    if (currentRect != null)
                    {
                        var point2 = ptrPoint.Position;
                        currentRect.SetCoordinates(point2);
                    }
                }   
            }
            catch (Exception)
            {
            }
        }

        private void mainCanvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                mainCanvas.ReleasePointerCaptures();

                if (currentRect.IsZeroSize())
                {
                    mainCanvas.Children.Remove(currentRect);
                }

                currentRect = null;

                e.Handled = true;
            }
            catch (Exception)
            {
            }
        }
    }
}
