using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private Transform testTransform;
    [SerializeField] private LayerMask mouseColliderLayerMask;
    private GridXZ<GridObject> grid;

    private void Awake() {
        int gridWidth  = 10;
        int gridHeight = 10;
        float cellSize = 10f;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridXZ<GridObject> gameObject, int x, int z) => new GridObject(gameObject, x, z));
    }

    public class GridObject {
        private GridXZ<GridObject> grid;
        private int x;
        private int z;

        public GridObject(GridXZ<GridObject> grid, int x, int z) {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public override string ToString()
        {
            return x + ", " + z;
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var coordinates = grid.GetXZ(GridUtils.GetMouseWorldPosition3d(mouseColliderLayerMask));
            Instantiate(testTransform, grid.GetWorldPosition(coordinates.x, coordinates.z), Quaternion.identity);
        }
    }
}
