using System.Collections.Generic;
using UnityEngine;


namespace Deforestation.Machine.Weapon
{

    public class Explosion : MonoBehaviour
    {


        #region Fields


        [SerializeField]
        private float _blastRadius = 15f;


        [SerializeField]
        private float _explosionForce = 1000f;


        [SerializeField]
        private float _maxDamage = 100f;



        private TreeTerrainController _terrainController =>
            GameController.Instance.TerrainController;


        #endregion



        #region Unity Callbacks


        private void Start()
        {

            Explode();


            Destroy(gameObject, 2);

        }


        #endregion



        #region Public Methods


        // Lógica principal de explosión
        public void Explode()
        {


            // Destruye árboles dentro del radio de explosión
            TreeInstance[] trees = _terrainController.Trees;


            for (int i = trees.Length - 1; i >= 0; i--)
            {

                TreeInstance tree = trees[i];


                Vector3 treeWorldPos = _terrainController.TreeToWorldPosition(tree);



                if (Vector3.Distance( transform.position, treeWorldPos ) <= _blastRadius)
                {

                    GameObject newTree = _terrainController.DestroyTree( i, treeWorldPos );



                    newTree.GetComponent<Rigidbody>() .AddTorque( _explosionForce * Random.Range(-20, 20), 0, _explosionForce * Random.Range(-20, 20));

                }

            }



            // Objetos con colisión dentro del radio de explosión
            Collider[] colliders =
                Physics.OverlapSphere( transform.position, _blastRadius );



            foreach (Collider hit in colliders)
            {

                Rigidbody rb = hit.GetComponent<Rigidbody>();


                if (rb != null)
                {
                    rb.AddExplosionForce( _explosionForce, transform.position, _blastRadius);



                    Vector3 torque = Random.insideUnitSphere * _explosionForce / 100;


                    rb.AddTorque(torque);
                }

                HealthSystem health = hit.GetComponentInParent<HealthSystem>();


                if (health != null)
                {

                    float distance = Vector3.Distance( hit.transform.position, transform.position);



                    float damage = Mathf.Lerp(_maxDamage, 0, distance / _blastRadius);



                    health.TakeDamage(damage);

                }

            }

        }


        #endregion

    }

}