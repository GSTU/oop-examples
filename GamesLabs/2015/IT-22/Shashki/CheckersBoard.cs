using System;
using System.Runtime.Serialization;


/// <sumary>
///   Represents the game logic.
/// </sumary>    
[Serializable]
internal class CheckersBoard: ISerializable {
  // Holds the checkers board representation.
  private byte[] pieces;

  // Pieces used in the game
  public const byte EMPTY = 0;
  public const byte WHITE = 2;
  public const byte WHITE_KING  = 3;
  public const byte BLACK = 4;
  public const byte BLACK_KING  = 5;

  // Allows the game to know which pieces are promoted
  //to king
  private   const byte KING = 1;

  // Piece counters for each side.
  private int whitePieces;
  private int blackPieces;


  // As it implies, holds the current player
  private int currentPlayer;

  
  /// <sumary>
  ///   Constructor
  /// </sumary>    
  public CheckersBoard () {
    pieces = new byte [32];
    clearBoard ();
  }

  /// <sumary>
  ///  Serialization constructor
  /// </sumary>    
 public CheckersBoard (SerializationInfo info, StreamingContext context)  {
     byte[] tempPieces = new byte [32];

     pieces = (byte[]) info.GetValue ("pieces", tempPieces.GetType ());

     System.Int32 temp = new System.Int32 ();
     whitePieces = (int) info.GetValue ("whitePieces", temp.GetType ());
     blackPieces = (int) info.GetValue ("blackPieces", temp.GetType ());
     currentPlayer = (int) info.GetValue ("currentPlayer", temp.GetType ());
 }

  /// <sumary>
  ///   Returns the current player
  /// </sumary>    
  /// <value>
  ///  An integer constant that represents
  /// the current player
  /// </value>
  public int getCurrentPlayer () {
    return currentPlayer;
  }
  
  /// <sumary>
  ///   Changes the current player
  /// </sumary>    
  /// <param name="player">
  ///  An integer constant that represents
  /// the current player
  /// </param>
  public void setCurrentPlayer (int player) {
    currentPlayer = player;
  }

  /// <sumary>
  ///   Returns the number of white pieces
  /// </sumary>    
  /// <value>
  ///  An integer with the number of white pieces
  /// </value>
  public int getWhitePieces () {
    return whitePieces;
  }

  /// <sumary>
  ///   Returns the number of black pieces
  /// </sumary>    
  /// <value>
  ///  An integer with the number of black pieces
  /// </value>
  public int getBlackPieces () {
    return blackPieces;
  }
   
   /// <sumary>
   ///   Deep clone of the class instance
   /// </sumary>    
   /// <value>
   ///  A clone of the class instance
   /// </value>
   public object clone () {
      CheckersBoard board = new CheckersBoard ();
      
      board.currentPlayer = currentPlayer;
      board.whitePieces = whitePieces;
      board.blackPieces = blackPieces;
      for (int i=0; i < 32; i++)
        board.pieces[i] = pieces [i];
      
      return board;
   }

   /// <sumary>
   ///   Writes the componento into the serialization stream.
   /// </sumary>    
   public void GetObjectData (SerializationInfo info, StreamingContext context) {
     info.AddValue ("pieces", pieces);
     info.AddValue ("whitePieces", whitePieces);
     info.AddValue ("blackPieces", blackPieces);
     info.AddValue ("currentPlayer", currentPlayer);
   }

   /// <sumary>
   ///   Finds all legal moves for the current player in the
   /// current board.
   /// </sumary>    
   /// <value>
   ///  A list with all the valid moves
   /// </value>
   public List legalMoves () {
     int color;
     int enemy;

     color = currentPlayer;
     if (color == WHITE)
       enemy = BLACK;
     else
       enemy = WHITE;

     if (mustAttack ())
       return generateAttackMoves (color, enemy);
     else
       return generateMoves (color, enemy);
   }

   /// <sumary>
   ///   Finds all legal attack moves for the current player.
   /// </sumary>    
   /// <param name="color">
   ///  Current player color
   /// </param>
   /// <param name="enemy">
   ///  Enemy piece's color
   /// </param>
   /// <value>
   ///  A list with all the valid attack moves
   /// </value>
    private List generateAttackMoves (int color, int enemy) {
      List moves = new List ();
      List tempMoves;
      
      
      for (int k = 0; k < 32; k++)
        if ((pieces [k] & ~KING) == currentPlayer) {
          if ((pieces [k] & KING) == 0)  // Simple piece
            tempMoves = simpleAttack (k, color, enemy);
          else { // It's a king piece
            List lastPos = new List ();

            lastPos.push_back (k);

            tempMoves = kingAttack (lastPos, k, NONE, color, enemy);
          }

          if (notNull (tempMoves))
            moves.append (tempMoves);
        }
      
      return moves;
    }




   /// <sumary>
   ///   Finds all legal attack moves for the current player
   ///  with simple pieces
   /// </sumary>
   /// <param name="pos">
   ///  Current piece position
   /// </param>    
   /// <param name="color">
   ///  Current player color
   /// </param>
   /// <param name="enemy">
   ///  Enemy piece's color
   /// </param>
   /// <value>
   ///  A list with all the valid attack moves
   /// </value>
  private List simpleAttack (int pos, int color, int enemy) {
    int x = posToCol (pos);
    int y = posToLine (pos);
    int i;
    List moves = new List ();
    List tempMoves;
    int enemyPos, nextPos;
    


    i = (color == WHITE) ? -1 : 1;

    
    // See the diagonals /^ e \v
    if (x < 6 && y + i > 0 && y + i < 7) {
      enemyPos = colLineToPos (x + 1, y + i);
      nextPos = colLineToPos (x + 2, y + 2 * i);

      if ((pieces [enemyPos] & ~KING) == enemy && pieces [nextPos]  == EMPTY) {
        tempMoves = simpleAttack (nextPos, color, enemy);
        moves.append (addMove (new Move (pos, nextPos), tempMoves));
      }
    }


    // See the diagonals v/ e ^\
    if (x > 1 && y + i > 0 && y + i < 7) {
      enemyPos = colLineToPos (x - 1, y + i);
      nextPos = colLineToPos (x - 2, y + 2 * i);

      if ((pieces [enemyPos] & ~KING) == enemy && pieces [nextPos]  == EMPTY) {
        tempMoves = simpleAttack (nextPos, color, enemy);
        moves.append (addMove (new Move (pos, nextPos), tempMoves));
      }
    }

    if (moves.isEmpty ())
      moves.push_back (new List ());
    
    return moves;
  }


  // Consts for the last direction used in the research
  private const int NONE = 0;        // First time
  private const int LEFT_BELOW  = 1; // Diagonal v/
  private const int LEFT_ABOVE  = 2; // Diagonal ^\
  private const int RIGHT_BELOW = 3; // Diagonal \v
  private const int RIGHT_ABOVE = 4; // Diagonal /^

   /// <sumary>
   ///   Finds all legal attack moves for the current player with
   /// king pieces
   /// </sumary>
   /// <param name="lastPos">
   ///   All piece movents until now
   /// </param>
   /// <param name="pos">
   ///   Current position
   /// </param>
   /// <param name="dir">
   ///   Last direction
   /// </param>    
   /// <param name="color">
   ///  Current player color
   /// </param>
   /// <param name="enemy">
   ///  Enemy piece's color
   /// </param>
   /// <value>
   ///  A list with all the valid attack moves
   /// </value>
  private List kingAttack (List lastPos, int pos, int dir, int color, int enemy) {
    List tempMoves, moves = new List ();

    if (dir != RIGHT_BELOW) {
      tempMoves = kingDiagAttack (lastPos, pos, color, enemy, 1, 1);

      if (notNull (tempMoves))
        moves.append (tempMoves);
    }
    
    if (dir != LEFT_ABOVE) {
      tempMoves = kingDiagAttack (lastPos, pos, color, enemy, -1, -1);

      if (notNull (tempMoves))
        moves.append (tempMoves);
    }
    

    if (dir != RIGHT_ABOVE) {
      tempMoves = kingDiagAttack (lastPos, pos, color, enemy, 1, -1);

      if (notNull (tempMoves))
        moves.append (tempMoves);
    }

    if (dir != LEFT_BELOW) {
      tempMoves = kingDiagAttack (lastPos, pos, color, enemy, -1, 1);

      if (notNull (tempMoves))
        moves.append (tempMoves);
    }


    return moves;
  }
  

   /// <sumary>
   ///   Finds all legal attack moves for the current player with
   /// king pieces for one diagonal
   /// </sumary>
   /// <param name="lastPos">
   ///   All piece movents until now
   /// </param>
   /// <param name="pos">
   ///   Current position
   /// </param>
   /// <param name="color">
   ///  Current player color
   /// </param>
   /// <param name="enemy">
   ///  Enemy piece's color
   /// </param>
   /// <value>
   ///  A list with all the valid attack moves
   /// </value>
  private List kingDiagAttack (List lastPos, int pos, int color, int enemy, int incX, int incY) {
    int x = posToCol (pos);
    int y = posToLine (pos);
    int i, j;
    List moves = new List ();
    List tempMoves, tempPos;


    int startPos = (int) lastPos.peek_head ();
    
    i = x + incX;
    j = y + incY;

    // Finds the enemy
    while (i > 0 && i < 7 && j > 0 && j < 7 &&
           (pieces [colLineToPos (i, j)] == EMPTY ||  colLineToPos (i, j) == startPos)) {
      i += incX;
      j += incY;
    }

    if (i > 0 && i < 7 && j > 0 && j < 7 && (pieces [colLineToPos (i, j)] & ~KING) == enemy &&
        !lastPos.has (colLineToPos (i, j))) {

      lastPos.push_back (colLineToPos (i, j));
      
      i += incX;
      j += incY;

      int saveI = i;
      int saveJ = j;      
      while (i >= 0 && i <= 7 && j >= 0 && j <= 7 &&  
           (pieces [colLineToPos (i, j)] == EMPTY ||  colLineToPos (i, j) == startPos)) {

        int dir;

        if (incX == 1 && incY == 1)
          dir = LEFT_ABOVE;
        else if (incX == -1 && incY == -1)
          dir = RIGHT_BELOW;
        else if (incX == -1 && incY == 1)
          dir = RIGHT_ABOVE;
        else
          dir = LEFT_BELOW;
        

        tempPos = (List) lastPos.clone ();
        tempMoves = kingAttack (tempPos, colLineToPos (i, j), dir, color, enemy);

        if (notNull (tempMoves))
          moves.append (addMove (new Move (pos, colLineToPos (i, j)), tempMoves));

        i += incX;
        j += incY;
      }

      lastPos.pop_back ();

      if (moves.isEmpty ()) {
        i = saveI;
        j = saveJ;

        while (i >= 0 && i <= 7 && j >= 0 && j <= 7 &&  
               (pieces [colLineToPos (i, j)] == EMPTY ||  colLineToPos (i, j) == startPos)) {

          tempMoves = new List ();
          tempMoves.push_back (new Move (pos, colLineToPos (i, j)));
          moves.push_back (tempMoves);

          i += incX;
          j += incY;
        }
      }
    }
    
    return moves;
  }
  

   /// <sumary>
   ///   Validates the list of lists isn't null.
   /// </sumary>
   /// <param name="moves">
   ///   A list of lists with game movements
   /// </param>
   /// <value>
   ///  true if the list isn't null
   /// </value>
  private bool notNull (List moves) {
    return !moves.isEmpty () && !((List) moves.peek_head ()).isEmpty ();
  }
  
   /// <sumary>
   ///   Adds a new game movement to the head of all lists
   /// </sumary>
   /// <param name="move">
   ///   Game movement to add
   /// </param>
   /// <param name="move">
   ///   A list of lists with game movements
   /// </param>
   /// <value>
   ///  The modified list of lists
   /// </value>
  private List addMove (Move move, List moves) {
    if (move == null)
      return moves;

    List current, temp = new List ();
    while (!moves.isEmpty ()) {
      current = (List) moves.pop_front ();
      current.push_front (move);
      temp.push_back (current);
    }

    return temp;
  }
  
  
  
   /**
    * Gera as jogadas para os movimentos que nao sao de ataque
    */
    private List generateMoves (int color, int enemy) {
      List moves = new List ();
      List tempMove;
      
      
      for (int k = 0; k < 32; k++)
        if ((pieces [k] & ~KING) == currentPlayer) {
          int x = posToCol (k);
          int y = posToLine (k);
          int i, j;
          
          if ((pieces [k] & KING) == 0) {  // Simple piece
            i = (color == WHITE) ? -1 : 1;

            // See the diagonals /^ e \v
            if (x < 7 && y + i >= 0 && y + i <= 7 &&
                pieces [colLineToPos (x + 1, y + i)]  == EMPTY) {
              tempMove = new List ();
              tempMove.push_back (new Move (k, colLineToPos (x + 1, y + i)));
              moves.push_back (tempMove);
            }
            
         
            // See the diagonals ^\ e v/
            if (x > 0 && y + i >= 0 && y + i <= 7 &&
                pieces [colLineToPos (x - 1, y + i)]  == EMPTY) {
              tempMove = new List ();
              tempMove.push_back (new Move (k, colLineToPos (x - 1, y + i)));
              moves.push_back (tempMove);
            };
          }
          else { // It's a king piece
            // See the diagonal \v
            i = x + 1;
            j = y + 1;
            
            while (i <= 7 && j <= 7 && pieces [colLineToPos (i, j)] == EMPTY) {
              tempMove = new List ();
              tempMove.push_back (new Move (k, colLineToPos (i, j)));
              moves.push_back (tempMove);

              i++;
              j++;
            }

    
            // See the diagonals ^\
            i = x - 1;
            j = y - 1;
            while (i >= 0  && j >= 0 && pieces [colLineToPos (i, j)] == EMPTY) {
              tempMove = new List ();
              tempMove.push_back (new Move (k, colLineToPos (i, j)));
              moves.push_back (tempMove);
              
              i--;
              j--;
            }

            // See the diagonals /^
            i = x + 1;
            j = y - 1;
            while (i <= 7 && j >= 0 && pieces [colLineToPos (i, j)] == EMPTY) {
              tempMove = new List ();
              tempMove.push_back (new Move (k, colLineToPos (i, j)));
              moves.push_back (tempMove);
              
              i++;
              j--;
            }

           // See the diagonals v/
           i = x - 1;
           j = y + 1;
           while (i >= 0 && j <= 7 && pieces [colLineToPos (i, j)] == EMPTY) {
             tempMove = new List ();
             tempMove.push_back (new Move (k, colLineToPos (i, j)));
             moves.push_back (tempMove);
              
             i--;
             j++;
           }
          }
        }

      return moves;
    }
  
  /// <sumary>
  ///   Validates a game move
  /// </sumary>
  /// <param name="from">
  ///   Starting board position
  /// </param>
  /// <param name="to">
  ///   Ending board position
  /// </param>
  /// <value>
  ///  true if the movement is valid
  /// </value>
  public bool isValidMove (int from, int to) {
    // If the arguments are invalid so is the game movement
    if (from < 0 || from > 32 || to < 0 || to > 32)
      return false;

    // If the from or the to houses aren't empty the game move is invalid
    if (pieces [from] == EMPTY || pieces [to] != EMPTY)
      return false;

    // Are we trying to move a pience from the current player ?
    if ((pieces [from] & ~KING) != currentPlayer)
      return false;
    

    int color;
    int enemy;
    color = pieces [from] & ~KING;
    if (color == WHITE)
      enemy = BLACK;
    else
      enemy = WHITE;


    int fromLine = posToLine (from);
    int fromCol  = posToCol (from);
    int toLine   = posToLine (to);
    int toCol    = posToCol (to);
    
    int incX, incY;

    // Set the increments
    if (fromCol > toCol)
      incX = -1;
    else
      incX = 1;


    if (fromLine > toLine)
      incY = -1;
    else
      incY = 1;

    int x = fromCol + incX;
    int y = fromLine + incY;
    

    if ((pieces [from] & KING) == 0) { // Simple piece
      bool goodDir;

      if ((incY == -1 && color == WHITE) || (incY == 1 && color == BLACK))
        goodDir = true;
      else
        goodDir = false;
      
      if (x == toCol && y == toLine) // Simple move
          return goodDir && !mustAttack ();

            

      // If it wasn't a simple move it can only be an attack move
      return goodDir && x + incX == toCol && y + incY == toLine &&
             (pieces [colLineToPos (x, y)] & ~KING) == enemy;
    }
    else { // Is a king piece
      while (x != toCol && y != toLine && pieces [colLineToPos (x, y)] == EMPTY) {
        x += incX;
        y += incY;
      }

      // Simple move with a king piece
      if (x == toCol && y == toLine)
        return !mustAttack ();

      if ((pieces [colLineToPos (x, y)] & ~KING) == enemy) {
        x += incX;
        y += incY;

        while (x != toCol && y != toLine && pieces [colLineToPos (x, y)] == EMPTY) {
          x += incX;
          y += incY;
        }

        if (x == toCol && y == toLine)
          return true;
      }
    }
    
    
    return false;
  }


  /// <sumary>
  ///   Validates if the current player must attack
  /// </sumary>
  /// <value>
  ///  true if there are enemy pieces to attack
  /// </value>
  public bool mustAttack () {
    for (int i = 0; i < 32; i++)
      if ((pieces [i] & ~KING) == currentPlayer && mayAttack (i))
        return true;

    return false;
  }
  
  /// <sumary>
  ///   Validates if the given house attacks any position
  /// </sumary>
  /// <param name="pos">
  ///   A checkers  board position
  /// </param>
  /// <value>
  ///  true if there is a piece at the position and it attacks enemy pieces.
  /// </value>
  public bool mayAttack (int pos) {
    if (pieces [pos] == EMPTY)
      return false;
    
    int color;
    int enemy;
    
    color = pieces [pos] & ~KING;
    if (color == WHITE)
      enemy = BLACK;
    else
      enemy = WHITE;

    int x = posToCol (pos);
    int y = posToLine (pos);

    if ((pieces [pos] & KING) == 0) { // It's a simple piece
      int i;

      i = (color == WHITE) ? -1 : 1;

      // See the diagonals /^ e \v
      if (x < 6 && y + i > 0 && y + i < 7 && (pieces [colLineToPos (x + 1, y + i)] & ~KING) == enemy &&
          pieces [colLineToPos (x + 2, y + 2 * i)]  == EMPTY)
        return true;

      // See the diagonals ^\ e v/
      if (x > 1 && y + i > 0 && y + i < 7 && (pieces [colLineToPos (x - 1, y + i)] & ~KING) == enemy &&
          pieces [colLineToPos (x - 2, y + 2 * i)]  == EMPTY)
        return true;

    }
    else { // It's a king piece
      int i, j;
      
 
      // See the diagonal \v
      i = x + 1;
      j = y + 1;
      while (i < 6 && j < 6 && pieces [colLineToPos (i, j)] == EMPTY) {
        i++;
        j++;
      }

      if (i < 7 && j < 7 && (pieces [colLineToPos (i, j)] & ~KING) == enemy) {
        i++;
        j++;
      
        if (i <= 7 && j <= 7 && pieces [colLineToPos (i, j)] == EMPTY) 
          return true;
      }
      
      // See the diagonals ^\
      i = x - 1;
      j = y - 1;
      while (i > 1 && j > 1 && pieces [colLineToPos (i, j)] == EMPTY) {
        i--;
        j--;
      }

      if (i > 0 && j > 0 && (pieces [colLineToPos (i, j)] & ~KING) == enemy) {
        i--;
        j--;
      
        if (i >= 0 && j >= 0 && pieces [colLineToPos (i, j)] == EMPTY) 
          return true;
      }
      
      // See the diagonals /^
      i = x + 1;
      j = y - 1;
      while (i < 6 && j > 1 && pieces [colLineToPos (i, j)] == EMPTY) {
        i++;
        j--;
      }

      if (i < 7 && j > 0 && (pieces [colLineToPos (i, j)] & ~KING) == enemy) {
        i++;
        j--;
      
        if (i <= 7 && j >= 0 && pieces [colLineToPos (i, j)] == EMPTY) 
          return true;
      }
      
      // See the diagonals v/
      i = x - 1;
      j = y + 1;
      while (i > 1 && j < 6 && pieces [colLineToPos (i, j)] == EMPTY) {
        i--;
        j++;
      }

      if (i > 0 && j < 7 && (pieces [colLineToPos (i, j)] & ~KING) == enemy) {
        i--;
        j++;
      
        if (i >= 0 && j <= 7 && pieces [colLineToPos (i, j)] == EMPTY) 
          return true;
      }
    }


    return false;
  }
  
  /// <sumary>
  ///   Performs a single game movement
  /// </sumary>
  /// <param name="from">
  ///   Starting position
  /// </param>
  /// <param name="to">
  ///   Ending position
  /// </param>
  public void move (int from, int to) {
    bool haveToAttack = mustAttack ();
    
    applyMove (from, to);

    if (!haveToAttack)
      changeSide ();
    else
      if (!mayAttack (to))
        changeSide ();
  }

  /// <sumary>
  ///   Performs a multiple game movement
  /// </sumary>
  /// <param name="moves">
  ///   List with all the movements of apply
  /// </param>
  public void move (List moves) {
    Move move;
    Enumeration iter = moves.elements ();

    while (iter.hasMoreElements ()) {
      move = (Move) iter.nextElement ();
      applyMove (move.getFrom (), move.getTo ());
    }

    changeSide ();
  }


  /// <sumary>
  ///   Changes the current player
  /// </sumary>
  private void changeSide () {
    if (currentPlayer == WHITE)
      currentPlayer = BLACK;
    else
      currentPlayer = WHITE;
  }


   /// <sumary>
   ///   Changes the board information by applying a game move
   /// </sumary>
   /// <param name="from">
   ///   Starting position
   /// </param>
   /// <param name="to">
   ///   Ending position
   /// </param>
   private void applyMove (int from, int to) {
     if (!isValidMove (from, to))
       throw new BadMoveException ();

     clearPiece (from, to);
     // performs the movement
     if (to < 4 && pieces [from] == WHITE)
       pieces [to] = WHITE_KING;
     else if (to > 27 && pieces [from] == BLACK)
       pieces [to] = BLACK_KING;
     else
       pieces [to] = pieces [from];

     pieces [from] = EMPTY;
   }


   /// <sumary>
   ///   Returns a piece at a given position
   /// </sumary>
   /// <param name="pos">
   ///   The checkers board position
   /// </param>
   /// <value>
   ///   The piece at the position
   /// </value>
  public byte getPiece (int pos)  {
    if (pos < 0 || pos > 32)
      throw new BadCoord ();

    return pieces [pos];
  }

   /// <sumary>
   ///   Verifies if the game has ended
   /// </sumary>
   /// <value>
   ///   true if the game has ended
   /// </value>
  public bool hasEnded () {
    return whitePieces == 0 || blackPieces == 0 || !notNull (legalMoves ());
  }


   /// <sumary>
   ///   Tells which side has own the game
   /// </sumary>
   /// <value>
   ///   An integer identifier with the winner
   /// </param>
  public int winner () {
    if (currentPlayer == WHITE)
      if (notNull (legalMoves ()))
        return WHITE;
      else
        return BLACK;
    else if (notNull (legalMoves ()))
        return BLACK;
      else
        return WHITE;
  }
  
  

   /// <sumary>
   ///   Removes a piece from the board between from and to
   /// </sumary>
   /// <param name="from">
   ///   Starting position
   /// </param>
   /// <param name="to">
   ///   Ending position
   /// </param>
  private void clearPiece (int from, int to) {
    int fromLine = posToLine (from);
    int fromCol = posToCol (from);
    int toLine = posToLine (to);
    int toCol = posToCol (to);

    int i, j;

    if (fromCol > toCol)
      i = -1;
    else
      i = 1;


    if (fromLine > toLine)
      j = -1;
    else
      j = 1;



    fromCol += i;
    fromLine += j;
    
    while (fromLine != toLine && fromCol != toCol) {
      int pos = colLineToPos (fromCol, fromLine);
      int piece = pieces [pos];

      if ((piece & ~KING) == WHITE)
        whitePieces--;
      else if ((piece & ~KING) == BLACK)
        blackPieces--;

      pieces [pos] = EMPTY;
      fromCol += i;
      fromLine += j;
    }
  }
  

   /// <sumary>
   ///   Sets the board ready to a new game
   /// </sumary>
  public void clearBoard () {
    int i;
    

    whitePieces = 12;
    blackPieces = 12;

    currentPlayer = BLACK;
    
    for (i = 0; i < 12; i++)
      pieces [i] = BLACK;

    for (i = 12; i < 20; i++)
      pieces [i] = EMPTY;

    for (i = 20; i < 32; i++)
      pieces [i] = WHITE;
  }

   /// <sumary>
   ///   Verifies if the argument is even
   /// </sumary>
   /// <param name="value">
   ///   A numeric value
   /// </param>
   /// <value>
   ///   true if value is even
   /// </value>
  private bool isEven (int value) {
    return value % 2 == 0;
  }


   /// <sumary>
   ///   Converts a column/line into a checkers position
   /// </sumary>
   /// <param name="col">
   ///   Checker board column (0-7)
   /// </param>
   /// <param name="line">
   ///   Checker board line (0-7)
   /// </param>
   /// <value>
   ///   The checkers board position
   /// </value>
  private int colLineToPos (int col, int line) {
    if (isEven (line))
      return line * 4 + (col - 1) / 2;
    else
      return line * 4 + col/ 2;
  }


   /// <sumary>
   ///   Converts the the position into a row
   /// </sumary>
   /// <param name="value">
   ///   The checkers board position
   /// </param>
   /// <value>
   ///   The row value for the checkers board position
   /// </value>
  private int posToLine (int value) {
    return value / 4;
  }


   /// <sumary>
   ///   Converts the the position into a column
   /// </sumary>
   /// <param name="value">
   ///   The checkers board position
   /// </param>
   /// <value>
   ///   The column value for the checkers board position
   /// </value>
  private int posToCol (int value) {
    return (value % 4) * 2 + ((value / 4) % 2 == 0 ? 1 : 0);
  }
  
 } 
    
  











