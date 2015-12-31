using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour
{
    public float fadeSpeed = 2f;
    public float highIntensity = 2f;
    public float lowIntensity = 0.5f;
    public float changeMargin = 0.2f;
    public bool alarmOn;

    private float targetIntensity;

    void Awake()
    {
        GetComponent<Light>().intensity = 0f;
        targetIntensity = highIntensity;

    }

    void Update()
    {
        if (alarmOn)
        {
            CheckTargetIntensity();
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, targetIntensity, fadeSpeed * Time.deltaTime);
            
        }
        else if( GetComponent<Light>().intensity > 0.01f )
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, 0f, fadeSpeed * Time.deltaTime);
    }

    void CheckTargetIntensity()
    {
        if( Mathf.Abs(GetComponent<Light>().intensity - targetIntensity) < changeMargin )
        {
            if (targetIntensity == highIntensity)
                targetIntensity = lowIntensity;
            else
                targetIntensity = highIntensity;
        }
    }


}
