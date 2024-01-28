using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerFactory OriginFactory { get; set; }
    [SerializeField, Range(0f, 1000f)] protected float _cost;
    public float Cost => _cost;
}
