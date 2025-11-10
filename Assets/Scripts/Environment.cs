using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public InteractableGrid interactableGrid;
    void Start()
    {
        for (int i = 0; i < 9; i++)
            transform.transform.GetChild(i).GetComponent<Wfc>().GenerateBloc();
    }
}
