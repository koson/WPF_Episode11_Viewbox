using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WpfDemoTemplate
{
    /// <summary>
    /// Partial class for handling screen position
    /// Automatically positions window on preferred screen for tutorial recording
    /// </summary>
    public partial class MainWindow : Window
    {
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var enableSecondScreen = configuration.GetValue<bool>("DisplaySettings:EnableSecondScreenInDebug", false);
            var preferredScreen = configuration.GetValue<int>("DisplaySettings:PreferredScreen", 1);
            var horizontalAlignment = configuration.GetValue<string>("DisplaySettings:HorizontalAlignment", "Center");
            var verticalAlignment = configuration.GetValue<string>("DisplaySettings:VerticalAlignment", "Center");

#if DEBUG
            if (enableSecondScreen && System.Windows.Forms.Screen.AllScreens.Length > 1)
            {
                // Move window to preferred screen in Debug mode
                var screens = System.Windows.Forms.Screen.AllScreens;
                if (screens.Length > preferredScreen)
                {
                    var targetScreen = screens[preferredScreen];
                    PositionWindowOnScreen(targetScreen, horizontalAlignment, verticalAlignment);
                }
            }
            else
            {
                // Center on primary screen
                CenterWindowOnPrimaryScreen();
            }
#else
            // In Release mode, always center on primary screen
            CenterWindowOnPrimaryScreen();
#endif
        }

        private void PositionWindowOnScreen(System.Windows.Forms.Screen screen, string hAlign, string vAlign)
        {
            this.WindowStartupLocation = WindowStartupLocation.Manual;

            // Calculate horizontal position
            double left = hAlign?.ToLower() switch
            {
                "left" => screen.WorkingArea.Left,
                "right" => screen.WorkingArea.Right - this.Width,
                _ => screen.WorkingArea.Left + (screen.WorkingArea.Width - this.Width) / 2 // Center (default)
            };

            // Calculate vertical position
            double top = vAlign?.ToLower() switch
            {
                "top" => screen.WorkingArea.Top,
                "bottom" => screen.WorkingArea.Bottom - this.Height,
                _ => screen.WorkingArea.Top + (screen.WorkingArea.Height - this.Height) / 2 // Center (default)
            };

            this.Left = left;
            this.Top = top;
        }

        private void CenterWindowOnPrimaryScreen()
        {
            // Use WPF's built-in centering (works in both Debug and Release)
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
