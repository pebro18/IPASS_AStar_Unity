using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using AStar;

public class AStarTests
{

    [UnityTest]
    public IEnumerator AStarTestSimplePath()
    {
        var _GameObject = new GameObject();
        var Grid = _GameObject.AddComponent<Grids>();

        Grid.gridWorldSize = new Vector2(10, 10);
        Grid.nodeRadius = 1;
        Grid.RegenGrid();
        yield return null;

        Node[,] TestGrid = Grid.GetGrid();

        Node StartNode = TestGrid[2, 0];
        Node EndNode = TestGrid[2, 4];

        List<Node> TestPath = AStarCalculation.AStar(TestGrid, StartNode, EndNode);

        List<Node> ExpectedPath = new List<Node> { TestGrid[2,0], TestGrid[2, 1], TestGrid[2, 2], TestGrid[2, 3], TestGrid[2, 4] };
        Assert.AreEqual(ExpectedPath, TestPath);

    }

    [UnityTest]
    public IEnumerator AStarTestComplexPath()
    {
        var _GameObject = new GameObject();
        var Grid = _GameObject.AddComponent<Grids>();
        Grid.UnWalkable = LayerMask.GetMask("Obstacles");
        Grid.gridWorldSize = new Vector2(10, 10);
        Grid.nodeRadius = 1;

        var _ColliderGameObject = new GameObject();
        var _BoxCollider = _ColliderGameObject.AddComponent<BoxCollider>();
        _ColliderGameObject.transform.position = new Vector3(-3, 0, 0);

        _BoxCollider.size = new Vector3(4,1,1);
        _ColliderGameObject.layer = 6;
        yield return null;

        Grid.RegenGrid();


        yield return null;

        Node[,] TestGrid = Grid.GetGrid();

        Node StartNode = TestGrid[2, 0];
        Node EndNode = TestGrid[2, 4];

        List<Node> TestPath = AStarCalculation.AStar(TestGrid, StartNode, EndNode);

        List<Node> ExpectedPath = new List<Node> { TestGrid[2, 0], TestGrid[2, 1], TestGrid[3, 2], TestGrid[2, 3], TestGrid[2, 4] };
        Assert.AreEqual(ExpectedPath, TestPath);
    }

    [UnityTest]
    public IEnumerator AStarTestFailure()
    {

        var _GameObject = new GameObject();
        var Grid = _GameObject.AddComponent<Grids>();

        Grid.gridWorldSize = new Vector2(10, 10);
        Grid.UnWalkable = LayerMask.GetMask("Obstacles");
        Grid.nodeRadius = 1;
        
        var _ColliderGameObject = new GameObject();
        var _BoxCollider = _ColliderGameObject.AddComponent<BoxCollider>();
        _BoxCollider.size = new Vector3(10, 1, 10);
        _ColliderGameObject.layer = 6;


        Grid.RegenGrid();
        yield return null;

        Node[,] TestGrid = Grid.GetGrid();

        Node StartNode = TestGrid[2, 0];
        Node EndNode = TestGrid[2, 4];
        List<Node> TestPath = AStarCalculation.AStar(TestGrid, StartNode, EndNode);

        Assert.AreEqual(null, TestPath);
    }

}
