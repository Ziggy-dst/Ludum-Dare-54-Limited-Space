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
        GameManager.Instance.OnDragging += SetGridState;
        GameManager.Instance.OnRelease += SetGridState;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnDragging -= SetGridState;
        GameManager.Instance.OnRelease -= SetGridState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetGridState()
    {
        viewPortGrid?.gameObject.SetActive(!viewPortGrid.gameObject.activeSelf);
    }
}
