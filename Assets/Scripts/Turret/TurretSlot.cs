using UnityEngine;

public class TurretSlot : MonoBehaviour
{
    [Header("Turret Info")] 
    [SerializeField] private string turretName = "Arrow Tower";
    [SerializeField] private int turretCost = 1; 
    
    [Header("Settings")]
    [SerializeField] private GameObject towerPrefab; 
    [SerializeField] private GameObject highlightVisual;
    [SerializeField] private Vector3 spawnOffset = Vector3.up * 0.5f;

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
        if (towerPrefab == null)
        {
            Debug.LogError($"{gameObject.name}: Tower Prefab이 비어 있음", this);
            return;
        }

        Instantiate(towerPrefab, transform.position + spawnOffset, Quaternion.identity);
        isOccupied = true;

        if (highlightVisual != null)
            highlightVisual.SetActive(false);
    }
}