using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Proyecto26;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.Android;

public class UIController : Singleton<UIController>
{
    GameObject panel_login,panel_menu,menus;
    //queue of actions to be executed
    public Queue<string> logsQueue = new Queue<string>();

    void Start()
    {
        panel_login=GameObject.Find("PanelLogin");
        panel_menu=GameObject.Find("PanelMenu");
        menus=GameObject.Find("Menus");
        panel_menu.SetActive(false);

        loadPlayerPrefs();
        StartCoroutine(displayLogs());
    }

    public void Login(){
        string username = GameObject.Find("Username").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("Password").GetComponent<TMP_InputField>().text;
        
        savePlayerPrefs();

        WWWForm form=new WWWForm();
        form.AddField("user", username);
        form.AddField("pass", password);

        Debug.Log(username+" "+password);
        RestClient.Post(new RequestHelper {
            Uri="https://dfhs.tk/unibuc/login.php",
            Method = "POST",
            Timeout = 10,
			FormData = form,
        }).Then(res => {

            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(res.Text);
            
            if(json.ContainsKey("error")){
                Debug.Log("error");
                GameObject.Find("Result").GetComponent<TextMeshProUGUI>().text=json["error"];
            }
            else{
                Debug.Log("no error");
                GameObject.Find("Result").GetComponent<TextMeshProUGUI>().text="";
                GameObject.Find("Username").GetComponent<TMP_InputField>().text="";
                GameObject.Find("Password").GetComponent<TMP_InputField>().text="";
                panel_login.SetActive(false);
                panel_menu.SetActive(true);
            }
            
        });
    }

    private void loadPlayerPrefs(){
        if(PlayerPrefs.HasKey("username")){
            GameObject.Find("Username").GetComponent<TMP_InputField>().text=PlayerPrefs.GetString("username");
        }
        if(PlayerPrefs.HasKey("password")){
            GameObject.Find("Password").GetComponent<TMP_InputField>().text=PlayerPrefs.GetString("password");
        }
    }

    private void savePlayerPrefs(){
        PlayerPrefs.SetString("username",GameObject.Find("Username").GetComponent<TMP_InputField>().text);
        PlayerPrefs.SetString("password",GameObject.Find("Password").GetComponent<TMP_InputField>().text);
    }

    public void LoadMap1(){
        if(SceneManager.GetSceneByName("Map1").isLoaded==false){
            if(SceneManager.GetSceneByName("Map2").isLoaded){
                SceneManager.UnloadScene("Map2");
            }
            SceneManager.LoadScene("Map1", LoadSceneMode.Additive);
        }
        
    }

   public void LoadMap2(){
        if(SceneManager.GetSceneByName("Map2").isLoaded==false){
            if(SceneManager.GetSceneByName("Map1").isLoaded){
                SceneManager.UnloadScene("Map1");
            }
            SceneManager.LoadScene("Map2", LoadSceneMode.Additive);
        }
    }

    public void AddActor(){
        GameObject actor=Instantiate(Resources.Load("Actor")) as GameObject;
        actor.gameObject.name="Actor"+actor.GetInstanceID();
        InputManager.Instance.actor=actor;
    }

    public void AddLog(string text){
        logsQueue.Enqueue(text);
    }

    IEnumerator displayLogs(){
        while(true){
            if(logsQueue.Count>0){
                
                GameObject.Find("Log").GetComponent<TextMeshProUGUI>().text="";
                string text="";
                foreach(string log in logsQueue){
                   text=log+"\n"+text;
                }
                GameObject.Find("Log").GetComponent<TextMeshProUGUI>().text=text;
                logsQueue.Dequeue();
                while(logsQueue.Count>30){
                    logsQueue.Dequeue();
                }
            }else{
                GameObject.Find("Log").GetComponent<TextMeshProUGUI>().text="";
            }
            yield return new WaitForSeconds(.3f);
        }
    }

    public void showGPS(){
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation)){
            AddLog("Location permission has been granted");
            StartCoroutine(getGPS());
        }
        else
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
       
    }

    IEnumerator getGPS()
    {   
        AddLog("Getting GPS");
        if (!Input.location.isEnabledByUser){
            AddLog("location not enabled");
            yield break;
        }
        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if (maxWait < 1)
        {
            AddLog("Timed out");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            AddLog("Unable to determine device location");
            yield break;
        }
        else
        {
            AddLog("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude);
        }

        Input.location.Stop();
    }

    public void showHideMenu(){
        menus.SetActive(!menus.activeSelf);
    }
  
}
