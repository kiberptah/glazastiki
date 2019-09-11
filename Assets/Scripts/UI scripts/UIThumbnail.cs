using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

using UnityEngine.UI;


public class UIThumbnail : MonoBehaviour, IPointerClickHandler
{
    GameObject SpawnUI;
    public GameObject Area;

    public string pageType = "Tiles";

    // Start is called before the first frame update
    void Start()
    {
        SpawnUI = GameObject.Find("SpawnUI");
    }

    // Update is called once per frame
    void Update()
    {
        colorChange();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SpawnUI.GetComponent<SpawnMenuController>().SelectPage(pageType, gameObject);
    }

    void colorChange()
    {
        Color lightGrey = new Color(0.6f, 0.6f, 0.6f, 255);
        Color darkGrey = new Color(0.4f, 0.4f, 0.4f, 255);

        if (Area.activeSelf)
        {
            GetComponent<RawImage>().color = lightGrey;
        }
        else
        {
            GetComponent<RawImage>().color = darkGrey;
        }
    }
}
