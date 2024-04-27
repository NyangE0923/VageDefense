using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹을 보관할 변수, 풀 담당을 하는 리스트 1:1 관계로 구성
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; // pool을 담는 배열 초기화

        for (int i = 0; i < pools.Length; i++)        // 각각의 리스트도 순회하면서 초기화
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀의 비활성화 된 게임 오브젝트 접근
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                // 발견하면 select 변수에 할당한다.
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // 못 찾았으면 새롭게 생성하고 select 변수에 할당
        if (!select)
        {
            select = Instantiate(prefabs[index], transform); //인스펙터창을 깔끔하게 하기 위해 자식으로 생성
            pools[index].Add(select);
        }

        return select;
    }
}
