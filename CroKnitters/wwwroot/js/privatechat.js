<script src="~/js/signalr.min.js"></script>
<script src="https://www.unpkg.com/axios@1.6.8/dist/axios.min.js"></script>

var connection = new signalR.HubConnectionBuilder().WithUrl("/PrivateChatHub").build();

var _connectionId = '';

//this will show the message sent by the other user on the page
connection.on("ReceiveMessage", function (data) {
    var message = document.createElement("div")
    message.classList.add('message block bg-secondary align-content-start text-white mb-3j')

    var p = document.createElement("p")
    p.appendChild(document.createTextNode(data.Content))

    var p2 = document.createElement("p")
    p2.appendChild(document.createTextNode(data.CreationDate))

    message.appendChild(p);
    message.appendChild(p2);

    document.querySelector('.messageList').append(message);
})

/*var joinRoom /*might need*/

//start the connection and get the connection id
connection.start().then(function () {
    connection.invoke('getConnectionId')
        .then(function (connectionId) {
            _connectionId = connectionId
            /*joinRoom();*/
        })
        .catch(function (err) {
            console.log(err)
        })
})

var sendMessage = function (event) {
    event.preventDefault();
    var data = new FormData(event.target);
    document.getElementById('message-input').value = '';
    axios.post('/Chats/SendMessage', data)
        .then(res => {
            console.log("Message sent!")
        })
        .catch(err => {
            console.log("Failed to send message!")
        })
}

