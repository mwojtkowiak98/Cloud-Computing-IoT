import { signalR } from "../lib/aspnet-signalr/signalr";

(function () {
    var textMessage = $('#textMessage');
    var btnSend = $('#btnSend');
    var listmessages = $('#list-message');
    var userName = $('#userName').val();
    var connection = new signalR.HubConnectionbuilder().withUrl('/chathub').build();

    $(btnSend).click(function () {
        var userMessage = $(textMessage).val();

        connection.invoke('SendMessage', {
            userName: userName,
            message: userMessage
        }).catch(function (error) {
            alert('Cant send the message.');
            console(error);
        })

        $(textMessage).val('');

    })

    connection.start().then(function () {
        console.log("logged to signlar");
        $(btnSend).removeAttr('disabled');
    })

    connection.on('ReceiveMessage', function (obj) {
        $(listmessages).prepend('<li>['
            + obj.TimeStampString + ']'
            + ' <span class = "font-weight-bold">user: ' + obj.userName
            + '</span> | message: ' + obj.message
            + '</li>')
    })
})();
