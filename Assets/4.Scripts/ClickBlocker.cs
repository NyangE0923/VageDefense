using UnityEngine;
using UnityEngine.EventSystems;

public class ClickBlocker : MonoBehaviour
{
    void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���Ǿ����� Ȯ��
        if (Input.GetMouseButtonDown(0))
        {
            // UI ���� ���콺 �����Ͱ� ������ Ŭ�� ó��
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // Ŭ�� ó�� �ڵ带 ���⿡ �߰�
            }
        }
    }
}
