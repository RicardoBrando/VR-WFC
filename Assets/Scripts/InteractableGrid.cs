using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractableGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject generatedMap;

    public GameObject gridPrefab;
    public GameObject tilePrefab;
    public GameObject[] miniPrefabs;

    public GameObject layer0;
    public GameObject layer1;

    private GameObject m_Map;
    private int m_MapSize = 15;
    public GameObject[] m_Tiles;

    private void Awake()
    {
        m_Tiles = new GameObject[m_MapSize * m_MapSize];
    }

    private void Start()
    {
        m_Map = Instantiate(gridPrefab, layer0.transform.position, Quaternion.identity);
        m_Map.transform.parent = layer0.transform;
        m_Map.transform.position += new Vector3(7.5f, 1f, 7.5f);
    }

    private void Update()
    {
        Vector3 normale = -Vector3.Cross(m_Map.transform.right, m_Map.transform.forward);
        transform.up = normale;
    }

    public void GenerateGrid()
    {
        string ext = "_Mini";
        int j = -1;
        GameObject bloc = generatedMap.transform.GetChild(0).GetChild(0).gameObject;
        for (int i = 0; i < m_MapSize * m_MapSize; i++)
        {
            if (i % 15 == 0) j++;
            string mini = bloc.transform.GetChild(i).name;
            mini = mini.Substring(0, mini.Length - 7);
            foreach (GameObject p in miniPrefabs)
            {
                if (p.name == mini + ext)
                {
                    m_Tiles[i] = Instantiate(tilePrefab, transform.position, transform.rotation);
                    m_Tiles[i].transform.parent = layer1.transform;
                    m_Tiles[i].GetComponent<Tile>().SetParent(transform.gameObject);
                    m_Tiles[i].GetComponent<Tile>().SetGeneratedTile(bloc.transform.GetChild(i).gameObject);
                    GameObject tile = Instantiate(p, transform.position, transform.rotation);
                    tile.transform.parent = m_Tiles[i].transform;
                    m_Tiles[i].GetComponent<Tile>().SetGeneratedTile(tile);
                    Vector3 tilePos = new Vector3((i % 15) + 0.5f, 0f, j + 0.5f);
                    m_Tiles[i].transform.position += tilePos;
                    m_Tiles[i].transform.rotation = bloc.transform.GetChild(i).transform.rotation;
                }
            }
        }
    }

    public void GenerateInteractableGrid(float time)
    {
        StopAllCoroutines();
        StartCoroutine(GenerateAfterSeconds(time));
    }

    private void ClearGrid()
    {
        for(int i=0; i<layer1.transform.childCount; i++)
        {
            Destroy(layer1.transform.GetChild(i).gameObject);
            m_Tiles[i] = null;
        }
    }

    IEnumerator GenerateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ClearGrid();
        GenerateGrid();
    }
}
