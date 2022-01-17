using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAllClues : MonoBehaviour
{
    public GameObject[] clues;
    public void CloseAllOpenClues()
    {
        foreach(var clue in clues)
        {
            if (clue.activeInHierarchy)
                clue.SetActive(false);
        }
    }
}
