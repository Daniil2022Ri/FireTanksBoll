using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUIButton : MonoBehaviour
{
    [Header("Settings Scale value")]

    [SerializeField] private Vector3 _scaleFom;

    [SerializeField] private Vector3 _scaleTo;

    [Header("Second max and min scale")]

    [SerializeField][Min(0.0f)] private float _duration;


    private void Start()
    {
        StartCoroutine(PlayLoopedScalingAnimation(transform,_scaleFom, _scaleTo, _duration));
    }

    private IEnumerator PlayLoopedScalingAnimation(Transform transformToAnimate, Vector3 from , Vector3 to , float duration)
    {
        while(true)
        {
            yield return StartCoroutine(PlayScalingAnimation(transformToAnimate , to , duration));
            yield return StartCoroutine(PlayScalingAnimation(transformToAnimate, from, duration));
        }
    }

    private IEnumerator PlayScalingAnimation(Transform transformToAnimate , Vector3 to , float duration)
    {
        float enteredTime = Time.time;
        Vector3 enteredScale = transformToAnimate.localScale;

        while(Time.time < enteredTime + duration)
        {
            float elapsedTimePercent = (Time.time - enteredTime) / duration;
            
            transformToAnimate.localScale = Vector3.Lerp(enteredScale, to, elapsedTimePercent);

            yield return null;
        }
    }
}
