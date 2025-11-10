using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractableGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject generatedMap;

    public GameObject gridPrefab;
    public GameObject blocPrefab;
    public GameObject tilePrefab;
    public GameObject[] miniPrefabs;

    public GameObject layer0;
    public GameObject layer1;

    private GameObject m_Grid;
    private int m_BlocCount = 9;
    private GameObject[] m_Blocs;

    private void Awake()
    {
        m_Blocs = new GameObject[m_BlocCount];
    }

    private void Start()
    {
        m_Grid = Instantiate(gridPrefab, transform.position, transform.rotation);
        m_Grid.transform.parent = layer0.transform;
        m_Grid.transform.position += new Vector3(7.5f, 0f, 7.5f);

        StopAllCoroutines();
        StartCoroutine(GenerateAfterSeconds(1f));
    }

    public void AddBlocToGrid()
    {
        int j = -1;
        for (int i = 0; i < m_BlocCount; i++)
        {
            if (i % 3 == 0) j++;
            m_Blocs[i] = Instantiate(blocPrefab, transform.position, transform.rotation);
            m_Blocs[i].transform.parent = layer1.transform;
            Vector3 blocPos = new Vector3(5f * (i % 3) + 2.5f, 1f, 5f * j + 2.5f);
            m_Blocs[i].transform.position += blocPos;
            m_Blocs[i].GetComponent<Bloc>().SetParent(m_Grid);
            m_Blocs[i].GetComponent<Bloc>().miniPrefabs = miniPrefabs;
            m_Blocs[i].GetComponent<Bloc>().AddTilesToBloc(generatedMap.transform.GetChild(i).GetChild(0).GetChild(0).gameObject, i, m_Blocs[i].transform);
            m_Blocs[i].GetComponent<Bloc>().SetLinkedBloc(generatedMap.transform.GetChild(i).gameObject);
        }
    }

    IEnumerator GenerateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        AddBlocToGrid();
    }
}
