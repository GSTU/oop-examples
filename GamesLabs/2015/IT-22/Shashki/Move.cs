/// <sumary>
///  Used to represent a player game move
/// </sumary>
internal class Move {
  // Departure board house
  private int from;

  // Target board house
  private int to;


  /// <sumary>
  ///  Initializes the class
  /// </sumary>
  /// <param name="moveFrom">
  ///   Board house where the move has
  ///  initiated.
  /// </param>
  /// <param name="moveTo">
  ///   Board house where the move has
  ///  initiated.
  /// </param>
  public Move (int moveFrom, int moveTo) {
    from = moveFrom;
    to = moveTo;
  }

  /// <sumary>
  ///  from property
  /// </sumary>
  /// <value>
  ///  An integer that gives the from house used
  /// in the game move.
  /// </value>
  public int getFrom () {
    return from;
  }
  
  /// <sumary>
  ///  to property
  /// </sumary>
  /// <value>
  ///  An integer that gives the to house used
  /// in the game move.
  /// </value>
  public int getTo () {
    return to;
  }


  /// <sumary>
  ///  Returns a string representation of the class
  /// </sumary>
  public override string ToString () {
    return "(" + from + "," + to + ")";
  }
 }
