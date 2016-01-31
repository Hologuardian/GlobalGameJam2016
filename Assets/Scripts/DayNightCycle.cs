using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle Cycle;

    public float dayLength = 300;
    public float dayStart = 0;
    public float updateThreshold = 0.1f;
    public float[] dayIntensities;

    public float dayElapsed;
    private float updateElapsed = 0;

    private Light me;

    // Use this for initialization
    void Start()
    {
        Cycle = this;

        dayElapsed = dayStart;
        updateElapsed = updateThreshold;
        me = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Cycle == null)
            Cycle = this;
        updateElapsed += Time.deltaTime;
        dayElapsed += Time.deltaTime;

        if (updateElapsed >= updateThreshold)
        {
            updateElapsed -= updateThreshold;

            if (dayElapsed >= dayLength)
                dayElapsed -= dayLength;

            float percentage = dayElapsed / dayLength;

            UpdateAngle(percentage);
            UpdateIntensity(percentage);
        }
    }

    void UpdateAngle(float percentage)
    {
        float inclination = percentage * (Mathf.PI * 2);
        transform.LookAt(new Vector3(0, Mathf.Sin(inclination), Mathf.Cos(inclination)));
    }

    void UpdateIntensity(float percentage)
    {
        int steps = dayIntensities.Length - 1;

        int currentStep = Mathf.CeilToInt(percentage * steps);

        currentStep = Mathf.Clamp(currentStep, 0, steps);

        int previous = currentStep - 1;

        if (previous < 0)
            previous = steps;

        // I need a measure of completion between the last step and the current step as a function of total percentage done.
        me.intensity = me.bounceIntensity = Mathf.Lerp(dayIntensities[previous], dayIntensities[currentStep], (percentage * steps) - (currentStep - 1));
    }
}
