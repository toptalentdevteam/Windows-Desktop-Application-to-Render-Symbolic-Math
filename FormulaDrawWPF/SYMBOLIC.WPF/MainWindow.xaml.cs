using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Analytics;
using Exversion;
using Exversion.Analytics;

namespace TeXConverter.WPF
{
    /// <summary>
    /// ANALYTICS and Physics project.
    /// https://vk.com/analytics_and_physics
    /// </summary>
    public partial class MainWindow : Window
    {
        private Translator translator;
        private BaseConverter converter;

        public MainWindow()
		{
			InitializeComponent();
		}

		private void LoadExtensions(string[] exts)
	    {
			int n = exts.Length;

			for (int i = 0; i < n; i++)
			{
				string sname = exts[i];

				try
				{
					Assembly.Load(sname);
				}
				catch (Exception)
				{
				}
			}
	    }

		private void InitializeAnalytics()
		{
            string[] exts = new string[] { "Analytics.Real" };
			LoadExtensions(exts);

            converter = new AnalyticsTeXConverter();
            translator = new Translator();
		    translator.Add("A", 2.0);
		    translator.Add("B",-1.0);
		    translator.Add("C", 0.5);
            translator.Add("x", 1.0);
		    translator.Add("y", 2.0/3);
		    translator.Add("z",-3.0);
		    translator.Add("m", 2);
		    translator.Add("n", 5);
        }

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		    InitializeAnalytics();
        }

        private void ButtonEvaluate_Click(object sender, RoutedEventArgs e)
        {
            string text1 = inputTextBox1.Text;
            string text2 = inputTextBox2.Text;

            if(text1 == text2)
            {
                MessageBox.Show("Two expressions are equivalent.");
            }
            else
            {
                MessageBox.Show("Two expressions aren't equivalent.");
            }
        }

        // This function is called when the text of the text editor is changed.
        private void OnTextChange1(object sender, TextChangedEventArgs e)
        {
            string text = inputTextBox1.Text;

            if(text == null)
            {
                return;
            }

            try
            {
                if (translator.CheckSyntax(text))
                {
                    object v = translator.Calculate(text);

                    string texf = converter.Convert(text);
                    string vs = Utilities.SafeToString(v);

                    formulaControl1.Formula = texf;
                }
            }
            catch (Exception ex)
            {
                formulaControl1.Formula = ex.Message;
            }
        }

        private void OnTextChange2(object sender, TextChangedEventArgs e)
        {
            string text = inputTextBox2.Text;

            if (text == null)
            {
                return;
            }

            try
            {
                if (translator.CheckSyntax(text))
                {
                    object v = translator.Calculate(text);
                    string texf = converter.Convert(text);
                    string vs = Utilities.SafeToString(v);

                    formulaControl2.Formula = texf;
                }
            }
            catch (Exception ex)
            {
                formulaControl2.Formula = ex.Message;
            }
        }
    }

}
