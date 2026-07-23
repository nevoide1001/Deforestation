namespace Deforestation.Interaction
{

    // Define objetos que pueden ser interactuados
    public interface IInteractable
    {

        public void Interact();


        public InteractableInfo GetInfo();

    }


    // Datos de la interacción para mostrar en UI
    [System.Serializable]
    public class InteractableInfo
    {

        public string Action;


        public string Type;

    }

}