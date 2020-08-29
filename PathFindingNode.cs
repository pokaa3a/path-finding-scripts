using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingNode {
  
  public int x;
  public int y;
  public PathFindingNode cameFrom;

  private int _gCost;
  private int _hCost;
  private int _fCost;

  // Cost to move from the starting point
  public int gCost {
    get {return _gCost;}
    set {_gCost = value;}
  }
  // Cost to move to the target point
  public int hCost {
    get {return _hCost;}
    set {_hCost = value;}
  }
  public int fCost {
    get {
      _fCost = gCost + hCost; 
      return _fCost;
    }
  }

  public PathFindingNode(int x, int y) {
    this.x = x;
    this.y = y;
    this.gCost = int.MaxValue;
    this.hCost = 0;
    this.cameFrom = null;

  }


}
