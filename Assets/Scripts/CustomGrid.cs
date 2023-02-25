using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid<T>
{
    private int height, width;
    private float scale;
    private T[,] gridValues;
    private List<Vector2> immutableCells;

    public CustomGrid(int height, int width, float scale)
    {
        this.height = height;
        this.width = width;
        this.scale = scale;

        gridValues = new T[width, height];
        immutableCells = new List<Vector2>();
    }

    // Given a world position, get an (x, y) coordinate.
    public bool GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPos.x / scale);
        y = Mathf.FloorToInt(worldPos.z / scale);
        
        if ((x < 0) || (y < 0) || (x >= width) || (y >= height))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Given an (x, y) coordinate, get the value of that cell.
    public T GetCellValue(int x, int y)
    {
        return gridValues[x, y];
    }

    // Given a world position, get the value of the cell in that position.
    public T GetCellValue(Vector3 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        return GetCellValue(x, y);
    }

    // Set the value of the cell corresponding to the given x, y values.
    public void SetCellValue(int x, int y, T value)
    {
        if (!immutableCells.Contains(new Vector2(x, y)))
        {
            gridValues[x, y] = value;
        }
    }

    // Set the value of the cell which the given world position falls into.
    public void SetCellValue(Vector3 worldPos, T value)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        if (!immutableCells.Contains(new Vector2(x, y)))
        {
            gridValues[x, y] = value;
        }
    }

    // Given an (x, y) coordinate, return the world position of the centre of that cell.
    public Vector3 GetGridPos(int x, int y)
    {
        return new Vector3((x * scale) + (scale / 2), 0, (y * scale) + (scale / 2));
    }

    // Given a normal world position, get the centre of whichever grid cell that world position falls into.
    public Vector3 GetNearestGridPos(Vector3 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        return GetGridPos(x, y);
    }

    public void MakeImmutable(int x, int y)
    {
        immutableCells.Add(new Vector2(x, y));
    }

    public void MakeImmutable(Vector3 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        immutableCells.Add(new Vector2(x, y));
    }

    public bool IsImmutable(Vector3 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        if (immutableCells.Contains(new Vector2(x, y)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsImmutable(int x, int y)
    {
        if (immutableCells.Contains(new Vector2(x, y)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RenderGrid(Material tileMaterial)
    {
        for (int i = 0; i < width; i++)
        {
            GameObject plane = renderTile(tileMaterial, i, -1);
            plane.transform.Rotate(new Vector3(90f, 0f, 0f));
            plane.transform.Translate(new Vector3(0f, scale/2, -scale/2));
            plane = renderTile(tileMaterial, i, height + 1);
            plane.transform.Rotate(new Vector3(-90f, 0f, 0f));
            plane.transform.Translate(new Vector3(0f, 1.5f*scale, scale/2));
        }
        for (int i = 0; i < height; i++)
        {
            GameObject plane = renderTile(tileMaterial, -1, i);
            plane.transform.Rotate(new Vector3(0f, 0f, -90f));
            plane.transform.Translate(new Vector3(-scale/2, scale / 2, 0f));
            plane = renderTile(tileMaterial, width + 1, i);
            plane.transform.Rotate(new Vector3(0f, 0f, 90f));
            plane.transform.Translate(new Vector3(scale/2, 1.5f * scale, 0f));
        }

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++)
            {
                renderTile(tileMaterial, i, j);
            }
        }
    }

    private GameObject renderTile(Material tileMaterial, int x, int y)
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.GetComponent<Renderer>().material = tileMaterial;
        float planeSize = scale / 10;
        plane.transform.localScale = new Vector3(planeSize, planeSize, planeSize);
        plane.transform.position = GetGridPos(x, y);
        return plane;
    }
}
