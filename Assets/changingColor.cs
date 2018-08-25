using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class changingColor : MonoBehaviour {

    Vector3 mouseposition;
    public float xCursor;
    public float yCursor;

    // Use this for initialization
    void Start ()
    {
        // Color cl = new Color(255, 255, 255, 1);
        //Debug.Log(cl.grayscale);

        mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);     
    }

    // Update is called once per frame
    void Update ()
    {
        Do();
    }


    void Do()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.GetComponentInParent<Transform>().GetComponentInParent<Transform>().position, Vector3.forward, 0, 1 << LayerMask.NameToLayer("Walls"));

        if (hit)
        {
            //Debug.Log((hit.transform.GetComponent<SpriteRenderer>().color.grayscale));
            if (hit.transform.GetComponent<SpriteRenderer>().color.grayscale < 0.5f)
            {
                gameObject.GetComponent<Text>().color = new Color(255f, 255f, 255f, gameObject.GetComponent<Text>().color.a);
            }
            if (hit.transform.GetComponent<SpriteRenderer>().color.grayscale >= 0.5f)
            {
                gameObject.GetComponent<Text>().color = new Color(0, 0, 0, gameObject.GetComponent<Text>().color.a);
            }
        }
        else
        {
            gameObject.GetComponent<Text>().color = new Color(0, 0, 0, gameObject.GetComponent<Text>().color.a);
        }
    }

}
