using UnityEngine;
using UnityEngine.EventSystems;

public class CreateTower : MonoBehaviour
{
    SelectTower selectTower;
    TowerTypeSelector towerTypeSelector;
    public TowerStats[] towerStats;
    public LayerMask towerLayer;
    Vector2 currentMousePos;

    private void Start()
    {
        selectTower = GetComponent<SelectTower>();
        towerTypeSelector = GetComponent<TowerTypeSelector>();
    }

    public void Update()
    {
        TowerSpawn();
    }

    public void TowerSpawn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
                currentMousePos = mousePosition;

                Collider2D hitCollider = Physics2D.OverlapPoint(currentMousePos, towerLayer);
                if (hitCollider == null)
                {
                    Debug.Log("설치 가능한 위치입니다.");

                    switch (selectTower.towerType)
                    {
                        case SelectTower.TowerType.SpamTower:
                            {
                                SpawnTower(5, 0);

                                towerTypeSelector.DefaultSelect();
                                break;
                            }

                        case SelectTower.TowerType.CornTower:
                            {
                                //자원 제한
                                SpawnTower(4, 1);

                                towerTypeSelector.DefaultSelect();
                                break;
                            }
                    }
                }
                else
                {
                    Debug.Log("설치 불가능한 위치입니다.");
                }
            }
        }
    }
    private void SpawnTower(int poolNumber, int statNumber)
    {
        //자원 제한 현재 유저가 가지고 있는 자원과 요구치를 비교
        if (GameManager.instance.vitamin >= towerStats[statNumber].vitamin && GameManager.instance.mineral >= towerStats[statNumber].mineral)
        {
            //타워를 마우스의 위치에 생성
            GameObject subTower = GameManager.instance.pool.Get(poolNumber);
            subTower.transform.position = currentMousePos;
            subTower.GetComponent<SubTower>().Init(towerStats[statNumber]);

            //자원 차감
            GameManager.instance.vitamin -= towerStats[statNumber].vitamin;
            GameManager.instance.mineral -= towerStats[statNumber].mineral;
        }
        else
        {
            Debug.Log("자원이 부족합니다.");
        }
    }

    [System.Serializable]
    public class TowerStats
    {
        public string name;
        public float maxHealth;
        public float damage;
        public int vitamin;
        public int mineral;
        public int dietaryFiber;
    }
}
