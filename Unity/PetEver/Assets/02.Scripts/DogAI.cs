using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // add AI navigation system


public class DogAI : MonoBehaviour
{

    public LayerMask isPlayer;
    private GameObject owner; // tracing target
    private NavMeshAgent navMeshAgent; // assign navmeshagent component
    private Animator dogAnimator;

    public GetColliderScript getCollider; 

    private float dog_normalSpeed = 3.5f;
    private float dog_trackingSpeed = 8f;
   // private float dog_collisionValue = 4.5f;


    private float range = 20f; // standard range for generating random point  
    private Vector3 point; // random point for dog AI moving 
    private Vector3 lastpos; // for determine dog is walking or not  
    private bool arrived = true;
    private bool trackingOwner = false;
    private float timer = 0f;
    private float escapeCount = 0f;
    private int collided_tag_number = -1; // give never-using value to initialize

    private bool meetOwner
    {
        get
        {
            if (getCollider.collided_tag == "Owner")
            {
                return true;
            }

            return false;
        }
    }

    private bool walking
    {
        get
        {
            if (!(gameObject.transform.position.x == lastpos.x))
            {
                return true;
            }
            return false;
        }
    }

    //make random point
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range; // take random point from 'range' radious sphere around center
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }



    void Awake()
    {


    }

    // Start is called before the first frame update
    void Start()
    {
        getCollider = gameObject.GetComponent<GetColliderScript>(); // get collider data from 'GetColliderScript' class 

        dogAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        owner = GameObject.FindGameObjectWithTag("Owner");

        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        if (!gameObject.CompareTag("NPC"))
        {
            Tracking();
            DogAnimation();
        }
        else
        {
            DogAnimation();
        }

        lastpos = gameObject.transform.position;

    }

    // randomly dog moves 
    IEnumerator UpdatePath()
    {
        while (true)
        {
            if (arrived && !trackingOwner)
            {
                arrived = false;
                if (RandomPoint(this.gameObject.transform.position, range, out point))
                {
                    navMeshAgent.speed = dog_normalSpeed;
                    navMeshAgent.SetDestination(point);
                    //Debug.DrawRay(point, Vector3.up, Color.red, 10.0f);
                }
            }

            if ((Mathf.Approximately(gameObject.transform.position.x, point.x) || timer > 4f) && !trackingOwner)
            {
                arrived = true;
                timer = 0;
            }

            yield return new WaitForSeconds(0.25f);
        }
    }


    void Tracking()
    {

            if (Input.GetKeyDown(KeyCode.Space)) // give priority when Owner calls. When Owner calls, dog only chases Owner even it meets Dog, Flower, Butterfly or etc.   
            {
                if (!meetOwner)
                { 
                    trackingOwner = true;
                    arrived = false;
                }

                else
                {
                    trackingOwner = false;
                    arrived = true;
                }
            }

            if (trackingOwner)
            {
                timer = 0;
                navMeshAgent.speed = dog_trackingSpeed;
                navMeshAgent.SetDestination(owner.transform.position);
            }

            else
            {
                switch (getCollider.collided_tag)
                {
                    case ("Flower"): // enum value in 'GetColliderScript' (int -> 1)
                        {                       
                            navMeshAgent.SetDestination(getCollider.collided_object.transform.position);
                            collided_tag_number = 1;
                            arrived = true;
                            break;
                        }

                    case ("Butterfly"): // enum value in 'GetColliderScript' (int -> 2)
                        {
                            collided_tag_number = 2;
                            navMeshAgent.speed = dog_trackingSpeed;
                            navMeshAgent.SetDestination(getCollider.collided_object.transform.position);
                            break;
                        }

                    case ("Dog"): // enum value in 'GetColliderScript' (int -> 3)
                        {
                            collided_tag_number = 3;
                            break;
                        }
                }
            }
        
    }


    void DogAnimation()
    {
        dogAnimator.SetBool("arrived", arrived);
        dogAnimator.SetBool("meetOwner", meetOwner);
        dogAnimator.SetBool("walking", walking);
        dogAnimator.SetBool("trackingOwner", trackingOwner);
        dogAnimator.SetFloat("escapeCount", escapeCount);
        dogAnimator.SetFloat("dogSpeed", navMeshAgent.velocity.magnitude);
        dogAnimator.SetInteger("whatCollided", collided_tag_number);

        
        if (meetOwner)
        {           
            if (dogAnimator.GetCurrentAnimatorStateInfo(0).IsName("turn_around") &&
                     (dogAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f))
            {
                escapeCount++;
                if (escapeCount > 3)
                {
                    arrived = true;
                    escapeCount = 0;
                    navMeshAgent.isStopped = false;

                }
            }
        }
    }
}

