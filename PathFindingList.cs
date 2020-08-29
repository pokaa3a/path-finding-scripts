using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFindingList {

  private Dictionary<string, PathFindingNode> dict;

  public int Count {get {return dict.Count;}}

  public PathFindingList() {
    dict = new Dictionary<string, PathFindingNode>();
  }
  public bool Contains(int x, int y) {
    return dict.ContainsKey(x.ToString() + "#" + y.ToString());
  }
  public void Add(PathFindingNode node) {
    if (!Contains(node.x, node.y))
      dict.Add(node.x.ToString() + "#" + node.y.ToString(), node);
  }
  public void Remove(PathFindingNode node) {
    if (Contains(node.x, node.y))
      dict.Remove(node.x.ToString() + "#" + node.y.ToString());
  }
  public PathFindingNode At(int x, int y) {
    if (!Contains(x, y)) return null;
    return dict[x.ToString() + "#" + y.ToString()];
  }
  public PathFindingNode GetLowestFCostNode() {
    if (dict.Count == 0) return null;
    PathFindingNode lowestNode = dict.ElementAt(0).Value;
    foreach (KeyValuePair<string, PathFindingNode> kvp in dict) {
      if (kvp.Value.fCost < lowestNode.fCost)
        lowestNode = kvp.Value;
    }
    return lowestNode;
  }
}
