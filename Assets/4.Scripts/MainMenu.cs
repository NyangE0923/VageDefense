using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Transform buttonScale;
    Vector3 defaultScale;

    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    public void OnClickGameStart()
    {
        Debug.Log("게임시작");
        AudioManager.instance.PlaySfx(AudioManager.sfx.UI);
        SceneManager.LoadScene(1);
    }

    public void OnClickGameQuit()
    {
        AudioManager.instance.PlaySfx(AudioManager.sfx.UI);
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
