using System;
using UnityEngine;


namespace Deforestation
{

    // Sistema de vida
    public class HealthSystem : MonoBehaviour
    {


        public event Action<float> OnHealthChanged;


        public event Action OnDeath;



        #region Fields


        [SerializeField]
        private float _maxHealth = 100f;


        private float _currentHealth;



        #endregion



        // Inicializa vida
        private void Awake()
        {

            _currentHealth = _maxHealth;

        }



        // Recibe daño
        public void TakeDamage(float damage)
        {

            _currentHealth -= damage;


            OnHealthChanged?.Invoke( _currentHealth);



            if (_currentHealth <= 0)
            {

                Die();

            }

        }


        // Cura vida
        public void Heal(float amount)
        {

            _currentHealth += amount;


            _currentHealth = Mathf.Min( _currentHealth, _maxHealth);



            OnHealthChanged?.Invoke( _currentHealth);

        }





        // Cambia vida manualmente
        public void SetHealth(float value)
        {

            _currentHealth = value;


            _currentHealth = Mathf.Min( _currentHealth, _maxHealth);


            OnHealthChanged?.Invoke( _currentHealth);

        }




        // Ejecuta muerte
        private void Die()
        {

            OnDeath?.Invoke();



        }


    }

}