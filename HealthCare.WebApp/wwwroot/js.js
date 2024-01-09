function isDevice() {
    return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
}

function SelectElementFix() {
    const elements = document.getElementsByClassName("service-select");

    Array.from(elements).forEach(element => {
        element.selectedIndex = 0;
    });
}