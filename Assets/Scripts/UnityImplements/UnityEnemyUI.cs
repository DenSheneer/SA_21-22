using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityEnemyUI : MonoBehaviour, System.IObserver<Enemy>
{
    [SerializeField]
    int enemyhealth;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Image greenHealthImage;


    float maxWidth;
    float scalePerHealthUnit;

    System.IDisposable cancellation;

    public void Start()
    {
        Enemy enemy = GetComponent<Enemy>();
        cancellation = enemy.Subscribe(this);
        canvas.worldCamera = Camera.main;

        maxWidth = greenHealthImage.transform.localScale.x;
        float maxHealth = enemy.GetMaxHealth;
        scalePerHealthUnit = maxWidth / maxHealth;
    }

    public void OnCompleted()
    {
        //cancellation.Dispose();
    }

    public void OnError(System.Exception error)
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        canvas.transform.LookAt(Camera.main.transform);
    }

    public void OnNext(Enemy enemy)
    {
        float calcValue = maxWidth - (enemy.GetHealth * scalePerHealthUnit);


        greenHealthImage.transform.localScale = new Vector3( enemy.GetHealth * scalePerHealthUnit,
                                                             greenHealthImage.transform.localScale.y,
                                                             greenHealthImage.transform.localScale.z);

        greenHealthImage.transform.localPosition = new Vector3( calcValue * 0.5f,
                                                                greenHealthImage.transform.localPosition.y,
                                                                greenHealthImage.transform.localPosition.z);
    }
}
