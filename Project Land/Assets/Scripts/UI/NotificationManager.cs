using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;
    [SerializeField] private Transform popupPoint;
    [SerializeField] private NotificationPopup prefab;
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private float padding = 20f;

    private List<NotificationPopup> popups = new List<NotificationPopup>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Notification(Sprite image, string title, string info)
	{
        if (image == null)
            image = defaultImage;
        NotificationPopup popup = PoolManager.Instance.Pop(prefab.gameObject.name) as NotificationPopup;
        popup.SetData(image, title, info);
        popup.transform.position = popupPoint.position;

        TakeUp();

        if (popups.Count > 5)
		{
            StopCoroutine(popups[0].popupCoroutine);
            popups[0].seq.Kill();
            PoolManager.Instance.Push(popups[0]);
            popups.Remove(popups[0]);
        }

        popups.Add(popup);
        popup.popupCoroutine = StartCoroutine(PopupNotification(popup));
	}

    private void TakeUp()
	{
		for (int i = 0; i < popups.Count; i++)
		{
            popups[i].transform.DOLocalMoveY(((prefab.transform as RectTransform).sizeDelta.y + padding) * (popups.Count - i), 0.5f).SetEase(Ease.Linear);
		}
	}

    private IEnumerator PopupNotification(NotificationPopup popup)
	{
        DOTween.Kill(popup.transform);
        DOTween.To(() => popup.transform.localPosition.x, x => popup.transform.localPosition = new Vector3(x, popup.transform.localPosition.y), -(popup.transform as RectTransform).sizeDelta.x, 0.5f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(3f);

        Sequence seq = DOTween.Sequence();
        popup.seq = seq;

        Tween t = DOTween.To(() => popup.transform.localPosition.x, x => popup.transform.localPosition = new Vector3(x, popup.transform.localPosition.y), 0, 0.5f).SetEase(Ease.Linear);
        seq.Append(t);
        seq.AppendCallback(() => {
            PoolManager.Instance.Push(popup); 
            popups.Remove(popup);
        });
    }
}
