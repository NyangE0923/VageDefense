using UnityEngine;
using UnityEngine.EventSystems;

public class ClickBlocker : MonoBehaviour
{
    void Update()
    {
        // 마우스 왼쪽 버튼이 클릭되었는지 확인
        if (Input.GetMouseButtonDown(0))
        {
            // UI 위에 마우스 포인터가 없으면 클릭 처리
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // 클릭 처리 코드를 여기에 추가
            }
        }
    }
}
