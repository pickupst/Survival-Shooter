using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickManager : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Image joystickSelf;
    public Image joystickBackground;

    Vector2 coordinate2;
    Vector3 coordinate3;

    private void Start()
    {

        joystickBackground = GetComponent<Image>();
        joystickSelf = transform.GetChild(0).GetComponent<Image>();

    }

    public void OnDrag(PointerEventData eventData)
    {

        coordinate2 = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground.rectTransform, eventData.position, eventData.pressEventCamera, out coordinate2))
        {
            coordinate2.x = (coordinate2.x / joystickBackground.rectTransform.sizeDelta.x);
            coordinate2.y = (coordinate2.y / joystickBackground.rectTransform.sizeDelta.y);

            float x = (joystickBackground.rectTransform.pivot.x == 1) ? coordinate2.x * 2 + 1 : coordinate2.x * 2 - 1;
            float y = (joystickBackground.rectTransform.pivot.y == 1) ? coordinate2.y * 2 + 1 : coordinate2.y * 2 - 1;

            coordinate3 = new Vector3(x, 0, y);
            coordinate3 = (coordinate3.magnitude > 1.0f) ? coordinate3.normalized : coordinate3;

            joystickSelf.rectTransform.anchoredPosition = new Vector3(coordinate3.x * (joystickBackground.rectTransform.sizeDelta.x / 3),
                                                                         coordinate3.z * (joystickBackground.rectTransform.sizeDelta.y / 3));


        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        coordinate3 = Vector3.zero;
        joystickSelf.rectTransform.anchoredPosition = Vector3.zero;
    }

    public Vector3 getJoystickVector()
    {

        return coordinate3;

    }
}
