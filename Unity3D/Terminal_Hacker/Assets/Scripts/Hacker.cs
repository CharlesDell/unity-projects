using UnityEngine;

public class Hacker : MonoBehaviour
{
    enum Screen { MainMenu, Password, Win };

    string[] level1Passwords = { "books", "aisle", "shelf", "password", "font", "borrow"};
    string[] level2Passwords = { "prisoner", "handcuffs", "holster", "uniform", "arrest" };
    string[] level3Passwords = { "starfield", "telescope", "environment", "exploration", "astronauts" };

    int level;
    Screen currentScreen = Screen.MainMenu;
    string password;
    string startText = "What would you like to hack into?\n\n" +
                       "Press 1 for the local library\n" +
                       "Press 2 for the police station\n" +
                       "Press 3 for NASA\n\n" +
                       "Enter your selection: ";
    string menuHint = "You may type menu at any time.";

    private void Start()
    {
        ShowMainMenu();
    }

    private void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
        else
        {
            Terminal.WriteLine("Please enter valid input");
        }
    }

    private void CheckPassword(string input)
    {
        if (input == password)
        {
            Terminal.ClearScreen();
            DisplayWinScreen();
        }
        else
        {
            Terminal.ClearScreen();
            Terminal.WriteLine("Wrong Password. Try Again");
            AskForPassword();
        }
    }

    private void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    private void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    ______
   /     //
  /     //
 /_____//
(_____)/
");
                break;
            case 2:
                Terminal.WriteLine("You got the prison key!");
                Terminal.WriteLine(@"
 __
/@ \________
\__/-=' = ''
");
                break;
            case 3:
                Terminal.WriteLine(@"
 _ __   __ _ ___  __ _
| '_ \ / _' / __|/ _' |
| | | | ( | \__ \ ( | |
|_| |_|\__,_|___)\__,_|
");
                Terminal.WriteLine("Welcome to NASA'a internal system!");
                break;
            default:
                Debug.LogError("invalid level reached");
                break;
        }
        Terminal.WriteLine(menuHint);
    }

    private void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            Terminal.ClearScreen();
            AskForPassword();
        }
        else
        {
            Terminal.WriteLine("Please enter valid input");
        }
    }

    private void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();

        Terminal.WriteLine(startText);
    }

    private void AskForPassword()
    {
        currentScreen = Screen.Password;
        SetRandomPassword();
        Terminal.WriteLine(menuHint);
        Terminal.WriteLine("Eenter your password; hint: " + password.Anagram());
    }

    private void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
    }

}
