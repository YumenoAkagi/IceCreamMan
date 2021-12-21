using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PasswordCheck : MonoBehaviour
{
    private string UnderTunnel = "dark";
    private string RefrigeratorRoom = "fake";
    private string DiningRoom = "cook";
    private string WorkspaceRoom = "work";
    private string WarehouseRoom = "ware";
    private string RightTunnel = "tunnel";
    private string LeftTunnel = "tunnel";
    private string WarehouseRoom2 = "house";

    public InputField UserInput, UserInput2;
    public string PasswordUserInput;

    private string Room;
    private string correct = "Scenes/correct";
    private string failed = "Scenes/failed";

    private string UTRoom = "UndergroundTunnel";
    private string FKRoom = "FakeRefrigerator";
    private string DRoom = "DiningRoom";
    private string WorkRoom = "WorkspaceRoom";
    private string WareRoom = "WarehouseRoom";
    private string RightTunnelRoom = "RightTunnel";
    private string LeftTunnelRoom = "LeftTunnel";

    public void CheckPassword()
    {
        PasswordUserInput = UserInput.text;

        if(PasswordUserInput != null)
        {
            if((UTRoom == SceneManager.GetActiveScene().name) && (PasswordUserInput == UnderTunnel))
            {
                SceneManager.LoadScene(correct);
            }else if((FKRoom == SceneManager.GetActiveScene().name) && (PasswordUserInput == RefrigeratorRoom))
            {
                SceneManager.LoadScene(correct);
            }else if((DRoom == SceneManager.GetActiveScene().name) && (PasswordUserInput == DiningRoom))
            {
                SceneManager.LoadScene(correct);
            }else if((WorkRoom == SceneManager.GetActiveScene().name) && (PasswordUserInput == WorkspaceRoom))
            {
                SceneManager.LoadScene(correct);
            }else if((WareRoom == SceneManager.GetActiveScene().name))
            {
                string PasswordUserInput2 = UserInput2.text;
                if((PasswordUserInput == WarehouseRoom) && (PasswordUserInput2 == WarehouseRoom2))
                {
                    SceneManager.LoadScene(correct);
                }else{
                    SceneManager.LoadScene(failed);
                }
            }else if((RightTunnelRoom == SceneManager.GetActiveScene().name) && (PasswordUserInput == RightTunnel))
            {
                SceneManager.LoadScene(correct);
            }else if((LeftTunnelRoom == SceneManager.GetActiveScene().name) && (PasswordUserInput == LeftTunnel))
            {
                SceneManager.LoadScene(correct);
            }else{
                SceneManager.LoadScene(failed);
            }
        }
    }
}
