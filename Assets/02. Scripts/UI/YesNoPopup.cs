using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNoPopup : AbstractPopup
{
    [SerializeField] private UILabel titleLabel;
    [SerializeField] private UILabel messageLabel;
    [SerializeField] private UIButton yesButton;
    [SerializeField] private UIButton noButton;
    [SerializeField] private UILabel yesButtonLabel;
    [SerializeField] private UILabel noButtonLabel;

    public override UILabel TitleLabel { get => titleLabel; protected set => titleLabel = value; }
    public override UILabel MessageLabel { get => messageLabel; protected set => messageLabel = value; }
    public override UIButton YesButton { get => yesButton; protected set => yesButton = value; }
    public override UIButton NoButton { get => noButton; protected set => noButton = value; }
    public override UILabel YesButtonLabel { get => yesButtonLabel; protected set => yesButtonLabel = value; }
    public override UILabel NoButtonLabel { get => noButtonLabel; protected set => noButtonLabel = value; }

    public override void Show()
    {
        gameObject.SetActive(true);
        IsVisible = true;
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
        IsVisible = false;
    }
}
