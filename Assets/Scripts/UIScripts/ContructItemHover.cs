using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContructItemHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ConstructionItem item;

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventManager.TooltipEvent(item.blueprint.identifier, item.blueprint.description, item.blueprint.perSecondChange);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventManager.TooltipEvent("", "", null);
    }
}
