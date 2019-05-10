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

connection.start().catch(function (err) {
    console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("message").value;
    var groupElement = document.getElementById("group");
    var groupValue = groupElement.options[groupElement.selectedIndex].value;
    var method = "SendMessageToAllClient";
    if (groupValue === "Myself") {
        connection.invoke("SendMessageToCaller", message).catch(function (err) {
            console.error(err.toString());
        });
    }
    else {
        connection.invoke("SendMessageToAllClient", message).catch(function (err) {
            console.error(err.toString()); 
        });
    }

    event.preventDefault();
});