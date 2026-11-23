document.addEventListener("DOMContentLoaded", function () {

    const notificationElement = document.getElementById("app-notification-data");
    if (!notificationElement) return;

    const rowData = notificationElement.getAttribute("data-payload");
    if (!rowData) return;

    try {
        const payload = JSON.parse(rowData);

        // 1. Konsola teknik detayları gruplanmış şekilde bas
        console.groupCollapsed(`%c[UI API Error] ${payload.statusCode} ${payload.statusText}`, "color: #ff4d4f; font-weight: bold;");
        console.error("Teknik Mesaj:", payload.technicalMessage || "Detay belirtilmemiş");
        console.info("Zaman (UTC):", payload.timestamp);
        console.debug("Ham Payload:", payload);
        console.groupEnd();

        // 2. Arayüze sadece kullanıcının anlayacağı sade pop-up'ı bas
        if (typeof Swal !== "undefined") {
            Swal.fire({
                position: "top-end",
                icon: payload.type,
                text: payload.userMessage,
                showConfirmButton: false,
                timer: 1500
            });
        }
    }
    catch (ex) {
        console.error("An error occured in notification js file.")
    }
});

async function confirmWindowAsync(url) {
    try {

        const response = await fetch(url);
        const fetchResponse = await response.text();

        if (!response.ok) {
            console.groupCollapsed(`Partial view did not load in page! For Detail Click Here!`)
            console.error("Partial view did not load in page!");
            console.error(fetchResponse.Message);
            return;
        }

        if (typeof Swal !== "undefined") {
            Swal.fire({
                icon: "error",
                html: fetchResponse,
                width: "40rem",
                showConfirmButton: false,
                showCloseButton: false,
                showCancelButton: false,
                showDenyButton: false
            });
        }
    }
    catch (ex) {
        console.error(`An error occured! Error: ${ex}`);
    }
}