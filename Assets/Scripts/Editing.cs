using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Editing : MonoBehaviour
{
    [SerializeField]
    private InputActionReference m_SwitchLayer;
    [SerializeField]
    private InputActionReference m_Select;
    [SerializeField]
    private InputActionReference m_Generate;
    [SerializeField]
    private InteractableGrid grid;
    [SerializeField]
    private Environment environment;

    public InteractionLayerMask[] layerMasks;

    [SerializeField]
    private XRRayInteractor m_Ray;  
    private int m_LayerMask;
    public GameObject layer0;
    public GameObject layer1;

    private bool m_WaitForStickRelease;
    private bool m_WaitForActivateRelease;
    private bool m_WaitForGenerateRelease;

    private Vector2 m_StickInput;
    private float m_SelectInput;
    private float m_GenerateInput;

    public HashSet<int> tilesToDelete;

    private void Start()
    {
        m_LayerMask = 0;
        m_WaitForStickRelease = false;
        tilesToDelete = new HashSet<int>();
    }

    private void Update()
    {
        ReadInput();

        if (m_WaitForStickRelease)
        {
            if (m_StickInput == Vector2.zero)
                m_WaitForStickRelease = false;
        }
        else if (m_StickInput.x > 0)
        {
            m_WaitForStickRelease = true;
            m_LayerMask = (m_LayerMask == 1 ? m_LayerMask : m_LayerMask + 1);
            for(int i = 0; i<layer0.transform.childCount; i++)
                layer0.transform.GetChild(i).GetComponentInChildren<BoxCollider>().enabled = false;
        }
        else if (m_StickInput.x < 0)
        {
            m_WaitForStickRelease = true;
            m_LayerMask = (m_LayerMask == 0 ? m_LayerMask : m_LayerMask - 1);
            for (int i = 0; i < layer0.transform.childCount; i++)
                layer0.transform.GetChild(i).GetComponentInChildren<BoxCollider>().enabled = true;
        }
        m_Ray.interactionLayers = layerMasks[m_LayerMask];

        if (m_WaitForActivateRelease)
        {
            if (m_SelectInput == 0)
                m_WaitForActivateRelease = false;
        }
        else if (m_SelectInput == 1 && m_LayerMask == 1)
        {
            m_WaitForActivateRelease = true;
            RaycastHit hit;
            if(m_Ray.TryGetCurrent3DRaycastHit(out hit))
            {
                GameObject target = hit.collider.transform.parent.gameObject;
                tilesToDelete.Add(GetObjectIndex(target));
            }
        }

        if (m_WaitForGenerateRelease)
        {
            if (m_GenerateInput == 0)
                m_WaitForGenerateRelease = false;
        }
        else if (m_GenerateInput == 1)
        {
            m_WaitForGenerateRelease = true;
            environment.GenerateEnvironmentWithDelay(3f);
        }
    }

    private void ReadInput()
    {
        m_StickInput = m_SwitchLayer.action.ReadValue<Vector2>();
        m_SelectInput = m_Select.action.ReadValue<float>();
        m_GenerateInput = m_Generate.action.ReadValue<float>();
    }

    private int GetObjectIndex(GameObject go)
    {
        for (int i=0; i < grid.m_Tiles.Length; i++)
        {
            if (grid.m_Tiles[i] == go)
                return i;
        }
        return -1;
    }
}
