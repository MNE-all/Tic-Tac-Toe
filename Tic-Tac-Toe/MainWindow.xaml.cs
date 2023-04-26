using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        byte steps = 0;
        char[,]? gameField = new char[3, 3];

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            if (button != null && String.IsNullOrEmpty(button.Content?.ToString()))
            {
                var column = int.Parse(button.Name[1].ToString());
                var row = int.Parse(button.Name[3].ToString());
                Image img = new Image();
                if (steps % 2 == 0)
                {
                    img.Source = new BitmapImage(new Uri("/Resources/cross.png", UriKind.Relative));
                    InfoText.Text = "Ходит второй игрок";
                    gameField[column, row] = 'X';
                }
                else
                {
                    img.Source = new BitmapImage(new Uri("/Resources/circle.png", UriKind.Relative));
                    InfoText.Text = "Ходит первый игрок";
                    gameField[column, row] = 'O';
                }
                steps++;
                button.Content = img;
                WinValidate();
            }

        }

        int streak = 0;
        private void WinValidate()
        {
            // Проверка главной диагонали
            streak = 0;
            for (int i = 0; i < 3; i++)
            {
                WhatIsHere(i, i);
            }
            WhoseStreak(streak);

            // Проверка побочной диагонали
            streak = 0;
            for (int i = 2; i >= 0; i--)
            {
                WhatIsHere(i, 2 - i);
            }
            WhoseStreak(streak);

            // Проверка по горизонтали
            for (int i = 0;i < 3;i++)
            {
                streak = 0;
                for (int j = 0; j < 3;j++)
                {
                    WhatIsHere(i, j);
                }
                WhoseStreak(streak);
            }

            // Проверка по вертикали
            for (int i = 0; i < 3; i++)
            {
                streak = 0;
                for (int j = 0; j < 3; j++)
                {
                    WhatIsHere(j, i);
                }
                WhoseStreak(streak);
            }


            
        }
        private bool WhatIsHere(int i, int j)
        {
            if (gameField[i, j] == 'X')
            {
                streak++;
                return true;
            }
            else if (gameField[i, j] == 'O')
            {
                streak--;
                return true;
            }
            return false; 
        }

        private void WhoseStreak(int streak)
        {
            if (streak == -3)
            {
                InfoText.Text = "Победил второй игрок!";
                EndMessageBox("Победил второй игрок!");
            }
            else if (streak == 3)
            {
                InfoText.Text = "Победил первый игрок";
                EndMessageBox("Победил первый игрок!");
            }
            else if (steps == 9)
            {
                InfoText.Text = "Ничья!";
                EndMessageBox("Ничья!");
            }
        }

        private void EndMessageBox(string str)
        {
            if (MessageBox.Show(str, "Сыграть ещё раз?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Restart();
            }
            else
            {
                Close();
            }
        }

        private void Restart()
        {
            for (int i = 0; i < 3;  i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameField[i, j] = ' ';
                }
            }

            foreach (Button btn in GameField.Children)
            {
                btn.Content = null;
            }
            steps = 0;
            InfoText.Text = "Ходит первый игрок";
        }
    }
}
