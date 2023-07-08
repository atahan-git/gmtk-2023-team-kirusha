using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Grid<TGridObject>
{

    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width, height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;

    public Grid(int _width, int _height, float _cellSize, Vector3 _originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {

        width = _width;
        height = _height;
        originPosition = _originPosition;
        cellSize = _cellSize;

        gridArray = new TGridObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x,y] = createGridObject(this, x, y);
            }
        }

        bool showDebug = true;
        if (showDebug)
        {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    debugTextArray[x, y] = CreateWorldText(null, gridArray[x, y]?.ToString(), GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center, 5000);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

            OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) => {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public Vector3 GetWorldPosition(int _x, int _y)
    {
        return new Vector3(_x, _y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 _worldPosition, out int _x, out int _y)
    {
        _x = Mathf.FloorToInt((_worldPosition - originPosition).x / cellSize);
        _y = Mathf.FloorToInt((_worldPosition - originPosition).y / cellSize);
    }

    public void SetGridObject(int _x, int _y, TGridObject _value)
    {
        if (_x >= 0 && _y >= 0 && _x < width && _y < height)
        {
            gridArray[_x, _y] = _value;
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = _x, y = _y });
        }
    }

    public void TriggerGridObjectChanged(int _x, int _y)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = _x, y = _y });
    }

    public void SetGridObject(Vector3 _worldPosition, TGridObject _value)
    {
        int x, y;
        GetXY(_worldPosition, out x, out y);
        SetGridObject(x, y, _value);
    }

    public TGridObject GetGridObject(int _x, int _y)
    {
        if (_x >= 0 && _y >= 0 && _x < width && _y < height)
        {
            return gridArray[_x, _y];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 _worldPosition)
    {
        int x, y;
        GetXY(_worldPosition, out x, out y);
        return GetGridObject(x, y);
    }




    static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
       GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
       Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

}
