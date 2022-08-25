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

namespace TicTacToe;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region Private Members
    /// <summary>
    /// Holds the current results of cells in the active game
    /// </summary>
    private MarkType[] mResults;

    /// <summary>
    /// True if it is player 1's turn (X) or player 2's turn (O)
    /// </summary>
    private bool mPlayer1Turn;

    /// <summary>
    /// True if the game has ended
    /// </summary>
    private bool mGameEnded;

    #endregion

    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();

        NewGame();
    }

    #endregion


    /// <summary>
    /// starts a new game and clears all values back to clean start
    /// </summary>
    /// 

    private void NewGame()
    {
        // Create a new blank array of free cells
        mResults = new MarkType[9];

        for (var i = 0; i < mResults.Length; i++)
            mResults[i] = MarkType.Free;

        // Make sure Player 1 starts the game
        mPlayer1Turn = true;

        // Iterate every button on the grid
        Container.Children.Cast<Button>().ToList().ForEach(button =>
        {
            // clear grid at start, change background default color, foreground (text) color default values
            button.Content = string.Empty;
            button.Background = Brushes.Gray;
            button.Foreground = Brushes.Black;

        });

        // Make sure the game hasn't finished
        mGameEnded = false;
    }

    /// <summary>
    /// handles a button click event
    /// </summary>
    /// <param name="sender">The button that was clicked</param>
    /// <param name="e">The result of the click</param>
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (mGameEnded)
        {
            NewGame();
            return;
        }

        //Cast the sender to a button
        var button = (Button)sender;

        //find the buttons position in the array
        var column = Grid.GetColumn(button);
        var row = Grid.GetRow(button);

        var index = column + (row * 3);

        //Don't do anything if the cell already has a value in it
        if (mResults[index] != MarkType.Free)
            return;

        //Set the cell value based on which player's turn it is
        mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

        // Set button text to the result
        button.Content = mPlayer1Turn ? "X" : "O";

        // Change noughts to green
        if (!mPlayer1Turn)
            button.Foreground = Brushes.White;

        // Toggle the players' turns
        mPlayer1Turn ^= true;

        //Check for a winner
        CheckForWinner();

    }
    /// <summary>
    /// Checks if there is a winner
    /// </summary>
    private void CheckForWinner()
    {
        #region Horizontal wins
        // Row 0
        //Check for horizontal wins
        if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
        {
            mGameEnded = true;

            //Highlight winning cells
            Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
        }
        // Row 1
        //Check for horizontal wins
        if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
        {
            mGameEnded = true;

            //Highlight winning cells
            Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
        }
        // Row 2
        //Check for horizontal wins
        if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
        {
            mGameEnded = true;

            //Highlight winning cells
            Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
        }
        #endregion

        #region Vertical wins   
        // Column 0
        //Check for vertical wins
        if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
        {
            mGameEnded = true;

            //Highlight winning cells
            Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
        }
        // Column 1
        //Check for vertical wins
        if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
        {
            mGameEnded = true;

            //Highlight winning cells
            Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
        }
        // Column 2
        //Check for vertical wins
        if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
        {
            mGameEnded = true;

            //Highlight winning cells
            Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
        }

        #endregion

        #region Diagonal wins
        // Column 2
        //Check for diagonal (top left to bottom right) wins
        if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
        {
            mGameEnded = true;

            //Highlight winning cells
            Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
        }
        // Column 2
        //Check for diagonal (top right to bottom left) wins
        if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
        {
            mGameEnded = true;

            //Highlight winning cells
            Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
        }

        #endregion

        #region No winner
        // Check for no winner
        if (!mResults.Any(f => f == MarkType.Free))
        {
            // Game has ended
            mGameEnded = true;

            //Turn all cells red (no winner)
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {               
                button.Background = Brushes.Red;

            });
        }
        #endregion

    }
}
