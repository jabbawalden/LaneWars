using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameEvents
{
    public static event Action eventSpawnUnits;
    public static event Action eventChangeSpeeds;
    public static event Action eventChangeHealth;

    public static void ReportSpawnUnits()
    {
        eventSpawnUnits?.Invoke();
    }

    public static void ReportChangeSpeeds()
    {
        eventChangeSpeeds?.Invoke();
    }

    public static void ReportChangeHealth()
    {
        eventChangeHealth?.Invoke();
    }

}
