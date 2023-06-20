"use strict";

const hubConnection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

hubConnection.start().catch(function (err) {
    return console.error(err.toString());
});

hubConnection.on('Send', function (currentTime, userName, message) {

    // create element <b> to save time
    let timeElem = document.createElement("b");
    timeElem.appendChild(document.createTextNode(currentTime + ' - '));

    // create element <b> to save username
    let userNameElem = document.createElement("b");
    userNameElem.appendChild(document.createTextNode(userName + ': '));

    // create lement <p> to save user message
    let elem = document.createElement("p");
    elem.appendChild(timeElem);
    elem.appendChild(userNameElem);
    elem.appendChild(document.createTextNode(message));

    var firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);

});