using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuReturn : MonoBehaviour
{
    [SerializeField] GameObject Yes;
    [SerializeField] GameObject No;

    private void Start()
    {
        Yes.SetActive(false);
        No.SetActive(false);
    }

    public void ShowButtons ()
    {
        Yes.SetActive(true);
        No.SetActive(true);
    }

    public void HideButtons()
    {
        Yes.SetActive(false);
        No.SetActive(false);
    }

    public void PlayButtonPress ()
    {
        AudioManager.Instance.PlaySFX("Button Key Press");
    }
}
