using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    RectTransform buttonRectangle;
    Image buttonImage;
    public UnityEvent eventToTrigger;

    // Start is called before the first frame update
    void Start()
    {
        buttonRectangle = GetComponent<RectTransform>();
        buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the mouse is within our button rectangle...
        if (RectTransformUtility.RectangleContainsScreenPoint(buttonRectangle, Input.mousePosition))
        {
            // Allow clicking on it
            buttonImage.color = Color.black;
            if (Input.GetButtonDown("Fire1"))
            {
                eventToTrigger.Invoke();
            }
        }
        else
        {
            buttonImage.color = Color.gray;
        }
    }
}
