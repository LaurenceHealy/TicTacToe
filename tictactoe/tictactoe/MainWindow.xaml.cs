
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace tictactoe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Fields
        private MarkType[] _Results;

        private bool _PlayerOneTurn;

        private bool _GameEnded; 
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion

        #region New Game method
        private void NewGame()
        {
            _Results = new MarkType[9];

            for (var i = 0; i < _Results.Length; i++)
            {
                _Results[i] = MarkType.Free;
            }

            _PlayerOneTurn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
            _GameEnded = false;
        } 
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(_GameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

           if (_Results[index] != MarkType.Free)
            {
                return;
            }

            _Results[index] = _PlayerOneTurn ? MarkType.Ex : MarkType.Zero;

            button.Content = _PlayerOneTurn ? "X" : "O";

            if (!_PlayerOneTurn)
            {
                button.Foreground = Brushes.Red;
            }
           
            _PlayerOneTurn ^= true;

            CheckForWinner();
           

        }

        private void CheckForWinner()
        {
            //row0
            if (_Results[0] != MarkType.Free && (_Results[0] & _Results[1] & _Results[2]) == _Results[0])
            {
                _GameEnded = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            //row1
            if (_Results[3] != MarkType.Free && (_Results[3] & _Results[4] & _Results[5]) == _Results[3])
            {
                _GameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //row2
            if (_Results[6] != MarkType.Free && (_Results[6] & _Results[7] & _Results[8]) == _Results[6])
            {
                _GameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            //Column 0
            if (_Results[0] != MarkType.Free && (_Results[0] & _Results[3] & _Results[6]) == _Results[0])
            {
                _GameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //Column 1
            if (_Results[1] != MarkType.Free && (_Results[1] & _Results[4] & _Results[7]) == _Results[1])
            {
                _GameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //Column 2
            if (_Results[2] != MarkType.Free && (_Results[2] & _Results[5] & _Results[8]) == _Results[2])
            {
                _GameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            //Diagonal 1
            if (_Results[0] != MarkType.Free && (_Results[0] & _Results[4] & _Results[8]) == _Results[0])
            {
                _GameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            //Diagonal 2
            if (_Results[2] != MarkType.Free && (_Results[2] & _Results[4] & _Results[6]) == _Results[2])
            {
                _GameEnded = true;
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }



            if (!_Results.Any(result => result == MarkType.Free))
            {
                _GameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });

            }
        }
    }
}
