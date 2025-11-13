using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Wfc : SimpleTiledWFC
{
    [SerializeField]
    private GameObject m_Editing;

    public GameObject prefab;
    public void GenerateMap() 
    {
        Generate();
        Run();
        //PrintTiles();
        //PrintObmap();
    }

    public void GenerateFrom()
    {
        for (int y = 0; y < depth; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (rendering[x, y] != null)
                {
                    Destroy(rendering[x, y]);
                    rendering[x, y] = null;
                }
            }
        }

        bool[][] waveCopy = new bool[model.FMX * model.FMY][];
        for (int i = 0; i < model.wave.Length; i++)
        {
            waveCopy[i] = new bool[model.T];
            for (int j = 0; j < model.wave[i].Length; j++)
            {
                waveCopy[i][j] = model.wave[i][j];
            }
        }

        DeleteTiles(waveCopy, m_Editing.GetComponent<Editing>().tilesToDelete);

        model.init = false;
        undrawn = true;

        for(int i=0; i<10; i++)
        {
            model.init = false;
            if (RunFrom(seed, iterations, waveCopy))
            {
                Draw();
                break;
            }
        }
    }

    public bool RunFrom(int seed, int limit, bool[][] waveCopy)
    {
        if (!model.init)
        {
            model.init = true;
            model.Clear();
        }

        if (seed == 0)
        {
            model.random = new System.Random();
        }
        else
        {
            model.random = new System.Random(seed);
        }

        for (int y=0; y<model.FMY; y++)
        {
            for (int x=0; x<model.FMX; x++)
            {
                int index = x + y * model.FMX;
                int tileIndex = GetTileIndex(waveCopy, index);
                if(tileIndex != -1) SetFixedTile(x, y, tileIndex);
            }
        }

        for (int l = 0; l < limit || limit == 0; l++)
        {
            bool? result = model.Observe();
            if (result != null) return (bool)result;
            model.Propagate();
        }

        return true;
    }

    private void SetFixedTile(int x, int y, int t)
    {
        int i = x + y * model.FMX;
        // 1. Désactive toutes les autres possibilités pour cette cellule
        for (int t2 = 0; t2 < model.T; t2++)
        {
            if (t2 != t)
            {
                model.Ban(i, t2);
            }
        }
    }

    private int GetTileIndex(bool[][] wave, int index)
    {
        int count = 0;
        int i = -1;
        for (int j = 0; j < wave[index].Length; j++)
        {
            if (wave[index][j] == true)
            {
                count++;
                i = j;
            }
        }
        if (count > 1) return -1;
        return i;
    }

    private void DeleteTiles(bool[][] waveCopy, HashSet<int> tiles)
    {
        List<int> ints = tiles.ToList<int>();
        for (int i = 0; i < ints.Count; i++)
        {
            for (int j = 0; j < waveCopy[i].Length; j++)
            {
                waveCopy[ints[i]][j] = true;
            }
        }
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
