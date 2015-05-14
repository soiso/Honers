using UnityEngine;
using System.Collections;

public class FieldObjectInterface : MonoBehaviour {

    public ParticleSystem m_Particle;

    [SerializeField]
    public PanelParametor.TIMEZONE[] m_SleepTimeZone; /*{ set; private get; } */        //眠り時の時間帯
    [SerializeField]
    public PanelParametor.TIMEZONE[] m_ActiveTimeZone ;/*{ set; private get; }*/        //起きてる時間帯

    public PanelParametor.TIMEZONE m_CurrentTimeZone { set; get; }

    protected void SleepEffectPlay()
    {
        m_Particle.Play();
    }

    protected void SleepEffectStop()
    {
        m_Particle.Stop();
        m_Particle.Clear();
    }

    public bool Is_ActiveTimeZone(PanelParametor.TIMEZONE CheckTimeZone)
    {
        foreach (PanelParametor.TIMEZONE it in m_ActiveTimeZone)
        {
            if (CheckTimeZone == it)
                return true;
        }
        return false;
    }

    public bool Is_SleepTimeZone(PanelParametor.TIMEZONE CheckTimeZone)
    {
        foreach (PanelParametor.TIMEZONE it in m_SleepTimeZone)
        {       
            if (CheckTimeZone == it)
                return true;
        }
        return false;
    }

    public  virtual void Damage(DamageTrigger.DamageObject damage_Info) { }
}
