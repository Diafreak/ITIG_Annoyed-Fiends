using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid<HeatMapGridObject> grid;

    private void Start()
    {
        grid = new Grid<HeatMapGridObject>(4, 2, 10f, Vector3.zero, (Grid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 position = GridUtils.GetMouseWorldPosition();
            HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);
            if (heatMapGridObject != null) {
                heatMapGridObject.AddValue(5);
            }
            // grid.AddValue(position, 100, 2, 25);
            // grid.SetValue(GetMouseWorldPosition(), 56);
        }

        if (Input.GetMouseButtonDown(1)) {
            // Debug.Log(grid.GetValue(GetMouseWorldPosition()));
        }
    }

}


public class HeatMapGridObject {
    
    private Grid<HeatMapGridObject> grid;
    private int x;
    private int y;
    private int value;

    public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue) {
        value += addValue;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString() {
        return value.ToString();
    }
}
