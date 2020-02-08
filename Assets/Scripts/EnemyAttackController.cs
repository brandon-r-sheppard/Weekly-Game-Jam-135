using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{

    public GameObject ProjectilePrefab;
    public GameObject BoneChild;
    public GameObject SpearChild;
    public Transform FireLocation;

    public float projectileSpeed;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator.GetBool("mIsRanged"))
        {
            BoneChild.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        else
        {
            SpearChild.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
    }

    public void Shoot(Transform target)
    {
        StartCoroutine(ShootProjectile(target));
    }

    IEnumerator ShootProjectile(Transform target)
    {
        animator.SetBool("mCanAttack", false);
        yield return new WaitForSeconds(0.2f);

        if (animator.GetBool("mIsRanged"))
        {
            SpearChild.GetComponent<SkinnedMeshRenderer>().enabled = false;

            GameObject projectile = Instantiate(ProjectilePrefab);
            projectile.transform.position = FireLocation.transform.position;

            //TODO: stop the projectile from goign slgihtly to the right
            projectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        }

        yield return new WaitForSeconds(0.5f);
        animator.SetBool("mCanAttack", true);
        SpearChild.GetComponent<SkinnedMeshRenderer>().enabled = animator.GetBool("IsRanged");
    }

}
