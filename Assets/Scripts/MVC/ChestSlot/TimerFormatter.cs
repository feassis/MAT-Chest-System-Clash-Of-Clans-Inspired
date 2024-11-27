using UnityEngine;

public class TimerFormatter
{
    public static string FormatTime(float timeInSeconds)
    {
        // Calcula as horas, minutos e segundos.
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // Formata no estilo "00h:00m:00s".
        return $"{hours:00}h:{minutes:00}m:{seconds:00}s";
    }
}