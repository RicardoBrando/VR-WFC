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

    public GameObject ghostPrefab;

    private List<Vector3> m_LastRecordedPositions;
    private List<GameObject> m_GameObjects;
    private List<GameObject> m_GameObjectsLinks;
    private GameObject m_LastObjectLink;

    private bool m_WaitForRelease;

    private void Start()
    {
        m_LastRecordedPositions = new List<Vector3>();
        m_GameObjects = new List<GameObject>();
        m_GameObjectsLinks = new List<GameObject>();
        m_LastObjectLink = null;

        m_WaitForRelease = false;
    }

    private void FixedUpdate()
    {
        UpdateLineEndPosition();
    }

    private void Update()
    {
        Vector2 teleport = teleportInput.action.ReadValue<Vector2>();

        if (m_WaitForRelease)
        {
            if (teleport == Vector2.zero)
            {
                m_WaitForRelease = false;
            }
                
        }
        else if (teleport.y > 0)
        {
            m_LastRecordedPositions.Add(xrRig.position);

            GameObject go = Instantiate(ghostPrefab, xrRig.position, Quaternion.identity);
            m_GameObjects.Add(go);

            if(m_LastRecordedPositions.Count > 1)
            {
                GameObject lr = CreateLineRenderer(m_LastRecordedPositions[m_LastRecordedPositions.Count - 2], m_LastRecordedPositions[m_LastRecordedPositions.Count - 1]);
                m_GameObjectsLinks.Add(lr);
            }

            if(m_LastObjectLink != null) Destroy(m_LastObjectLink);
            m_LastObjectLink = CreateLineRenderer(m_LastRecordedPositions[m_LastRecordedPositions.Count - 1], xrRig.position);

            m_WaitForRelease = true;
        }
        else if (teleport.y < 0)
        {
            if (m_LastRecordedPositions.Count == 0) return;
            Vector3 oldPos = m_LastRecordedPositions[m_LastRecordedPositions.Count - 1];
            m_LastRecordedPositions.RemoveAt(m_LastRecordedPositions.Count-1);
            xrRig.position = oldPos;

            if (m_GameObjectsLinks.Count > 0)
            {
                Destroy(m_GameObjectsLinks[m_GameObjectsLinks.Count - 1]);
                m_GameObjectsLinks.RemoveAt(m_GameObjectsLinks.Count - 1);
            }

            if (m_GameObjects.Count > 0)
            {
                Destroy(m_GameObjects[m_GameObjects.Count - 1]);
                m_GameObjects.RemoveAt(m_GameObjects.Count - 1);
            }

            if (m_LastObjectLink != null) Destroy(m_LastObjectLink);
            if (m_LastRecordedPositions.Count > 0)
                m_LastObjectLink = CreateLineRenderer(m_LastRecordedPositions[m_LastRecordedPositions.Count - 1], xrRig.position);

            m_WaitForRelease = true;
        }
    }

    private GameObject CreateLineRenderer(Vector3 from, Vector3 to)
    {
        //For creating line renderer object
        GameObject lineRenderer = new GameObject("Line");
        lineRenderer.AddComponent<LineRenderer>();
        lineRenderer.GetComponent<LineRenderer>().startColor = Color.blue;
        lineRenderer.GetComponent<LineRenderer>().endColor = Color.blue;
        lineRenderer.GetComponent<LineRenderer>().startWidth = 0.1f;
        lineRenderer.GetComponent<LineRenderer>().endWidth = 0.1f;
        lineRenderer.GetComponent<LineRenderer>().positionCount = 2;
        lineRenderer.GetComponent<LineRenderer>().useWorldSpace = true;

        //For drawing line in the world space, provide the x,y,z values
        lineRenderer.GetComponent<LineRenderer>().SetPosition(0, from + new Vector3(0f, .5f, 0f)); //x,y and z position of the starting point of the line
        lineRenderer.GetComponent<LineRenderer>().SetPosition(1, to + new Vector3(0f, .5f, 0f)); //x,y and z position of the end point of the line

        return lineRenderer;
    }

    private void UpdateLineEndPosition()
    {
        if (m_LastObjectLink == null) return;
        m_LastObjectLink.GetComponent<LineRenderer>().SetPosition(1, xrRig.position + new Vector3(0f, .5f, 0f));
    }
}
