using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [Header("Main Settings")]
    Light LightSource;
    public KeyCode FlashlightKey;

    [Header("Battery Settings")]
    public float currentBatteryLife;
    public float maxBatteryLife;

    [Header("Shake Settings")]
    public KeyCode ShakeKey;
    public Vector3 ShakeRandomizerMax;
    public Vector3 ShakeRandomizerMin;
    public float shakeBatteryLifeGain;
    public float shakeTime;
    Vector3 FlashlightStartingPosition;
    bool isShaking;

    bool IsOn;

    private void Start()
    {
        LightSource = GetComponent<Light>();

        if (LightSource.enabled == true)
            IsOn = true;
        else
            IsOn = false;

        FlashlightStartingPosition = transform.localPosition;

        currentBatteryLife = maxBatteryLife;
    }

    private void Update()
    {
        StartCoroutine(FlashlightController());
        BatteryLifeSystem();

        if (isShaking == true)
        {
            Shaker();
        }
    }

    void Shaker()
    {
        transform.localPosition = new Vector3(Random.Range(ShakeRandomizerMin.x, ShakeRandomizerMax.x), Random.Range(ShakeRandomizerMin.y, ShakeRandomizerMax.y), Random.Range(ShakeRandomizerMin.z, ShakeRandomizerMax.z));
    }

    void BatteryLifeSystem()
    {
        if (currentBatteryLife <= 20)
        {
            LightSource.intensity = currentBatteryLife / 100 * 5;
        }

        if (currentBatteryLife >= maxBatteryLife)
        {
            currentBatteryLife = maxBatteryLife;
        }
    }

    IEnumerator FlashlightController()
    {
        if (Input.GetKeyDown(FlashlightKey))
        {
            if (IsOn == false)
            {
                LightSource.enabled = true;
                IsOn = true;
            }
            else
            {
                LightSource.enabled = false;
                IsOn = false;
            }
        }

        if(Input.GetKeyDown(ShakeKey))
        {
            isShaking = true;
            yield return new WaitForSeconds(shakeTime);
            isShaking = false;
            currentBatteryLife += shakeBatteryLifeGain;
            transform.localPosition = FlashlightStartingPosition;
        }
    }
}
