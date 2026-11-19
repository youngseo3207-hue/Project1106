using UnityEngine;
using TMPro;

public class TurretInfoUI : MonoBehaviour
{
    public static TurretInfoUI Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI towerNameText;
    [SerializeField] private TextMeshProUGUI towerCostText;

    private TurretSlot selectedSlot;

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    public void ShowTowerInfo(TurretSlot slot, string towerName, int towerCost)
    {
        selectedSlot = slot;

        if (towerNameText != null)
            towerNameText.text = towerName;

        if (towerCostText != null)
            towerCostText.text = $"Cost: {towerCost}";

        if (panel != null)
            panel.SetActive(true);
    }

    public void Hide()
    {
        selectedSlot = null;
        if (panel != null)
            panel.SetActive(false);
    }

    // // Build 버튼의 On Click ()에서 연결할 메서드
    // public void OnClickBuild()
    // {
    //     Debug.Log("UI를 클릭함");
    //     if (selectedSlot != null)
    //     {
    //         selectedSlot.BuildTower();
    //     }
    //     else
    //     {
    //         Debug.LogWarning("선택된 TurretSlot이 없습니다.");
    //     }
    //
    //     Hide();
    // }
}