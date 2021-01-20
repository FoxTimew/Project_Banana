using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Special_Attack : MonoBehaviour
{
    public float animationDelay;
    Controler control;

    Vector2 direction;

    [SerializeField]
    Transform attackPoint;

    [SerializeField]
    float slowdownFactor = .05f, backToNormalTransition, maxDuration;

    bool isSpecialing { get { return control.isSpecialing; } }
    
    public bool isSlowMotion;

    [SerializeField]
    LayerMask enemyLayer;

    [SerializeField]
    GameObject[] fleche;

    float deltaTimeBackup;

    Collider2D enemy;

    private void Awake()
    {
        control = this.GetComponent<Controler>();
        deltaTimeBackup = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (isSpecialing && !isSlowMotion)
        {
            Collider2D enemi = Physics2D.OverlapCircle(attackPoint.position, 2f, enemyLayer);
            if (enemi == null) return;
            enemy = enemi;
            SlowMotionStart();
            //enemi.GetComponent<Ejecting>();
        }
        if (!isSpecialing && isSlowMotion)
        {
            StopAllCoroutines();
            Time.timeScale = 1;
            isSlowMotion = false;
            Time.fixedDeltaTime = deltaTimeBackup;
            StartCoroutine(AnimationDelay(enemy.tag));
            //EjectingOn();
            /*enemy = null;
            control.isSpecialing = false;*/
            ResetFleche();
        }
        if (isSlowMotion)
        {
            FlecheUpdate(control.currentDirection);
        }
    }

    void SlowMotionStart()
    {
        if (isSlowMotion) return;
        isSlowMotion = true;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    void FlecheUpdate(Vector3 _direction)
    {
        float angle = CalculArcTangante(_direction);

        ResetFleche();

        if ((angle > -22.5f && angle < 0.0f) || (angle >= 0.0f && angle < 22.5f))
        {
            fleche[0].SetActive(true);
            direction = Vector2.up;
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            fleche[1].SetActive(true);
            direction = new Vector2(1, 1).normalized;
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            fleche[2].SetActive(true);
            direction = Vector2.right;
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            fleche[3].SetActive(true);
            direction = new Vector2(1, -1).normalized;
        }
        else if ((angle >= 157.5 && angle <= 180) || (angle > -180 && angle < -157.5))
        {
            fleche[4].SetActive(true);
            direction = Vector2.down;
        }
        else if (angle >= -157.5f && angle < -112.5f)
        {
            fleche[5].SetActive(true);
            direction = new Vector2(-1, -1).normalized;
        }
        else if (angle >= -112.5f && angle < -67.5f)
        {
            fleche[6].SetActive(true);
            direction = Vector2.left;
        }
        else
        {
            fleche[7].SetActive(true);
            direction = new Vector2(-1, 1).normalized;
        }
    }

    float CalculArcTangante(Vector3 position)
    {
        return Mathf.Atan2(position.x, position.y) * Mathf.Rad2Deg;
    }

    void ResetFleche()
    {
        foreach (GameObject obj in fleche)
        {
            obj.SetActive(false);
        }
    }

    IEnumerator AnimationDelay(string tag)
    {
        yield return new WaitForSeconds(animationDelay);
        EjectingOn(tag);
        enemy = null;
        control.isSpecialing = false;
    }

    void EjectingOn(string tag)
    {
        if (tag == "Enemy")
        {
            enemy.GetComponent<Ejecting>().direction = direction;
        enemy.GetComponent<Ejecting>().pousseForce = this.GetComponent<Attack>().pousseeSpecial;
        enemy.GetComponent<Ejecting>().bonusForce = this.GetComponent<Attack>().forceBonus;
        enemy.GetComponent<Ejecting>().forcePoussee = this.GetComponent<Attack>().pousseeForce;
        enemy.GetComponent<Ejecting>().mauditBonusPoussee = this.GetComponent<Attack>().mCharmPousseeValue / 100;
        enemy.GetComponent<Ejecting>().timeElapsed = 0f;
        enemy.GetComponent<Ejecting>().enabled = true;
        }
        else enemy.GetComponent<Barril_Sys>().EjectingStart(this.GetComponent<Attack>().pousseeSpecial, direction);
    }
}
