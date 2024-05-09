using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float per;
    public float speed;
    public float duration;
    public float durationTimer;

    Rigidbody2D rb;
    Collider2D enemyCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        BulletDisable();
    }

    public void Init(float _damage, int _per, Vector3 _dir, float _speed, float _duration, float _durationTimer)
    {
        damage = _damage;
        per = _per;
        speed = _speed;
        duration = _duration;
        durationTimer = _durationTimer;

        if (_per >= 0)
        {
            rb.velocity = _dir * _speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy") || per == -100)
        {
            return;
        }

        if(enemyCollider != null && collision != enemyCollider)
        {
            return;
        }

        enemyCollider = collision;

        if (collision.CompareTag("Enemy"))
        {
            // per ���� ���ҽ�Ŵ
            per--;

            // per ���� -1�� �Ǹ� ó��
            if (per < 0)
            {
                // Rigidbody�� �ӵ��� 0���� ����
                rb.velocity = Vector2.zero;
                // ���� ������Ʈ�� ��Ȱ��ȭ
                gameObject.SetActive(false);
            }
        }
    }
    public void BulletDisable()
    {
        durationTimer += Time.deltaTime;

        if (durationTimer >= duration)
        {
            durationTimer = 0;
            enemyCollider = null;
            gameObject.SetActive(false);
        }
    }
}
