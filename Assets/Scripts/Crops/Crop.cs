using UnityEngine;

public class Crop : MonoBehaviour
{
    private int growthStage = 0;
    private int timeToGrow = 5;
    private int stageProgress = 0;

    void Start()
    {
        TickManager.Instance.SubscribeToRandomTick(Grow);

    }


    private void Grow()
    {
        stageProgress++;
        if (stageProgress >= timeToGrow)
        {
            growthStage++;
            stageProgress = 0;
            Debug.Log("Crop has grown to stage: " + growthStage);
        }
    }
}
