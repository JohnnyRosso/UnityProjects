using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] KeyCode keyOne;
    [SerializeField] KeyCode keyTwo;
    [SerializeField] KeyCode keyThree;
    [SerializeField] KeyCode keyFour;
    [SerializeField] Vector3 directionAD;
    [SerializeField] Vector3 directionWS;

    private void FixedUpdate()
    {
        if (Input.GetKey(keyOne))
        {
            GetComponent<Rigidbody>().velocity += directionAD;
        }

        if (Input.GetKey(keyTwo))
        {
            GetComponent<Rigidbody>().velocity -= directionAD;
        }

        if (Input.GetKey(keyThree))
        {
            GetComponent<Rigidbody>().velocity += directionWS;
        }

        if (Input.GetKey(keyFour))
        {
            GetComponent<Rigidbody>().velocity -= directionWS;
        }
    }
}
