using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private Environment environment;

    public Material clicked;
    public Material unclicked;

    public void ButtonClicked()
    {
        environment.GenerateEnvironmentWithDelay(1f);
        transform.GetComponent<MeshRenderer>().material = clicked;

        StopAllCoroutines();
        StartCoroutine(SetToUnclick());
    }

    private IEnumerator SetToUnclick()
    {
        yield return new WaitForSeconds(1f);
        transform.GetComponent<MeshRenderer>().material = unclicked;
    }
}
