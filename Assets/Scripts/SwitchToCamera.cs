using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToCamera : MonoBehaviour
{
    [SerializeField] private Camera currentCamera;
    [SerializeField] private Camera newCamera;
    [SerializeField] private int delay = 311;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(switchCamera());
    }

    public IEnumerator switchCamera()
    {
        yield return new WaitForSeconds(delay);
        currentCamera.enabled = !currentCamera.enabled;
        newCamera.enabled = !newCamera.enabled;
    }
}
