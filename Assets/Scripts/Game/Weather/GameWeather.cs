using UnityEngine;
using UnityEngine.Serialization;

public class GameWeather : MonoBehaviour
{
    public enum ETimeOfDay
    {
        Day,
        Night
    }

    [Header("Game Time Settings :")]
    public int minutesPerDay = 10;
    public int startHour = 6;           

    [Header("Debugging :")]
    public int currentHour;         
    public int currentMinute;         
    [FormerlySerializedAs("currentTimeOfDay")] public ETimeOfDay currentTimeOfDay;

    private float timeElapsed;
    private float secondsPerMinute; 

    void Start()
    {
        secondsPerMinute = (minutesPerDay * 60f) / 1440f; // 1440 = 24h * 60 minutes
        timeElapsed = 0f;
        
        UpdateGameTime();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= secondsPerMinute)
        {
            timeElapsed -= secondsPerMinute;
            IncrementGameTime();
        }
    }

    private void IncrementGameTime()
    {
        currentMinute++;

        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;

            if (currentHour >= 24)
            {
                currentHour = 0;
            }
        }
        
        UpdateTimeOfDay();
    }

    private void UpdateTimeOfDay()
    {
        if (currentHour is >= 6 and < 18)
        {
            currentTimeOfDay = ETimeOfDay.Day;
        }
        else
        {
            currentTimeOfDay = ETimeOfDay.Night;
        }
    }

    private void UpdateGameTime()
    {
        currentHour = startHour;
        currentMinute = 0;
        UpdateTimeOfDay();
    }

    public string GetFormattedTime()
    {
        return $"{currentHour:D2}h{currentMinute:D2}";
    }
}