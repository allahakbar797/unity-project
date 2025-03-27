using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
public class Player : MonoBehaviour
{

    public float speed = 5f;
    public float forzarimbalzo = 10f;
    public Animator animator;
    
    private float horizontalInput;
    private float verticalInput;
    private float jumpSpeed = 10f;
    public float longitudRaycast = 0.1f;
    public LayerMask floor;
    private bool capfloor; 
    private bool takingdamage;
    private Rigidbody2D rb;

    public float GetKeyDown;
    public int lowerBound = -10;

  
  


    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
      
    {
    
     
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right*speed*Time.deltaTime*horizontalInput);


        animator.SetFloat("movement",horizontalInput * speed);

        if (horizontalInput < 0)
        {
        transform.localScale = new Vector4(-5,5,5);
        }

        if(horizontalInput > 0) 
        {
        transform.localScale = new Vector4(5,5,5);
        }
        Vector4 position = transform.position;
        if (!takingdamage)
        {
          
        }
          
          RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, floor );
          capfloor =hit.collider != null; 
          if (capfloor && Input.GetKeyDown(KeyCode.Space) && !takingdamage)
          {
            rb.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
          }
          animator.SetBool("capfloor", capfloor);
          animator.SetBool("takedamage", takingdamage);

         
        if (transform.position.y <lowerBound)
        { 
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
         Debug.Log("Sono Caduto!");
        }


    }
    public void takedamage(Vector2 direction, int quantdamage)
    {

      if(!takingdamage)
      {
        takingdamage = true;
        Vector2 rimbalzo = new Vector2(transform.position.x - direction.x, 1).normalized;
        rb.AddForce(rimbalzo * forzarimbalzo, ForceMode2D.Impulse);
      }
    }
    public void disattivadamage()
    {
      takingdamage = false;
    }
   




}