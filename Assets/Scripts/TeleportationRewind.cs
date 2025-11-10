using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportationRewind : MonoBehaviour
{
    [SerializeField]
    InputActionReference teleportInput;
    [SerializeField]
    Transform xrRig;

    private List<Vector3> m_LastRecordedPositions;
    private bool m_WaitForRelease;

    private void Start()
    {
        m_LastRecordedPositions = new List<Vector3>();
        m_WaitForRelease = false;
    }

    private void Update()
    {
        Vector2 teleport = teleportInput.action.ReadValue<Vector2>();

        if (m_WaitForRelease)
        {
            if (teleport == Vector2.zero)
                m_WaitForRelease = false;
        }
        else if (teleport.y > 0)
        {
            m_LastRecordedPositions.Add(xrRig.position);
            m_WaitForRelease = true;
        }
        else if (teleport.y < 0)
        {
            if (m_LastRecordedPositions.Count == 0) return;
            Vector3 oldPos = m_LastRecordedPositions[m_LastRecordedPositions.Count - 1];
            m_LastRecordedPositions.RemoveAt(m_LastRecordedPositions.Count-1);
            xrRig.position = oldPos;
            m_WaitForRelease = true;
        }
    }
}
