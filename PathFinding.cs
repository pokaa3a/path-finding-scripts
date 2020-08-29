using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathFinding {
  
  private const int MOVE_STRAIGHT_COST = 10;
  private const int MOVE_DIAGONAL_COST = 14;

  public static List<Vector2> FindPath(int srcX, int srcY,
                                        int dstX, int dstY) {
    PathFindingNode srcNode = new PathFindingNode(srcX, srcY);
    PathFindingNode dstNode = new PathFindingNode(dstX, dstY);

    PathFindingList openList = new PathFindingList();
    openList.Add(srcNode);
    PathFindingList closedList = new PathFindingList();

    srcNode.gCost = 0;
    srcNode.hCost = CalculateDistanceCost(srcNode, dstNode);

    while (openList.Count > 0) {
      PathFindingNode curNode = openList.GetLowestFCostNode();
      // TODO: Using RayCast, if there is no obstacle between
      // curNode and dstNode, then we are done
      if (curNode.x == dstX && curNode.y == dstY) {
        return CalculatePath(curNode);
      }
      openList.Remove(curNode);
      closedList.Add(curNode);

      foreach (Vector2Int vec2 in GetNeighborList(curNode)) {
        if (closedList.Contains(vec2.x, vec2.y)) continue;
        if (openList.Contains(vec2.x, vec2.y)) {
          PathFindingNode neighborNode = openList.At(vec2.x, vec2.y);
          int tentativeCost = curNode.gCost + CalculateDistanceCost(curNode, neighborNode);
          if (tentativeCost < neighborNode.gCost) {
            neighborNode.gCost = tentativeCost;
            neighborNode.hCost = CalculateDistanceCost(neighborNode, dstNode);
            neighborNode.cameFrom = curNode;
          }
        } else {
          PathFindingNode neighborNode = new PathFindingNode(vec2.x, vec2.y);
          neighborNode.gCost = curNode.gCost + CalculateDistanceCost(curNode, neighborNode);
          neighborNode.hCost = CalculateDistanceCost(neighborNode, dstNode);
          neighborNode.cameFrom = curNode;
          openList.Add(neighborNode);
        }
      }
    }
    return null;
  }

  private static List<Vector2> CalculatePath(PathFindingNode dstNode) {
    // TODO: Smooth the path
    List<Vector2> path = new List<Vector2>{new Vector2(dstNode.x, dstNode.y)};
    PathFindingNode curNode = dstNode;
    while (curNode.cameFrom != null) {
      path.Add(new Vector2(curNode.cameFrom.x, curNode.cameFrom.y));
      curNode = curNode.cameFrom;
    }
    path.Reverse();
    return path;
  }

  private static int CalculateDistanceCost(PathFindingNode node1, PathFindingNode node2) {
    int xDistance = Mathf.Abs(node1.x - node2.x);
    int yDistance = Mathf.Abs(node1.y - node2.y);
    int remaining = Mathf.Abs(xDistance - yDistance);
    return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
  }

  private static List<Vector2Int> GetNeighborList(PathFindingNode curNode) {
    List<Vector2Int> neighborList = new List<Vector2Int>();
    // TODO: Take wall corner case into account, i.e. an object cannot pass
    // from (0,0) to (1,1) if (0,1) and (1,0) are walls
    for (int x = -1; x <= 1; x++) {
      for (int y = -1; y <= 1; y++) {
        if (x == 0 && y == 0) continue;
        neighborList.Add(new Vector2Int(curNode.x + x, curNode.y + y));
      }
    }
    return neighborList;
  }
}
