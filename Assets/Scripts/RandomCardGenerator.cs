using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    public Image Player1;
    public Image Player2;
    public Image PC1;
    public Image PC2;
    // Add references to UI elements
    public Image[] playerScoreUIElements;
    public Image[] pcScoreUIElements;

    private bool isWaiting = false;
    private bool isButtonPressed = false;
    public AudioSource CardFlip;
    
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

        playerCardIndex = Random.Range(0, 19);
        pcCardIndex = Random.Range(0, 19);

        playerCard = cards[playerCardIndex];
        pcCard = cards[pcCardIndex];

        // Use playerCard and pcCard here
        playerCardObject = Instantiate(playerCard.prefab, playerCardSpawnPoint.position, playerCardSpawnPoint.rotation);

        // Spawn PC card facing away from camera
       
        
        var pcRotation = Quaternion.Euler(0, 180, 0);
        pcCardObject = Instantiate(pcCard.prefab, pcCardSpawnPoint.position, pcRotation);

        isButtonPressed = false;
     
        
        
    }


    IEnumerator ShowPcCardAndStartNewRound()
    {
        // Rotate PC card 180 degrees around y-axis
        Quaternion targetRotation = pcCardObject.transform.rotation * Quaternion.Euler(0, 180, 0);
        CardFlip.Play();
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
                if (playerRoundWins == 1)
                {
                    Player1.enabled = true;
                }
                if (playerRoundWins == 2)
                {
                    Player2.enabled = true;
                }
            }
            else if (pcScore > playerScore)
            {
                // PC wins round
                pcRoundWins++;
                if (pcRoundWins == 1)
                {
                    PC1.enabled = true;
                }
                if (pcRoundWins == 2)
                {
                    PC2.enabled = true;
                }
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

            // Reset visibility of UI elements
            foreach (Image image in playerScoreUIElements)
            {
                image.enabled = false;
            }
        
            foreach (Image image in pcScoreUIElements)
            {
                image.enabled = false;
            }

            if (playerRoundWins >= 2 || pcRoundWins >= 2)
            {
                // Game over
                Debug.Log("Game Over");
                SceneManager.LoadScene("EndScreen");
            
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
    
        // Update visibility of UI elements
        for (int i = 0; i < playerScoreUIElements.Length; i++)
        {
            playerScoreUIElements[i].enabled = i < playerScore;
        }
    
        for (int i = 0; i < pcScoreUIElements.Length; i++)
        {
            pcScoreUIElements[i].enabled = i < pcScore;
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

            // Reset visibility of UI elements
            foreach (Image image in playerScoreUIElements)
            {
                image.enabled = false;
            }
        
            foreach (Image image in pcScoreUIElements)
            {
                image.enabled = false;
            }

            if (playerRoundWins >= 2 || pcRoundWins >= 2)
            {
                // Game over
                Debug.Log("Game Over");
                SceneManager.LoadScene("EndScreen");
            
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
    
        // Update visibility of UI elements
        for (int i = 0; i < playerScoreUIElements.Length; i++)
        {
            playerScoreUIElements[i].enabled = i < playerScore;
        }
    
        for (int i = 0; i < pcScoreUIElements.Length; i++)
        {
            pcScoreUIElements[i].enabled = i < pcScore;
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

            // Reset visibility of UI elements
            foreach (Image image in playerScoreUIElements)
            {
                image.enabled = false;
            }
        
            foreach (Image image in pcScoreUIElements)
            {
                image.enabled = false;
            }

            if (playerRoundWins >= 2 || pcRoundWins >= 2)
            {
                // Game over
                Debug.Log("Game Over");
                SceneManager.LoadScene("EndScreen");
            
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
    
        // Update visibility of UI elements
        for (int i = 0; i < playerScoreUIElements.Length; i++)
        {
            playerScoreUIElements[i].enabled = i < playerScore;
        }
    
        for (int i = 0; i < pcScoreUIElements.Length; i++)
        {
            pcScoreUIElements[i].enabled = i < pcScore;
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
