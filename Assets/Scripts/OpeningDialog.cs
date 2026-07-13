using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpeningDialog : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;

    [Header("Dialog Settings")]
    [TextArea(5, 20)]
    public string message = "Cara bermain: Gunakan tombol panah atau tombol A, W, S, D. \nKumpulkan biji-biji yang tersebar, lalu tanam di manapun kamu mau. \nSetiap ada tanaman yang berhasil tumbuh, skor akan bertambah 10 poin. \nMaksimal skor adalah 110.";
    public float typingSpeed = 0.03f;
    public float autoCloseDelay = 5f;

    void Start()
    {
        if (dialogPanel == null || dialogText == null)
        {
            Debug.LogError("OpeningDialog: UI elements not assigned!");
            return;
        }

        dialogPanel.SetActive(true);
        StartCoroutine(ShowDialog());
    }

    IEnumerator ShowDialog()
    {
        dialogText.text = "";
        bool skipped = false;

        foreach (char c in message)
        {
            dialogText.text += c;
            yield return new WaitForSecondsRealtime(typingSpeed);

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                dialogText.text = message;
                skipped = true;
                break;
            }
        }

        if (!skipped)
        {
            yield return new WaitForSecondsRealtime(autoCloseDelay);
        }

        dialogPanel.SetActive(false);
    }
}