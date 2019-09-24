using UnityEngine;
using System.Collections;
using Assets.MyGame.Scripts;
using UnityEngine.AI;
using Assets.Game.Scripts;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField]
    private PlayerConnection health;

    [Header("Arma")]

    [SerializeField]
    public GameObject armaGO;

    [Header("Atributos")]
    [SerializeField]
    private int vida;
    [SerializeField]
    private int mana;
    [SerializeField]
    private int lvl;

    //Controladores
    private int danoMaximo, currentSkill;
    [SerializeField]
    private float attackSpeed = 3, timer;

    [Header("Skills")]
    [SerializeField]
    private int skillMelee;
    [SerializeField]
    private int skillRanged;
    [SerializeField]
    private int skillMagic;
    public bool casting;
    public bool attacking;


    [Header("Outros")]
    [SerializeField]
    private float range;
    [SerializeField]
    private float missPercentage;
    [SerializeField]
    private float criticPercentage;
    [SerializeField]
    private int additionalCriticalDamage;
    [SerializeField]
    private int danoArma;

    //componentes
    protected NavMeshAgent playerNavAgent;
    protected bool mWalk;
    protected Animator mAnimator;
    private Weapon arma;
    [SerializeField]
    private GameObject select;
    private Character player;
    private Damage damage;

    public float distance;

    private void Awake()
    {
        health.Initialize();
        mAnimator = GetComponent<Animator>();
        playerNavAgent = GetComponent<NavMeshAgent>();
        if (armaGO != null)
        {
            arma = armaGO.GetComponent<Weapon>();
            danoArma = arma.dano;
        }
        else if (armaGO == null)
        {
            arma = FindObjectOfType<Punhos>();
            danoArma = arma.dano;
        }

    }
    private void FixedUpdate()
    {
        Timer();
        MouseSelect();
        MoveAnimation();
        CheckWeapon();
        Run();  
        if(select != null)
        {
            Selected();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //CombatTextManager.Instance.CreatText(select.transform.position, skillMelee.ToString(), Color.white);
        }
        /*
       if (Input.GetKeyDown(KeyCode.A))
       {
           mAnimator.SetBool("hit", false);
           casting = false;
       }*/
    }
    public void TakeDamage(Damage dam)
    {
        int takeDamage = dam.DamageVal;
        health.CurrentVal -= takeDamage;

        if (takeDamage > 0)
        {
            if (dam.Type == type.normal)
            {
                CombatTextManager.Instance.AttackText(transform, dam.DamageVal);
                return;
            }
            else if (dam.Type == type.critic)
            {
                CombatTextManager.Instance.CriticText(transform, dam.DamageVal);
                return;
            }
        }
        else if (takeDamage <= 0 && dam.Type == type.miss)
        {
            CombatTextManager.Instance.MissText(transform);
            return;
        }
        return;
    }
    private void Casting()
    {
        casting = true;
        mAnimator.SetBool("hit", true);
    }
    private void Timer()
    {
        if (timer < attackSpeed)
        {
            timer += Time.deltaTime;
        }

    }
    private void MouseSelect()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {

                if (hit.transform.gameObject.tag == "Terrain")
                {

                    select = null;
                    Move(hit.point);
                    if (playerNavAgent.isStopped)
                    {
                        
                    }
                }
                else
                {
                    select = hit.transform.gameObject;               
                }


            }
        }



    }
    private void CheckWeapon()
    {
        if (armaGO != null)
        {
            arma = armaGO.GetComponent<Weapon>();
            danoArma = arma.dano;
        }
        else if (armaGO == null)
        {
            arma = FindObjectOfType<Punhos>();
            danoArma = arma.dano;
        }
    }
    private void Move(Vector3 destino)
    {

        playerNavAgent.destination = destino;



    }
    private void MoveAnimation()
    {
        float speedPercent = playerNavAgent.velocity.magnitude / playerNavAgent.speed;
        mAnimator.SetFloat("speedPercent", speedPercent);// 0.1f, Time.deltaTime);

    }
    private void Run()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mAnimator.SetBool("run", true);
            playerNavAgent.speed = 5;
            playerNavAgent.acceleration = 10;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            mAnimator.SetBool("run", false);
            playerNavAgent.speed = 2;
            playerNavAgent.acceleration = 4;
        }

    }
    private void Selected()
    {

        distance = Vector3.Distance(this.gameObject.transform.position, select.gameObject.transform.position);
        if (select.tag == "Enemy")
        {

            if (!casting && !attacking)
            {
                if (distance < range)
                {

                  
                    StartCoroutine(Attack());

                }
                else
                {
                    
                    Move(select.transform.position);
                }
            }
        }

    }
    private IEnumerator Attack()
    {
        attacking = true;
        GameObject alvo = select;
        
        CharacterManager.RotateTowards(alvo.transform, transform);
        currentSkill = CharacterManager.CheckSkill(arma, skillMelee, skillRanged, skillMagic);
        danoMaximo = CharacterManager.MaxDamage(arma.dano, currentSkill);


        if (timer >= attackSpeed)
        {
            timer = 0;
            bool miss = CharacterManager.RandomBool(missPercentage, 100f + currentSkill);


            if (!miss)
            {
                bool critico = CharacterManager.RandomBool(criticPercentage, 100f);
                if (critico)
                {
                    mAnimator.SetTrigger("Attack");
                    yield return new WaitForSeconds(0.8f);
                    damage = new Damage(CharacterManager.Critical(danoMaximo, additionalCriticalDamage), type.critic, typeDamage.physic);
                    if (alvo != null) alvo.GetComponent<Character>().TakeDamage(damage);
                   


                }
                else
                {
                    mAnimator.SetTrigger("Attack");
                    yield return new WaitForSeconds(0.8f);
                    damage = new Damage(CharacterManager.Damage(danoMaximo), type.normal, typeDamage.physic);
                    if (alvo != null) alvo.GetComponent<Character>().TakeDamage(damage);
                    
                }

            }
            else
            {
                mAnimator.SetTrigger("Attack");
                yield return new WaitForSeconds(0.8f);
                damage = new Damage(0, type.miss, typeDamage.physic);
                if(alvo != null) alvo.GetComponent<Character>().TakeDamage(damage);
               
            }
        }

        attacking = false;

    }
}

