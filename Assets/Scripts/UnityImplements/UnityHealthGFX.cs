using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityHealthGFX : MonoBehaviour, iEnemyHealthGFX
{
    float healthbarWidth = 0;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Image greenHealthImage;

    void Start()
    {
        if (canvas != null)
        {
            canvas.worldCamera = Camera.main;
        }
        if (greenHealthImage != null)
        {
            healthbarWidth = greenHealthImage.transform.localScale.x;
        }
    }

    void Update()
    {
        canvas.transform.rotation = Quaternion.identity;
    }

    public void SetPercentage(float percentage)
    {
        if (percentage > 1.0f) { percentage = 1.0f; }
        if (percentage < 0.0f) { percentage = 0.0f; }

        greenHealthImage.transform.localScale = new Vector3( healthbarWidth * percentage,
                                                             greenHealthImage.transform.localScale.y,
                                                             greenHealthImage.transform.localScale.z);
    }
}
