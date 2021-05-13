using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Esto lo vamos a utilizar posteriormente, para reiniciar la escena
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    //Esto es para obtener el renderer
    public Renderer fondo;

    public GameObject column;

    //Esta variable la vamos a usar para guardar la instancia de la columna
    public List<GameObject> columns;

    public float velocidad = 2;

    public GameObject piedra1;
    public List<GameObject> obstaculos;

    public GameObject piedra2;

    public bool gameOver = false;

    public bool startGame = false;

    public GameObject menuInicio;
    public GameObject GameOverMenu;

    //Start, solo se llama una vez, al iniciar el juego
    void Start()
    {
        //Crear Mapa
        //Vamos a crear el mapa con un bucle for 
        //El 21, es el numero de columnas que vamos a crear 
        //Este bucle se va a repetir 20 veces 
        for (int i = 0; i < 21; i++) {
            //Con instantiate, creamos nuevos objetos
            //Aqui, instanciamos la columna, despues le decimos donde queremos instanciar la columna
            //En x, queremos que se cree al principio en -10 y en y que se cree en -1
            //Pero queremos que cada que se cree uno nuevo, se recora una posicion a la derecha
            //Por eso, ponemos -10 + i
            //Al final le ponemos la rotacion del objeto, Quaternion, y el identity es la rotacion incial del objeto, osea no va a rotar nada
            columns.Add(Instantiate(column, new Vector2(-10 + i, -3), Quaternion.identity));
        }

        //Crear piedras
        obstaculos.Add(Instantiate(piedra1, new Vector2(1, -2), Quaternion.identity));
        obstaculos.Add(Instantiate(piedra2, new Vector2(10, -2), Quaternion.identity));
        
    }
    //Update, se llama 1 vez por frame, osea mas o menos 60 veces por segundo, dependiendo la pc
    void Update()
    {
        if (startGame == false) {
            if (Input.GetKeyDown(KeyCode.X)) {
                startGame = true;
            }
        }

        if (startGame == true && gameOver == true) {
            GameOverMenu.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X)) {
                //Con esto, reiniciamos la escena
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (startGame == true && gameOver == false) {
            //Esto sirve para esconder el menuInicio
            menuInicio.SetActive(false);
            //Mandamos a llamar a la variable fondo, y a una propiedad que se llama material y ese material 
            //va a mandar a llamar otra propiedad que se llama mainTextureoffset, que es para mover el fondo 
            fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.02f, 0) * Time.deltaTime;

            //Mover el mapa
            //creamos una variable i con valor 0, despues usamos columns.Count, para que cuente cuantas columnas hay 
            for (int i = 0; i < columns.Count; i++)
            {
                //columns.[i].transform.position.x, si la posicion de lo columna de x, es menor o igual a -10
                if (columns[i].transform.position.x <= -10)
                {
                    //Ingresa a la posicion de columns, y le decimos que coloce nuestra columa en la posicion X: 10 y la Y: 1
                    columns[i].transform.position = new Vector3(10, -3, 0);
                }

                //aqui accedemos a una columna en especifico (i) y vemos cual es la posicion de esa columna
                //Despues le decimos, que se mueva una unidad hacia la izquierda (-1) y lo demas que no se mueva
                columns[i].transform.position = columns[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
            }

            //Mover obstaculos
            for (int i = 0; i < obstaculos.Count; i++)
            {
                //Si la posicion x del obstaculo es menor o igual a -10
                if (obstaculos[i].transform.position.x <= -10)
                {
                    //Creamos una variable randomObs
                    float randomObs = Random.Range(1, 15);
                    //Crea un nuevo vector 3, y le decimos que en el eje X
                    //escoja un valor random
                    obstaculos[i].transform.position = new Vector3(randomObs, -2, 0);
                }
                obstaculos[i].transform.position = obstaculos[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
            }
        }
    }
}
