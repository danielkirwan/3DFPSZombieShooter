using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _minHealth;
    [SerializeField] private int _maxHealth;
    private int _curentHealth;


    private void Start()
    {
        _curentHealth = _maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        _curentHealth -= damageAmount;
        Debug.Log("Health is now: " + _curentHealth);
        if(_curentHealth < _minHealth)
        {
            Debug.Log("Dead baby");
            Destroy(this.gameObject);
        }
    }


}
