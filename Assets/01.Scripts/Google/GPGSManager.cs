using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGSManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI logText;
    // Start is called before the first frame update
    void Start()
    {
        PlayGamesPlatform.Activate();
        SecondLogin();
    }

    public void SecondLogin()
    {
        if(PlayGamesPlatform.Instance.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool sucess) =>
            {
                if (sucess)
                {
                    //�α� �ؽ�Ʈ ����
                    logText.text = $"{Social.localUser.id}\n {Social.localUser.userName}";
                }
                else
                {
                    //����
                    logText.text = "Failed";
                }
            });
        }
    }
/*
    //��� �Ұ� 
    public void Login()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        //������ ���� ������ �г���
        string displayName = PlayGamesPlatform.Instance.GetUserDisplayName();
        //���� �Ұ��� ���� ID
        string userID = PlayGamesPlatform.Instance.GetUserId();

        if (status == SignInStatus.Success)
        {
            Debug.Log("Login Success : " + displayName + " / " + userID);
            logText.text = "Login Success : " + displayName + " / " + userID;
        }
        else
        {
            Debug.Log("Login Fail");
            logText.text = "Login Fail";
        }
    }
*/
}
