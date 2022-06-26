using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public Transform shakeCamera;
    public bool shakeRotate = false;
    Vector3 originPos;
    Quaternion originRot;
    public Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        originPos = shakeCamera.localPosition;
        originRot = shakeCamera.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShakeCameras(float duration = 0.05f, float magnitudePos = 0.03f,float magnitudeRot = 0.0f)
    {
        float passTime =0;
        while(passTime < duration)
        {
            Vector3 shakePos = Random.insideUnitSphere;
            shakeCamera.localPosition = shakePos * magnitudePos;
            if (shakeRotate)
            {
                Vector3 shakeRot = new Vector3(0, 0, Mathf.PerlinNoise(Time.time * magnitudeRot, 0f));
                shakeCamera.localRotation = Quaternion.Euler(shakeRot);
            }
            passTime += Time.deltaTime;
            yield return null;
        }
        shakeCamera.localPosition = originPos;
        shakeCamera.localRotation = originRot;
    }
    public IEnumerator ShakeCamerasMis(float duration = 0.2f, float magnitudePos = 0.1f, float magnitudeRot = 0.0f)
    {
        float passTime = 0;
        while (passTime < duration)
        {
            Vector3 shakePos = Random.insideUnitSphere;
            shakeCamera.localPosition = shakePos * magnitudePos;
            if (shakeRotate)
            {
                Vector3 shakeRot = new Vector3(0, 0, Mathf.PerlinNoise(Time.time * magnitudeRot, 0f));
                shakeCamera.localRotation = Quaternion.Euler(shakeRot);
            }
            passTime += Time.deltaTime;
            yield return null;
        }
        shakeCamera.localPosition = originPos;
        shakeCamera.localRotation = originRot;
    }

    public IEnumerator Recoil()
    {
        tr.transform.rotation = Quaternion.Euler(tr.rotation.x-1, 0, 0);
        yield return null;
    }
}
