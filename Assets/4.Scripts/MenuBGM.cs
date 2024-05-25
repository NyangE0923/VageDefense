using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGM : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayBgm(true, 0);
    }
}
