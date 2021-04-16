using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
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

        public MainWindow()
        {
            InitializeComponent();

            EILevel = new int[] { 3, 4, 6 };
            EOLevel = new int[] { 4, 5, 7 };
            EQLevel = new int[] { 3, 4, 6 };
            ILFLevel = new int[] { 7, 10, 15 };
            EIFLevel = new int[] { 5, 7, 10 };
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

                double final = res * (0.65 + 0.01 * fp.Sum());
                double loc = final * 0.85 * 53 + final * 0.15 * 125;
                Label_Final.Content =
                    $"Нормированное количество функциональных точек: {Math.Round(final, 3)}\n" +
                    $"Количество функциональных точек: {res}\n" +
                    $"Количество строк исходного кода: {Math.Round(loc)}";
            }
            catch (FPTextBlockParseException)
            {
                MessageBox.Show("Введите число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FPTextBlockValueException)
            {
                MessageBox.Show("Характеристики продукта должны быть в диапазоне от 0 до 5", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

            if (result <= 0)
            {
                throw new FPTextBlockValueException();
            }

            return result;
        }
    }
}
