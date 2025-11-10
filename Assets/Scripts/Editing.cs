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

    public InteractionLayerMask[] layerMasks;

    [SerializeField]
    private XRRayInteractor m_Ray;  
    private int m_LayerMask;

    private bool m_WaitForRelease;

    private void Start()
    {
        m_LayerMask = 0;
        m_WaitForRelease = false;
    }

    private void Update()
    {
        Vector2 input = m_SwitchLayer.action.ReadValue<Vector2>();
        if (m_WaitForRelease)
        {
            if (input == Vector2.zero)
                m_WaitForRelease = false;
        }
        else if (input.x > 0)
        {
            m_WaitForRelease = true;
            m_LayerMask = (m_LayerMask == 2 ? m_LayerMask : m_LayerMask + 1);
        }
        else if (input.x < 0)
        {
            m_WaitForRelease = true;
            m_LayerMask = (m_LayerMask == 0 ? m_LayerMask : m_LayerMask - 1);
        }
        m_Ray.interactionLayers = layerMasks[m_LayerMask];
    }
}
