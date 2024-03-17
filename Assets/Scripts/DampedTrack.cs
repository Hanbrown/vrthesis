using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Causes an object to rotate to point its Z-axis toward a given target
 * target: The object toward which to point
 * offset: A vector specifying a rotation to apply after pointing to the target. Useful for corrections.
 */
public class DampedTrack : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public Vector3 offset;

    //private Vector3 originalUp;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform.position, transform.up);
        transform.Rotate(offset);
    }
}
