using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class FreezeTowerTrigger : MonoBehaviour
{
    public event Action<TargetPoint> Entered;
    public event Action<TargetPoint> Exited;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out TargetPoint tragetPoint))
        {
            Entered?.Invoke(tragetPoint);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out TargetPoint tragetPoint))
        {
            Exited?.Invoke(tragetPoint);
        }
    }
}
