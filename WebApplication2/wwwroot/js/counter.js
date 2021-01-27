var connection = new signalR.HubConnectionBuilder().withUrl("/counter").build();

connection.on("UserCount", function (message) {
    document.getElementById("counter").innerText = message;
});

connection.start();