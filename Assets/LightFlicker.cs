using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{
    public Light light;

    [Range(0, 10)]
    public float lightIntensityMin;
    [Range(0, 10)]
    public float lightIntensityMax;
    [Range(0, 10)]
    public float lightIntensityFlickerRate;
    [Range(0.1f, 1)]
    public float lightIntensityFlickerVarianceMin;
    [Range(1, 10)]
    public float lightIntensityFlickerVarianceMax;

    private float lightIntensityFlickerCurrent;
    private float lightIntensityFlickerGoal;

    private float lightIntensityStart;
    private float lightIntensityGoal;

    // Use this for initialization
    void Start()
    {
        lightIntensityFlickerGoal = lightIntensityFlickerRate * Random.Range(lightIntensityFlickerVarianceMin, lightIntensityFlickerVarianceMax);
        lightIntensityFlickerCurrent = 0;

        lightIntensityStart = light.intensity;
        lightIntensityGoal = Random.Range(lightIntensityMin, lightIntensityMax);
    }

    // Update is called once per frame
    void Update()
    {
        lightIntensityFlickerCurrent += Time.deltaTime;

        if (lightIntensityFlickerCurrent >= lightIntensityFlickerGoal)
        {
            lightIntensityFlickerGoal = lightIntensityFlickerRate * Random.Range(lightIntensityFlickerVarianceMin, lightIntensityFlickerVarianceMax);
            lightIntensityFlickerCurrent = 0;

            lightIntensityStart = light.intensity;
            lightIntensityGoal = Random.Range(lightIntensityMin, lightIntensityMax);
        }

        light.intensity = Mathf.Lerp(lightIntensityStart, lightIntensityGoal, lightIntensityFlickerCurrent / lightIntensityFlickerGoal);
    }
}
