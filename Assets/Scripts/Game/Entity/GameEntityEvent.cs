using UnityEngine;
using UnityEngine.Events;

public class GameEntityEvent : MonoBehaviour{
    [Header("Event Settings :")] 
    public GameEntityEventSO Event;
    public GameEntityGUID GUID;
    
    UnityEvent EntityEvent = new UnityEvent();

    [Header("Additional Event Parameters :")]
    public Object[] Params;
    
    private void Start(){
        EntityEvent.AddListener(() =>
        {
            if (Event.CanExecute())
            {
                print("ok");
                switch (Params.Length)
                {
                    case 3:
                        Event.Execute(GUID, Params[0], Params[1], Params[2]);
                        break;
                    case 2:
                        Event.Execute(GUID, Params[0], Params[1]);
                        break;
                    case 1:
                        Event.Execute(GUID, Params[0]);
                        break;
                    default:
                        Event.Execute(GUID);
                        break;
                }

            }
        });
    }

    private void Update(){
        if (Input.GetKeyDown(Event.KeyToListen))
        {
            EntityEvent.Invoke();
        }
    }
}