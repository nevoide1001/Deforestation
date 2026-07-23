using UnityEngine;
using System;


namespace Deforestation.Machine.Weapon
{

    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {


        #region Fields


        [SerializeField] private GameObject _explosionPrefab;


        [SerializeField] private float _force = 100;


        [SerializeField] private float _damage = 10;


        private Rigidbody _rb;


        #endregion



        #region Unity Callbacks


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }



        // Aplica impulso inicial
        private void Start()
        {
            _rb.velocity = transform.forward * 20f;

            Debug.DrawRay(transform.position, transform.forward * 10, Color.red, 5);
            Debug.Log(transform.forward);
            Debug.Log(_rb);
        }



        // Impacto con otro collider
        private void OnTriggerEnter(Collider other)
        {

            Debug.Log("He golpeado: " + other.name);

            HealthSystem health = other.GetComponent<HealthSystem>();


            if (health != null) 
                health.TakeDamage(_damage);



            Instantiate( _explosionPrefab, transform.position, Quaternion.identity );



            GetComponent<Collider>().enabled = false;



            Destroy(gameObject, 1);

        }


        #endregion


    }

}