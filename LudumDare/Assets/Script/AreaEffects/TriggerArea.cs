using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    [SerializeField] Effects effect;


    enum Effects{
        Slow,
        LowJump,
        Timer,           
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (effect)
        {
            case Effects.Slow:
                Enemy.Instance.Slow();
                break;
            case Effects.LowJump:
                Enemy.Instance.LowJump();
                break;
            case Effects.Timer:
                Enemy.Instance.StartTimer();
                break;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (effect) { 
            case Effects.Slow:
                Enemy.Instance.RevertSlow();
                break;
            case Effects.LowJump:
                Enemy.Instance.RevertJump();
                break;
            case Effects.Timer:
                Enemy.Instance.StopTimer();
                break;
        }
    }
}
