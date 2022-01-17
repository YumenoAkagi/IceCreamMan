using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClueSystem : MonoBehaviour
{
    bool openCluePanel = false;
    public GameObject cluePanel;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            openCluePanel = !openCluePanel;

            cluePanel.SetActive(openCluePanel);
        }
    }
}
