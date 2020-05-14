using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    [SerializeField] Transform nextSpawnTransform = null;

    float heightToDestroy = 5f;

    PlayerController_FSM player = null;
    RampsPoolManager rampsManager = null;

    [SerializeField] List<ScoreBox> scoreBoxes = new List<ScoreBox>();

    public Vector3 nextSpawnPosition { get { return nextSpawnTransform.position; } }
    public Quaternion nextSpawnRotation{get { return nextSpawnTransform.rotation;} }

    private void Awake()
    {
        scoreBoxes.Clear();
        scoreBoxes = GetComponentsInChildren<ScoreBox>().ToList();
    }

    public void Init(RampsPoolManager rampsManager, PlayerController_FSM player, ScoreManager scoreManager) 
    {
        this.rampsManager = rampsManager;
        this.player = player;

        foreach(ScoreBox scoreBox in scoreBoxes)
        {
            scoreBox.Init(scoreManager);
        }
    }

    public void ActivateScoreBoxes(ref int currentScoreBoxSum)
    {
        if (scoreBoxes != null)
        {
            foreach (ScoreBox scoreBox in scoreBoxes)
            {
                scoreBox.UpdateHoldingScore(currentScoreBoxSum);
                currentScoreBoxSum++;
            }
        }
    }

    private void Update()
    {
        if (nextSpawnPosition.y - heightToDestroy > player.transform.position.y)
        {
            rampsManager.DeSpawn(this);
        }
    }
}
