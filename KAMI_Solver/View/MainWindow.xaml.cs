using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace KAMI_Solver
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[,] board;

        private readonly List<SolidColorBrush> candidateColors = new List<SolidColorBrush>();
        private int selectedColorIndex = 0;

        public MainWindow()
        {
            candidateColors.Add(new SolidColorBrush(Color.FromRgb(0xFF, 0xD5, 0x6F))); // #FFD56F
            candidateColors.Add(new SolidColorBrush(Color.FromRgb(0x6F, 0xDC, 0xF2))); // #6FDCF2
            candidateColors.Add(new SolidColorBrush(Color.FromRgb(0xEB, 0x80, 0x80))); // #EB8080
            candidateColors.Add(new SolidColorBrush(Color.FromRgb(0xA2, 0xF3, 0xD2))); // #A2F3D2
            candidateColors.Add(new SolidColorBrush(Color.FromRgb(0xD9, 0xCF, 0xC7))); // #D9CFC7

            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            // init ratio buttons
            for (int i = 0; i < candidateColors.Count; i++)
            {

                RadioButton rb = new RadioButton()
                {
                    Content = new Border()
                    {
                        Background = candidateColors[i]
                    }
                };

                if (i == selectedColorIndex) rb.IsChecked = true;

                int index = i; // whithout this line, i always be same
                rb.Click += (sender, eve) =>
                {
                    selectedColorIndex = index;
                    // System.Diagnostics.Debug.WriteLine(index);
                };
                ColorSelectorPanel.Children.Add(rb);
            }

            // init tiles
            int row = Convert.ToInt32(row_textBox.Text);
            int col = Convert.ToInt32(col_textBox.Text);

            UpdateBoardWithRandomState(col, row);
        }

        private void UpdateBoardWithRandomState(int col, int row)
        {
            if (col <= 0 || row <= 0) return;

            board = new int[col, row]; // initial board

            Random random = new Random();

            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    board[c, r] = random.Next(0, candidateColors.Count); //for ints
                }
            }

            UpdateTableGridView();
        }

        private void UpdateTableGridView()
        {
            if (this.board == null) return;

            int col = board.GetLength(0);
            int row = board.GetLength(1);

            TableGrid.ColumnDefinitions.Clear();
            TableGrid.RowDefinitions.Clear();
            TableGrid.Children.Clear();

            for (int r = 0; r < row; r++)
            {
                TableGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int c = 0; c < col; c++)
            {
                TableGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            //One row first, then next row
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    Button button = new Button()
                    {
                        Background = candidateColors[board[c, r]]
                    };

                    int rowCopy = r;
                    int colCopy = c;
                    button.Click += (sender, e) =>
                    {
                        if (sender is Button b)
                        {
                            board[colCopy, rowCopy] = selectedColorIndex;
                            b.Background = candidateColors[selectedColorIndex];
                        }
                    };
                    Grid.SetColumn(button, c);
                    Grid.SetRow(button, r);
                    TableGrid.Children.Add(button);
                }
            }

            double tileLength = (double)FindResource("tileLength");
            TableGrid.Height = row * tileLength;
            TableGrid.Width = col * tileLength;
        }

        private void SetRowCol_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // init tiles
                int row = Convert.ToInt32(row_textBox.Text);
                int col = Convert.ToInt32(col_textBox.Text);

                UpdateBoardWithRandomState(col, row);
                UpdateTableGridView();
            }
            catch (Exception)
            {
                // invalid input
            }
        }
    }
}
