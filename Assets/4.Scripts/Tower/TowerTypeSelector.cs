using UnityEngine;
using UnityEngine.UI;

public class TowerTypeSelector : MonoBehaviour
{
    public SelectTower selectTower;
    public Button spamTowerButton;
    public Button cornTowerButton;

    void Start()
    {
        cornTowerButton.onClick.AddListener(OnCornTowerButtonClick);
        spamTowerButton.onClick.AddListener(OnSpamTowerButtonClick);
    }

    private void Update()
    {
        SelectCancel();
    }
    private static void SelectColor(SpriteRenderer sr)
    {
        Color color = sr.color; //가져오고
        color.a = 0.5f; //설정하고
        sr.color = color; //할당한다.
    }

    void OnCornTowerButtonClick()
    {
        selectTower.towerType = SelectTower.TowerType.CornTower;
        SpriteRenderer sr = GameManager.instance.mouse.sr;
        sr.sprite = GameManager.instance.mouse.cornTowerSprite;
        SelectColor(sr);
    }
    void OnSpamTowerButtonClick()
    {
        selectTower.towerType = SelectTower.TowerType.SpamTower;
        SpriteRenderer sr = GameManager.instance.mouse.sr;
        sr.sprite = GameManager.instance.mouse.spamTowerSprite;
        SelectColor(sr);
    }
    void SelectCancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DefaultSelect();
        }
    }
    public void DefaultSelect()
    {
        selectTower.towerType = SelectTower.TowerType.DefalutSelect;
        SpriteRenderer sr = GameManager.instance.mouse.sr;
        sr.sprite = GameManager.instance.mouse.defaultMouseSprite;
        Color color = sr.color;
        color.a = 1f;
        sr.color = color;
    }
}
