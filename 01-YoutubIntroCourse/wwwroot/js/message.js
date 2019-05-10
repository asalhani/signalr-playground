"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/messages")
    .build();

connection.on("ReceiveMessage", function (message) {

    // recive new message from server
    //var msg = sanitizeHtml(message);
    var msg = message;

    // create new div for incoming message 
    var div = document.createElement("div");
    div.innerHTML = msg + "<hr/>";

    // add new div to existing div message
    document.getElementById("messages").appendChild(div);
});

connection.on("UserConnected", function(connectionId){
    var groupElement = document.getElementById("group");
    var option = document.createElement("option");
    option.text = connectionId;
    option.value = connectionId;
    groupElement.options.add(option);
});

connection.on("UserDisconnected", function(connectionId){
    var groupElement = document.getElementById("group");
    for(var i = 0; i < groupElement.length; i++){
        if(groupElement.options[i].value === connectionId){
            groupElement.remove(i);
        }
    }
});

connection.start().catch(function (err) {
    console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("message").value;
    var groupElement = document.getElementById("group");
    var groupValue = groupElement.options[groupElement.selectedIndex].value;
  
    if(groupValue === "All" || groupValue === "Myself"){
        var method = groupValue === "All" ? "SendMessageToAllClient" : "SendMessageToCaller";
        connection.invoke(method, message).catch(function(ex){
            console.error(ex.toString());
        });
    }
    else{
        connection.invoke("SendMessageToUser", groupValue, message).catch(function(ex){
            console.error(ex.toString());
        });
    }

    event.preventDefault();
});