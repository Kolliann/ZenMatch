using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject WinPanel;
    public GameObject KOPanel;

    private void Awake()
    {
        ResetUI();
    }

    public void ResetUI()
    {
        WinPanel.gameObject.SetActive(false);
        KOPanel.gameObject.SetActive(false);
    }
}
