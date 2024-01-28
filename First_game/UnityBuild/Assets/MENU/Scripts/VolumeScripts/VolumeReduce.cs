using UnityEngine;

public class VolumeReduce : MonoBehaviour
{
    [SerializeField] private Animator _volumeReduce;

    public void ReduceVolume()
    {
        _volumeReduce = GetComponent<Animator>();
        _volumeReduce.enabled = true;
    }
}
