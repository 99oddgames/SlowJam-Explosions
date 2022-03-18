using UnityEngine;

//Based on this talk: Math for Game Programmers - Juicing Your Cameras With Math https://youtu.be/tu-Qe66AvtY
public class ScreenshakeCamera : GameplayComponent
{
    public float TranslationShakeFrequency = 32f;
    public float RotationShakeFrequency = 32f;

    public float MaxShakeTranslation = 3f;
    public float MaxShakeRotation = 10f;

    [Space]

    public AnimationCurve TraumaCurve;

    private Vector2 perlinSeed1;
    private Vector2 perlinSeed2;
    private Vector2 perlinSeed3;

    private float traumaLevel = 0f;
    private float decay = 1f;

    public override void Subscribe(IEventSubscriber eventHandler)
    {
        eventHandler.Subscribe<VFXScreenshake>(OnScreenshake);
    }

    private void Start()
    {
        perlinSeed1 = Random.onUnitSphere * Random.Range(-100f, 100f);
        perlinSeed2 = Random.onUnitSphere * Random.Range(-100f, 100f);
        perlinSeed3 = Random.onUnitSphere * Random.Range(-100f, 100f);
        enabled = false;
    }

    private void OnScreenshake(VFXScreenshake shake)
    {
        traumaLevel = Mathf.Clamp01(traumaLevel + (1f - traumaLevel) * shake.Trauma01);
        decay = shake.Decay;
        enabled = true;
    }

    private void LateUpdate()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        float t = TraumaCurve.Evaluate(traumaLevel);

        var angle = MaxShakeRotation * t * SampleNoise(perlinSeed1, RotationShakeFrequency);
        var offsetX = MaxShakeTranslation * t * SampleNoise(perlinSeed2, TranslationShakeFrequency);
        var offsetY = MaxShakeTranslation * t * SampleNoise(perlinSeed3, TranslationShakeFrequency);

        transform.localPosition = new Vector3(offsetX, offsetY);
        transform.localRotation = Quaternion.identity * Quaternion.Euler(0f, 0f, angle);

        traumaLevel = Mathf.MoveTowards(traumaLevel, 0f, Time.unscaledDeltaTime * decay);

        if(Mathf.Approximately(traumaLevel, 0f))
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            enabled = false;
        }
    }

    private float SampleNoise(Vector2 point, float freq)
    {
        var perlin01 = Mathf.PerlinNoise(point.x * freq * Time.time, point.y * freq * Time.time);
        var result = Mathf.Lerp(-1f, 1f, perlin01);
        return result;
    }
}
