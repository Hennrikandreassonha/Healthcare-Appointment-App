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

function ShowChatPopup(){
    const chatpopUp = document.getElementById("chat-main-div");
    chatpopUp.classList.remove('display-none');
    chatpopUp.classList.add('display-block');
}
function openPopup() {
    var popup = document.getElementById("popup");
    popup.classList.add("show");
}

function closePopup() {
    var popup = document.getElementById("popup");
    popup.classList.remove("show");
}
