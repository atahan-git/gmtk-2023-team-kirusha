using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding 
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;
    public Pathfinding(int _width, int _height)
    {
        grid = new Grid<PathNode>(_width, _height, 10f,Vector3.zero,(Grid<PathNode> g,int x,int y)
               => new PathNode(g,x,y));
    }

    public Grid<PathNode> GetGrid()
    {
        return grid;
    }

    public List<PathNode> FindPath(int _startX, int _startY, int _endX, int _endY)
    {
        PathNode startNode = grid.GetGridObject(_startX, _startY);
        PathNode endNode = grid.GetGridObject(_endX, _endY);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for(int x = 0; x < grid.GetWidth(); x++) 
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }
        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while(openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if(currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(PathNode neightbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neightbourNode)) continue;

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neightbourNode);

                if(tentativeGCost < neightbourNode.gCost)
                {
                    neightbourNode.cameFromNode = currentNode;
                    neightbourNode.gCost = tentativeGCost;
                    neightbourNode.hCost = CalculateDistanceCost(neightbourNode,endNode);
                    neightbourNode.CalculateFCost();

                    if(!openList.Contains(neightbourNode))
                    {
                        openList.Add(neightbourNode);   
                    }
                }
            }
        }

        // out of nodes on openList
        return null;
    }

    private List<PathNode> GetNeighbourList(PathNode _currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (_currentNode.x - 1 >= 0)
        {
            neighbourList.Add(GetNode(_currentNode.x - 1, _currentNode.y));
            if (_currentNode.y - 1 >= 0) neighbourList.Add(GetNode(_currentNode.x - 1, _currentNode.y - 1));
            if (_currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(_currentNode.x - 1, _currentNode.y + 1));
        }
        if (_currentNode.x + 1 < grid.GetWidth())
        {
            neighbourList.Add(GetNode(_currentNode.x + 1, _currentNode.y));
            if (_currentNode.y - 1 >= 0) neighbourList.Add(GetNode(_currentNode.x + 1, _currentNode.y - 1));
            if (_currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(_currentNode.x + 1, _currentNode.y + 1));
        }
        if (_currentNode.y - 1 >= 0) neighbourList.Add(GetNode(_currentNode.x, _currentNode.y - 1));
        if (_currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(_currentNode.x, _currentNode.y + 1));

        return neighbourList;
    }
    private PathNode GetNode(int x, int y) 
    {
        return grid.GetGridObject(x, y);
    }

    private List<PathNode> CalculatePath(PathNode _endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(_endNode);
        PathNode currentNode = _endNode;
        while(currentNode.cameFromNode != null) 
        {
            path.Add(currentNode.cameFromNode); 
            currentNode = currentNode.cameFromNode; 
        }
        path.Reverse();

        return path;
    }

   private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for(int i = 1; i < pathNodeList.Count; i++) 
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost) 
            {
                lowestFCostNode = pathNodeList[i];
            }
        }

        return lowestFCostNode; 
    }

}
