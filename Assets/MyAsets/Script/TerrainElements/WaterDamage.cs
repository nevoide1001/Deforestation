using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.TerrainElements
{
    public class WaterDamage : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _damagePerSecond = 10f;

        #endregion

        #region private Methods

        private void OnTriggerStay(Collider other)
        {
            // Hacer daño al cualquier objeto que tenga un componente de salud
            HealthSystem health = other.GetComponent<HealthSystem>();

            if (health != null)
            {
                health.TakeDamage(_damagePerSecond * Time.deltaTime);
            }
        }

        #endregion
    }
}
