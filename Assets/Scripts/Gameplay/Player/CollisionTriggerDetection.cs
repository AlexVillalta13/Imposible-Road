using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTriggerDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ScoreBox scoreBox = other.GetComponent<ScoreBox>();
        Gem gem = other.GetComponent<Gem>();

        if (scoreBox != null)
        {
            scoreBox.SumScore();
            return;
        }

        if (gem != null)
        {
            gem.PickupGem();
            return;
        }
    }
}
