using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField]
    private GameObject output_tiles;
    [SerializeField]
    private InteractableGrid grid;

    private bool m_FirstGeneration = true;

    void Start()
    {
        GenerateEnvironment();
    }

    private void GenerateEnvironment()
    {
        if (m_FirstGeneration)
        {
            m_FirstGeneration = false;
            if (output_tiles.transform.childCount == 0)
                transform.GetChild(0).GetComponent<Wfc>().GenerateMap();
            grid.GenerateInteractableGrid(1f);
        }
        else
        {
            transform.GetChild(0).GetComponent<Wfc>().GenerateFrom();
        }
    }

    public void GenerateEnvironmentWithDelay(float delay)
    {
        StopAllCoroutines();
        StartCoroutine(GenerateAfterSeconds(delay));
    }

    private IEnumerator GenerateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GenerateEnvironment();
    }
}
