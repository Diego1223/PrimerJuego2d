using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jugador : MonoBehaviour
{
    private Animator animator;
    
    public float fuerzaSalto;

    private Rigidbody2D rigidbody2d;

    public gameManager gameManager;

    void Start()
    {
        //Esto va a agarrar el componente animator
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //El if, lo que hace es que checa si se esta precionando la tecla space
        if (Input.GetKeyDown(KeyCode.Space)) {
            //Si se preciona, usamos SetBool, para cambiar la variable estaSaltando a true
            animator.SetBool("estaSaltando", true);
            //Con esto le agregamos la fuerza de salto
            rigidbody2d.AddForce(new Vector2(0.0f, fuerzaSalto));
        }
    }

    //Esta funcion, es la que detecta, cada que nuestro jugador choca con algo
    //Solo puse onCollisionEnter y di enter y se me autocompleto
    private void OnCollisionEnter2D(Collision2D collision) {
        //Si el gameObject, tiene un tag que "Suelo"
        //cambia la variable estaSaltando a false 
        if (collision.gameObject.tag == "Suelo") {
            animator.SetBool("estaSaltando", false);
        }

        if (collision.gameObject.tag == "Obstaculo") {
            gameManager.gameOver = true;
        }
    }
}
