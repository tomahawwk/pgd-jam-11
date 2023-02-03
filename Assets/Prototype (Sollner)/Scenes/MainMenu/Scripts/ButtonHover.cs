using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _tint;

    private void Awake()
    {
        _tint.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tint.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tint.SetActive(false);
    }
}
