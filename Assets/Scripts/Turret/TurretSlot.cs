using UnityEngine;

public class TurretSlot : MonoBehaviour
{
    [Header("Turret Info")] 
    [SerializeField] private string turretName = "Arrow Tower";
    [SerializeField] private int turretCost = 1; 
    
    [Header("Settings")]
    [SerializeField] private GameObject towerPrefab;      // 생성할 Arrow Tower 프리팹
    [SerializeField] private GameObject highlightVisual;  // 마우스 올렸을 때 켜질 흰색 원 테두리
    [SerializeField] private Vector3 spawnOffset = Vector3.up * 0.5f; // 타워가 지면에 파묻히지 않게 높이 보정

    private bool isOccupied = false;

    private void Start()
    {
        if (highlightVisual != null)
            highlightVisual.SetActive(false);
    }

    // 마우스가 슬롯 콜라이더 위로 올라왔을 때
    private void OnMouseEnter()
    {
        if (isOccupied) return;

        if (highlightVisual != null)
            highlightVisual.SetActive(true);
    }

    // 마우스가 슬롯 밖으로 나갔을 때
    private void OnMouseExit()
    {
        if (highlightVisual != null)
            highlightVisual.SetActive(false);
    }

    // 슬롯을 클릭했을 때
    private void OnMouseDown()
    {
        if (isOccupied) return;

        Debug.Log("OnMouseDown 진입");

        if (TurretInfoUI.Instance != null)
        {
            TurretInfoUI.Instance.ShowTowerInfo(this, turretName, turretCost);
        }
    }

    public void BuildTower()
    {
        Debug.Log($"[BuildTower 실행] 슬롯: {gameObject.name}, 프리팹: {towerPrefab?.name}", this);
        if (towerPrefab == null) return;

        // 슬롯 위치 + 오프셋에 타워 생성
        Instantiate(towerPrefab, transform.position + spawnOffset, Quaternion.identity);
        
        isOccupied = true;

        if (highlightVisual != null)
            highlightVisual.SetActive(false);
    }
}