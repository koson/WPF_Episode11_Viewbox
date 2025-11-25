using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF_Episode11_Viewbox
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowBasicViewbox(object sender, RoutedEventArgs e)
        {
            var mainStack = CreateMainStack("Demo 1: Basic Viewbox");

            AddDescription(mainStack, "Without Viewbox, content may overflow or be too small. With Viewbox, content automatically scales to fit:");

            var grid = new Grid { Margin = new Thickness(0, 20, 0, 0) };
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            // Without Viewbox
            var leftStack = new StackPanel { Margin = new Thickness(10) };
            leftStack.Children.Add(new TextBlock { Text = "Without Viewbox:", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 10) });

            var smallBorder = new Border
            {
                Width = 150,
                Height = 80,
                BorderBrush = Brushes.Red,
                BorderThickness = new Thickness(2),
                Margin = new Thickness(0, 0, 0, 10),
                ClipToBounds = true
            };
            smallBorder.Child = new TextBlock { Text = "LARGE TEXT", FontSize = 48, FontWeight = FontWeights.Bold };
            leftStack.Children.Add(smallBorder);

            leftStack.Children.Add(new TextBlock { Text = "Problem: Text overflows!", Foreground = Brushes.Red, FontStyle = FontStyles.Italic });
            Grid.SetColumn(leftStack, 0);
            grid.Children.Add(leftStack);

            // With Viewbox
            var rightStack = new StackPanel { Margin = new Thickness(10) };
            rightStack.Children.Add(new TextBlock { Text = "With Viewbox:", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 10) });

            var viewboxBorder = new Border
            {
                Width = 150,
                Height = 80,
                BorderBrush = Brushes.Green,
                BorderThickness = new Thickness(2),
                Margin = new Thickness(0, 0, 0, 10)
            };
            var viewbox = new Viewbox();
            viewbox.Child = new TextBlock { Text = "LARGE TEXT", FontSize = 48, FontWeight = FontWeights.Bold };
            viewboxBorder.Child = viewbox;
            rightStack.Children.Add(viewboxBorder);

            rightStack.Children.Add(new TextBlock { Text = "Solution: Perfect fit!", Foreground = Brushes.Green, FontStyle = FontStyles.Italic });
            Grid.SetColumn(rightStack, 1);
            grid.Children.Add(rightStack);

            mainStack.Children.Add(grid);

            // Different sizes demo
            AddDescription(mainStack, "\nSame content, different container sizes:");

            var sizesGrid = new UniformGrid { Rows = 1, Columns = 3, Margin = new Thickness(0, 20, 0, 0) };

            for (int i = 0; i < 3; i++)
            {
                double size = 80 + (i * 60);
                var border = new Border
                {
                    Width = size,
                    Height = size / 2,
                    BorderBrush = Brushes.Blue,
                    BorderThickness = new Thickness(2),
                    Margin = new Thickness(5)
                };
                var vb = new Viewbox();
                vb.Child = new TextBlock { Text = "HELLO", FontSize = 50, FontWeight = FontWeights.Bold };
                border.Child = vb;

                var container = new StackPanel { Margin = new Thickness(5) };
                container.Children.Add(border);
                container.Children.Add(new TextBlock { Text = $"{size}x{size / 2}", TextAlignment = TextAlignment.Center, FontSize = 10 });
                sizesGrid.Children.Add(container);
            }

            mainStack.Children.Add(sizesGrid);
            UpdateContent(mainStack);
        }

        private void ShowStretchModes(object sender, RoutedEventArgs e)
        {
            var mainStack = CreateMainStack("Demo 2: Stretch Modes");

            AddDescription(mainStack, "Viewbox supports 4 stretch modes that control how content scales:");

            var grid = new UniformGrid { Rows = 2, Columns = 2, Margin = new Thickness(0, 20, 0, 0) };

            // Uniform (default)
            var uniformContainer = CreateStretchDemo("Uniform (Default)", Stretch.Uniform,
                "Maintains aspect ratio\nFits within container\nMay have empty space");
            grid.Children.Add(uniformContainer);

            // Fill
            var fillContainer = CreateStretchDemo("Fill", Stretch.Fill,
                "Ignores aspect ratio\nFills entire container\nMay distort content");
            grid.Children.Add(fillContainer);

            // UniformToFill
            var uniformToFillContainer = CreateStretchDemo("UniformToFill", Stretch.UniformToFill,
                "Maintains aspect ratio\nFills container\nMay clip content");
            grid.Children.Add(uniformToFillContainer);

            // None
            var noneContainer = CreateStretchDemo("None", Stretch.None,
                "No scaling\nOriginal size\nMay overflow/underflow");
            grid.Children.Add(noneContainer);

            mainStack.Children.Add(grid);
            UpdateContent(mainStack);
        }

        private void ShowScalableIcons(object sender, RoutedEventArgs e)
        {
            var mainStack = CreateMainStack("Demo 3: Scalable Icons");

            AddDescription(mainStack, "Use Viewbox to create icons that work at any size:");

            var grid = new UniformGrid { Rows = 2, Columns = 4, Margin = new Thickness(0, 20, 0, 0) };

            var icons = new[] { "🏠", "📁", "⚙️", "🔍", "💾", "📧", "📊", "🎨" };
            var sizes = new[] { 40.0, 60.0, 80.0, 100.0 };

            foreach (var icon in icons)
            {
                var border = new Border
                {
                    Width = sizes[icons.ToList().IndexOf(icon) % 4],
                    Height = sizes[icons.ToList().IndexOf(icon) % 4],
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(5),
                    Background = Brushes.LightGray
                };
                var viewbox = new Viewbox { Margin = new Thickness(5) };
                viewbox.Child = new TextBlock { Text = icon, FontSize = 48 };
                border.Child = viewbox;
                grid.Children.Add(border);
            }

            mainStack.Children.Add(grid);
            UpdateContent(mainStack);
        }

        private void ShowResponsiveLogo(object sender, RoutedEventArgs e)
        {
            var mainStack = CreateMainStack("Demo 4: Responsive Logo");

            AddDescription(mainStack, "Logo that scales with its container:");

            var outerGrid = new Grid { Margin = new Thickness(0, 20, 0, 0) };
            outerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
            outerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(120) });
            outerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(160) });

            var heights = new[] { 80.0, 120.0, 160.0 };

            for (int i = 0; i < 3; i++)
            {
                var border = new Border
                {
                    Background = Brushes.Navy,
                    Padding = new Thickness(10),
                    Margin = new Thickness(0, 5, 0, 5)
                };
                Grid.SetRow(border, i);

                var viewbox = new Viewbox { HorizontalAlignment = HorizontalAlignment.Left };
                var logoStack = new StackPanel { Orientation = Orientation.Horizontal };
                logoStack.Children.Add(new TextBlock { Text = "🚀", FontSize = 48, Foreground = Brushes.White, VerticalAlignment = VerticalAlignment.Center });
                logoStack.Children.Add(new TextBlock { Text = " MyApp", FontSize = 48, FontWeight = FontWeights.Bold, Foreground = Brushes.White, VerticalAlignment = VerticalAlignment.Center });
                viewbox.Child = logoStack;
                border.Child = viewbox;

                outerGrid.Children.Add(border);
            }

            mainStack.Children.Add(outerGrid);
            UpdateContent(mainStack);
        }

        private void ShowDashboardCards(object sender, RoutedEventArgs e)
        {
            var mainStack = CreateMainStack("Demo 5: Dashboard Metric Cards");

            AddDescription(mainStack, "Dashboard cards that scale perfectly:");

            var grid = new UniformGrid { Rows = 1, Columns = 3, Margin = new Thickness(0, 20, 0, 0) };

            var cards = new[]
            {
                new { Title = "Sales", Value = "$12,345", Change = "+12%", Bg = "#D4EDDA", Border = "#C3E6CB", IsPositive = true },
                new { Title = "Users", Value = "1,234", Change = "+5%", Bg = "#D1ECF1", Border = "#BEE5EB", IsPositive = true },
                new { Title = "Orders", Value = "567", Change = "-3%", Bg = "#F8D7DA", Border = "#F5C6CB", IsPositive = false }
            };

            foreach (var card in cards)
            {
                var border = new Border
                {
                    Background = (Brush)new BrushConverter().ConvertFrom(card.Bg),
                    BorderBrush = (Brush)new BrushConverter().ConvertFrom(card.Border),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(10),
                    Padding = new Thickness(20)
                };

                var viewbox = new Viewbox();
                var cardStack = new StackPanel { Width = 200 };
                cardStack.Children.Add(new TextBlock { Text = card.Title, FontSize = 16 });
                cardStack.Children.Add(new TextBlock { Text = card.Value, FontSize = 48, FontWeight = FontWeights.Bold });
                cardStack.Children.Add(new TextBlock { Text = card.Change, FontSize = 14, Foreground = card.IsPositive ? Brushes.Green : Brushes.Red });
                viewbox.Child = cardStack;
                border.Child = viewbox;

                grid.Children.Add(border);
            }

            mainStack.Children.Add(grid);
            UpdateContent(mainStack);
        }

        private void ShowIconButtons(object sender, RoutedEventArgs e)
        {
            var mainStack = CreateMainStack("Demo 6: Scalable Icon Buttons");

            AddDescription(mainStack, "Buttons with perfectly sized icons:");

            var grid = new UniformGrid { Rows = 2, Columns = 4, Margin = new Thickness(0, 20, 0, 0) };

            var buttons = new[]
            {
                new { Icon = "📁", Label = "Open" },
                new { Icon = "💾", Label = "Save" },
                new { Icon = "✂️", Label = "Cut" },
                new { Icon = "📋", Label = "Copy" },
                new { Icon = "📄", Label = "Paste" },
                new { Icon = "🗑️", Label = "Delete" },
                new { Icon = "🔍", Label = "Search" },
                new { Icon = "⚙️", Label = "Settings" }
            };

            foreach (var btn in buttons)
            {
                var button = new Button { Margin = new Thickness(5), Padding = new Thickness(10) };

                var viewbox = new Viewbox();
                var btnStack = new StackPanel { Width = 80 };
                btnStack.Children.Add(new TextBlock { Text = btn.Icon, FontSize = 36, TextAlignment = TextAlignment.Center });
                btnStack.Children.Add(new TextBlock { Text = btn.Label, FontSize = 12, TextAlignment = TextAlignment.Center });
                viewbox.Child = btnStack;
                button.Content = viewbox;

                grid.Children.Add(button);
            }

            mainStack.Children.Add(grid);
            UpdateContent(mainStack);
        }

        private void ShowStretchDirection(object sender, RoutedEventArgs e)
        {
            var mainStack = CreateMainStack("Demo 7: StretchDirection");

            AddDescription(mainStack, "Control which direction scaling is allowed:");

            var grid = new UniformGrid { Rows = 1, Columns = 3, Margin = new Thickness(0, 20, 0, 0) };

            // UpOnly - only grow
            var upOnlyContainer = new StackPanel { Margin = new Thickness(10) };
            upOnlyContainer.Children.Add(new TextBlock { Text = "UpOnly (Only Grow)", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 10) });
            var upOnlyBorder = new Border
            {
                Width = 150,
                Height = 150,
                BorderBrush = Brushes.Blue,
                BorderThickness = new Thickness(2)
            };
            var upOnlyViewbox = new Viewbox { StretchDirection = StretchDirection.UpOnly };
            upOnlyViewbox.Child = new TextBlock { Text = "Small", FontSize = 12 };
            upOnlyBorder.Child = upOnlyViewbox;
            upOnlyContainer.Children.Add(upOnlyBorder);
            upOnlyContainer.Children.Add(new TextBlock { Text = "Small text grows", FontSize = 10, TextAlignment = TextAlignment.Center });
            grid.Children.Add(upOnlyContainer);

            // DownOnly - only shrink
            var downOnlyContainer = new StackPanel { Margin = new Thickness(10) };
            downOnlyContainer.Children.Add(new TextBlock { Text = "DownOnly (Only Shrink)", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 10) });
            var downOnlyBorder = new Border
            {
                Width = 150,
                Height = 150,
                BorderBrush = Brushes.Red,
                BorderThickness = new Thickness(2)
            };
            var downOnlyViewbox = new Viewbox { StretchDirection = StretchDirection.DownOnly };
            downOnlyViewbox.Child = new TextBlock { Text = "LARGE", FontSize = 100 };
            downOnlyBorder.Child = downOnlyViewbox;
            downOnlyContainer.Children.Add(downOnlyBorder);
            downOnlyContainer.Children.Add(new TextBlock { Text = "Large text shrinks", FontSize = 10, TextAlignment = TextAlignment.Center });
            grid.Children.Add(downOnlyContainer);

            // Both - default
            var bothContainer = new StackPanel { Margin = new Thickness(10) };
            bothContainer.Children.Add(new TextBlock { Text = "Both (Default)", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 10) });
            var bothBorder = new Border
            {
                Width = 150,
                Height = 150,
                BorderBrush = Brushes.Green,
                BorderThickness = new Thickness(2)
            };
            var bothViewbox = new Viewbox { StretchDirection = StretchDirection.Both };
            bothViewbox.Child = new TextBlock { Text = "HELLO", FontSize = 50, FontWeight = FontWeights.Bold };
            bothBorder.Child = bothViewbox;
            bothContainer.Children.Add(bothBorder);
            bothContainer.Children.Add(new TextBlock { Text = "Can grow or shrink", FontSize = 10, TextAlignment = TextAlignment.Center });
            grid.Children.Add(bothContainer);

            mainStack.Children.Add(grid);
            UpdateContent(mainStack);
        }

        private void ShowComplexContent(object sender, RoutedEventArgs e)
        {
            var mainStack = CreateMainStack("Demo 8: Complex Content Scaling");

            AddDescription(mainStack, "Viewbox can scale entire layouts:");

            var grid = new Grid { Margin = new Thickness(0, 20, 0, 0) };
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            // Small version
            var smallBorder = new Border
            {
                Width = 250,
                Height = 180,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(10)
            };
            Grid.SetColumn(smallBorder, 0);

            var smallViewbox = new Viewbox();
            smallViewbox.Child = CreateProductCard();
            smallBorder.Child = smallViewbox;
            grid.Children.Add(smallBorder);

            // Large version
            var largeBorder = new Border
            {
                Width = 350,
                Height = 250,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(10)
            };
            Grid.SetColumn(largeBorder, 1);

            var largeViewbox = new Viewbox();
            largeViewbox.Child = CreateProductCard();
            largeBorder.Child = largeViewbox;
            grid.Children.Add(largeBorder);

            mainStack.Children.Add(grid);
            UpdateContent(mainStack);
        }

        private void ShowAspectRatios(object sender, RoutedEventArgs e)
        {
            var mainStack = CreateMainStack("Demo 9: Maintaining Aspect Ratios");

            AddDescription(mainStack, "Use Viewbox to maintain specific aspect ratios:");

            var grid = new UniformGrid { Rows = 2, Columns = 2, Margin = new Thickness(0, 20, 0, 0) };

            // 16:9 aspect ratio
            var ratio169Border = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(10)
            };
            var ratio169Viewbox = new Viewbox();
            var ratio169Grid = new Grid { Width = 1600, Height = 900, Background = Brushes.Black };
            ratio169Grid.Children.Add(new TextBlock { Text = "16:9\nWidescreen", FontSize = 72, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, TextAlignment = TextAlignment.Center });
            ratio169Viewbox.Child = ratio169Grid;
            ratio169Border.Child = ratio169Viewbox;
            grid.Children.Add(ratio169Border);

            // 4:3 aspect ratio
            var ratio43Border = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(10)
            };
            var ratio43Viewbox = new Viewbox();
            var ratio43Grid = new Grid { Width = 800, Height = 600, Background = Brushes.Navy };
            ratio43Grid.Children.Add(new TextBlock { Text = "4:3\nStandard", FontSize = 72, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, TextAlignment = TextAlignment.Center });
            ratio43Viewbox.Child = ratio43Grid;
            ratio43Border.Child = ratio43Viewbox;
            grid.Children.Add(ratio43Border);

            // 1:1 aspect ratio (square)
            var ratio11Border = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(10)
            };
            var ratio11Viewbox = new Viewbox();
            var ratio11Grid = new Grid { Width = 500, Height = 500, Background = Brushes.Green };
            ratio11Grid.Children.Add(new TextBlock { Text = "1:1\nSquare", FontSize = 48, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, TextAlignment = TextAlignment.Center, FontWeight = FontWeights.Bold });
            ratio11Viewbox.Child = ratio11Grid;
            ratio11Border.Child = ratio11Viewbox;
            grid.Children.Add(ratio11Border);

            // 21:9 ultra-wide
            var ratio219Border = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(10)
            };
            var ratio219Viewbox = new Viewbox();
            var ratio219Grid = new Grid { Width = 2100, Height = 900, Background = Brushes.Purple };
            ratio219Grid.Children.Add(new TextBlock { Text = "21:9 Ultra-Wide", FontSize = 72, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, TextAlignment = TextAlignment.Center });
            ratio219Viewbox.Child = ratio219Grid;
            ratio219Border.Child = ratio219Viewbox;
            grid.Children.Add(ratio219Border);

            mainStack.Children.Add(grid);
            UpdateContent(mainStack);
        }

        // Helper methods
        private StackPanel CreateMainStack(string title)
        {
            var stack = new StackPanel();
            stack.Children.Add(new TextBlock
            {
                Text = title,
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 10)
            });
            return stack;
        }

        private void AddDescription(StackPanel parent, string text)
        {
            parent.Children.Add(new TextBlock
            {
                Text = text,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 10)
            });
        }

        private StackPanel CreateStretchDemo(string title, Stretch stretch, string description)
        {
            var container = new StackPanel { Margin = new Thickness(10) };

            container.Children.Add(new TextBlock
            {
                Text = title,
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 10)
            });

            var border = new Border
            {
                Width = 200,
                Height = 120,
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(2),
                Margin = new Thickness(0, 0, 0, 10)
            };

            var viewbox = new Viewbox { Stretch = stretch };
            var ellipse = new Ellipse
            {
                Width = 100,
                Height = 50,
                Fill = Brushes.Blue
            };
            viewbox.Child = ellipse;
            border.Child = viewbox;

            container.Children.Add(border);
            container.Children.Add(new TextBlock
            {
                Text = description,
                FontSize = 11,
                Foreground = Brushes.Gray,
                TextWrapping = TextWrapping.Wrap
            });

            return container;
        }

        private Border CreateProductCard()
        {
            var card = new Border
            {
                Width = 400,
                Height = 300,
                Background = Brushes.White,
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(20)
            };

            var stack = new StackPanel();
            stack.Children.Add(new TextBlock { Text = "Product Card", FontSize = 24, FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 10) });

            var imageBox = new Border
            {
                Background = Brushes.LightGray,
                Height = 150,
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(0, 0, 0, 10)
            };
            imageBox.Child = new TextBlock { Text = "[Product Image]", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            stack.Children.Add(imageBox);

            stack.Children.Add(new TextBlock { Text = "Product Name", FontSize = 18, FontWeight = FontWeights.Bold });
            stack.Children.Add(new TextBlock { Text = "$299.99", FontSize = 28, Foreground = Brushes.Green, FontWeight = FontWeights.Bold });
            stack.Children.Add(new TextBlock { Text = "In Stock", Foreground = Brushes.Green });

            card.Child = stack;
            return card;
        }

        private void UpdateContent(StackPanel content)
        {
            var container = new Border
            {
                Background = Brushes.White,
                Margin = new Thickness(20),
                Padding = new Thickness(30),
                CornerRadius = new CornerRadius(10)
            };
            container.Child = content;
            ContentPanel.Content = container;
        }
    }
}
