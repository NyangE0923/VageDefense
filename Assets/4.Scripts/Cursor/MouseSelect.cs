using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelect : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite spamTowerSprite;
    public Sprite cornTowerSprite;
    public Sprite defaultMouseSprite;
    public Vector2 currentMousePos { get; private set; }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
        currentMousePos = mousePosition;
        transform.position = mousePosition;
    }
}
