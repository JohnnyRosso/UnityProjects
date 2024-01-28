using System;
using UnityEngine;

[SelectionBase]
public class FreezeTower : Tower
{
    [SerializeField, Range(0.1f, 0.99f)] private float _freezeModifier;
    [SerializeField, Range(1.5f, 5f)] private float _freezeRadius = 1.5f;

    [SerializeField] private FreezeTowerTrigger _freezeTrigger;
    private SphereCollider trigger;



    private void Awake()
    {
        _freezeTrigger.Entered += OnTargetEntered;
        _freezeTrigger.Exited += OnTargetExited;
        SphereCollider trigger = _freezeTrigger.GetComponent<SphereCollider>();
        if (trigger != null)
        {
            trigger.radius = _freezeRadius;
        }
    }

    public void Update()
    {
        if (trigger == null)
        {
            trigger = _freezeTrigger.GetComponent<SphereCollider>();
        }
        else { trigger.radius = _freezeRadius; }
    }

    private void OnTargetEntered(TargetPoint targetPoint)
    {
        targetPoint.Enemy.ModifySpeed(_freezeModifier);
    }

    private void OnTargetExited(TargetPoint targetPoint)
    {
        targetPoint.Enemy.ModifySpeed(1f);
    }

    private void OnDrawGizmosSelected()
    {
        if (trigger == null)
        {
            trigger = _freezeTrigger.GetComponent<SphereCollider>();
        }
        else { trigger.radius = _freezeRadius; }

        Gizmos.color = Color.green;
        Vector3 position = transform.localPosition;
        position.y += 0.01f;
        Gizmos.DrawWireSphere(position, _freezeRadius);
    }
}
