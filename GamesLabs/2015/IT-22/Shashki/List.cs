using System;
/// <sumary>
///   All collection classes that wish to support
///  enumerations using Java style iterators should
///  implement this interface.
/// </sumary>    
internal interface Enumeration {
    /// <sumary>
    ///   Used to verify if that are more
    ///  elements to iterate.
    /// </sumary>
    /// <value>
    ///  true if there are more elements to iterate, false
    /// otherwise
    /// </value>
    bool hasMoreElements ();

    /// <sumary>
    ///  Returns the current element and advances the iterator.
    /// </sumary>
    /// <value>
    ///  Current element pointed by the iterator
    /// </value>
    object nextElement ();
}

/// <sumary>
///  Thrown when someone tries to call nextElement beyond
///  the end of a collection.
/// </sumary>
internal class NoSuchElementException: Exception {
}

/// <sumary>
///  List nodes
/// </sumary>
internal class ListNode {
  internal ListNode prev, next;
  internal object   value;

  public ListNode (object elem, ListNode prevNode, ListNode nextNode) {
    value = elem;
    prev = prevNode;
    next = nextNode;
  }
}

/// <sumary>
///  Generic lists.
///  The applcation could use ArrayList instead but it
///  is more efficient to use a list. If the ArrayList
///  has used, it had to be constatly changing size.
///  This class doesn't implement the .Net collections
///  interface for legacy reasons. The code was ported
///  from a Java and it has calls to List instances all
///  over the place.
/// </sumary>
internal class List {
  private ListNode head;
  private ListNode tail;
  private int count;

  public List () {
    count = 0;
  }
  
  /// <sumary>
  ///  Adds an element to the list's head
  /// </sumary>
  /// <param name="elem">
  ///  Element to be added
  /// </param>
  public void push_front (object elem) {
    ListNode node = new ListNode (elem, null, head);
    
    if (head != null)
      head.prev = node;
    else
      tail = node;

    head = node;
    count++;
  }

  /// <sumary>
  ///  Adds an element to the list's tail
  /// </sumary>
  /// <param name="elem">
  ///  Element to be added
  /// </param>
  public void push_back (object elem) {
    ListNode node = new ListNode (elem, tail, null);

    if (tail != null)
      tail.next = node;
    else
      head = node;

    tail = node;
    count++;
  }

  /// <sumary>
  ///  Removes an element from the list's head
  /// </sumary>
  /// <value>
  ///  Removed element
  /// </value>
  public object pop_front () {
    if (head == null)
      return null;

    ListNode node = head;
    head = head.next;

    if (head != null)
      head.prev = null;
    else
      tail = null;

    count--;
    return node.value;
  }

  /// <sumary>
  ///  Removes an element from the list's tail
  /// </sumary>
  /// <value>
  ///  Removed element
  /// </value>
  public object pop_back () {
    if (tail == null)
      return null;

    ListNode node = tail;
    tail = tail.prev;

    if (tail != null)
      tail.next = null;
    else
      head = null;

    count--;
    return node.value;
  }

  /// <sumary>
  ///  Validates if the list is empty
  /// </sumary>
  /// <value>
  ///  true if the list is empty, false otherwise
  /// </value>
  public bool isEmpty () {
    return head == null;
  }


  /// <sumary>
  ///  Returns the number of list's elements
  /// </sumary>
  /// <value>
  ///  An integer with the total number of list's elements
  /// </value>
  public int length () {
    return count;
  }

  /// <sumary>
  ///  Adds another list to the list's tail
  /// </sumary>
  /// <param name="other">
  ///  list to be added
  /// </param>
  public void append (List other) {
    ListNode node = other.head;
    
    while (node != null) {
      push_back (node.value);
      node = node.next;
    }
  }
  
  /// <sumary>
  ///  Removes all list's elements
  /// </sumary>
  public void clear () {
    head = tail = null;
  }

  /// <sumary>
  ///  Returns an element from the list's head without
  ///  removing it.
  /// </sumary>
  /// <value>
  ///  List's head
  /// </value>
  public object peek_head () {
    if (head != null)
      return head.value;
    else
      return null;
  }

  /// <sumary>
  ///  Returns an element from the list's tail without
  ///  removing it.
  /// </sumary>
  /// <value>
  ///  List's tail
  /// </value>
  public object peek_tail () {
    if (tail != null)
      return tail.value;
    else
      return null;
  }
  
    
  /// <sumary>
  ///  Verifies if the given element exists in the list.
  /// </sumary>
  /// <param name="elem">
  ///  Element to be searched for in the list
  /// </param>
  /// <value>
  ///  true if the element was found, false otherwise
  /// </value>
  public bool has (object elem) {
    ListNode node = head;

    while (node != null && !node.value.Equals (elem))
      node = node.next;

    return node != null;
  }

  /// <sumary>
  ///  Clones the list, in shallow copy
  /// </sumary>
  /// <value>
  ///  A clone of the instance where the method was
  ///  invoked
  /// </value>
  public object clone () {
    List temp = new List ();
    ListNode node = head;

    while (node != null) {
      //temp.push_back (node.value.clone ());
      temp.push_back (node.value);
      node = node.next;
    }

    return temp;
  }

  /// <sumary>
  ///  Creates a string representation of the list.
  ///  In the form  [elem1, elem2, ... ]
  /// </sumary>
  /// <value>
  ///  A string representation of the list.
  /// </value>
  public override string ToString () {
    string temp = "[";
    ListNode node = head;

    while (node != null) {
      temp += node.value.ToString ();
      node = node.next;
      if (node != null)
        temp += ", ";
    }
    temp += "]";

    return temp;
  }

  /// <sumary>
  ///  List iterator
  /// </sumary>
  class Enum: Enumeration {
    // Current element
    private ListNode node;

    internal Enum (ListNode start) {
      node = start;
    }

    /// <sumary>
    ///   Used to verify if that are more
    ///  elements to iterate.
    /// </sumary>
    /// <value>
    ///  true if there are more elements to iterate, false
    /// otherwise
    /// </value>
    public bool hasMoreElements () {
      return node != null;
    }

    /// <sumary>
    ///  Returns the current element and advances the iterator.
    /// </sumary>
    /// <value>
    ///  Current element pointed by the iterator
    /// </value>
    public object nextElement () {
      Object temp;

      if (node == null)
        throw new NoSuchElementException ();

      temp = node.value;
      node = node.next;

      return temp;
    }
  }

  /// <sumary>
  ///  Returns a list iterator.
  /// </sumary>
  /// <value>
  ///  A list iterator
  /// </value>
  public Enumeration elements () {
    return new Enum (head);
  }
 
 }  
//}

  
