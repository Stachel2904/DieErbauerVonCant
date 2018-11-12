using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkClientDiscovery : NetworkDiscovery {

    public float timeout = 5f;
    public string ipAddress;
    public string serverMessage;
    public int port;
    public Dictionary<ClientLanConnectionInfo, float> lanAddresses = new Dictionary<ClientLanConnectionInfo, float>();

    //private void Awake() {
    //    Initialize();
    //    StartAsClient();
    //    StartCoroutine(CleanupExpiredEntries());
    //}
    public void StartClientDiscovery() {
        //StopBroadcast();
        Initialize();
        StartAsClient();
    }
    private IEnumerator CleanupExpiredEntries() {
        while (true) {
            bool changed = false;
            var keys = lanAddresses.Keys.ToList();
            foreach(var key in keys) {
                if(lanAddresses[key] <= Time.time) {
                    lanAddresses.Remove(key);
                    changed = true;
                }
            }
            if (changed)
            {
                //this.gameObject.GetComponent<NetworkClientUI>().OutputAdresses();
                //In Tutorial: GameListController.AddLanMatches(lanAddresses.Keys.ToList());
            }
            yield return new WaitForEndOfFrame();
        }
    }
    //public override void OnReceivedBroadcast(string _fromAddress_, string _data_) {
    //    ClientLanConnectionInfo info = new ClientLanConnectionInfo(_fromAddress_, _data_);
    //    if(lanAddresses.ContainsKey(info) == false) {
    //        lanAddresses.Add(info, Time.time + timeout);
    //        Debug.Log(_fromAddress_ + " / " + _data_);
    //        //this.gameObject.GetComponent<NetworkClientUI>().OutputAdresses();
    //        //In Tutorial: GameListController.AddLanMatches(lanAddresses.Keys.ToList());
    //    }
    //    else {
    //        lanAddresses[info] = Time.time + timeout;
    //    }
    //}
    //OLD DISCOVERY
    public override void OnReceivedBroadcast(string _fromAddress_, string _data_)
    {
        ipAddress = _fromAddress_.Substring(_fromAddress_.LastIndexOf(":") + 1, _fromAddress_.Length - (_fromAddress_.LastIndexOf(":") + 1));
        serverMessage = _data_.Substring(_data_.LastIndexOf(":") + 1, _data_.Length - (_data_.LastIndexOf(":") + 1));
        Debug.Log(serverMessage);
        string s_port = _data_.Substring(_data_.LastIndexOf("|") + 1, _data_.Length - (_data_.LastIndexOf("|") + 1));
        int.TryParse(s_port, out port);
        Debug.Log(ipAddress + " / " + port);
        this.gameObject.GetComponent<NetworkClientUI>().OutputAdresses();
    }
}
