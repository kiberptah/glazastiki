using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;


public class GridGeneration : MonoBehaviour
{

    public GameObject GridSample;
    public GameObject FrameSample;
    public int xSize;
    public int ySize;

    public Canvas LetterCanvas;
    public Text Letter;

    //public string gridBackGroundColor;
    public GameObject GridTexture;

    void Start()
    {
        //xSize = ySize = 26;
        GenerateGrid();
        GenerateLetters();
        //Debug.Log(FrameSample.GetComponent<SpriteRenderer>().sprite.pivot);

        GridBackGround();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void GenerateGrid()
    {      
        //Сетка
        for (int x = 0; x < xSize; ++x)
        {        
            for (int y = 0; y < ySize; ++y)
            {
                GameObject newTile = Instantiate(GridSample, new Vector3(x, y, 1), GridSample.transform.rotation);     
                newTile.transform.SetParent(gameObject.transform);
            }
        }

        //Рамка

        GameObject newFramePiece = FrameSample;

        for (int x = 0; x < xSize; ++x)
        {
            
            FrameSample.transform.rotation = Quaternion.LookRotation(transform.forward);
            newFramePiece = FrameSample;
            newFramePiece.transform.Rotate(new Vector3 (0, 0, -90));
            newFramePiece = Instantiate(newFramePiece, new Vector3(x, ySize, 1), FrameSample.transform.rotation);
            newFramePiece.transform.SetParent(gameObject.transform);
            
            FrameSample.transform.rotation = Quaternion.LookRotation(transform.forward);
            newFramePiece = FrameSample;
            newFramePiece.transform.Rotate(new Vector3(0, 0, 90));
            newFramePiece = Instantiate(newFramePiece, new Vector3(x, -1, 1), FrameSample.transform.rotation);
            newFramePiece.transform.SetParent(gameObject.transform);
        }
        for (int y = 0; y < ySize; ++y)
        {
            FrameSample.transform.rotation = Quaternion.LookRotation(transform.forward);
            newFramePiece = FrameSample;
            newFramePiece.transform.Rotate(new Vector3(0, 0, 0));
            newFramePiece = Instantiate(newFramePiece, new Vector3(-1, y, 1), FrameSample.transform.rotation);
            newFramePiece.transform.SetParent(gameObject.transform);
            

            FrameSample.transform.rotation = Quaternion.LookRotation(transform.forward);
            newFramePiece = FrameSample;
            newFramePiece.transform.Rotate(new Vector3(0, 0, 180));
            newFramePiece = Instantiate(newFramePiece, new Vector3(xSize, y, 1), FrameSample.transform.rotation);
            newFramePiece.transform.SetParent(gameObject.transform);
            
        }

        //FrameSample.transform.rotation = Quaternion.LookRotation(transform.forward);

    }

    void GenerateLetters()
    {
        string final;
        

        //Буквы
        
        double math;
        char X;
        X = 'A';

        for (int y = ySize - 1; y >= 0; --y)
        {
            final = "";
            math = (X - 64) - 1;
                        
            for(int i = 0; i <= (Math.Floor(math / 26)); ++i)
            {                
                final = final +  (  (char) ((int)(X - 26*(Math.Floor(math / 26))))  ).ToString();
            }
            
            Letter.text = final;


            Text newLetter = Letter;

            newLetter.alignment = TextAnchor.MiddleLeft;
            newLetter = Instantiate(Letter, new Vector3(-1.5f, y, 1), Letter.transform.rotation);            
            newLetter.transform.SetParent(LetterCanvas.transform);

            newLetter.alignment = TextAnchor.MiddleRight;
            newLetter = Instantiate(Letter, new Vector3(xSize + .5f, y, 1), Letter.transform.rotation);
            newLetter.transform.SetParent(LetterCanvas.transform);
  
            ++X;
        }
        
        //Цифры
        for (int x = 0; x < xSize; ++x)
        {

            final = (x + 1).ToString();          

            Letter.text = final;
            Text newLetter = Letter;

            newLetter.alignment = TextAnchor.MiddleCenter;

            newLetter = Instantiate(Letter, new Vector3(x, ySize + .5f, 1), Letter.transform.rotation);
            newLetter.transform.SetParent(LetterCanvas.transform);

            newLetter = Instantiate(Letter, new Vector3(x, -1.5f, 1), Letter.transform.rotation);
            newLetter.transform.SetParent(LetterCanvas.transform);
        }
    }


    void GridBackGround()
    {
        GameObject BGtexture = GridTexture;
        //GridTexture.GetComponent<SpriteRenderer>().color = new Color(36, 43, 51, 1);

        BGtexture.transform.localScale = new Vector3(xSize, ySize, 1);
        BGtexture.GetComponent<SpriteRenderer>().sortingLayerName = "BG";
        Instantiate(BGtexture, new Vector3(-.5f, -.5f, 1), BGtexture.transform.rotation);
    }
}
