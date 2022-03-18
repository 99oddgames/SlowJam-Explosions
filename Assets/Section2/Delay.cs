using UnityEngine;

[System.Serializable]
public struct Delay
{
    public float Min;
    public float Max;

    private float nextDelay;
    private float timestamp;
    private float time => UseScaledTime ? Time.time : Time.unscaledTime;

    private bool consumed;

    public bool UseScaledTime
    {
        get;
        set;
    }

    public Delay(float delay)
    {
        Min = delay;
        Max = delay;

        timestamp = -delay;
        nextDelay = 0.0f;
        consumed = false;
        UseScaledTime = true;
    }

    public Delay(float min, float max)
    {
        Min = min;
        Max = max;

        timestamp = -max;
        nextDelay = 0.0f;
        consumed = false;
        UseScaledTime = true;
    }

    public bool Once => IsUp && consumed;
    public bool IsUp => time >= timestamp + nextDelay;
    public float Remaining => Mathf.Max(nextDelay - Elapsed, 0f);
    public float Elapsed => time - timestamp;

    public float Elapsed01
    {
        get
        {
            if (nextDelay == 0)
                return 1f;

            return Mathf.Clamp01(Elapsed / nextDelay);
        }
    }

    public float ElapsedQuad
    {
        get
        {
            if (nextDelay == 0)
                return 1f;

            var norm = Mathf.Clamp01(Elapsed / nextDelay);
            return norm * norm;
        }
    }

    public Delay Next()
    {
        nextDelay = Random.Range(Min, Max);
        timestamp = time;
        consumed = true;

        return this;
    }

    public Delay Next(float min, float max)
    {
        Min = min;
        Max = max;

        nextDelay = Random.Range(Min, Max);
        timestamp = time;
        consumed = true;

        return this;
    }

    public void Consume()
    {
        consumed = true;
    }

    public void Stop()
    {
        timestamp = -nextDelay;
        nextDelay = 0f;
        consumed = false;
    }

    public override string ToString()
    {
        return $"Delay: IsUp({IsUp}), Min({Min}), Max({Max}), Next({nextDelay}), Elapsed({Elapsed})";
    }
}
