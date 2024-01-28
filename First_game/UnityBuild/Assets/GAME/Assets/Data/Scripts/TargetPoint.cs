using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class TargetPoint : MonoBehaviour
{
    public Enemy Enemy { get; private set; }
    public Vector3 Position => transform.position;

    public float ColliderSize { get; private set; }

    private void Awake()
    {
        Enemy = transform.root.GetComponent<Enemy>();
        ColliderSize = GetComponent<SphereCollider>().radius * transform.localScale.x;
    }
}
