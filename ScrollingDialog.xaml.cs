using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ScrollableDialog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ScrollingDialog : Window
    {
        public new HorizontalAlignment HorizontalContentAlignment
        {
            get { return txtMessage.HorizontalContentAlignment; }
            set { txtMessage.HorizontalContentAlignment = value; }
        }

        public new VerticalAlignment VerticalContentAlignment
        {
            get { return txtMessage.VerticalContentAlignment; }
            set { txtMessage.VerticalContentAlignment = value; }
        }

        public ScrollingDialog()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        /// <summary>
        /// how a scrollable dialog with the provided message and an OK button.
        /// </summary>
        /// <param name="message"></param>
        public void Show(string message)
        {
            txtMessage.Text = message;
            this.Title = "";

            ChooseButtons(MessageBoxButton.OK);
            this.ShowDialog();
        }

        /// <summary>
        /// Show a scrollable dialog with the provided message, caption, and an OK button.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        public void Show(string message, string caption)
        {
            txtMessage.Text = message;
            this.Title = caption;

            ChooseButtons(MessageBoxButton.OK);
            this.ShowDialog();
        }

        /// <summary>
        /// Show a scrollable dialog with the provided message, caption, and user-defined buttons.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        /// <param name="buttonType">Limited to 'OK' 'OKCancel' and 'YesNo'. Other values will default to OK</param>
        public void Show(string message, string caption, MessageBoxButton buttonType)
        {
            txtMessage.Text = message;
            this.Title = caption;
            ChooseButtons(buttonType);
            this.ShowDialog();
        }

        /// <summary>
        /// Show a scrollable dialog with the provided message, caption, user-defined buttons, and a user-defined icon.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        /// <param name="buttonType">Limited to 'OK' 'OKCancel' and 'YesNo'. Other values will default to OK</param>
        /// <param name="imageType">Limited to 'Error' 'Warning' 'Information' and 'Question'. Other values will default to no image</param>
        public void Show(string message, string caption, MessageBoxButton buttonType, MessageBoxImage imageType)
        {
            txtMessage.Text = message;
            this.Title = caption;
            //Move the textbox to the right to accomodate the image.
            txtMessage.Margin = new Thickness(50, 0, 0, 0);
            ChooseButtons(buttonType);
            ChooseImage(imageType);
            this.ShowDialog();
        }

        private void ChooseButtons(MessageBoxButton buttonType)
        {
            switch (buttonType)
            {
                case MessageBoxButton.OK:
                    AddOkButton();
                    break;

                case MessageBoxButton.YesNo:
                    AddYesNoButton();
                    break;

                case MessageBoxButton.OKCancel:
                    AddOKCancelButton();
                    break;

                default:
                    AddOkButton();
                    break;
            }
        }

        private void ChooseImage(MessageBoxImage imageType)
        {
            switch (imageType)
            {
                case MessageBoxImage.Error:
                    AddErrorImage();
                    break;

                case MessageBoxImage.Information:
                    AddInformationImage();
                    break;

                case MessageBoxImage.Warning:
                    AddWarningImage();
                    break;

                case MessageBoxImage.Question:
                    AddQuestionImage();
                    break;

                default:
                    break;
            }
        }

        private void AddOkButton()
        {
            var btnOk = new Button();
            btnOk.Content = "OK";
            btnOk.Width = 75;
            btnOk.IsDefault = true;

            Grid.SetColumn(btnOk, 1);
            Grid.SetRow(btnOk, 3);
            Grid.SetColumnSpan(btnOk, 6);
            btnOk.Width = 75;
            btnOk.HorizontalAlignment = HorizontalAlignment.Center;

            btnOk.Click += new RoutedEventHandler(OK_Click);

            gridMain.Children.Add(btnOk);
        }

        private void AddOKCancelButton()
        {
            var btnOk = new Button();
            btnOk.Content = "OK";
            btnOk.Width = 75;

            var btnCancel = new Button();
            btnCancel.Content = "Cancel";
            btnCancel.Width = 75;

            Grid.SetColumn(btnOk, 4);
            Grid.SetRow(btnOk, 3);
            btnOk.Click += new RoutedEventHandler(YES_Click);

            Grid.SetColumn(btnCancel, 6);
            Grid.SetRow(btnCancel, 3);
            btnCancel.Click += new RoutedEventHandler(NO_Click);

            gridMain.Children.Add(btnOk);
            gridMain.Children.Add(btnCancel);
        }

        private void AddYesNoButton()
        {
            var btnYes = new Button();
            btnYes.Content = "Yes";
            btnYes.Width = 75;

            var btnNo = new Button();
            btnNo.Content = "No";
            btnNo.Width = 75;

            Grid.SetColumn(btnYes, 4);
            Grid.SetRow(btnYes, 3);
            btnYes.Click += new RoutedEventHandler(YES_Click);

            Grid.SetColumn(btnNo, 6);
            Grid.SetRow(btnNo, 3);
            btnNo.Click += new RoutedEventHandler(NO_Click);

            gridMain.Children.Add(btnYes);
            gridMain.Children.Add(btnNo);
        }

        private void AddErrorImage()
        {
            var image = new System.Windows.Controls.Image();
            image.Source = Imaging.CreateBitmapSourceFromHIcon(
                                SystemIcons.Error.Handle,
                                Int32Rect.Empty,
                                BitmapSizeOptions.FromEmptyOptions());

            Grid.SetColumn(image, 1);
            Grid.SetRow(image, 1);
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.VerticalAlignment = VerticalAlignment.Top;
            gridMain.Children.Add(image);
        }

        private void AddInformationImage()
        {
            var image = new System.Windows.Controls.Image();
            image.Source = Imaging.CreateBitmapSourceFromHIcon(
                                SystemIcons.Information.Handle,
                                Int32Rect.Empty,
                                BitmapSizeOptions.FromEmptyOptions());

            Grid.SetColumn(image, 1);
            Grid.SetRow(image, 1);
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.VerticalAlignment = VerticalAlignment.Top;
            gridMain.Children.Add(image);
        }

        private void AddWarningImage()
        {
            var image = new System.Windows.Controls.Image();
            image.Source = Imaging.CreateBitmapSourceFromHIcon(
                                SystemIcons.Warning.Handle,
                                Int32Rect.Empty,
                                BitmapSizeOptions.FromEmptyOptions());

            Grid.SetColumn(image, 1);
            Grid.SetRow(image, 1);
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.VerticalAlignment = VerticalAlignment.Top;
            gridMain.Children.Add(image);
        }

        private void AddQuestionImage()
        {
            var image = new System.Windows.Controls.Image();
            image.Source = Imaging.CreateBitmapSourceFromHIcon(
                                SystemIcons.Question.Handle,
                                Int32Rect.Empty,
                                BitmapSizeOptions.FromEmptyOptions());

            Grid.SetColumn(image, 1);
            Grid.SetRow(image, 1);
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.VerticalAlignment = VerticalAlignment.Top;
            gridMain.Children.Add(image);
        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void YES_Click(object sender, EventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void NO_Click(object sender, EventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}