using DG.Tweening;
using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneObj_DroneMinigame2 : MonoBehaviour
{
    public SpriteRenderer happyIcon, sadIcon;
    public Vector3 valueScaleStart;
    public Color startColor;
    public PathCreator pathCreator;
    public float speed;
    float distanceTravelled;

    private void Start()
    {
        speed = 3;
        startColor = happyIcon.color;
        valueScaleStart = happyIcon.transform.localScale;
        happyIcon.gameObject.SetActive(false);
        sadIcon.gameObject.SetActive(false);
        happyIcon.transform.localScale = Vector3.zero;
        sadIcon.transform.localScale = Vector3.zero;
    }

    public void Happy()
    {
        happyIcon.transform.DOKill();
        happyIcon.gameObject.SetActive(true);
        happyIcon.transform.DOScale(valueScaleStart, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            happyIcon.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() => 
            {
                happyIcon.gameObject.SetActive(false);
                happyIcon.transform.localScale = Vector3.zero;
                happyIcon.color = startColor;
            });
        });
    }

    public void Sad()
    {
        sadIcon.transform.DOKill();
        sadIcon.gameObject.SetActive(true);
        sadIcon.transform.DOScale(valueScaleStart, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            sadIcon.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                sadIcon.gameObject.SetActive(false);
                sadIcon.transform.localScale = Vector3.zero;
                sadIcon.color = startColor;
            });
        });
    }

    private void FixedUpdate()
    {
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        }

    }
}
