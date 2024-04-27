using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelectTest : MonoBehaviour
{
    SpriteRenderer sr;
    Vector2 currentMousePos;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.CompareTag("Enemy"))
            {
                sr.color = Color.red;
                //공격 로직 메소드 호출
                GameManager.Instance.pool.Get(2);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if( collision != null)
        {
            if (collision.CompareTag("Enemy"))
            {
                sr.color = Color.white;
            }
        }
    }
}
