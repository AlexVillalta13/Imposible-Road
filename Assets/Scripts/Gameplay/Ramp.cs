using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    [SerializeField] Transform nextSpawnTransform = null;

    float heightToDestroy = 5f;

    PlayerController_FSM player = null;
    RampsManager rampsManager = null;

    [SerializeField] List<ScoreBox> scoreBoxes = new List<ScoreBox>();

    public Vector3 nextSpawnPosition
    {
        get
        {
            return nextSpawnTransform.position;
        }
    }

    public Quaternion nextSpawnRotation
    {
        get
        {
            return nextSpawnTransform.rotation;
        }
    }


    public void Init(RampsManager rampsManager, PlayerController_FSM player) 
    {
        this.rampsManager = rampsManager;
        this.player = player;
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
