using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSystem : MonoBehaviour
{
    public  GameObject   TheGrid;

    public Color gridColor;
    public Color gridLettersColor;
    public Color gridBGColor;

    GameObject MainCamera;

    public Color unitVisionLinesColor;
    void Start()
    {     
        Color tempColor;
        
        // Цвет задника вокруг поля
        ColorUtility.TryParseHtmlString("#181D22", out tempColor);
        MainCamera = GameObject.Find("Main Camera");
        MainCamera.GetComponent<Camera>().backgroundColor = tempColor;

        // Цвет задника поля
        ColorUtility.TryParseHtmlString("#202D3D", out tempColor);
        tempColor.a = 100f;
        gridBGColor = tempColor;
        

        //Сетка
        ColorUtility.TryParseHtmlString("#000000", out tempColor);
        tempColor.a = 100f;
        gridColor = tempColor;

        //координаты
        ColorUtility.TryParseHtmlString("#E0E0E0", out tempColor);
        gridLettersColor = tempColor;

        // цвет линий зрения/прицела
        ColorUtility.TryParseHtmlString("#7f0000", out tempColor);
        unitVisionLinesColor = tempColor;


        TheGrid.SetActive(true);
    }


}
