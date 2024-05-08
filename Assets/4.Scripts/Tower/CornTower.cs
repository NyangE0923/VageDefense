using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornTower : MonoBehaviour
{
    public Scanner scanner;

    private void Awake()
    {
        scanner = GetComponent<Scanner>();
    }
}
