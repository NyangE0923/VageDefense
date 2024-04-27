using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float per;
    public float speed;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(float _damage, int _per, Vector3 _dir, float _speed)
    {
        damage = _damage;
        per = _per;
        speed = _speed;

        if (_per > -1)
        {
            rb.velocity = _dir * _speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy") || per == -1)
        {
            return;
        }

        if (collision.CompareTag("Enemy"))
        {
            // per 값을 감소시킴
            per--;

            // per 값이 -1이 되면 처리
            if (per == -1)
            {
                // Rigidbody의 속도를 0으로 설정
                rb.velocity = Vector2.zero;
                // 게임 오브젝트를 비활성화
                gameObject.SetActive(false);
            }
        }
    }
}
