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
            string f = inputTextBox.Text;

            try
            {
                if (translator.CheckSyntax(f))
                {
                    object v = translator.Calculate(f);

                    string texf = converter.Convert(f);
                    string vs = Utilities.SafeToString(v);

                    formulaControl.Formula = texf + " = {" + vs + "}";
                }
            }
            catch (Exception ex)
            {
                formulaControl.Formula = ex.Message;
            }
            
        }

        // This function is called when the text of the text editor is changed.
        private void OnTextChange(object sender, TextChangedEventArgs e)
        {
            string f = inputTextBox.Text;

            if(f == null)
            {
                return;
            }

            try
            {
                if (translator.CheckSyntax(f))
                {
                    object v = translator.Calculate(f);

                    string texf = converter.Convert(f);
                    string vs = Utilities.SafeToString(v);

                    formulaControl.Formula = texf + " = {" + vs + "}";
                }
            }
            catch (Exception ex)
            {
                formulaControl.Formula = ex.Message;
            }
        }

        #region Example interface 



        #endregion Example interface				
    }

}
