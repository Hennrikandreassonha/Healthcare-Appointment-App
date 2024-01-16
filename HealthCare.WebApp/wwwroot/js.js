function isDevice() {
    return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
}

function SelectElementFix() {
    const elements = document.getElementsByClassName("service-select");

    Array.from(elements).forEach(element => {
        element.selectedIndex = 0;
    });
}

//For the chatbot.
function ShowChatPopup() {
    const chatpopUp = document.getElementById("chat-main-div");
    chatpopUp.classList.remove('display-none');
    chatpopUp.classList.add('display-block');
}
function openPopup() {
    var popup = document.getElementsByClassName("popup-container")[0];
    popup.classList.toggle('show');
}

function scrollToBottom() {
    var chatContainer = document.getElementById('chat-div');
    chatContainer.scrollTop = chatContainer.scrollHeight;
}
function enterToSendMessage() {
    var chatContainer = document.getElementById("question-area");
    chatContainer.addEventListener("keydown", submitOnEnter);
}
let testObj;
setDotnetObject = function (dotNetObject) {
    testObj = dotNetObject;
};
function submitOnEnter(event) {
    if (event.which === 13 && !event.shiftKey) {
        if (!event.repeat) {
            var chatContainer = document.getElementById("question-area");
            testObj.invokeMethodAsync('SendQuestionJS', chatContainer.value);
            chatContainer.value = "";
        }
        event.preventDefault();
    }
}