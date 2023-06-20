"use strict";

//const hubConnection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

//disableButton(true);

//hubConnection.start().then(function () {
//    disableButton(false);
//}).catch(function (err) {
//    return console.error(err.toString());
//});

//let userName = '';

//// set username
//document.getElementById("loginBtn").addEventListener("click", function (e) {
//    userName = document.getElementById("userName").value;
//    document.getElementById("header").innerHTML = '<h3>Welcome ' + userName + '</h3>';
//});

//// send message to server
//document.getElementById("sendBtn").addEventListener("click", function (e) {
//    let message = document.getElementById("message").value;
//    hubConnection.invoke("Send", message, userName).catch(function (err) {
//        return console.error(err.toString());
//    });
//});

//document.getElementById("applyBtn").addEventListener("click", function (e) {
//    let message = document.getElementById("worker_message").value;
//    ApplyMessage(message);
//});

//async function ApplyMessage(message) {
//    try {
//        await fetch('api/Worker/set_message', {
//            method: 'POST',
//            headers: {
//                'Accept': 'application/json',
//                'Content-Type': 'application/json'
//            },
//            body: JSON.stringify(message)
//        });
//    } catch (error) {
//        console.error(error.toString())
//    }
//};

//document.getElementById("stopBtn").addEventListener("click", function (e) {
//    StopWorker();
//});

//async function StopWorker() {
//    try {
//        await fetch('api/Worker/stop', {
//            method: 'POST',
//            headers: {
//                'Accept': 'application/json',
//                'Content-Type': 'application/json'
//            }
//        });
//    } catch (error) {
//        console.error(error.toString())
//    }
//};

//document.getElementById("startBtn").addEventListener("click", function (e) {
//    StartWorker();
//});

//async function StartWorker() {
//    try {
//        await fetch('api/Worker/start', {
//            method: 'POST',
//            headers: {
//                'Accept': 'application/json',
//                'Content-Type': 'application/json'
//            }
//        });
//    } catch (error) {
//        console.error(error.toString())
//    }
//};

//document.getElementById("pauseBtn").addEventListener("click", function (e) {
//    PauseWorker();
//});

//async function PauseWorker() {
//    try {
//        await fetch('api/Worker/pause', {
//            method: 'POST',
//            headers: {
//                'Accept': 'application/json',
//                'Content-Type': 'application/json'
//            }
//        });
//    } catch (error) {
//        console.error(error.toString())
//    }
//};

//document.getElementById("continueBtn").addEventListener("click", function (e) {
//    ContinueWorker();
//});

//async function ContinueWorker() {
//    try {
//        await fetch('api/Worker/continue', {
//            method: 'POST',
//            headers: {
//                'Accept': 'application/json',
//                'Content-Type': 'application/json'
//            }
//        });
//    } catch (error) {
//        console.error(error.toString())
//    }
//};

//function disableButton(marker) {
//    document.getElementById("startBtn").disabled = marker;
//    document.getElementById("stopBtn").disabled = marker;
//    document.getElementById("pauseBtn").disabled = marker;
//    document.getElementById("pauseBtn").disabled = marker;
//    document.getElementById("continueBtn").disabled = marker;
//}
