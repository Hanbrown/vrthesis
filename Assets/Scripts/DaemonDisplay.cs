using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaemonDisplay : MonoBehaviour
{

    [SerializeField] private RandomBlinkValue value;
    // Start is called before the first frame update
    void Start()
    {
    }

    public IEnumerator DelayCoroutine()
    {
        float delay = Random.Range(value.minimumTime, value.maximumTime);
        Debug.Log(delay);
        yield return new WaitForSeconds(delay);
    }
}
