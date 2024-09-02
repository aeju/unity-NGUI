using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastPopup : PopupBase
{
    public float duration;
    public float animationDuration = 0.3f; // 애니메이션 지속 시간
    public float moveDistance = 50f; // 이동할 거리

    private TweenPosition tweenPosition;
    
    public override void Show(string title)
    {
        SetMessage(title);
        
        Vector3 startPos = transform.localPosition - Vector3.up * moveDistance;
        Vector3 endPos = transform.localPosition;

        tweenPosition = TweenPosition.Begin(gameObject, animationDuration, endPos);
        tweenPosition.from = startPos;
        tweenPosition.to = endPos;
        tweenPosition.method = UITweener.Method.EaseOut; // 부드러운 감속 효과를 위해 EaseOut 사용
        tweenPosition.style = UITweener.Style.Once;
        
        StartCoroutine(HideAfterDuration());
    }
    
    public override void Hide()
    {
        // 스택에 추가 x 
        // PopupManager.Instance.CloseTopPopup();
        Destroy(gameObject);
    }
    
    private void SetMessage(string message)
    {
        if (TitleLabel != null)
        {
            TitleLabel.text = message;
        }
        else
        {
            Debug.LogError("MessageLabel is not assigned in ToastPopup");
        }
    }

    private IEnumerator HideAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        Hide();
    }
}
