using UnityEngine;
using UnityEngine.EventSystems;

public class CreateTower : MonoBehaviour
{
    SelectTower selectTower;
    TowerTypeSelector towerTypeSelector;
    public TowerStats[] towerStats;
    public LayerMask towerLayer;
    Vector2 currentMousePos;

    GameManager gameManager;

    private void Start()
    {
        selectTower = GetComponent<SelectTower>();
        towerTypeSelector = GetComponent<TowerTypeSelector>();

        gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            gameManager.SetTowerStats(towerStats);
        }
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
                    Debug.Log("��ġ ������ ��ġ�Դϴ�.");

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
                                //�ڿ� ����
                                SpawnTower(4, 1);

                                towerTypeSelector.DefaultSelect();
                                break;
                            }
                    }
                }
                else
                {
                    Debug.Log("��ġ �Ұ����� ��ġ�Դϴ�.");
                }
            }
        }
    }
    private void SpawnTower(int poolNumber, int statNumber)
    {
        //�ڿ� ���� ���� ������ ������ �ִ� �ڿ��� �䱸ġ�� ��
        if (GameManager.instance.vitamin >= towerStats[statNumber].vitamin && GameManager.instance.mineral >= towerStats[statNumber].mineral)
        {
            AudioManager.instance.PlaySfx(AudioManager.sfx.TowerCreate);
            //Ÿ���� ���콺�� ��ġ�� ����
            GameObject subTower = GameManager.instance.pool.Get(poolNumber);
            subTower.transform.position = currentMousePos;
            subTower.GetComponent<SubTower>().Init(towerStats[statNumber]);

            //�ڿ� ����
            GameManager.instance.vitamin -= towerStats[statNumber].vitamin;
            GameManager.instance.mineral -= towerStats[statNumber].mineral;
        }
        else
        {
            Debug.Log("�ڿ��� �����մϴ�.");
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
