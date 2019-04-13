using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LogoController : MonoBehaviour
{

    public List<GameObject> LogoItemList;

    private Vector3 _StartingScale;

    private void Awake()
    {
        if (LogoItemList == null)
            throw new System.Exception("A LogoItemList must be defined");

        _StartingScale = this.gameObject.transform.localScale;
        this.gameObject.transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        StartCoroutine(DelayAnim());
    }

    private void AnimIn(bool isImmediate = false)
    {
        this.gameObject.transform.localScale = _StartingScale;
        float animSpeed = 0.3f;
        if (!isImmediate)
        {
            for (int i = 0; i < LogoItemList.Count; i++)
            {
                float delay = i * 0.1f;
                GameObject logoItem = LogoItemList[i];
                logoItem.transform.localScale = Vector3.zero;
                logoItem.transform.DOScale(Vector3.one, animSpeed).SetEase(Ease.OutBack).SetDelay(delay);
            }
        }
        StartCoroutine(DelayAnim(false));
    }

    private IEnumerator DelayAnim(bool isAnimIn = true)
    {
        float ranDelay = Random.RandomRange(10.0f, 20.0f);
        yield return new WaitForSeconds(ranDelay);
        if(isAnimIn)
        {
            AnimIn();
        }else
        {
            AnimOut();
        }
    }

    private void AnimOut()
    {
        float animSpeed = 0.3f;

        for (int i = 0; i < LogoItemList.Count; i++)
        {
            float delay = i * 0.1f;
            GameObject logoItem = LogoItemList[i];
            logoItem.transform.DOScale(Vector3.zero, animSpeed).SetEase(Ease.InBack).SetDelay(delay);
        }
        StartCoroutine(DelayAnim());
    }

 
}
