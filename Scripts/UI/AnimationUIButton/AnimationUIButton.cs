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

    [Header("Animation")]
    [SerializeField] private AnimationCurve _scaleCurve;


    private void Start()
    {
        StartCoroutine(PlayLoopedScalingAnimation(transform,_scaleFom, _scaleTo, _duration , _scaleCurve));
    }

    private IEnumerator PlayLoopedScalingAnimation(Transform transformToAnimate, Vector3 from , Vector3 to , float duration , 
        AnimationCurve scaleCurve)
    {
        while(true)
        {
            yield return StartCoroutine(PlayScalingAnimation(transformToAnimate , to , duration, scaleCurve));
            yield return StartCoroutine(PlayScalingAnimation(transformToAnimate, from, duration, scaleCurve));
        }
    }

    private IEnumerator PlayScalingAnimation(Transform transformToAnimate , Vector3 to , float duration , AnimationCurve scaleCurve)
    {
        float enteredTime = Time.time;
        Vector3 enteredScale = transformToAnimate.localScale;

        while(Time.time < enteredTime + duration)
        {
            float elapsedTimePercent = (Time.time - enteredTime) / duration;
            
            Vector3 difference = to - enteredScale;

            Vector3 scaleDifference = scaleCurve.Evaluate(elapsedTimePercent) * difference;

            transformToAnimate.localScale = enteredScale + scaleDifference;

            yield return null;
        }
    }
}
