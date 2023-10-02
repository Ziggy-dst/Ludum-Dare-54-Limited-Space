using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewportManager : MonoBehaviour
{
    public GridManager viewPortGrid;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnDraggingUI += SetGridState;
        GameManager.Instance.OnReleaseUI += SetGridState;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnDraggingUI -= SetGridState;
        GameManager.Instance.OnReleaseUI -= SetGridState;
    }

    private void SetGridState()
    {
        viewPortGrid?.gameObject.SetActive(!viewPortGrid.gameObject.activeSelf);
    }
}
