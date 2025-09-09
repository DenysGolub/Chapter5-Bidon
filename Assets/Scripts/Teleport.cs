using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    bool isGazing = false;
    public float gazeTime = 2f;
    private float timer;

    public Image loadingImg;
    public GameObject player;

    void OnPointerEnter()
    {
        isGazing = true;
        timer = 0f;

        if (loadingImg != null)
        {
            loadingImg.fillAmount = 0f;
        }
    }

    void OnPointerExit()
    {
        isGazing = false;
        timer = 0f;

        if (loadingImg != null)
        {
            loadingImg.fillAmount = 0f;
        }
    }

    void Update()
    {
        if (isGazing)
        {
            timer += Time.deltaTime;

            if (loadingImg != null)
            {
                loadingImg.fillAmount = timer / gazeTime;
            }

            if (timer >= gazeTime)
            {
                isGazing = false;
                MakeTeleport();

                if (loadingImg != null)
                {
                    loadingImg.fillAmount = 0f;
                }
            }
        }
    }

    private void MakeTeleport()
    {
        player.transform.position = new Vector3(transform.position.x, 2.574f, transform.position.z);
    }
}
