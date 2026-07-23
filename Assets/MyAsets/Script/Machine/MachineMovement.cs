using Deforestation.Dinosaurus;
using Deforestation.Recolectables;
using UnityEngine;


namespace Deforestation.Machine
{

    // Controla el movimiento físico de la máquina
    public class MachineMovement : MonoBehaviour
    {


        #region Fields


        [SerializeField] private float _speedForce = 50;


        [SerializeField] private float _speedRotation = 15;


        private Rigidbody _rb;


        private Vector3 _movementDirection;


        private Inventory _inventory => GameController.Instance.Inventory;



        [Header("Energy")]

        [SerializeField] private float energyDecayRate = 20f;


        private float energyTimer = 0f;


        [Header("Jump")]

        [SerializeField] private float _jumpForce = 15f;

        [SerializeField] private int _jumpCost = 1;

        private bool _isGrounded;


        #endregion



        #region Unity Callbacks


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }



        private void Update()
        {
            if (_inventory == null)
                return;

            Debug.Log(_movementDirection);

            Debug.Log("Tiene HyperCrystal: " + _inventory.HasResource(RecolectableType.HyperCrystal));

            Debug.Log("¿Existe HyperCrystal? " + _inventory.InventoryStack.ContainsKey(RecolectableType.HyperCrystal));
            if (_inventory.InventoryStack.ContainsKey(RecolectableType.HyperCrystal))
            {
                Debug.Log("Cantidad: " + _inventory.InventoryStack[RecolectableType.HyperCrystal]);
            }

            // Si tiene energía, permite movimiento
            if (_inventory.HasResource(RecolectableType.HyperCrystal))
            {


                _movementDirection = new Vector3(Input.GetAxis("Vertical"), 0, 0);



                transform.Rotate(Vector3.up * _speedRotation * Time.deltaTime * Input.GetAxis("Horizontal"));


                Debug.DrawRay(transform.position,transform.InverseTransformDirection(_movementDirection.normalized) * _speedForce);


                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {

                    energyTimer += Time.deltaTime;


                    if (energyTimer >= energyDecayRate)
                        _inventory.UseResource(RecolectableType.HyperCrystal);

                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }

            }
            else
            {

                GameController.Instance.MachineController.StopMoving();

            }




            CheckGround();

            Debug.Log("Enabled: " + enabled);

        }



        // Aplica movimiento físico
        private void FixedUpdate()
        {

            _rb.AddRelativeForce( _movementDirection.normalized * _speedForce, ForceMode.Impulse);

        }



        // Verifica si la máquina está en el suelo
        void CheckGround()
        {

            RaycastHit hit;


            float maxDistance = 4f;


            float force = 100000;


            Vector3 direction = -transform.up;



            Debug.DrawRay(transform.position,direction * maxDistance,Color.red);


            int layerMask = 1 << LayerMask.NameToLayer("Terrain");

            _isGrounded = Physics.Raycast(transform.position,direction,out hit,maxDistance,layerMask);


            if (!_isGrounded)
            {

                _rb.AddRelativeForce(direction * force);

            }

        }




        // Colisión con árboles
        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Tree")
            {

                int index =
                    other.GetComponent<Tree>().Index;


                GameController.Instance.TerrainController.DestroyTree(index,other.transform.position);

            }

        }



        // Daño por colisión
        private void OnCollisionEnter(Collision collision)
        {

            // Busca sistema de vida
            HealthSystem target = collision.gameObject.GetComponent<HealthSystem>();


            // Hace daño
            if (target != null)
            {
                target.TakeDamage(10);
            }

        }

        // Verifica si la máquina está en el suelo y salta si no lo está
        private void Jump()
        {
            
            if (!_isGrounded)
                return;

            if (!_inventory.HasResource(RecolectableType.JumpCrystal, _jumpCost))
                return;

            _inventory.UseResource(RecolectableType.JumpCrystal, _jumpCost);

            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }


        #endregion



        #region Public Methods

        #endregion



        #region Private Methods

        #endregion


    }

}