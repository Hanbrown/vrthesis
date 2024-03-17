using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Daemon", menuName = "Daemon")]
public class RandomBlinkValue : ScriptableObject
{
    [SerializeField] public float minimumTime = 0.0f; // In seconds
    [SerializeField] public float maximumTime = 1.0f; // In seconds

}
