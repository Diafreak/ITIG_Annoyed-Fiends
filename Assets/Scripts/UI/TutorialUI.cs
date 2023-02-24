using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TutorialUI : MonoBehaviour {

    [Header("UI")]
    public GameObject tutorialUI;

    [Header("Buttons")]
    public Button previousButton;
    public Button nextButton;

    [Header("Pages")]
    public GameObject[] pages;

    private int currentPageIndex;

    private bool isLastPage = false;



    private void Start() {

        // show Tutorial if Player hasn't completed it yet
        if (PlayerPrefs.GetInt("TutorialCompleted", 0) == 0) {
            tutorialUI.SetActive(true);
            ShowTutorial();
        }
        else {
            tutorialUI.SetActive(false);
        }
    }



    public void ShowTutorial() {

        currentPageIndex = 0;
        isLastPage = false;

        tutorialUI.SetActive(true);

        // disable all Pages
        foreach (GameObject page in pages) {
            page.SetActive(false);
        }

        // show first Page
        pages[currentPageIndex].SetActive(true);

        nextButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Next";
        // disable "previous"-Button on first Slide
        previousButton.gameObject.SetActive(false);
    }


    public void Next() {

        // hide Tutorial if on last Page
        if (isLastPage) {
            tutorialUI.SetActive(false);
            PlayerPrefs.SetInt("TutorialCompleted", 1);
            return;
        }

        // hide current Page
        pages[currentPageIndex].SetActive(false);
        // show next Page
        currentPageIndex++;
        pages[currentPageIndex].SetActive(true);

        // reset "Previous"-Button
        previousButton.gameObject.SetActive(true);

        // display "Start" instead of "Next" on last Page
        if (currentPageIndex >= pages.Length-1) {
            isLastPage = true;
            nextButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Start";
        }
        else {
            isLastPage = false;
            nextButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Next";
        }
    }


    public void Previous() {

        previousButton.gameObject.SetActive(true);
        // hide current Page
        pages[currentPageIndex].SetActive(false);
        // show previous Page
        currentPageIndex--;
        pages[currentPageIndex].SetActive(true);

        // reset "Next"-Button
        isLastPage = false;
        nextButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Next";

        // disable "previous"-Button on first Slide
        if (currentPageIndex == 0) {
            previousButton.gameObject.SetActive(false);
        }
    }
}
