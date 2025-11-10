using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc : MonoBehaviour
{
    public GameObject[] miniPrefabs;

    private int m_BlocSize = 5;
    private GameObject[] m_Tiles;
    private GameObject m_Parent;

    private GameObject m_LinkedGeneratedBloc;

    private void Awake()
    {
        m_Tiles = new GameObject[m_BlocSize * m_BlocSize];
    }

    public void AddTilesToBloc(GameObject bloc, int bloc_index, Transform parent)
    {
        string ext = "_Mini";
        int j = -1;
        for (int i = 0; i < m_BlocSize*m_BlocSize; i++)
        {
            if (i % 5 == 0) j++;
            string mini = bloc.transform.GetChild(i).name;
            mini = mini.Substring(0, mini.Length - 7);
            foreach (GameObject p in miniPrefabs)
            {
                if (p.name == mini + ext)
                {
                    m_Tiles[i] = Instantiate(p, transform.position, transform.rotation);
                    m_Tiles[i].transform.parent = parent;
                    Vector3 tilePos = new Vector3(-2.5f + (i % 5) + 0.5f, 1f, -2.5f + j + 0.5f);
                    m_Tiles[i].transform.position += tilePos;
                    m_Tiles[i].transform.rotation = bloc.transform.GetChild(i).transform.rotation;
                    m_Tiles[i].GetComponent<Tile>().SetParent(transform.gameObject);
                    m_Tiles[i].GetComponent<Tile>().SetGeneratedTile(bloc.transform.GetChild(i).gameObject);
                }
            }
        }
    }

    public void SetLinkedBloc(GameObject bloc)
    {
        m_LinkedGeneratedBloc = bloc;
    }

    public void SetParent(GameObject parent)
    {
        m_Parent = parent;
    }
}
