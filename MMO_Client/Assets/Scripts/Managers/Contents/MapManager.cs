﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    public Grid CurrentGrid { get; private set; }

    public int MinX { get; set; }
    public int MaxX { get; set; }
    public int MinY { get; set; }
    public int MaxY { get; set; }

    bool[,] _collision;

    public bool CanMove(Vector3Int cellPos)
    {
        if (cellPos.x < MinX || cellPos.x > MaxX)
        {
            return false;
        }
        if (cellPos.y < MinY || cellPos.y > MaxY)
        {
            return false;
        }

        int x = cellPos.x - MinX;
        int y = MaxY - cellPos.y;
        return !_collision[y, x];
    }

    public void LoadMap(int mapId)
    {
        DestroyMap();

        string mapName = "Map_" + mapId.ToString("000");
        GameObject go = Managers.Resource.Instantiate($"Map/{mapName}");
        go.name = "Map";

        GameObject collision = Util.FindChild(go, "Tilemap_Collision", true);
        if (collision != null)
        {
            collision.SetActive(false);
        }

        CurrentGrid = go.GetComponent<Grid>();

        TextAsset mapData = Managers.Resource.Load<TextAsset>($"Map/{mapName}");
        StringReader reader = new StringReader(mapData.text);

        MinX = int.Parse(reader.ReadLine());
        MaxX = int.Parse(reader.ReadLine());
        MinY = int.Parse(reader.ReadLine());
        MaxY = int.Parse(reader.ReadLine());

        int xCount = MaxX - MinX;
        int yCount = MaxY - MinY;
        _collision = new bool[yCount, xCount];

        Debug.Log($"{xCount} {yCount}");
        for (int y = 0; y < yCount; y++)
        {
            string line = reader.ReadLine();
            Debug.Log(line.Length);
            for (int x = 0; x < xCount; x++)
            {
                _collision[y, x] = (line[x] == '1' ? true : false);
            }
        }
    }

    public void DestroyMap()
    {
        GameObject map = GameObject.Find("Map");
        if (map != null)
        {
            GameObject.Destroy(map);
            CurrentGrid = null;
        }
    }
}
