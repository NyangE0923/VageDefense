using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] buttons;

    public void Lose()
    {
        buttons[0].SetActive(true);
    }
    public void Win()
    {
        buttons[1].SetActive(true);
    }
}
