using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    // Cible que l'ennemi doit suivre (par exemple, le joueur)
    public Transform target;

    // Vitesse de déplacement de l'ennemi
    public float speed = 120f;

    // Distance à laquelle l'ennemi considère qu'il a atteint un waypoint
    public float nextWpDistance = 1f;

    // Distance minimale pour déclencher une attaque (l'ennemi s'arrête en dehors de cette portée)
    public float attackRange = 2f;

    // Distance de détection
    public float detectionRange = 7f;

    // Chemin calculé par le Seeker
    public Path path;

    // Indice du waypoint actuel que l'ennemi essaie d'atteindre
    int currWp = 0;

    // Composant Seeker utilisé pour calculer le chemin
    public Seeker seeker;

    // Composant Rigidbody2D utilisé pour le mouvement physique de l'ennemi
    public Rigidbody2D rb;

    // Animator
    public Animator animator;

    // SpriteRenderer
    public SpriteRenderer spriteRenderer;

    // Cooldown entre les attaques
    public float attackCooldown = 2f;  // 2 secondes de cooldown
    private float currentCooldown = 0f;

    // Dégats de l'ennemi
    public int damage = 1;

    // Vie
    public int maxHealth = 2;
    private int currentHealth;

    // Bool indiquant si l'ennemi est mort
    private bool isAlive = true;

    // Call pause menu script
    public PauseMenu pauseMenu;


    // Méthode appelée lors de l'initialisation de l'ennemi
    void Awake()
    {
        // Initialisation de la santé actuelle de l'ennemi
        currentHealth = maxHealth;
    }

    // Méthode appelée au début de l'exécution
    void Start()
    {
        // Met à jour le chemin toutes les 0,5 secondes pour suivre la position du joueur
        InvokeRepeating("UpdatePath", 0, 0.5f);
    }

    // Méthode pour mettre à jour le chemin vers la cible
    void UpdatePath()
    {
        // Vérifie si le Seeker est prêt à calculer un nouveau chemin et si l'ennemi est vivant
        if (seeker.IsDone() && isAlive && (pauseMenu == null || !pauseMenu.isPaused) && Vector2.Distance(transform.position, target.position) <= detectionRange)
            // Demande un nouveau chemin du Seeker entre la position actuelle et la cible
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    // Méthode appelée lorsque le Seeker a terminé de calculer un chemin
    void OnPathComplete(Path p)
    {
        // Si le calcul a réussi (pas d'erreur), met à jour le chemin et réinitialise l'indice du waypoint
        if (!p.error)
        {
            path = p;
            currWp = 0;
        }
    }

    void Update()
    {
        // Si l'ennemi est mort ou le jeu en pause, ne fait rien
        if (!isAlive || (pauseMenu != null && pauseMenu.isPaused))
            return;

        // Met à jour les paramètres de l'Animator en fonction de la vitesse actuelle
        animator.SetFloat("Speed", rb.linearVelocity.sqrMagnitude);

        // Gère le flip du sprite en fonction de la direction du mouvement
        if (rb.linearVelocity.x != 0)
        {
            spriteRenderer.flipX = rb.linearVelocity.x < 0;
        }

        // Gère le cooldown d'attaque en réduisant le temps restant
        currentCooldown -= Time.deltaTime;

        // Eviter que le cooldown devienne négatif
        if (currentCooldown < 0)
        {
            currentCooldown = 0;
        }
    }

    // Méthode appelée à chaque frame fixe pour gérer les mouvements physiques
    void FixedUpdate()
    {
        // Si aucun chemin n'a été calculé ou si tous les waypoints ont été atteints, ne fait rien ou si l'ennemi est mort ou le jeu en pause
        if (path == null || currWp >= path.vectorPath.Count || !isAlive || (pauseMenu != null && pauseMenu.isPaused))
        {
            return;
        }

        // Calcule la distance entre l'ennemi et le joueur
        float playerDistance = Vector2.Distance(target.transform.position, transform.position);

        // Si le joueur est en dehors de la portée d'attaque = on le poursuit
        if (playerDistance > attackRange)
        {
            // Calcule la direction vers le prochain waypoint
            Vector2 direction = ((Vector2)path.vectorPath[currWp] - rb.position).normalized;

            // Lisser la direction pour éviter des changements brusques (interpolation)
            Vector2 smoothDirection = Vector2.Lerp(rb.linearVelocity.normalized, direction, 0.1f);

            // Calcule la vitesse en fonction de la direction et de la vitesse spécifiée
            Vector2 velocity = smoothDirection * speed * Time.fixedDeltaTime;

            // Applique la vitesse calculée au Rigidbody2D
            rb.linearVelocity = velocity;

            // Calcule la distance entre l'ennemi et le waypoint actuel
            float distance = Vector2.Distance(rb.position, path.vectorPath[currWp]);

            // Si l'ennemi est suffisamment proche du waypoint, passe au suivant
            if (distance < nextWpDistance)
            {
                currWp++;
            }
        } else
        {
            // Si l'ennemi est dans la portée d'attaque
            if (currentCooldown <= 0)
            {
                // Appelle de la fonction d'attaque
                Attack();
            }
        }
    }

    // Gestion de l'attaque
    void Attack()
    {
        // Gestion animation d'attaque
        animator.SetBool("isAttacking", true);

        // Réinitialise le cooldown d'attaque
        currentCooldown = attackCooldown;

        // Appel du trigger d'attaque
        animator.SetTrigger("Attack");
    }

    // Méthode pour infliger des dégâts à l'ennemi
    public void TakeDamage(int damage)
    {
        // On ne peut infliger de dégâts que si l'ennemi est vivant
        if (isAlive)
        {
            // Réduit la santé actuelle de l'ennemi
            currentHealth -= damage;

            // Vérifie si la santé est inférieure ou égale à zéro
            if (currentHealth <= 0)
            {
                animator.SetTrigger("Die");
                isAlive = false;
                // Détruit l'ennemi après 3 secondes pour laisser le temps de jouer l'animation de mort
                Destroy(gameObject, 3f); 
            } else
            {
                // Joue l'animation de blessure si l'ennemi est toujours vivant
                animator.SetTrigger("Hit");
                // Annule attaque en cours et réinitialise le cooldown
                currentCooldown = attackCooldown;
            }
        }
    }

    public void EndOfAttack()
    {
        // Fin de l'animation d'attaque
        animator.SetBool("isAttacking", false);

        // Vérifier si le joueur est toujours dans la portée d'attaque
        if (Vector2.Distance(target.position, transform.position) <= attackRange)
        {
            // Infliger des dégâts au joueur
            target.GetComponentInChildren<Player_Health>().TakeDamage(damage);
        }
    }

    // Visualisation de la portée d'attaque et de détection dans l'éditeur
    void OnDrawGizmosSelected()
    {
        // Dessine une sphère filaire rouge pour représenter la portée d'attaque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Rayon de détection
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}