using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessControls : MonoBehaviour
{
    [SerializeField] private Volume postProcessingVolume;
    [SerializeField] private Transform _camera;

    [Tooltip("Rotation thresholding for normal view, outside this red")]
    [SerializeField] [Range(0.0f, 90.0f)] private float referenceAngle = 0.0f;

    [Tooltip("Rotation thresholding maximum (like a strength setting). Must be greater than ref angle")]
    [SerializeField] [Range(0.0f, 90.0f)] private float maxAngle = 90.0f;
    
    private ColorCurves _colorcurves;
    private Vector3 initialTransform;

    private Vector3 initalParentForward;
    private float originalParentRotation;

    private float denominator;
    private Keyframe originalRedKeyframe;

    // Start is called before the first frame update
    void Start()
    {
        postProcessingVolume.profile.TryGet(out _colorcurves);

        // Will be used to calculate transitions later
        initialTransform = _camera.forward;
        denominator = maxAngle - referenceAngle;

        initalParentForward = _camera.root.forward;
        originalParentRotation = _camera.root.rotation.eulerAngles.y;

        // Get the red curve before any transformations are done to it
        TextureCurveParameter original = _colorcurves.red;
        TextureCurve originalCurve = original.GetValue<TextureCurve>();
        originalRedKeyframe = originalCurve[1];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newUp = _camera.forward;
        //float offset = originalParentRotation - _camera.root.rotation.eulerAngles.y; // Change in parent's rotation
        float newAngle = Vector3.Angle(initialTransform, newUp); // Remove influence of parent's rotation

        // Calculate offset for when parent rotates
        Vector3 newParentForward = _camera.root.forward;
        float angle1 = Vector3.Angle(initalParentForward, initialTransform);
        //float parentDelta = Vector3.Angle(initalParentForward, newParentForward);

        Vector3 tempNew = Quaternion.AngleAxis(angle1, _camera.root.right) * newParentForward;
        float angle2 = Vector3.Angle(tempNew, initialTransform) / 2;

        newAngle += angle2;

        if (newAngle > referenceAngle) {

            // Normalize rotation delta so that it can adjust the red keyframe value
            float transition = (newAngle - referenceAngle) / denominator;
            transition = originalRedKeyframe.value + Mathf.Min(transition, 1.0f-originalRedKeyframe.value);

            Debug.Log(newAngle + "      " + newUp.ToString() + "       " + initialTransform.ToString() + "        " + tempNew.ToString());

            // Adjust color curve accordingly
            Keyframe newKey = originalRedKeyframe;
            newKey.value = transition;
            _colorcurves.red.GetValue<TextureCurve>().MoveKey(1, newKey);
        }
        else
        {
            Debug.Log(newAngle + "      " + newUp.ToString() + "       " + initialTransform.ToString() + "        " + tempNew.ToString());
        }
    }
}
