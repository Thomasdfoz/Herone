using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

/* Controls the player. Here we choose our "focus" and where to move. */

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    //constantes
    const float Radius = 25;

    //
    public Interactable focus;  // Our current focus: Item, Enemy etc.
    [SerializeField]
    LayerMask movementMask;  // Filter out everything not walkable

    //
    [Space]
    public GameObject magic;

    //effect of selected
    [Space]
    public GameObject selectedEffect;
    public GameObject directionEffect;

    [Space]
    [Header("Bottons")]
    public Button btnQ;
    public Button btnW;
    public Button btnE;
    public Button btnR;

    //
    CharacterCombat combat;
    PlayerStats playerStats;
    Collider[] colliders;
    bool mag;
    GameObject temp;
    Camera cam;         // Reference to our camera
    PlayerMotor motor;  // Reference to our motor
    GameObject effects; // para poder destroir o objeto sem precisar dar um find.



    // Get references
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Inputs();

        if (focus != null)
        {
            if (focus.GetComponent<EnemyStats>())
            {
                if (focus.GetComponent<EnemyStats>().race != playerStats.race)
                {
                    PreparingToAttack();
                }
            }

        }
    }
    private void Inputs()
    {

        // If we press left mouse

        if (Input.GetMouseButtonDown(0))
        {
            // We create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits
            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                motor.MoveToPoint(hit.point);   // Move to where we hit    
                RemoveFocus();
                if (effects != null)
                    DestroyEffects(effects);
                InstanceEffects(hit.point, directionEffect);

            }
        }

        // If we press right(eu coloquei Left) mouse
        if (Input.GetMouseButtonDown(1))
        {
            // We create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits
            if (Physics.Raycast(ray, out hit, 100))
            {

                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float menorDistance = Radius;
            colliders = Physics.OverlapSphere(transform.position, Radius, 1);

            foreach (Collider col in colliders)
            {
                if (col.GetComponent<Interactable>())
                {
                    if (col.GetComponent<CharacterStats>())
                    {
                        if (playerStats.race != col.GetComponent<CharacterStats>().race)
                        {
                            float distance = Vector3.Distance(transform.position, col.transform.position);
                            if (distance < menorDistance)
                            {
                                menorDistance = distance;
                                SetFocus(col.GetComponent<Interactable>());
                            }

                        }
                    }


                }
            }

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            btnQ.onClick.Invoke();

        }
    }
    private void PreparingToAttack()
    {
        playerStats = GetComponent<PlayerStats>();

        if (Vector3.Distance(transform.position, focus.transform.position) <= playerStats.rangeAttack)
        {
            combat = GetComponent<CharacterCombat>();
            if (combat != null)
            {
                if (playerStats.ranged)
                {
                    combat.AttackRanged(focus.GetComponent<EnemyStats>());
                }
                else
                {
                    combat.Attack(focus.GetComponent<EnemyStats>());
                }
            }
        }
    }

    // Set our focus to a new focus
    void SetFocus(Interactable newFocus)
    {

        // If our focus has changed
        if (newFocus != focus)
        {
            // Defocus the old one
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;   // Set our new focus
            motor.FollowTarget(newFocus);   // Follow the new focus
        }

        newFocus.OnFocused(transform);
        InstanceEffects(selectedEffect, newFocus.transform.position);
    }

    // Remove our current focus
    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        motor.StopFollowingTarget();

    }
    void InstanceEffects(GameObject goEffect, Vector3 posAlvo)
    {
        DestroyEffects(effects);
        effects = Instantiate(goEffect, posAlvo, Quaternion.identity);
    }
    void InstanceEffects(Vector3 posAlvo, GameObject goEffect)
    {
        Instantiate(goEffect, posAlvo, Quaternion.identity);
    }
    void DestroyEffects(GameObject go)
    {
        Destroy(go);
    }
    public void BtnQ()
    {

        mag = !mag;
        if (mag)
        {
            temp = Instantiate(magic, transform);
            temp.GetComponent<MagicScript>().dono = this.gameObject;
        }
        else
        {
            Destroy(temp);
        }

    }

}
