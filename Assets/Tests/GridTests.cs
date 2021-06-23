using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridTests
{
    [UnityTest]
    public IEnumerator GridTestsGen20x20()
    {
        var _GameObject = new GameObject();
        var Grid = _GameObject.AddComponent<Grids>();

        Grid.gridWorldSize = new Vector2(20,20);
        Grid.nodeRadius = 1;
        Grid.RegenGrid();
        yield return null;

        Node[,] TestGrid = Grid.GetGrid();
        Node[,] Expected = new Node[10, 10];

        int Expectedcount = Expected.GetLength(0) * Expected.GetLength(1);
        int Testcount = TestGrid.GetLength(0) * TestGrid.GetLength(1);

        Assert.AreEqual(Expectedcount,Testcount);

    }
    [UnityTest]
    public IEnumerator GridTestsCheckUnWalkable()
    {
        var _GridGameObject = new GameObject();
        var Grid = _GridGameObject.AddComponent<Grids>();

        Grid.gridWorldSize = new Vector2(10, 10);
        Grid.UnWalkable = LayerMask.GetMask("Obstacles");
        Grid.nodeRadius = 1;

        var _ColliderGameObject = new GameObject();
        var _BoxCollider = _ColliderGameObject.AddComponent<BoxCollider>();
        _ColliderGameObject.layer = 6;

        Grid.RegenGrid();
        yield return null;

        Node[,] TestGrid = Grid.GetGrid();

        Assert.AreEqual(false, TestGrid[2,2].Walkable);

    }
}
