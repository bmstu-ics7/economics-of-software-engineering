using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Lab7.Exceptions;

namespace Lab7
{
    /// <summary>
    /// Логика взаимодействия для ChooseLanguagesWindow.xaml
    /// </summary>
    public partial class ChooseLanguagesWindow : Window
    {
        public List<Pair<int, int>> Languages;

        private Dictionary<string, int> size;
        private Dictionary<string, TextBox> txt;

        public ChooseLanguagesWindow(List<Pair<int, int>> languages)
        {
            Languages = languages;

            size = new Dictionary<string, int>
            {
                { "Asm", 320 },
                { "C", 128 },
                { "Cobol", 106 },
                { "Pascal", 90 },
                { "Cpp", 53 },
                { "Ada", 49 },
                { "VBasic", 24 },
                { "VCpp", 34 },
                { "DPascal", 29 },
                { "Perl", 21 },
                { "Prolog", 54 },
                { "SQL", 125 },
            };

            InitializeComponent();

            txt = new Dictionary<string, TextBox>
            {
                { "Asm", TextBox_Asm },
                { "C", TextBox_C },
                { "Cobol", TextBox_Cobol },
                { "Pascal", TextBox_Pascal },
                { "Cpp", TextBox_Cpp },
                { "Ada", TextBox_Ada },
                { "VBasic", TextBox_VBasic },
                { "VCpp", TextBox_VCpp },
                { "DPascal", TextBox_DPascal },
                { "Perl", TextBox_Perl },
                { "Prolog", TextBox_Prolog },
                { "SQL", TextBox_SQL },
            };

            foreach (var lang in languages)
            {
                foreach (var s in size)
                {
                    if (lang.Value == s.Value)
                    {
                        txt[s.Key].Text = Convert.ToString(lang.Key);
                    }
                }
            }
        }

        private void Button_FinishSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int sum = 0;
                this.Languages = new List<Pair<int, int>>();
                foreach (var t in this.txt)
                {
                    int per = ConvertPercent(t.Value);
                    sum += per;

                    if (per > 0)
                    {
                        this.Languages.Add(new Pair<int, int>(per, size[t.Key]));
                    }
                }

                if (sum > 100)
                {
                    throw new FPPersentMore100Exception();
                }

                this.DialogResult = true;
                this.Close();
            }
            catch (FPTextBlockParseException)
            {
                MessageBox.Show("Должно быть введено число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FPTextBlockValueException)
            {
                MessageBox.Show("Значения должны быть в диапазоне от 0 до 100", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FPPersentMore100Exception)
            {
                MessageBox.Show("Сумма всех значений больше 100", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int ConvertPercent(TextBox text)
        {
            if (!int.TryParse(text.Text, out int result))
            {
                throw new FPTextBlockParseException();
            }

            if (result < 0 || result > 100)
            {
                throw new FPTextBlockValueException();
            }

            return result;
        }
    }
}
