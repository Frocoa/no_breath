using UnityEngine;

public class Crop : MonoBehaviour
{

    [SerializeField]
    private Sprite[] growthStageSprites;
    private SpriteRenderer spriteRenderer;
    private int growthStage = 0;
    private int timeToGrow = 5;
    private int stageProgress = 0;
    private int maxGrowthStage = 5;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        TickManager.Instance.SubscribeToRandomTick(Grow);

    }


    private void Grow()
    {
        if (growthStage >= maxGrowthStage) return;

        stageProgress++;
        if (stageProgress >= timeToGrow)
        {
            growthStage++;
            stageProgress = 0;
            spriteRenderer.sprite = growthStageSprites[growthStage];
            Debug.Log("Crop has grown to stage: " + growthStage);
        }
    }
}
