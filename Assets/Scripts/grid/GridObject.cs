using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object-definition of the object that is placed on the Grid
public class GridObject
{
    // Reference to the grid | contains a GridObject on each grid-position
    private GridXZ<GridObject> grid;
    private int x;
    private int z;
    private PlacedTower placedTower;

    // constructor
    public GridObject(GridXZ<GridObject> grid, int x, int z) {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }


    public void SetPlacedTower(PlacedTower placedTower) {
        this.placedTower = placedTower;
        grid.TriggerGridObjectChanged(x, z);
    }

    public void ClearPlacedTower() {
        this.placedTower = null;
        grid.TriggerGridObjectChanged(x, z);
    }

    public PlacedTower GetPlacedTower() {
        return this.placedTower;
    }

    public bool CanBuild() {
        return placedTower == null;
    }

    public (int x, int z) GetPosition() {
        return (x, z);
    }

    public override string ToString()
    {
        return x + ", " + z + "\n" + placedTower;
    }
}