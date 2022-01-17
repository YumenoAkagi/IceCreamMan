using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClueSystem : MonoBehaviour
{
    bool openCluePanel = false;
    public GameObject cluePanel;
    public CloseAllClues closeAllCluesSys;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            openCluePanel = !openCluePanel;
            closeAllCluesSys.CloseAllOpenClues();
            cluePanel.SetActive(openCluePanel);
        }
    }
}
