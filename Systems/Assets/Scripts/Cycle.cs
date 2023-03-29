using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycle : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float cyclePeriod;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(cyclePeriod * rotation * Time.deltaTime);
    }
}
