using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;
using Lab7.Exceptions;

namespace Lab7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int[] EILevel;
        private readonly int[] EOLevel;
        private readonly int[] EQLevel;
        private readonly int[] ILFLevel;
        private readonly int[] EIFLevel;

        private readonly double[] PRECValue;
        private readonly double[] FLEXValue;
        private readonly double[] RESLValue;
        private readonly double[] TEAMValue;
        private readonly double[] PMATValue;

        private readonly double?[] PERSLevel;
        private readonly double?[] RCPXLevel;
        private readonly double?[] RUSELevel;
        private readonly double?[] PDIFLevel;
        private readonly double?[] PREXLevel;
        private readonly double?[] FCILLevel;
        private readonly double?[] SCEDLevel;

        private readonly int[] ExpLevel;

        private double p = 0;
        private int loc = 0;
        private List<Pair<int, int>> languages;

        public MainWindow()
        {
            EILevel = new int[]  { 3, 4,  6  };
            EOLevel = new int[]  { 4, 5,  7  };
            EQLevel = new int[]  { 3, 4,  6  };
            ILFLevel = new int[] { 7, 10, 15 };
            EIFLevel = new int[] { 5, 7,  10 };

            PRECValue = new double[] { 6.20, 4.96, 3.72, 2.48, 1.24, 0.00 };
            FLEXValue = new double[] { 5.07, 4.05, 3.04, 2.03, 1.01, 0.00 };
            RESLValue = new double[] { 7.00, 5.65, 4.24, 2.83, 1.41, 0.00 };
            TEAMValue = new double[] { 5.48, 4.38, 3.29, 2.19, 1.10, 0.00 };
            PMATValue = new double[] { 7.00, 6.24, 4.68, 1.12, 1.56, 0.00 };

            PERSLevel = new double?[] { 1.62, 1.26, 1.00, 0.83, 0.63, 0.50 };
            RCPXLevel = new double?[] { 0.60, 0.83, 1.00, 1.33, 1.91, 2.72 };
            RUSELevel = new double?[] { null, 0.95, 1.00, 1.07, 1.15, 1.24 };
            PDIFLevel = new double?[] { null, 0.87, 1.00, 1.29, 1.81, 2.61 };
            PREXLevel = new double?[] { 1.33, 1.22, 1.00, 0.87, 0.74, 0.62 };
            FCILLevel = new double?[] { 1.30, 1.10, 1.00, 0.87, 0.73, 0.62 };
            SCEDLevel = new double?[] { 1.43, 1.14, 1.00, 1.00, 1.00, null };

            ExpLevel = new int[] { 4, 7, 13, 25, 50 };

            languages = new List<Pair<int, int>>
            {
                new Pair<int, int>(85, 53),
                new Pair<int, int>(15, 125),
            };

            InitializeComponent();

            Button_FP_Click(Button_FP, null);
        }

        private void Button_FP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int[] fp = new int[]
                {
                    ConvertCharacteristic(TextBox_FP1),
                    ConvertCharacteristic(TextBox_FP2),
                    ConvertCharacteristic(TextBox_FP3),
                    ConvertCharacteristic(TextBox_FP4),
                    ConvertCharacteristic(TextBox_FP5),
                    ConvertCharacteristic(TextBox_FP6),
                    ConvertCharacteristic(TextBox_FP7),
                    ConvertCharacteristic(TextBox_FP8),
                    ConvertCharacteristic(TextBox_FP9),
                    ConvertCharacteristic(TextBox_FP10),
                    ConvertCharacteristic(TextBox_FP11),
                    ConvertCharacteristic(TextBox_FP12),
                    ConvertCharacteristic(TextBox_FP13),
                    ConvertCharacteristic(TextBox_FP14), 
                };

                int EIres = ConvertCount(TextBox_CountEI) * EILevel[ComboBox_LevelEI.SelectedIndex];
                int EOres = ConvertCount(TextBox_CountEO) * EOLevel[ComboBox_LevelEO.SelectedIndex];
                int EQres = ConvertCount(TextBox_CountEQ) * EQLevel[ComboBox_LevelEQ.SelectedIndex];
                int ILFres = ConvertCount(TextBox_CountILF) * ILFLevel[ComboBox_LevelILF.SelectedIndex];
                int EIFres = ConvertCount(TextBox_CountEIF) * EIFLevel[ComboBox_LevelEIF.SelectedIndex];
                int res = EIres + EOres + EQres + ILFres + EIFres;

                Label_ResultEI.Content = EIres;
                Label_ResultEO.Content = EOres;
                Label_ResultEQ.Content = EQres;
                Label_ResultILF.Content = ILFres;
                Label_ResultEIF.Content = EIFres;
                Label_Result.Content = res;

                double coeff = 0.65 + 0.01 * fp.Sum();
                double final = res * coeff;

                double loc = 0;
                foreach (var lang in languages)
                {
                    loc += final * (lang.Key / 100.0) * lang.Value;
                }

                this.loc = (int)Math.Round(loc);

                Label_Final.Content =
                    $"Нормированное количество функциональных точек: {Math.Round(final, 3)}\n" +
                    $"Количество функциональных точек: {res}\n" +
                    $"Количество строк исходного кода: {this.loc}\n" +
                    $"Коэффициент: {coeff}";
            }
            catch (FPTextBlockParseException)
            {
                MessageBox.Show("Введите число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FPTextBlockValueException)
            {
                MessageBox.Show("Характеристики продукта должны быть в диапазоне от 0 до 5, а количество >= нуля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ComboBox_P_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                double[] parameters = new double[]
                {
                    PRECValue[ComboBox_PREC.SelectedIndex],
                    FLEXValue[ComboBox_FLEX.SelectedIndex],
                    RESLValue[ComboBox_RESL.SelectedIndex],
                    TEAMValue[ComboBox_TEAM.SelectedIndex],
                    PMATValue[ComboBox_PMAT.SelectedIndex],
                };

                this.p = parameters.Sum() / 100 + 1.01;
                Label_P.Content = $"P = {this.p}";
            }
            catch (NullReferenceException) { }
        }

        private void Button_EarlyArch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int avgSalary = ConvertCount(TextBox_AvgSalary);

                double[] parameters = new double[]
                {
                    PERSLevel[ComboBox_PERS.SelectedIndex].Value,
                    RCPXLevel[ComboBox_RCPX.SelectedIndex].Value,
                    RUSELevel[ComboBox_RUSE.SelectedIndex].Value,
                    PDIFLevel[ComboBox_PDIF.SelectedIndex].Value,
                    PREXLevel[ComboBox_PREX.SelectedIndex].Value,
                    FCILLevel[ComboBox_FCIL.SelectedIndex].Value,
                    SCEDLevel[ComboBox_SCED.SelectedIndex].Value,
                };

                double people = Math.Round(parameters.Aggregate((total, next) => total * next) * 2.45 * Math.Pow(this.loc / 1000.0, this.p));
                double time = Math.Round(3.0 * Math.Pow(people, 0.33 + 0.2 * (this.p - 1.01)));

                Label_EarlyArchPeople.Content = $"Трудозатраты(чел/мес): {people}";
                Label_EarlyArchTime.Content = $"Время(мес): {time}";
                Label_EarlyArchBudget.Content = $"Бюджет: {people * avgSalary}";
            }
            catch (FPTextBlockParseException)
            {
                MessageBox.Show("Введите число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FPTextBlockValueException)
            {
                MessageBox.Show("Количество должно быть >= нуля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Composition_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int avgSalary = ConvertCount(TextBox_AvgSalary);

                int ruse = ConvertCount(TextBox_RUSE);
                int exp = ExpLevel[ComboBox_Exp.SelectedIndex];

                int easyForms = ConvertCount(TextBox_FormsEasy);
                int normalForms = ConvertCount(TextBox_FormsNormal);
                int hardForms = ConvertCount(TextBox_FormsHard);

                int easyReport = ConvertCount(TextBox_ReportEasy);
                int normalReport = ConvertCount(TextBox_ReportNormal);
                int hardReport = ConvertCount(TextBox_ReportHard);

                int modules = ConvertCount(TextBox_Modules);

                double points = easyForms + normalForms * 2.0 + hardForms * 3.0 + easyReport * 2.0 + normalReport * 5.0 + hardReport * 8.0 + modules * 10.0;
                double people = Math.Round((points * (100.0 - ruse) / 100.0) / exp);
                double time = Math.Round(3.0 * Math.Pow(people, 0.33 + 0.2 * (this.p - 1.01)));

                Label_CompositionPeople.Content = $"Трудозатраты(чел/мес): {people}";
                Label_CompositionTime.Content = $"Время(мес): {time}";
                Label_CompositionPoints.Content = $"Объектн. точки: {points}";
                Label_CompositionBudget.Content = $"Бюджет: {people * avgSalary}";
            }
            catch (FPTextBlockParseException)
            {
                MessageBox.Show("Введите число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FPTextBlockValueException)
            {
                MessageBox.Show("Количество должно быть >= нуля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int ConvertCharacteristic(TextBox text)
        {
            if (!int.TryParse(text.Text, out int result))
            {
                throw new FPTextBlockParseException();
            }

            if (result < 0 || result > 5)
            {
                throw new FPTextBlockValueException();
            }

            return result;
        }

        private int ConvertCount(TextBox text)
        {
            if (!int.TryParse(text.Text, out int result))
            {
                throw new FPTextBlockParseException();
            }

            if (result < 0)
            {
                throw new FPTextBlockValueException();
            }

            return result;
        }

        private void Button_OpenLanguages_Click(object sender, RoutedEventArgs e)
        {
            var window = new ChooseLanguagesWindow(this.languages);
            window.Owner = this;
            window.ShowDialog();

            if (window.DialogResult == true)
            {
                this.languages = window.Languages;
                Button_FP_Click(Button_FP, null);
            }
        }
    }
}
