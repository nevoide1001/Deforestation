using UnityEngine;


namespace Deforestation
{

    // Clase base para crear Singleton
    public class Singleton<T> : MonoBehaviour
        where T : MonoBehaviour
    {


        private static T _instance;



        // Acceso global al Singleton
        public static T Instance
        {

            get
            {

                if (_instance == null)
                {

                    _instance = FindObjectOfType<T>();



                    if (_instance == null)
                    {

                        GameObject obj = new GameObject();


                        obj.name = typeof(T).Name;


                        _instance = obj.AddComponent<T>();

                    }

                }


                return _instance;

            }

        }




        // Controla que solo exista uno
        protected virtual void Awake()
        {


            if (_instance == null)
            {

                _instance =this as T;



                DontDestroyOnLoad( this.gameObject );

            }
            else
            {

                Destroy( gameObject );

            }

        }


    }

}
