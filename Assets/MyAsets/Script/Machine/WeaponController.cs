using UnityEngine;
using System;


namespace Deforestation.Machine.Weapon
{

    public class WeaponController : MonoBehaviour
    {


        #region Properties


        // Evento de disparo
        public Action OnMachineShoot;

        public Transform TowerWeapon => _towerWeapon;


        #endregion



        #region Fields


        [SerializeField] private Transform _towerWeapon;


        [SerializeField] private Transform _spawnPoint;


        [SerializeField] private float _speedRotation = 5f;


        [SerializeField] private Bullet _bulletPrefab;


        [SerializeField] private GameObject _smokeShoot1;
        [SerializeField] private GameObject _smokeShoot2;


        #endregion



        #region Unity Callbacks


        private void Awake()
        {
        }



        void Update()
        {
            if (!GameController.Instance.MachineModeOn)
                return;

            Ray ray = GameController.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(_towerWeapon.position, hit.point, Color.red);
                Debug.Log(hit.collider.name);

                Vector3 direccion = _towerWeapon.parent.InverseTransformPoint(hit.point);

                direccion.y = 0;

                Quaternion objetivo = Quaternion.LookRotation(direccion);

                // Si el modelo apunta hacia -Z
                objetivo *= Quaternion.Euler(0, 180, 0);

                _towerWeapon.localRotation = Quaternion.Slerp(
                    _towerWeapon.localRotation,
                    objetivo,
                    _speedRotation * Time.deltaTime);

            }

            // Disparo
            if (Input.GetMouseButtonUp(0) && GameController.Instance.MachineModeOn && GameController.Instance.Inventory.UseResource(Recolectables.RecolectableType.SuperCrystal))
            {
                 Shoot(hit.point);
            }
        }




        // Disparo de bala
        public void Shoot(Vector3 lookAtPoint)
        {



            Bullet bullet = Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);

            Debug.DrawRay( bullet.transform.position, bullet.transform.forward * 5, Color.red, 10f);


            _smokeShoot1.SetActive(true);
            _smokeShoot2.SetActive(true);


            OnMachineShoot?.Invoke();

        }


        #endregion



        #region Public Methods

        #endregion



        #region Private Methods

        #endregion


    }

}