using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{

    private Light _light;
    
    [Range(0, 10)] public float minFlickerTime = 0.00f;
    [Range(0, 10)] public float maxFlickerTime = 0.85f;

    private void Start()
    {
        _light = GetComponent<Light>();
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            _light.enabled = !_light.enabled;
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
        }
    }
    
}
