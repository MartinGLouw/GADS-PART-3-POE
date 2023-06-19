using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomCardGenerator : MonoBehaviour
{
    public int playerCardIndex;
    public int pcCardIndex;
    public Card[] cards;
    public Transform playerCardSpawnPoint;
    public Transform pcCardSpawnPoint;
    private Card playerCard;
    private Card pcCard;
    private GameObject playerCardObject;
    private GameObject pcCardObject;
    public int round = 1;
    public int playerScore = 0;
    public int pcScore = 0;
    public int playerRoundWins = 0;
    public int pcRoundWins = 0;
    public int playerTotalScore = 0;
    public int pcTotalScore = 0;

    private bool isWaiting = false;
    private bool isButtonPressed = false;

    void Start()
    {
        InitializeCards();
    }

    public void InitializeCards()
    {
        // Destroy previous card prefabs
        if (playerCardObject != null)
        {
            Destroy(playerCardObject);
        }
        if (pcCardObject != null)
        {
            Destroy(pcCardObject);
        }

        playerCardIndex = Random.Range(0, 3);
        pcCardIndex = Random.Range(0, 3);

        playerCard = cards[playerCardIndex];
        pcCard = cards[pcCardIndex];

        // Use playerCard and pcCard here
        playerCardObject = Instantiate(playerCard.prefab, playerCardSpawnPoint.position, playerCardSpawnPoint.rotation);

        // Spawn PC card facing horizon
        pcCardObject = Instantiate(pcCard.prefab, pcCardSpawnPoint.position, Quaternion.identity);
        pcCardObject.transform.rotation = Quaternion.Euler(0, 180, 0);

        isButtonPressed = false;
    }


    IEnumerator ShowPcCardAndStartNewRound()
    {
        // Rotate PC card 180 degrees around y-axis
        Quaternion targetRotation = pcCardObject.transform.rotation * Quaternion.Euler(0, 180, 0);
        float duration = 1f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            pcCardObject.transform.rotation = Quaternion.Slerp(pcCardObject.transform.rotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pcCardObject.transform.rotation = targetRotation;

        // Wait for 5 seconds
        isWaiting = true;
        yield return new WaitForSeconds(5f);
        isWaiting = false;

        // Start new round
        InitializeCards();
        if (playerScore >= 5 || pcScore >= 5)
        {
            if (playerScore > pcScore)
            {
                // Player wins round
                playerRoundWins++;
            }
            else if (pcScore > playerScore)
            {
                // PC wins round
                pcRoundWins++;
            }
            round++;
            playerTotalScore += playerScore;
            pcTotalScore += pcScore;
            playerScore = 0;
            pcScore = 0;

            if (playerRoundWins >= 2 || pcRoundWins >= 2)
            {
                // Game over
                Debug.Log("Game Over");
                SceneManager.LoadScene("EndScreen");
            
            }
        }
    }


    public void Health()
    {
         if (isWaiting || isButtonPressed) return;

         isButtonPressed = true;

         if (playerScore >= 5 || pcScore >= 5)
         {
             if (playerScore > pcScore)
             {
                 // Player wins round
                 playerRoundWins++;
             }
             else if (pcScore > playerScore)
             {
                 // PC wins round
                 pcRoundWins++;
             }
             round++;
             playerTotalScore += playerScore;
             pcTotalScore += pcScore;
             playerScore = 0;
             pcScore = 0;

             if (playerRoundWins >= 2 || pcRoundWins >= 2)
             {
                 // Game over
                 Debug.Log("Game Over");
                 return;
             }
         }

         if (playerCard.health > pcCard.health)
         {
             // Player wins
             playerScore++;
         }
         else if (playerCard.health < pcCard.health)
         {
             // PC wins
             pcScore++;
         }
         else
         {
             // Tie
         }
         Debug.Log("Player Score" + playerScore);
         Debug.Log("PC Score" + pcScore);

         StartCoroutine(ShowPcCardAndStartNewRound());
     }

     public void Strength()
     {
         if (isWaiting || isButtonPressed) return;

         isButtonPressed = true;

         if (playerScore >= 5 || pcScore >= 5)
         {
             if (playerScore > pcScore)
             {
                 // Player wins round
                 playerRoundWins++;
             }
             else if (pcScore > playerScore)
             {
                 // PC wins round
                 pcRoundWins++;
             }
             round++;
             playerTotalScore += playerScore;
             pcTotalScore += pcScore;
             playerScore = 0;
             pcScore = 0;

             if (playerRoundWins >= 2 || pcRoundWins >= 2)
             {
                 // Game over
                 Debug.Log("Game Over");
                 return;
             }
         }

         if (playerCard.strength > pcCard.strength)
         {
             // Player wins
             playerScore++;
         }
         else if (playerCard.strength < pcCard.strength)
         {
             // PC wins
             pcScore++;
         }
         else
         {
             // Tie
         }
         Debug.Log("Player Score" + playerScore);
         Debug.Log("PC Score" + pcScore);

         StartCoroutine(ShowPcCardAndStartNewRound());
     }

     public void Speed()
     {
         if (isWaiting || isButtonPressed) return;

         isButtonPressed = true;

         if (playerScore >= 5 || pcScore >= 5)
         {
             if (playerScore > pcScore)
             {
                 // Player wins round
                 playerRoundWins++;
             }
             else if (pcScore > playerScore)
             {
                 // PC wins round
                 pcRoundWins++;
             }
             round++;
             playerTotalScore += playerScore;
             pcTotalScore += pcScore;
             playerScore = 0;
             pcScore = 0;

             if (playerRoundWins >= 2 || pcRoundWins >= 2)
             {
                 // Game over
                 Debug.Log("Game Over");
                 return;
             }
         }

         if (playerCard.agility > pcCard.agility)
         {
             // Player wins
             playerScore++;
         }
         else if (playerCard.agility < pcCard.agility)
         {
             // PC wins
             pcScore++;
         }
         else
         {
             // Tie
         }
         Debug.Log("Player Score" + playerScore);
         Debug.Log("PC Score" + pcScore);

         StartCoroutine(ShowPcCardAndStartNewRound());
     }
}

[System.Serializable]
public class Card
{
     public string Name;
     public int health;
     public int strength;
     public int agility;
     public GameObject prefab;
}
