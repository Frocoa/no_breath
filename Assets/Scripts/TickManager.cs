using System;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    public static TickManager Instance { get; private set; }

    private const float MegaTickInterval = 1f; // Intervalo de 1 segundo
    private float _elapsedTime = 0f;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= MegaTickInterval)
        {
            _elapsedTime -= MegaTickInterval;
            ExecuteMegaTick();
        }
    }
    private List<Action> TickEvents = new List<Action>();

    public void SubscribeToRandomTick(Action action)
    {
        int randomIndex = UnityEngine.Random.Range(0, TickEvents.Count);
        TickEvents[randomIndex] += action;
    }

    private void ExecuteMegaTick()
    {
        Debug.Log("Mega Tick Executed");
        foreach (var tickEvent in TickEvents)
        {
            Debug.Log("Executing tick event");
            tickEvent?.Invoke();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeTickEvents(4); // Cambia el número 4 por la cantidad de ticks que necesites
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeTickEvents(int numberOfTicks)
    {
        for (int i = 0; i < numberOfTicks; i++)
        {
            TickEvents.Add(null);
        }
    }
}
