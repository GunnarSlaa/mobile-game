using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text popUpText;

    public void Show(string text, bool temp)
    {
        popUpText.text = text;
        GetComponent<Canvas>().enabled = true;
        StartCoroutine(HideAfterSeconds(2));
    }

    IEnumerator HideAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        GetComponent<Canvas>().enabled = false;
    }
}
