using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBox : MonoBehaviour
{
    int scoreHolding;

    ScoreManager scoreManager;

    GameObject meshGameObject;
    ParticleSystem collisionEffect;

    private void Awake()
    {
        collisionEffect = GetComponentInChildren<ParticleSystem>();
        meshGameObject = transform.GetChild(0).gameObject;
    }

    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }

    public void UpdateHoldingScore(int score)
    {
        scoreHolding = score;
        meshGameObject.SetActive(true);
    }

    public void SumScore()
    {
        scoreManager.AddScore(scoreHolding);
        collisionEffect.Play();
        meshGameObject.SetActive(false);
    }
}
