using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TwoHandGrabAttach : MonoBehaviour
{
    [Header("Attach Points")]
    public Transform AttachTransformRight;  // точка для правой руки
    public Transform AttachTransformLeft;   // точка для левой руки

    private XRGrabInteractable grabInteractable;
    private Transform originalAttach;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        originalAttach = grabInteractable.attachTransform;
    }

    public void OnGrab(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject.transform.GetComponent<XRDirectInteractor>();
        if (interactor == null) return;

        // Определяем, какая рука взяла
        bool isLeftHand = interactor.name.Contains("Left") || interactor.name.Contains("LeftHand");

        // Выбираем нужную точку привязки
        Transform targetAttach = isLeftHand ? AttachTransformLeft : AttachTransformRight;

        if (targetAttach != null)
        {
            grabInteractable.attachTransform = targetAttach;
        }
    }

    public void OnUngrab(SelectExitEventArgs args)
    {
        // Возвращаем стандартную точку (опционально)
        grabInteractable.attachTransform = originalAttach;
    }
}