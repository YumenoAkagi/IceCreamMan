using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionLoader : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<PlayerMovements>().transform.position = transform.position;
    }
}
