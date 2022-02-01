using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class UnityGameOverChecker : MonoBehaviour
{
    private static UnityGameOverChecker _instance;
    public static UnityGameOverChecker Instance { get { return _instance; } }

    [SerializeField]
    uint lifes = 10;

    public System.Action<uint> OnLifesChange;
    public System.Action OnGameOver;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Collider col = GetComponent<Collider>();

        rb.isKinematic = true;
        col.isTrigger = true;
        rb.useGravity = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enteringEnemy))
        {
            lifes--;
            enteringEnemy.Delete();
            OnLifesChange?.Invoke(lifes);

            if (lifes < 1)
            {
                OnGameOver?.Invoke();
            }
        }
    }

    public uint Lifes
    {
        get { return lifes; }
    }
    public void SetLifes(uint lifes)
    {
        this.lifes = lifes;
        OnLifesChange?.Invoke(lifes);

    }
}
