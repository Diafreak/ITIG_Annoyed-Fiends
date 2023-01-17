using UnityEngine;

// Object-definition of the object that is placed on the Grid
public class GridObject {

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

    public PlacedTower GetTower() {
        return this.placedTower;
    }

    public bool CanBuild() {
        return this.placedTower == null;
    }

    public (int x, int z) GetGridPosition() {
        return (x, z);
    }

    public Vector3 GetWorldPosition() {
        return grid.GetWorldPosition(x, z);
    }
}