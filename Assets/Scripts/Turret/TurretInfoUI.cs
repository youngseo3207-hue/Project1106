using UnityEngine;
using TMPro;

public class TurretInfoUI : MonoBehaviour
{
    public static TurretInfoUI Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject panel;
    [SerializeField] private RectTransform panelRect;
    [SerializeField] private TextMeshProUGUI towerNameText;
    [SerializeField] private TextMeshProUGUI towerCostText;

    [Header("Position Offset")]
    [SerializeField] private Vector2 screenOffset = new Vector2(90f, 20f); // 슬롯 옆 위치 보정

    private TurretSlot selectedSlot;
    private Camera mainCam;

    private void Awake()
    {
        Instance = this;
        mainCam = Camera.main;
        Hide();
    }

    private void LateUpdate()
    {
        if (selectedSlot == null || panel == null || !panel.activeSelf)
            return;

        if (mainCam == null)
            mainCam = Camera.main;

        if (mainCam == null || panelRect == null)
            return;

        Vector3 screenPos = mainCam.WorldToScreenPoint(selectedSlot.transform.position);
        panelRect.position = screenPos + (Vector3)screenOffset;
    }

    public void ShowTowerInfo(TurretSlot slot, string towerName, int towerCost)
    {
        Debug.Log("ShowTowerInfo 실행됨");
        selectedSlot = slot;

        if (towerNameText != null)
            towerNameText.text = towerName;

        if (towerCostText != null)
            towerCostText.text = towerCost.ToString();

        if (panelRect != null && mainCam != null)
        {
            Vector3 screenPos = mainCam.WorldToScreenPoint(slot.transform.position);
            panelRect.position = screenPos + (Vector3)screenOffset;
        }

        if (panel != null)
            panel.SetActive(true);
    }

    public void Hide()
    {
        selectedSlot = null;
        if (panel != null)
            panel.SetActive(false);
    }

    public void OnClickBuild()
    {
        Debug.Log("UI를 클릭함");
        if (selectedSlot != null)
        {
            selectedSlot.BuildTower();
        }
        else
        {
            Debug.LogWarning("선택된 TurretSlot이 없습니다.");
        }

        Hide();
    }
}