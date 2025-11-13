using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wfc : SimpleTiledWFC
{
    public GameObject prefab;
    public void GenerateMap() 
    {
        Generate();
        //AddToRendering(0,0);
        Run();
        //PrintTiles();
        //PrintObmap();
        //PrintRendering();
    }

    private void AddToRendering(int x, int y)
    {
        Vector3 pos = new Vector3(x * gridsize, y * gridsize, 0f);
        GameObject tile = (GameObject)Instantiate(prefab, new Vector3(), Quaternion.identity);
        Vector3 fscale = tile.transform.localScale;
        tile.transform.parent = group;
        tile.transform.localPosition = pos;
        tile.transform.localScale = fscale;
        rendering[x, y] = tile;
    }

    private void PrintObmap()
    {
        foreach(string key in obmap.Keys)
            Debug.Log("Key : " + key + ", value : " + obmap[key]);
    }

    private void PrintTiles()
    {
        int i = 0;
        foreach (string s in model.tiles)
        {
            Debug.Log("Index " + i + " : " + s);
            i++;
        }
    }

    private void PrintRendering()
    {
        for (int y = 0; y < depth; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Debug.Log("rendering["+x+","+y+"] = "+rendering[x, y].name);
            }
        }
    }
}
