using UnityEngine;

public class TurretData : MonoBehaviour
{
    // 터렛 종류, 데미지, 사거리, 설치 비용
    public TurretType Type;
    
    public int InstallCost;
    public int DeleteCost;
    
    public float Damage;
    public float Range;
    public float AttackInterval;
}
