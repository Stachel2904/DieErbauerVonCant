public struct ClientLanConnectionInfo {
    public string ipAdress;
    public string broadcastText;
    public int port;

    public ClientLanConnectionInfo(string _fromAddress_, string _data_) {
        ipAdress = _fromAddress_.Substring(_fromAddress_.LastIndexOf(":") + 1, _fromAddress_.Length - (_fromAddress_.LastIndexOf(":") + 1));
        broadcastText = _data_.Substring(_data_.LastIndexOf(":") + 1, _data_.Length - (_data_.LastIndexOf(":") + 1));
        string s_port = _data_.Substring(_data_.LastIndexOf("|") + 1, _data_.Length - (_data_.LastIndexOf("|") + 1));
        int.TryParse(s_port, out port);
        //port = 5555; //ToDo: Port aus broadcast Text auslesen + belegte Slots aus Broadcaster auslesen (Wenn Serverseitig Updater Implementiert)
    }
}
