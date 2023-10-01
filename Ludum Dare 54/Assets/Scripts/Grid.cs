using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    [HideInInspector] public bool isOccupied = false;

    public enum gridState
    {
        Hover,
        Occupied
    }

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: change color when hovering

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("PlayableUI"))
        {
            _lineRenderer.startColor = Color.red;
            _lineRenderer.endColor = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("PlayableUI"))
        {
            _lineRenderer.startColor = Color.white;
            _lineRenderer.endColor = Color.white;
        }
    }
}
