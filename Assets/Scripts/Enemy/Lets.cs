using UnityEngine;

public class TestMusicManager : MonoBehaviour
{

    public enum MusicState
    {
        FreeRoam,
        Chase,
        Anxious
    }

    private MusicState currentState = MusicState.Anxious;

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     ChangeMusicState(MusicState.FreeRoam);
        // }

        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     ChangeMusicState(MusicState.Chase);
        // }

        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     ChangeMusicState(MusicState.Anxious);
        // }

        // if (Input.GetKeyDown(KeyCode.D))
        // {
        //     StopAllMusic(); // Stop all tracks
        // }
    }

    void ChangeMusicState(MusicState newState)
    {
        if (currentState != newState)
        {
            StopAllMusic(); // Fade out current music
            currentState = newState;

            switch (currentState)
            {
                case MusicState.FreeRoam:
                    MusicTimelineManager.playerHunted?.Invoke(0, true, 1f); // Fade in Track 1
                    break;
                case MusicState.Chase:
                    MusicTimelineManager.playerHunted?.Invoke(2, true, 1f); // Fade in Track 2
                    break;
                case MusicState.Anxious:
                    MusicTimelineManager.playerHunted?.Invoke(1, true, 1f); // Fade in Track 3
                    break;
            }
        }
    }

    void StopAllMusic()
    {
        for (int i = 0; i < 3; i++)
        {
            MusicTimelineManager.playerHunted?.Invoke(i, false, 1f); // Fade out
        }
    }
    public void SetMusicState(MusicState state)
    {
        ChangeMusicState(state);
    }

}

