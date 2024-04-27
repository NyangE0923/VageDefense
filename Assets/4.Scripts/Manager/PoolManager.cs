using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // �������� ������ ����, Ǯ ����� �ϴ� ����Ʈ 1:1 ����� ����
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; // pool�� ��� �迭 �ʱ�ȭ

        for (int i = 0; i < pools.Length; i++)        // ������ ����Ʈ�� ��ȸ�ϸ鼭 �ʱ�ȭ
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ������ Ǯ�� ��Ȱ��ȭ �� ���� ������Ʈ ����
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                // �߰��ϸ� select ������ �Ҵ��Ѵ�.
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // �� ã������ ���Ӱ� �����ϰ� select ������ �Ҵ�
        if (!select)
        {
            select = Instantiate(prefabs[index], transform); //�ν�����â�� ����ϰ� �ϱ� ���� �ڽ����� ����
            pools[index].Add(select);
        }

        return select;
    }
}
