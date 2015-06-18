using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Threading;
using System.Diagnostics;

/// <sumary>
///   Checkers game control. It's a control that
///  encapsulates a checkers game. You just have to
///  add it to your application in order to allow your
///  users to have a little fun.
/// </sumary>    
public class BoardView: Control {
    
   
    
   

    
  // Keeps information about the current board state.
  private CheckersBoard board;


  
  // Upper left corner of the board
  internal int startX;
  internal int startY;

  // Size of the boards squares
  internal int cellWidth;


  // Selected squares in a play move. The first element is the first
  //movement 
  internal List selected;

  // Our adversary, the computer
  internal Computer computer;
  
  // Pieces size
  private static int SIZE = 0;
  

  // Form that holds the control
  private Form parent;



  
  /// <sumary>
  ///  Builds the checkers control.
  /// </sumary>
  /// <param name="parentComponent">
  ///  The Form that holds the control.
  /// </param>
  public BoardView (Form parentComponent) {
    selected = new List ();
    board = new CheckersBoard ();
    parent = parentComponent;
    computer = new Computer (board);
    reset ();
    
  }

  /// <sumary> 
  ///   Allows the user to change the max depth of
  ///  the min-max tree used by the computer calculations
  /// </sumary>
  public int depth  {
    get {
      return computer.depth;
    }
    set {
      computer.depth = value;
    }
  }

  /// <sumary>
  ///  Starts a new game. The human is the
  /// first to play with the black(red) pieces.
  /// </sumary>
  public void newGame () {

     
    board.clearBoard ();
    selected.clear ();
    Invalidate ();
    reset ();
    ChangeTitle ();
  }


  /// <sumary>
  ///  Changes the form title, to reflect the current player
  /// </sumary>
  public void ChangeTitle () {
    if (board.getCurrentPlayer () == CheckersBoard.WHITE)
      parent.Text = "Checkers - White";
    else
      parent.Text = "Checkers - Black";
  }

  /// <sumary>
  ///  Saves the current game in a file stream
  /// </sumary>
  /// <param name="file">
  ///  The destination stream
  /// </param>
  public void saveBoard (FileStream  file) {
    try {
      IFormatter formatter = (IFormatter) new BinaryFormatter();

      // Serialize the object graph to stream
      formatter.Serialize(file, board);
         
      
    }
    catch {
      MessageBox.Show ("Error while saving", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
  }

  /// <sumary>
  ///  Loads a saved game from a file stream
  /// </sumary>
  /// <param name="file">
  ///  The source stream
  /// </param>
  public void loadBoard (Stream file) {
    try {
      IFormatter formatter = (IFormatter) new BinaryFormatter();

      // Clear the selected moves, just in case
      selected.clear ();
      Invalidate ();
      reset ();

      // Deserializes the object graph to stream
      board = (CheckersBoard) formatter.Deserialize(file);

      // Create a new computer instace for this board
      computer = new Computer (board);
    }
    catch {
      MessageBox.Show ("Error while loading", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
  }
  
    
  /// <sumary>
  ///   Draws the checkers control in response to
  ///  the paint event.
  /// </sumary>
  /// <param name="ev">
  ///  Event arguments
  /// </param>
   protected override void OnPaint (PaintEventArgs ev) {
    Graphics g = ev.Graphics;
    Size d = ClientSize;
    int marginX;
    int marginY;
    int incValue;

    // Calculates the increments so that we can get a squared
    // board
    if (d.Width < d.Height) {
      marginX = 0;
      marginY = (d.Height - d.Width) / 2;
      
      incValue = d.Width / 8;
    }
    else  {
      marginX = (d.Width - d.Height) / 2;
      marginY = 0;
      
      incValue = d.Height / 8;
    }

    startX = marginX;
    startY = marginY;
    cellWidth = incValue;

    drawBoard (g, marginX, marginY, incValue);
    drawPieces (g, marginX, marginY, incValue);
 }

  /// <sumary>
  ///  Draws the board background
  /// </sumary>
  /// <param name="g">
  ///  Graphics object used to do the drawing
  /// </param>
  /// <param name="marginX">
  ///  Board's horizontal margin
  /// </param>
  /// <param name="marginY">
  ///  Board's vertical  margin
  /// </param>
  /// <param name="incValue">
  ///  Increment factor between board houses
  /// </param>
  private void drawBoard (Graphics g, int marginX, int marginY, int incValue) {
    int pos;
    Brush cellColor;
    Color whiteCell = ColorTranslator.FromWin32(Checkers.wR + Checkers.wG * 256 + Checkers.wB * 65536);
    Color blackCell = ColorTranslator.FromWin32(Checkers.bR + Checkers.bG * 256 + Checkers.bB * 65536);
    for (int y = 0; y < 8; y++)
      for (int x = 0; x < 8; x++) {
        if ((x + y) % 2 == 0)
          cellColor = new SolidBrush (whiteCell);
        else {
          pos = y * 4 + (x + ((y % 2 == 0) ? - 1 : 0)) / 2;
          
          if (selected.has (pos))
            cellColor = new SolidBrush (Color.LightGreen);
          else
            cellColor = new SolidBrush (blackCell);
        }     

        g.FillRectangle (cellColor, marginX + x * incValue, marginY + y * incValue, incValue - 1, incValue - 1);    
      }
  }


  // Drawing margin for king pieces
  private static int KING_SIZE = 3;

  /// <sumary>
  ///  Draws the checkers pieces
  /// </sumary>
  /// <param name="g">
  ///  Graphics object used to do the drawing
  /// </param>
  /// <param name="marginX">
  ///  Board's horizontal margin
  /// </param>
  /// <param name="marginY">
  ///  Board's vertical  margin
  /// </param>
  /// <param name="incValue">
  ///  Increment factor between board houses
  /// </param>
  private void drawPieces (Graphics g, int marginX, int marginY, int incValue) {
    int x, y;
    Brush pieceColor;

    for (int i = 0; i < 32; i++)
      try {
        if (board.getPiece (i) != CheckersBoard.EMPTY) {
          if (board.getPiece (i) == CheckersBoard.BLACK ||
              board.getPiece (i) == CheckersBoard.BLACK_KING)
            pieceColor = new SolidBrush (Color.Gray);
          else 
            pieceColor = new SolidBrush (Color.White);

          y = i / 4;
          x = (i % 4) * 2 + (y % 2 == 0 ? 1 : 0);
          g.FillEllipse (pieceColor, SIZE + marginX + x * incValue, SIZE + marginY + y * incValue,
                      incValue - 1 - 2 * SIZE, incValue - 1 - 2 * SIZE);

          if (board.getPiece (i) == CheckersBoard.WHITE_KING) {
            pieceColor = new SolidBrush (Color.Black);
            g.DrawEllipse (new Pen(pieceColor), KING_SIZE + marginX + x * incValue, KING_SIZE + marginY + y * incValue,
                        incValue - 1 - 2 * KING_SIZE, incValue - 1 - 2 * KING_SIZE);
          }
          else if (board.getPiece (i) == CheckersBoard.BLACK_KING) {
            pieceColor = new SolidBrush (Color.White);
            g.DrawEllipse (new Pen(pieceColor), KING_SIZE + marginX + x * incValue, KING_SIZE + marginY + y * incValue,
                        incValue - 1 - 2 * KING_SIZE, incValue - 1 - 2 * KING_SIZE);
          }
        }
      }
      catch (BadCoord bad) {
        Debug.WriteLine (bad.StackTrace);
        Application.Exit ();
      }
  }
 

  
  


  // Temporary stack board for the multiple moves
  Stack boards;
  

  /// <sumary>
  ///  Handles the message of the user pressing the
  /// mouse button over the board. If it is pressed over
  /// a piece and it's a player piece then it becomes
  /// selected/unselected.
  /// </sumary>
  /// <param name="e">
  ///  Event data.
  /// </param>
   protected override void OnMouseDown(MouseEventArgs e) {
    int pos;

    pos = getPiecePos (e.X , e.Y);
    if (pos != -1)
      try {
        int piece = board.getPiece (pos);
        
        if (piece != CheckersBoard.EMPTY &&
            (((piece == CheckersBoard.WHITE || piece == CheckersBoard.WHITE_KING) &&
              board.getCurrentPlayer () == CheckersBoard.WHITE) ||
              ((piece == CheckersBoard.BLACK || piece == CheckersBoard.BLACK_KING) &&
              board.getCurrentPlayer () == CheckersBoard.BLACK))) {
          if (selected.isEmpty ())  //If none was selected
            selected.push_back (pos);
          else {
            int temp = (int) selected.peek_tail ();

            if (temp == pos) // If selected, unselect
              selected.pop_back ();
            else {
             
            }
          }
          
          
          Invalidate ();
          Update ();
          return;
        }
        else {
          bool good = false;
          CheckersBoard tempBoard;
                    
          if (!selected.isEmpty ()) {
            // Gets current board
            if (boards.Count == 0) {
              tempBoard = (CheckersBoard) board.clone ();
              boards.Push (tempBoard);
            }
            else
              tempBoard = (CheckersBoard) boards.Peek ();
            

            int from = (int) selected.peek_tail ();
            if (tempBoard.isValidMove (from, pos)) {
              tempBoard = (CheckersBoard) tempBoard.clone ();

              bool isAttacking = tempBoard.mustAttack ();
              
              tempBoard.move (from, pos);
              
              if (isAttacking && tempBoard.mayAttack (pos)) {
                selected.push_back (pos);
                boards.Push (tempBoard);

              }
              else {
                selected.push_back (pos);
                makeMoves (selected, board);
                boards = new Stack ();
              }
              
              good = true;
            }
            else if (from == pos) {
              selected.pop_back ();
              boards.Pop ();

              good = true;
            }
          }
          
          if (!good) {
            
          }
          else {
            Invalidate ();
            Update ();
          }
        }
      }
      catch (BadCoord bad) {
        Debug.WriteLine (bad.StackTrace);
        Application.Exit ();
      }
      catch (BadMoveException bad) {
        Debug.WriteLine (bad.StackTrace);
        Application.Exit ();
      }
    }


    /// <sumary>
    ///  Cleans the temporary strucures used during each move
    /// </sumary>
    public void reset () {
      boards = new Stack ();
    } 
  

  /// <sumary>
  ///  Applies the player moves to the board and after that
  /// tells the computer to play.
  /// </sumary>
  /// <param name="moves">
  ///  List with all the movements to perform for the player.
  /// </param>
  /// <param name="board">
  ///  The board where to apply the moves
  /// </param>
    private void makeMoves(List moves, CheckersBoard board)
    {
    List moveList = new List ();
    int from, to = 0;

    from = (int) moves.pop_front ();
    while (!moves.isEmpty ()) {
      to = (int) moves.pop_front ();
      moveList.push_back (new Move (from, to));
      from = to;
    }

    board.move (moveList);
    Invalidate ();
    Update ();
    selected.clear ();
    reset ();
        

    if (!gameEnded ()) {
      Thread.Sleep (1000); // Just to allow the user to see his /her move
      ChangeTitle ();
      computer.play ();
      Invalidate ();
      Update ();

      if (!gameEnded ())
        ChangeTitle ();
    }
  }
    
  /// <sumary>
  ///  Returns the index of the selected piece
  /// </sumary>
  /// <param name="currentX">
  ///  The X coordinate between 0 and 7
  /// </param>
  /// <param name="currentY">
  ///  The Y coordinate between 0 and 7
  /// </param>
  /// <value>
  ///  The index value between 0 and 31 if there's a piece
  /// in the given location. Otherwise it returns -1.
  /// </value>
  private int getPiecePos (int currentX, int currentY) {
    for (int i = 0; i < 32; i++) {
      int x, y;

      y = i / 4;
      x = (i % 4) * 2 + (y % 2 == 0 ? 1 : 0);
      if (startX + x * cellWidth < currentX &&
          currentX < startX + (x + 1) * cellWidth &&
          startY + y * cellWidth < currentY &&
          currentY < startY + (y + 1) * cellWidth)
        return i;
    }

    return -1;
  }

  /// <sumary>
  ///  Deals with the end of game situation.
  /// </sumary>
  /// <value>
  ///  true if the game ended, false otherwise.
  /// </value>
  private bool gameEnded () {

      
      
    bool result;

    int white = board.getWhitePieces ();
    int black = board.getBlackPieces ();
    if (board.hasEnded ()) {
        if (board.winner() == CheckersBoard.BLACK)
        {
            MessageBox.Show("Черные Выиграли!" , "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);  
        }
        
        else
            MessageBox.Show("Белые победили!", "Конец игры");
      result = true;
    }
    else
      result = false;

    return result;
  }

 }
