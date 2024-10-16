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
    //추가 로그인 확인필요
    void Start()
    {
        PlayGamesPlatform.Activate();
        SecondLogin();
    }

    //TODO: 로그인 시스템 연동까진 확인, 추가 확인 필요
    public void SecondLogin()
    {
        if(PlayGamesPlatform.Instance.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool sucess) =>
            {
                if (sucess)
                {
                    //로그 텍스트 설정
                    logText.text = $"{Social.localUser.id}\n {Social.localUser.userName}";
                }
                else
                {
                    //실패
                    logText.text = "Failed";
                }
            });
        }
    }
/*
    //사용 불가 
    TODO : 이부분 수정
    public void Login()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        //유저가 변경 가능한 닉네임
        string displayName = PlayGamesPlatform.Instance.GetUserDisplayName();
        //변경 불가한 고유 ID
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
