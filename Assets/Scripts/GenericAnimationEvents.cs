using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class GenericAnimationEvents : MonoBehaviour
{
    [BoxGroup("Animation Event 1")]
    [SerializeField, ResizableTextArea, NaughtyAttributes.Drawer] private string _notes1; 
    [BoxGroup("Animation Event 1")]
    public UnityEvent UEvent1;
    
    [Space]
    [BoxGroup("Animation Event 2")]
    [SerializeField, ResizableTextArea] private string _notes2; 
    [BoxGroup("Animation Event 2")]
    public UnityEvent UEvent2;
    
    [Space]
    [BoxGroup("Animation Event 3")]
    [SerializeField, ResizableTextArea] private string _notes3; 
    [BoxGroup("Animation Event 3")]
    public UnityEvent UEvent3;
    
    [Space]
    [BoxGroup("Animation Event 4")]
    [SerializeField, ResizableTextArea] private string _notes4; 
    [BoxGroup("Animation Event 4")]
    public UnityEvent UEvent4;
    
    [Space]
    [BoxGroup("Animation Event 5")]
    [SerializeField, ResizableTextArea] private string _notes5; 
    [BoxGroup("Animation Event 5")]
    public UnityEvent UEvent5;
    
    [Space]
    [BoxGroup("Animation Event 6")]
    [SerializeField, ResizableTextArea] private string _notes6; 
    [BoxGroup("Animation Event 6")]
    public UnityEvent UEvent6;
    
    public void Event1()
    {
        UEvent1.Invoke();
    }

    public void Event2()
    {
        UEvent2.Invoke();
    }

    public void Event3()
    {
        UEvent3.Invoke();
    }
    
    public void Event4()
    {
        UEvent4.Invoke();
    }

    public void Event5()
    {
        UEvent5.Invoke();
    }

    public void Event6()
    {
        UEvent6.Invoke();
    }
}
