using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameStates state;
    public event Action OnGame;

    private void Start()
    {
        state = GameStates.Game;
    }

    public void ChangeState(GameStates newState)
    {
        state = newState;

        switch (state)
        {
            case GameStates.Game:

                OnGame.Invoke();
                break;

            case GameStates.Pause:

                
                break;
                
            case GameStates.Win:

                //Win Logic
               
                break;
                
            case GameStates.Lose:

                //Lose logic
          

                break;
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

    }

  
}
