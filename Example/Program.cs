﻿using HTTR;

var client = new HttrClient("https://www.databazeknih.cz/knihy/brana-smrti-draci-kridlo-34968");
var request = new HttrRequest(tagToRetrive:"h2");
//request.AddCondition("itemprop", "name");
var resp = client.SendRequest(request);

Console.WriteLine(resp);
