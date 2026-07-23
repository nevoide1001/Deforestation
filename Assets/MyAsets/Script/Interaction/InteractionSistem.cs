using UnityEngine;
using Deforestation.UI;
using Deforestation.Recolectables;
using System;


namespace Deforestation.Interaction
{
    public class InteractionSystem : MonoBehaviour
    {


        #region Properties

        // Evento para mostrar texto de interacción
        public event Action<string> OnShowInteraction;


        // Evento para ocultar texto
        public event Action OnHideInteraction;

        // Evento para mostrar el panel de entrega
        public event Action OnShowDelivery;

        // Evento para ocultar el panel de entrega
        public event Action OnHideDelivery;

        #endregion



        #region Fields


        [SerializeField] float _widthDetector = 1;


        [SerializeField] float _distanceDetector = 5;


        [SerializeField] Inventory _inventory;


        private bool _interactebleDetected = false;


        private IInteractable _currentInteraction;


        #endregion



        #region Unity Callbacks


        private void Update()
        {

            // Si hay objeto y se pulsa E para interactuar añade el recolectable al inventario y llama a la función de interacción
            if (_interactebleDetected && Input.GetKeyUp(KeyCode.E))
            {

                if (_currentInteraction is Recolectable recolectable)
                {

                    _inventory.AddRecolectable( recolectable.Type, recolectable.Count );


                    recolectable.Interact();

                }


                if (_currentInteraction is MachineInteraction machineInteraction)
                {
                    machineInteraction.Interact();
                }
            }

        }



        // Detecta objetos delante del jugador
        void FixedUpdate()
        {

            RaycastHit hit;


            if (Physics.SphereCast( Camera.main.transform.position, 0.5f, Camera.main.transform.forward, out hit, 5))
            {

                IInteractable interaction =hit.collider.GetComponent<IInteractable>();


                if (interaction != null)
                {

                    InteractableInfo info = interaction.GetInfo();


                    OnShowInteraction.Invoke( "E - To " + info.Action + " " + info.Type );

                    if (interaction is MachineInteraction machineInteraction && machineInteraction.Type == MachineInteractionType.Delivery)
                    {
                        OnShowDelivery?.Invoke();
                    }
                    else
                    {
                        OnHideDelivery?.Invoke();
                    }


                    _interactebleDetected = true;
                    _currentInteraction = interaction;


                    return;
                }
            }


            _interactebleDetected = false;
            OnHideInteraction.Invoke();
            OnHideDelivery.Invoke();

        }


        #endregion



        #region Public Methods

        #endregion



        #region Private Methods

        #endregion



        // Dibuja detector en escena
        void OnDrawGizmos()
        {

            if (Camera.main != null)
            {

                Vector3 startPosition = Camera.main.transform.position;


                Vector3 direction = Camera.main.transform.forward;


                float radius = 0.5f;
                float distance = 5f;


                Gizmos.color = Color.blue;


                Gizmos.DrawWireSphere( startPosition, radius );


                Gizmos.DrawWireSphere( startPosition + direction * distance, radius );


                Gizmos.DrawLine( startPosition, startPosition + direction * distance );
            }

        }

    }
}