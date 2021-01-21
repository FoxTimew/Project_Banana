using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public WeaponsInfos stats;

    public List<int> damageCombos = new List<int>();

    public List<AnimationCurve> pousseeCombos = new List<AnimationCurve>();

    public List<float> stunTime = new List<float>();

    public int damageSpecial;

    public AnimationCurve pousseeSpecial;

    public float range;

    public Transform attackPoint;

    public LayerMask enemyLayer;

    public float annulerCombosTemps, annulerCharmCombosTemps, charmCausticRadius, charmCausticValue, berzerkValue;

    public float vitesse, forceBonus, pousseeForce, charmCombosDamage, mCharmPousseeValue, volDeVieValue;
    public float[] dammageBonus;

    Controler controler;

    public int combos, force, charmCombos = 1;

    public bool charmCombosEnable, causticCharmEnable, causticCharmPourcent, combosCharmEnable, volDeVieCharm;

    private Coroutine combosCoroutine;
    private Coroutine charmCombosCoroutine;

    Health heal;

    public bool attackBlocked, ghostAttack;

    public float dureeGhostAttack;

    Coroutine ghostAttackCoroutine;

    [SerializeField]
    float animationDelay;

    private void Start()
    {
        heal = this.GetComponent<Health>();
        controler = this.GetComponent<Controler>();

        for (int i = 0; i < stats.damageCombos.Length; i++)
        {
            damageCombos.Add(stats.damageCombos[i]);
        }

        for (int i = 0; i < stats.pousseeCombos.Length; i++)
        {
            pousseeCombos.Add(stats.pousseeCombos[i]);
        }

        for (int i = 0; i < stats.stunTime.Length; i++)
        {
            stunTime.Add(stats.stunTime[i]);
        }

        damageSpecial = stats.damageSpecial;

        pousseeSpecial = stats.pousseeSpecial;

        range = stats.range;

        annulerCombosTemps = stats.annulerCombosTemps;

        vitesse = stats.vitesse;
        dammageBonus = new float[6];
    }

	private void Update()
	{
        if (ghostAttack) AttackSysteme();
	}

	public void AttackSysteme()
    {
        if (attackBlocked && controler.isAttacking)
        {
            GhostAttack();
            return;
        }
        else if (attackBlocked) return;
        ghostAttack = false;
        if (ghostAttackCoroutine != null) StopCoroutine(ghostAttackCoroutine);
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer);
        if (enemy == null) return;
        foreach (Collider2D enemyTouche in enemy)
        {
            int finalDamage = Mathf.RoundToInt((damageCombos[combos] + force +  dammageBonus[0]) + (damageCombos[combos] + force + dammageBonus[0]) * berzerkValue);
            if (enemyTouche.tag == "Enemy")
            {
                enemyTouche.gameObject.GetComponent<EnemySys>().TakeDamage(finalDamage, stunTime[combos]);
                PousseeEnnemi(pousseeCombos[combos], enemyTouche.gameObject, attackPoint);
            }
            else if (enemyTouche.tag == "BossHand" && (enemyTouche.gameObject.name == "Main_1" || enemyTouche.gameObject.name == "Main_0"))
            {
                enemyTouche.gameObject.GetComponent<PoinAttaque>().TakeDammage(finalDamage);
            }
            else if (enemyTouche.tag == "BossHand" && (enemyTouche.gameObject.name == "Main_2" || enemyTouche.gameObject.name == "Main_3"))
            {
                enemyTouche.gameObject.GetComponent<SlapAttaque>().TakeDammage(finalDamage);
            }
            else if (enemyTouche.tag == "Barril" && pousseeCombos[combos].length != 0)
            {
                Debug.Log("action");
                enemyTouche.GetComponent<Barril_Sys>().EjectingStart(pousseeCombos[combos], attackPoint.localPosition);
            }
            if (combosCharmEnable)
            {
                if (combos == damageCombos.Count - 1)
                {
                    if (enemyTouche.tag == "Enemy") enemyTouche.gameObject.GetComponent<EnemySys>().TakeDamage(Mathf.RoundToInt(charmCombos), 0f);
                    if (enemyTouche.tag == "BossHand" && (enemyTouche.gameObject.name == "Main_1_Ombre" || enemyTouche.gameObject.name == "Main_0_Ombre")) enemyTouche.gameObject.GetComponent<PoinAttaque>().TakeDammage(Mathf.RoundToInt(charmCombos));
                    if (enemyTouche.tag == "BossHand" && (enemyTouche.gameObject.name == "Main_2_Ombre" || enemyTouche.gameObject.name == "Main_3_Ombre")) enemyTouche.gameObject.GetComponent<SlapAttaque>().TakeDammage(Mathf.RoundToInt(charmCombos));
                }
            }
            if (volDeVieCharm)
            {
                heal.Heal(finalDamage * (volDeVieValue / 100), false);
            }
        }

        if (combos == damageCombos.Count - 1)
        {
            if (combosCoroutine != null) StopCoroutine(combosCoroutine);
            combos = 0;
        }
        else
        {
            if (combosCoroutine != null) StopCoroutine(combosCoroutine);
            combosCoroutine = StartCoroutine(combosCount(annulerCombosTemps));
        }
        StartCoroutine(WaitForAttackAgain(vitesse));
        controler.isAttacking = false;
    }

    void PousseeEnnemi(AnimationCurve force, GameObject enemy, Transform attackDirection)
    {
        float angle = CalculArcTangante(attackDirection);
        Vector2 direction;

        if ((angle > -22.5f && angle < 0.0f) || (angle >= 0.0f && angle < 22.5f))
        {
            direction = Vector2.up;
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            direction = new Vector2(1f, 1f).normalized;
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            direction = Vector2.right;
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            direction = new Vector2(1f, -1f).normalized;
        }
        else if ((angle >= 157.5 && angle <= 180) || (angle > -180 && angle < -157.5))
        {
            direction = Vector2.down;
        }
        else if (angle >= -157.5f && angle < -112.5f)
        {
            direction = new Vector2(-1f, -1f).normalized;
        }
        else if (angle >= -112.5f && angle < -67.5f)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = new Vector2(-1f, 1f).normalized;
        }
        StartCoroutine(EjectingDelay(force, enemy, direction));

        //enemy.gameObject.GetComponent<Rigidbody2D>().velocity = direction * force.Evaluate();
    }

    public void charmCaustic(int damage)
    {
        if (!causticCharmEnable) return;
        Collider2D[] enemyCharm = Physics2D.OverlapCircleAll(this.transform.position, charmCausticRadius, enemyLayer);
        if (enemyCharm == null) return;
        foreach (Collider2D obj in enemyCharm)
        {
            if (causticCharmPourcent) obj.gameObject.GetComponent<EnemySys>().TakeDamage(Mathf.RoundToInt(damage * (charmCausticValue /100)), 0f);
            else obj.gameObject.GetComponent<EnemySys>().TakeDamage(Mathf.RoundToInt(charmCausticValue), 0f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, range);
    }

    public void UpgradeCharmCombos()
    {
        if (!charmCombosEnable) return;
        if (charmCombosCoroutine != null) StopCoroutine(charmCombosCoroutine);
        charmCombosCoroutine = StartCoroutine(CharmCombosCount(annulerCharmCombosTemps));
    }

    IEnumerator combosCount(float time)
    {
        combos++;
        yield return new WaitForSeconds(time);
        combos = 0;
    }

    IEnumerator WaitForAttackAgain(float delay)
    {
        attackBlocked = true;
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    IEnumerator CharmCombosCount(float time)
    {
        if (charmCombos == dammageBonus.Length) charmCombos--;
        charmCombos++;
        yield return new WaitForSeconds(time);
        charmCombos = 0;
    }

    float CalculArcTangante(Transform position)
    {
        return Mathf.Atan2(position.localPosition.x, position.localPosition.y) * Mathf.Rad2Deg;
    }

    public void BonusUpdate(int value)
    {
        dammageBonus[0] = value;
        Debug.Log(Mathf.RoundToInt((damageCombos[combos] + force + dammageBonus[0]) + (damageCombos[combos] + force + dammageBonus[0]) * berzerkValue));
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(attackPoint.position, range);
	}

    void GhostAttack()
    {
        ghostAttack = true;
        if (ghostAttackCoroutine != null) StopCoroutine(ghostAttackCoroutine);
        ghostAttackCoroutine = StartCoroutine(GhostAttackDelay());
    }

    IEnumerator GhostAttackDelay()
    {
        yield return new WaitForSeconds(dureeGhostAttack);
        ghostAttack = false;
    }

    IEnumerator EjectingDelay(AnimationCurve force, GameObject enemy, Vector2 direction)
    {
        yield return new WaitForSeconds(animationDelay);
        enemy.GetComponent<Ejecting>().direction = direction;
        enemy.GetComponent<Ejecting>().pousseForce = force;
        enemy.GetComponent<Ejecting>().bonusForce = forceBonus;
        enemy.GetComponent<Ejecting>().forcePoussee = pousseeForce;
        enemy.GetComponent<Ejecting>().mauditBonusPoussee = mCharmPousseeValue / 100;
        enemy.GetComponent<Ejecting>().timeElapsed = 0f;
        enemy.GetComponent<Ejecting>().enabled = true;
    }
}
