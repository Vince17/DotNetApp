"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var caSonne = false;

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;
/*
connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " : " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});*/

connection.on("ReceiveDring", function (isDring) {
    var message = isDring;
    /*var  = " test ";
    var d = new Date();
    if (isDring) {
        h = "ça sonne !";
    } else {
        h = "ça ne sonne pas !";
    }
    h = h + " at : " + d.getHours() + "h" + d.getMinutes() + ":" + d.getSeconds();
    document.getElementById("sonne").innerHTML = h;*/
    document.getElementById("sonne").innerHTML = message;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    caSonne = !caSonne;
    
    connection.invoke("SendMessage",caSonne).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});