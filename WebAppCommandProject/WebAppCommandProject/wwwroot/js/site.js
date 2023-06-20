// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.getElementById("logoutBtn").addEventListener("click", async function (e) {
    await Logout();
});

async function Logout() {
    try {
        const response = await fetch("/account/logout", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        });
        if (response.ok) {
            let message = await response.json()
            window.alert(message.message);
            location.href = window.location.origin;
        }
    } catch (e) {
        console.error(e.toString())
    }
};
